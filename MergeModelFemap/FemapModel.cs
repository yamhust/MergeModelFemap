using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using femap;
using System.Windows.Forms;

namespace MergeModelFemap
{
        public static  class FemapModel
        {
            public static model feModel;
            public static string MSG_ERR_NONE_START_FEMAP = "Femap is not opened";
            public static string TITLE_BOX = "FEMAP API TOOL SYSTEM BY CANHPM";
            public static Set se;
            
            public static model GetFemapModel()
            {
                if (feModel == null)
                {
                    try
                    {
                        feModel = (femap.model)Marshal.GetActiveObject("femap.model");
                    }
                    catch (COMException e)
                    {
                        System.Windows.Forms.MessageBox.Show(FemapModel.MSG_ERR_NONE_START_FEMAP, FemapModel.TITLE_BOX, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }
                return feModel;
            }
            public static ModelInfor RenumberAllData(object rangeID )
            {
                int[] ID = (int[])rangeID;
                ModelInfor modelInfo = new ModelInfor();
            const int t = 100;
                
                feModel = FemapModel.GetFemapModel();
                se = feModel.feSet;

                se.ID = 100;
                se.clear();
                se.AddAll(zDataType.FT_POINT);
                modelInfo.num_POINT_ID = se.Count();
                feModel.feRenumber(zDataType.FT_POINT, 100, ID[0]);
                Thread.Sleep(t);
                modelInfo.max_POINT_ID = ID[0] + modelInfo.num_POINT_ID - 1;

                se.ID = 200;
                se.clear();
                se.AddAll(zDataType.FT_CURVE);
                modelInfo.num_CURVE_ID = se.Count();
                feModel.feRenumber(zDataType.FT_CURVE, 200, ID[1]);
                Thread.Sleep(t);
                modelInfo.max_CURVE_ID = ID[1]+modelInfo.num_CURVE_ID - 1;

                se.ID = 300;
                se.clear();
                se.AddAll(zDataType.FT_SURFACE);
                modelInfo.num_SURFACE_ID = se.Count();
                feModel.feRenumber(zDataType.FT_SURFACE, 300, ID[2]);
                Thread.Sleep(t);
                modelInfo.max_SURFACE_ID = ID[2]+modelInfo.num_SURFACE_ID-1;

                se.ID = 400;
                se.clear();
                se.AddAll(zDataType.FT_SOLID);
                modelInfo.num_SOLID_ID = se.Count();
                feModel.feRenumber(zDataType.FT_SOLID, 400, ID[3]);
                Thread.Sleep(t);
                modelInfo.max_SOLID_ID = ID[3]+ modelInfo.num_SOLID_ID-1;

                se.ID = 500;
                se.clear();
                se.AddAll(zDataType.FT_NODE);
                modelInfo.num_NODE_ID = se.Count();
                feModel.feRenumber(zDataType.FT_NODE, 500, ID[4]);
                Thread.Sleep(t);
                modelInfo.max_NODE_ID = ID[4]+modelInfo.num_NODE_ID-1;

                se.ID = 600;
                se.clear();
                se.AddAll(zDataType.FT_ELEM);
                modelInfo.num_ELEMENT_ID = se.Count();
                if (se.Count()!=0)
                {
                    feModel.feMeasureMeshMassProp(600, 0, false, false, out double Total_Length_Line_Element, out double Total_Area_Plate_Element,
                    out double Total_Volume, out double Structural_Mass, out double Non_Structural_Mass, out double Total_Mass, out object structCG,
                    out object non_structCG, out object total_structCG, out object inertia, out object inertiaCG);

                    modelInfo.Total_Length_Line_Element = Total_Length_Line_Element;
                    modelInfo.Total_Area_Plate_Element = Total_Area_Plate_Element;
                    modelInfo.Total_Volume = Total_Volume;
                    modelInfo.Structural_Mass = Structural_Mass;
                    modelInfo.Non_Structural_Mass = Non_Structural_Mass;
                    modelInfo.Total_Mass = Total_Mass;
                    modelInfo.structCG = structCG;
                    modelInfo.non_structCG = non_structCG;
                    modelInfo.total_structCG = total_structCG;
                    modelInfo.inertia = inertia;
                    modelInfo.inertiaCG = inertiaCG;
                }

                feModel.feRenumber(zDataType.FT_ELEM, 600, ID[5]);
                Thread.Sleep(t);
                modelInfo.max_ELEMENT_ID = ID[5] + modelInfo.num_ELEMENT_ID-1;

                se.ID = 700;
                se.clear();
                se.AddAll(zDataType.FT_MATL);
                modelInfo.num_MATERIAL_ID = se.Count();
                feModel.feRenumber(zDataType.FT_MATL, 700, ID[6]);
                Thread.Sleep(t);
                modelInfo.max_MATERIAL_ID = ID[6]+modelInfo.num_MATERIAL_ID-1;

                se.ID = 800;
                se.clear();
                se.AddAll(zDataType.FT_PROP);
                modelInfo.num_PROPERTY_ID = se.Count();
                feModel.feRenumber(zDataType.FT_PROP, 800, ID[7]);
                Thread.Sleep(t);
                modelInfo.max_PROPERTY_ID = ID[7]+modelInfo.num_PROPERTY_ID-1;

                se.ID = 900;
                se.clear();
                se.AddAll(zDataType.FT_LAYER);
                modelInfo.num_LAYER_ID = se.Count();
                feModel.feRenumber(zDataType.FT_LAYER, 900, ID[8]);
                Thread.Sleep(t);
                modelInfo.max_LAYER_ID = ID[8]+modelInfo.num_LAYER_ID-1;

                se.ID = 1000;
                se.clear();
                se.AddAll(zDataType.FT_GROUP);
                modelInfo.num_GROUP_ID = se.Count();
                feModel.feRenumber(zDataType.FT_GROUP, 1000, ID[9]);
                Thread.Sleep(t);
                modelInfo.max_GROUP_ID = ID[9]+modelInfo.num_GROUP_ID-1;

                return modelInfo;
            }
        public static ModelInfor GetModelID()
        {
            ModelInfor modelInfo = new ModelInfor();

            feModel = FemapModel.GetFemapModel();
            se = feModel.feSet;

            se.ID = 100;
            se.clear();
            se.AddAll(zDataType.FT_POINT);
            modelInfo.num_POINT_ID = se.Count();
            modelInfo.max_POINT_ID = se.Last();

            se.ID = 200;
            se.clear();
            se.AddAll(zDataType.FT_CURVE);
            modelInfo.num_CURVE_ID = se.Count();
            modelInfo.max_CURVE_ID = se.Last();

            se.ID = 300;
            se.clear();
            se.AddAll(zDataType.FT_SURFACE);
            modelInfo.num_SURFACE_ID = se.Count();
            modelInfo.max_SURFACE_ID = se.Last();

            se.ID = 400;
            se.clear();
            se.AddAll(zDataType.FT_SOLID);
            modelInfo.num_SOLID_ID = se.Count();
            modelInfo.max_SOLID_ID = se.Last();

            se.ID = 500;
            se.clear();
            se.AddAll(zDataType.FT_NODE);
            modelInfo.num_NODE_ID = se.Count();
            modelInfo.max_NODE_ID = se.Last();

            se.ID = 600;
            se.clear();
            se.AddAll(zDataType.FT_ELEM);
            modelInfo.num_ELEMENT_ID = se.Count();
            if (se.Count() != 0)
            {
                feModel.feMeasureMeshMassProp(600, 0, false, false, out double Total_Length_Line_Element, out double Total_Area_Plate_Element,
                out double Total_Volume, out double Structural_Mass, out double Non_Structural_Mass, out double Total_Mass, out object structCG,
                out object non_structCG, out object total_structCG, out object inertia, out object inertiaCG);

                modelInfo.Total_Length_Line_Element = Total_Length_Line_Element;
                modelInfo.Total_Area_Plate_Element = Total_Area_Plate_Element;
                modelInfo.Total_Volume = Total_Volume;
                modelInfo.Structural_Mass = Structural_Mass;
                modelInfo.Non_Structural_Mass = Non_Structural_Mass;
                modelInfo.Total_Mass = Total_Mass;
                modelInfo.structCG = structCG;
                modelInfo.non_structCG = non_structCG;
                modelInfo.total_structCG = total_structCG;
                modelInfo.inertia = inertia;
                modelInfo.inertiaCG = inertiaCG;
            }
            modelInfo.max_ELEMENT_ID = se.Last();

            se.ID = 700;
            se.clear();
            se.AddAll(zDataType.FT_MATL);
            modelInfo.num_MATERIAL_ID = se.Count();
            modelInfo.max_MATERIAL_ID = se.Last();

            se.ID = 800;
            se.clear();
            se.AddAll(zDataType.FT_PROP);
            modelInfo.num_PROPERTY_ID = se.Count();
            modelInfo.max_PROPERTY_ID = se.Last();

            se.ID = 900;
            se.clear();
            se.AddAll(zDataType.FT_LAYER);
            modelInfo.num_LAYER_ID = se.Count();
            modelInfo.max_LAYER_ID = se.Last();

            se.ID = 1000;
            se.clear();
            se.AddAll(zDataType.FT_GROUP);
            modelInfo.num_GROUP_ID = se.Count();
            modelInfo.max_GROUP_ID = se.Last();

            return modelInfo;
        }
}

        public class ModelInfor
        {
            public int max_POINT_ID { get; set; }
            public int max_CURVE_ID { get; set; }
            public int max_SURFACE_ID { get; set; }
            public int max_SOLID_ID { get; set; }

            public int max_NODE_ID { get; set; }
            public int max_ELEMENT_ID { get; set; }

            public int max_PROPERTY_ID { get; set; }
            public int max_MATERIAL_ID { get; set; }

            public int max_LAYER_ID { get; set; }
            public int max_GROUP_ID { get; set; }

            public int num_POINT_ID { get; set; }
            public int num_CURVE_ID { get; set; }
            public int num_SURFACE_ID { get; set; }
            public int num_SOLID_ID { get; set; }

            public int num_NODE_ID { get; set; }
            public int num_ELEMENT_ID { get; set; }

            public int num_PROPERTY_ID { get; set; }
            public int num_MATERIAL_ID { get; set; }

            public int num_LAYER_ID { get; set; }
            public int num_GROUP_ID { get; set; }

            public double Structural_Mass { get; set; }
            public double Non_Structural_Mass { get; set; }
            public double Total_Mass { get; set; }

            public double Total_Length_Line_Element { get; set; }
            public double Total_Area_Plate_Element { get; set; }
            public double Total_Volume { get; set; }

            public object structCG { get; set; }
            public object non_structCG { get; set; }
            public object total_structCG { get; set; }
            public object inertia { get; set; }
            public object inertiaCG { get; set; }
             
            public string ModelName { get; set; }
        }   
}
