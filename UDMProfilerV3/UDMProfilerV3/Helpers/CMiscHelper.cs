// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CMiscHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Text.RegularExpressions;

namespace UDMProfilerV3
{
    public static class CMiscHelper
    {
        private static bool m_bTestMode = false;

        public static bool IsTestMode
        {
            get
            {
                return CMiscHelper.m_bTestMode;
            }
            set
            {
                CMiscHelper.m_bTestMode = value;
            }
        }

        //yjk, 19.09.10 - CTimeChartHelper Class의 SortAddress로 통일 시킴으로 인해 주석처리
        //public static int SortAddress(string sValue1, string sValue2)
        //{
        //    try
        //    {
        //        if (sValue1.Length <= 0 || sValue2.Length <= 0 || (int)sValue1[0] != (int)sValue2[0])
        //            return sValue1.CompareTo(sValue2);
        //        if (sValue1[0] == 'K' || sValue1[0] == 'Z' || sValue1[0] == 'S')
        //        {
        //            sValue1 = sValue1.Remove(0, 1);
        //            sValue2 = sValue2.Remove(0, 1);
        //            if ((int)sValue1[0] != (int)sValue2[0])
        //                return sValue1.CompareTo(sValue2);
        //            sValue1 = sValue1.Remove(0, 1);
        //            sValue2 = sValue2.Remove(0, 1);
        //            if ((int)sValue1[0] != (int)sValue2[0])
        //                return sValue1.CompareTo(sValue2);
        //        }
        //        else
        //        {
        //            if (sValue1[0] == 'U')
        //                return sValue1.CompareTo(sValue2);
        //            sValue1 = sValue1.Remove(0, 1);
        //            sValue2 = sValue2.Remove(0, 1);
        //        }
        //        sValue1 = sValue1.Split('Z')[0];
        //        sValue2 = sValue2.Split('Z')[0];
        //        if (sValue1.Length == 0 && sValue2.Length == 0)
        //            return 0;
        //        if (sValue1.Length == 0 && sValue2.Length > 0)
        //            return -1;
        //        if (sValue1.Length > 0 && sValue2.Length == 0)
        //            return 1;
        //        string str1 = "";
        //        string[] strArray1 = sValue1.Split('.');
        //        if (strArray1.Length == 2)
        //        {
        //            sValue1 = strArray1[0];
        //            str1 = strArray1[1];
        //        }
        //        string str2 = "";
        //        string[] strArray2 = sValue2.Split('.');
        //        if (strArray2.Length == 2)
        //        {
        //            sValue2 = strArray2[0];
        //            str2 = strArray2[1];
        //        }
        //        if (str1.Length != 0 && str2.Length != 0 && sValue1 == sValue2)
        //            return Convert.ToInt32(str1, 16).CompareTo(Convert.ToInt32(str2, 16));
        //        return Convert.ToInt32(sValue1, 16).CompareTo(Convert.ToInt32(sValue2, 16));
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Clear();
        //        return sValue1.CompareTo(sValue2);
        //    }
        //}

        public static double ParseDouble(string sValue)
        {
            double result = -1.0;
            if (!double.TryParse(sValue, out result))
                result = -1.0;
            return result;
        }

        public static bool IsAvailableDirectoryName(string sText)
        {
            return !new Regex("[\\/:*?\"<>|^]").IsMatch(sText);
        }

        public static bool IsSpecalCharacterContins(string sText)
        {
            return new Regex("[~!@\\#$%^&*\\()\\=+|\\\\/:;?\"<>']").IsMatch(sText);
        }
    }
}
