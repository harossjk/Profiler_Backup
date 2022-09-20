// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CViewLogicFileInfo
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.IO;
using System.Text;
using UDM.Common;

namespace UDMProfilerV3
{
    public class CViewLogicFileInfo
    {
        protected bool m_bValid = false;
        protected string m_sName = "";
        protected string m_sFormat = "";
        protected EMPLCMaker m_emPlcMaker = EMPLCMaker.None;
        protected int m_iSize = 0;
        protected byte[] m_baData = null;

        public CViewLogicFileInfo()
        {
        }

        public CViewLogicFileInfo(string sFilePath, EMPLCMaker emPlcMaker)
        {
            CreateInstance(sFilePath, emPlcMaker);
        }

        public bool IsValid
        {
            get
            {
                return m_bValid;
            }
            set
            {
                m_bValid = value;
            }
        }

        public string Name
        {
            get
            {
                return m_sName;
            }
            set
            {
                m_sName = value;
            }
        }

        public string Format
        {
            get
            {
                return m_sFormat;
            }
            set
            {
                m_sFormat = value;
            }
        }

        public int Size
        {
            get
            {
                return m_iSize;
            }
            set
            {
                m_iSize = value;
            }
        }

        public byte[] Data
        {
            get
            {
                return m_baData;
            }
            set
            {
                SetData(value);
            }
        }

        public string GetText()
        {
            if (this.m_baData == null || this.m_baData.Length == 0)
                return "";
            return Encoding.Default.GetString(this.m_baData);
        }

        public void SetText(string sText)
        {
            this.SetData(Encoding.Default.GetBytes(sText));
        }

        private void CreateInstance(string sFilePath, EMPLCMaker emPlcMaker)
        {
            if (!File.Exists(sFilePath))
                return;
            StreamReader streamReader = (StreamReader)null;
            try
            {
                streamReader = new StreamReader(sFilePath, Encoding.Default);
                string end = streamReader.ReadToEnd();
                this.m_sName = Path.GetFileName(sFilePath);
                m_emPlcMaker = emPlcMaker;
                this.SetText(end);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                }
            }
            //if (!File.Exists(sFilePath))
            //    return;
            //StreamReader streamReader = (StreamReader)null;
            //try
            //{
            //    streamReader = new StreamReader(sFilePath, Encoding.Default);
            //    string end = streamReader.ReadToEnd();
            //    this.m_sName = Path.GetFileName(sFilePath);
            //    switch (iFormatIndex)
            //    {
            //        case 1:
            //            this.m_sFormat = "Developer";
            //            break;
            //        case 2:
            //            this.m_sFormat = "Works2";
            //            break;
            //        case 3:
            //            this.m_sFormat = "LS";
            //            break;
            //    }
            //    this.SetText(end);
            //}
            //catch (Exception ex)
            //{
            //    ex.Data.Clear();
            //}
            //finally
            //{
            //    if (streamReader != null)
            //    {
            //        streamReader.Close();
            //        streamReader.Dispose();
            //    }
            //}
        }

        private void SetData(byte[] baData)
        {
            this.m_iSize = 0;
            if (baData == null || baData.Length == 0)
                return;
            this.m_iSize = baData.Length / 1024;
            if (this.m_iSize == 0)
                this.m_iSize = 1;
            this.m_baData = baData;
        }
    }
}
