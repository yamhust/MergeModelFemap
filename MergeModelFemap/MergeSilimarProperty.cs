using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using femap;

namespace MergeModelFemap
{
    public struct PropertyData
    {
        public int PID;
        public int MID;
        public double PlateThickness;
        public double DIM1;
        public double DIM2;
        public double DIM3;
        public double DIM4;
        public double DIM5;
        public double DIM6;
        public double refLoc;
        public int shape;
        public void InputBeam(int PID, int MID, int shape, double DIM1, double DIM2, double DIM3, double DIM4, double DIM5, double DIM6, double refLoc)
        {
            this.PID = PID;
            this.MID = MID;
            this.shape = shape;
            this.DIM1 = DIM1;
            this.DIM2 = DIM2;
            this.DIM3 = DIM3;
            this.DIM4 = DIM4;
            this.DIM5 = DIM5;
            this.DIM6 = DIM6;
            this.refLoc= refLoc;
        }
        public void InputShell(int PID, int MID, double PlateThickness)
        {
            this.PID = PID;
            this.MID = MID;
            this.PlateThickness = PlateThickness;
        }

    }
    class PlateComparer : IComparer<PropertyData>
    {
        public int Compare(PropertyData p1, PropertyData p2)
        {
            int ret = p1.MID.CompareTo(p2.MID);
            if (ret == 0)
            {
                return p1.PlateThickness.CompareTo(p2.PlateThickness);
            }
            else
                return ret;
        }
    }
    class BeamComparer : IComparer<PropertyData>
    {
        public int Compare(PropertyData p1, PropertyData p2)
        {
            int ret0 = p1.MID.CompareTo(p2.MID);
            if (ret0 == 0)
            {
                int ret1 = p1.shape.CompareTo(p2.shape);
                if (ret1 == 0)
                {
                    int ret = p1.refLoc.CompareTo(p2.refLoc);
                    if (ret == 0)
                    {
                        int ret2 = p1.DIM1.CompareTo(p2.DIM1);
                        if (ret2 == 0)
                        {
                            int ret3 = p1.DIM2.CompareTo(p2.DIM2);
                            if (ret3 == 0)
                            {
                                int ret4 = p1.DIM3.CompareTo(p2.DIM3);
                                if (ret4 == 0)
                                {
                                    int ret5 = p1.DIM4.CompareTo(p2.DIM4);
                                    if (ret5 == 0)
                                    {
                                        int ret6 = p1.DIM5.CompareTo(p2.DIM5);
                                        if (ret6 == 0)
                                        {
                                            return p1.DIM6.CompareTo(p2.DIM6);
                                        }
                                        else
                                            return ret6;
                                    }
                                    else
                                        return ret5;
                                }
                                else
                                    return ret4;
                            }
                            else
                                return ret3;
                        }
                        else
                            return ret2;
                    }
                    else
                        return ret;
                }
                else
                    return ret1;
            }
            return ret0;
        }
    }

    public class MergeProperty
    {
        static femap.model fApp;
        static femap.Set fSet;
        static femap.Prop fProp;
        static femap.Elem fElem;
        static femap.Curve cur;
        static femap.Surface sur;
        static string txt;
        static int ti = 1;

        static List<PropertyData> PropPlate = new List<PropertyData>();
        static List<PropertyData> PropBeam = new List<PropertyData>();

