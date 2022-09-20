// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CParameterHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace UDMProfilerV3
{
    public static class CParameterHelper
    {
        private static bool m_bTestMode = false;
        private static string m_sFilePath = Application.StartupPath + "\\Parameter.xml";
        private static CParameter m_cParameter = new CParameter();

        public static bool IsTestMode
        {
            get
            {
                return CParameterHelper.m_bTestMode;
            }
            set
            {
                CParameterHelper.m_bTestMode = value;
            }
        }

        public static CParameter Parameter
        {
            get
            {
                return m_cParameter;
            }
            set
            {
                m_cParameter = value;
            }
        }

        public static string XmlFilePath
        {
            get { return m_sFilePath; }
        }

        public static bool Save()
        {
            bool flag = false;
            FileStream fileStream = (FileStream)null;

            try
            {
                //fileStream = new FileStream(m_sFilePath, FileMode.Create);
                //new XmlSerializer(typeof(CParameter)).Serialize((Stream)fileStream, m_cParameter);
                //flag = true;

                //yjk, 19.01.16 - DataContract로 변경(속성에 Dictionary를 추가했기 때문)
                using (FileStream fs = new FileStream(m_sFilePath, FileMode.Create))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(CParameter));
                        serializer.WriteObject(fs, m_cParameter);
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            return flag;
        }

        public static bool Open()
        {
            if (!File.Exists(m_sFilePath))
            {
                m_cParameter.Initialize();
                return true;
            }

            bool flag = true;
            FileStream fileStream = null;

            try
            {
                //fileStream = new FileStream(m_sFilePath, FileMode.Open);
                //m_cParameter = (CParameter)new XmlSerializer(typeof(CParameter)).Deserialize((Stream)fileStream);
                //if (m_cParameter == null)
                //{
                //    m_cParameter = new CParameter();
                //    m_cParameter.Initialize();
                //}
                //flag = true;

                //yjk, 19.01.16 - DataContract로 변경(속성에 Dictionary를 추가했기 때문)
                using (FileStream fs = new FileStream(m_sFilePath, FileMode.Open))
                {
                    using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                    {
                        DataContractSerializer ser = new DataContractSerializer(typeof(CParameter));

                        // Deserialize the data and read it from the instance.
                        m_cParameter = (CParameter)ser.ReadObject(reader, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                CParameterHelper.m_cParameter.Initialize();
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            return flag;
        }
    }
}
