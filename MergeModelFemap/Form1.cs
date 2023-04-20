using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using femap;

namespace MergeModelFemap
{
    public partial class MergeFemapModel : Form
    {
        public MergeFemapModel()
        {
            InitializeComponent();
            tbPath.Text = "C:\\Users\\CAETech-Server\\Desktop\\06042023";
            ckb1.Checked = true;
        }

        static model fem;
        static Set fset;
        static Group group;
        static TrackData track;
        List<string> fileModel, fileModelneu;
        int maxGID = 1;

        private void btMerge_Click(object sender, EventArgs e)
        {
            fem = FemapModel.GetFemapModel();
            fem.feFileNew();
            fset = fem.feSet;
            group = fem.feGroup;
            track = fem.feTrackData;
            fileModel = GetData(tbPath.Text);
            string pathAssem = tbPath.Text + "\\Assem_R";
            CreatFolderIfMissing(ref pathAssem);

            const int t1 = 1000;
            const int t2 = 1000;

            List<ModelInfor> modelInfors = new List<ModelInfor>();
            ModelInfor currentModel = null;
            int[] IDarray = null;
            for (int i = 0; i<fileModel.Count;i++)
            {
                fem.feFileOpen(false, fileModel[i]);
                Thread.Sleep(t1);
                if (i==0)
                {
                    IDarray = Enumerable.Repeat(1, 10).ToArray();
                }
                else
                {
                    IDarray = new int[] { currentModel.max_POINT_ID + 1, currentModel.max_CURVE_ID + 1,
                                          currentModel.max_SURFACE_ID + 1, currentModel.max_SOLID_ID + 1,
                                          currentModel.max_NODE_ID + 1 ,currentModel.max_ELEMENT_ID + 1,
                                          currentModel.max_MATERIAL_ID, currentModel.max_PROPERTY_ID+1,
                                          currentModel.max_LAYER_ID+1,currentModel.max_GROUP_ID+1};
                    
                }
                
                currentModel = FemapModel.RenumberAllData(IDarray);

                fem.feFileWriteNeutral(0, pathAssem + "\\Model" + i + ".neu", true, true, true, true, true, true, 6, 0.0, 0);
                currentModel.ModelName = "\\Model" + i + ".neu";

                maxGID = currentModel.max_GROUP_ID + 1;
                //fem.feFileSave(false);
                fem.feFileClose(false);
                Thread.Sleep(t1);
                modelInfors.Add(currentModel);
            }
            

            fileModelneu = GetData(pathAssem);
            fem.feFileRebuild(false, true);
            Thread.Sleep(t2);
            fem.feFileNew();
            for (int i = 0; i < fileModelneu.Count; i++)
            {
                track.Start(zDataType.FT_ELEM);
                fem.feFileReadNeutral2(0, pathAssem + "\\Model" + i + ".neu", true, true, true, true, true, false, 0);
                track.clear();
                track.Stop(zDataType.FT_ELEM);
                fset.ID = 2000;
                fset.clear();
                track.Created(zDataType.FT_ELEM, 2000, true);
                if (fset.Count()>0)
                {
                    group.Get(maxGID);
                    group.title = "Model" + i + ".neu";
                    group.RangeDeleteAll(zGroupDefinitionType.FGD_ELEM_ID);
                    group.RangeDeleteAll(zGroupDefinitionType.FGD_NODE_ID);
                    group.RangeDeleteAll(zGroupDefinitionType.FGD_PROP_ID);
                    group.RangeDeleteAll(zGroupDefinitionType.FGD_MATL_ID);
                    group.SetAdd(zDataType.FT_ELEM, 2000);
                    group.AddRelated();
                    group.Put(maxGID);
                    maxGID++;
                }
                
                Thread.Sleep(t1);
            }

            currentModel = FemapModel.GetModelID();
            fem.feFileSaveAs(false, pathAssem + "\\AssemBeforeMegreProperty.modfem");
            Thread.Sleep(t1);
            currentModel.ModelName = "\\AssemBeforeMegreProperty.modfem";
            modelInfors.Add(currentModel);

            if (ckb1.Checked == true)
            {
                MergeProperty.Merge();
                currentModel = FemapModel.GetModelID();
                fem.feFileSaveAs(false, pathAssem + "\\AssemAfterMegreProperty.modfem");
                Thread.Sleep(t1);
                currentModel.ModelName = "\\AssemAfterMegreProperty.modfem";
                modelInfors.Add(currentModel);
            }

            // Create CSV
            using (var file = File.CreateText(pathAssem + "\\CheckData.csv"))
            {
                string[] temp = new string[]{"Model Name", ",",
                                                     "max_POINT_ID", ",",
                                                     "num_POINT_ID", ",",
                                                     "max_CURVE_ID", ",",
                                                     "num_CURVE_ID", ",",
                                                     "max_SURFACE_ID", ",",
                                                     "num_SURFACE_ID", ",",
                                                     "max_SOLID_ID", ",",
                                                     "num_SOLID_ID", ",",
                                                     "max_NODE_ID", ",",
                                                     "num_NODE_ID", ",",
                                                     "max_ELEMENT_ID", ",",
                                                     "num_ELEMENT_ID", ",",
                                                     "max_MATERIAL_ID", ",",
                                                     "num_MATERIAL_ID", ",",
                                                     "max_PROPERTY_ID", ",",
                                                     "num_PROPERTY_ID", ",",
                                                     "max_LAYER_ID", ",",
                                                     "num_LAYER_ID", ",",
                                                     "max_GROUP_ID", ",",
                                                     "num_GROUP_ID", ",",
                                                     "Total_Length_Line_Element",",",
                                                     "Total_Area_Plate_Element", ",",
                                                     "Total_Volume", ",",
                                                     "Structural_Mass", ",",
                                                     "Non_Structural_Mass", ",",
                                                     "Total_Mass" };
                foreach (string t in temp)
                {
                    file.Write(t);
                }
                file.WriteLine();

                int j = 0;
                foreach(var modelInfo in modelInfors)
                {
                        
                        string[] temp1 = new string []{modelInfo.ModelName.ToString(),",",
                                                    modelInfo.max_POINT_ID.ToString(),",",
                                                    modelInfo.num_POINT_ID.ToString(),",",
                                                    modelInfo.max_CURVE_ID.ToString(),",",
                                                    modelInfo.num_CURVE_ID.ToString(),",",
                                                    modelInfo.max_SURFACE_ID.ToString(),",",
                                                    modelInfo.num_SURFACE_ID.ToString(),",",
                                                    modelInfo.max_SOLID_ID.ToString(),",",
                                                    modelInfo.num_SOLID_ID.ToString(),",",
                                                    modelInfo.max_NODE_ID.ToString(),",",
                                                    modelInfo.num_NODE_ID.ToString(),",",
                                                    modelInfo.max_ELEMENT_ID.ToString() ,",",
                                                    modelInfo.num_ELEMENT_ID.ToString() ,",",
                                                    modelInfo.max_MATERIAL_ID.ToString(),",",
                                                    modelInfo.num_MATERIAL_ID.ToString(),",",
                                                    modelInfo.max_PROPERTY_ID.ToString() ,",",
                                                    modelInfo.num_PROPERTY_ID.ToString() ,",",
                                                    modelInfo.max_LAYER_ID.ToString(),",",
                                                    modelInfo.num_LAYER_ID.ToString(),",",
                                                    modelInfo.max_GROUP_ID.ToString(),",",
                                                    modelInfo.num_GROUP_ID.ToString(),",",
                                                    modelInfo.Total_Length_Line_Element.ToString(),",",
                                                    modelInfo.Total_Area_Plate_Element.ToString(),",",
                                                    modelInfo.Total_Volume.ToString(),",",
                                                    modelInfo.Structural_Mass.ToString(),",",
                                                    modelInfo.Non_Structural_Mass.ToString(),",",
                                                    modelInfo.Total_Mass.ToString()};
                        foreach (string t in temp1)
                        {
                            file.Write(t);
                        }
                        file.WriteLine();


                }
            }

            fem.feFileClose(false);
            Thread.Sleep(t1);

            fem.feViewRegenerate(0);

        }

        private List<string> GetData(string Path)
        {
            List<string> filePath = new List<string>();
            DirectoryInfo d = new DirectoryInfo(Path);
            if (d==null)
            {
                System.Windows.Forms.MessageBox.Show(FemapModel.MSG_ERR_NONE_START_FEMAP, FemapModel.TITLE_BOX, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            FileInfo[] fmod = d.GetFiles("*.modfem");
            FileInfo[] fneu = d.GetFiles("*.neu");
            foreach (FileInfo f in fmod)
            {
                filePath.Add(f.FullName);
            }
            
            foreach (FileInfo f in fneu)
            {
                filePath.Add(f.FullName);
            }
            return filePath;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MergeProperty.Merge();
        }

        private void CreatFolderIfMissing ( ref string path)
        {
            int i = 0;
            loop:
            string path01 = path + i.ToString();
            if (!Directory.Exists(path01))
            {
                DirectoryInfo directory1 = Directory.CreateDirectory(path01);
                path = path01;
            }
            else
            {
                i++;
                goto loop;
            }

        }
    }
}