        public static void Merge()
        {

            try
            {
                fApp = (femap.model)Marshal.GetActiveObject("femap.model");
            }
            catch (Exception)
            {
                fApp = null;
                System.Environment.Exit(1);
            }

            fSet = fApp.feSet;
            fProp = fApp.feProp;
            fElem = fApp.feElem;
            cur = fApp.feCurve;
            sur = fApp.feSurface;

            fSet.ID = 1000;
            fSet.clear();
            fSet.AddAll(femap.zDataType.FT_PROP);
            fApp.feRenumber(femap.zDataType.FT_PROP, 1000, 1);

            fSet.ID = 1100;
            fSet.clear();
            fSet.AddAll(femap.zDataType.FT_ELEM);

            fSet.ID = 100;
            fSet.clear();
            fSet.AddAll(femap.zDataType.FT_PROP);
            if (fSet.Count() < 1)
            {
                System.Environment.Exit(1);
            }
            int fID = fSet.First();


            while (fID > 0)
            {
                fProp.Get(fID);
                if (fProp.type == femap.zElementType.FET_L_PLATE)
                {
                    PropertyData pPlate = new PropertyData();
                    pPlate.InputShell(fProp.ID, fProp.matlID, fProp.pval[0]);
                    PropPlate.Add(pPlate);
                }
                else if (fProp.type == femap.zElementType.FET_L_BEAM || fProp.type == femap.zElementType.FET_L_BAR)
                {
                    PropertyData pBeam = new PropertyData();
                    //if (txt1[2].Trim() == "HP")
                    //{
                    //    ti = 0;
                    //}
                    pBeam.InputBeam(fProp.ID, fProp.matlID, fProp.flagI[1], fProp.pval[40], fProp.pval[41], fProp.pval[42], fProp.pval[43], fProp.pval[44], fProp.pval[45], fProp.pval[51]);
                    PropBeam.Add(pBeam);
                }
                fID = fSet.Next();
            }

            //Thread newthread = new Thread(ArrangeBeamProp);
            //newthread.Start();

            foreach (PropertyData prop1 in PropPlate)
            {
                foreach (PropertyData prop2 in PropPlate)
                {
                    if (prop1.PID != prop2.PID)
                    {
                        if (prop1.MID == prop2.MID && prop1.PlateThickness == prop2.PlateThickness)
                        {
                            fSet.ID = 5000;
                            fSet.clear();
                            fSet.AddRule(prop2.PID, femap.zGroupDefinitionType.FGD_ELEM_BYPROP);
                            fApp.feModifyElemPropID(5000, prop1.PID);
                            fSet.clear();
                            fSet.AddRule(prop2.PID, femap.zGroupDefinitionType.FGD_SURFACE_BYPROP);
                            fID = fSet.First();
                            while (fID > 0)
                            {
                                sur.Get(fID);
                                sur.attrPID = prop1.PID;
                                sur.Put(fID);
                                fID = fSet.Next();
                            }
                            //DeletePID[m] = prop2.PID;
                            //m++;
                        }
                    }
                }
            }
            MergeBeamProp();

            fSet.ID = 1;
            fSet.clear();
            fSet.AddAll(femap.zDataType.FT_AERO_PROP);
            fApp.feDelete(femap.zDataType.FT_PROP, 1);


            PropPlate.Sort(new PlateComparer());
            PropBeam.Sort(new BeamComparer());

            int i = 0;
            foreach (PropertyData propertyData in PropPlate)
            {
                fApp.feRenumber(femap.zDataType.FT_PROP, -propertyData.PID, 50001 + i);
            }

            int j = 0;
            foreach (PropertyData propertyData in PropBeam)
            {
                fApp.feRenumber(femap.zDataType.FT_PROP, -propertyData.PID, 100001 + j);
            }

            fApp.feFileRebuild(false,true);
            fSet.ID = 1000;
            fSet.clear();
            fSet.AddAll(femap.zDataType.FT_PROP);
            fApp.feDelete(zDataType.FT_PROP, 1000);
            fSet.ID = 1000;
            fSet.clear();
            fSet.AddAll(femap.zDataType.FT_PROP);
            fApp.feRenumber(femap.zDataType.FT_PROP, 1000, 1);
            fApp.feFileRebuild(false, true);

            fApp.feViewRegenerate(0);
        }
        public static void MergeBeamProp()
        {

            int fi;

            foreach (PropertyData prop3 in PropBeam)
            {
                foreach (PropertyData prop4 in PropBeam)
                {
                    if (prop3.PID != prop4.PID)
                    {
                        if (prop3.MID == prop4.MID && prop3.shape == prop4.shape)
                        {
                            switch (prop4.shape)
                            {
                                case 1: // FB

                                    if (prop3.DIM1 == prop4.DIM1 && prop3.DIM2 == prop4.DIM2)

                                    {
                                        if (prop3.refLoc == prop4.refLoc)
                                        {
                                            fSet.ID = 1000;
                                            fSet.clear();
                                            fSet.AddRule(prop4.PID, femap.zGroupDefinitionType.FGD_ELEM_BYPROP);
                                            fApp.feModifyElemPropID(1000, prop3.PID);
                                            fSet.clear();
                                            fSet.AddRule(prop4.PID, femap.zGroupDefinitionType.FGD_CURVE_BYPROP);
                                            fi = fSet.First();
                                            while (fi > 0)
                                            {
                                                cur.Get(fi);
                                                cur.attrPID = prop3.PID;
                                                cur.Put(fi);
                                                fi = fSet.Next();
                                            }
                                        }

                                    }
                                    break;

                                default:
                                    if (prop3.DIM1 == prop4.DIM1 && prop3.DIM2 == prop4.DIM2 && prop3.DIM3 == prop4.DIM3 && prop3.DIM4 == prop4.DIM4
                                         && prop3.DIM5 == prop4.DIM5 && prop3.DIM6 == prop4.DIM6)
                                    {
                                        fSet.ID = 3000;
                                        fSet.clear();
                                        fSet.AddRule(prop4.PID, femap.zGroupDefinitionType.FGD_ELEM_BYPROP);
                                        fApp.feModifyElemPropID(3000, prop3.PID);
                                        fSet.clear();
                                        fSet.AddRule(prop4.PID, femap.zGroupDefinitionType.FGD_CURVE_BYPROP);
                                        fi = fSet.First();
                                        while (fi > 0)
                                        {
                                            cur.Get(fi);
                                            cur.attrPID = prop3.PID;
                                            cur.Put(fi);
                                            fi = fSet.Next();
                                        }
                                    }
                                    break;

                            }
                        }
                    }
                }
            }
        }


    }
}
