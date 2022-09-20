// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CMessageHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.IO;
using System.Windows.Forms;

namespace UDMProfilerV3
{
    public static class CMessageHelper
    {
        private static bool m_bTestMode = false;

        public static bool IsTestMode
        {
            get
            {
                return CMessageHelper.m_bTestMode;
            }
            set
            {
                CMessageHelper.m_bTestMode = value;
            }
        }

        public static DialogResult ShowPopup(string sMessage, MessageBoxButtons emButtons, MessageBoxIcon emIcon)
        {
            return MessageBox.Show((IWin32Window)new Form()
            {
                TopMost = true
            }, sMessage, "UDM Profiler", emButtons, emIcon);
        }

        public static DialogResult ShowPopup(IWin32Window owner, string sMessage, MessageBoxButtons emButtons, MessageBoxIcon emIcon)
        {
            return MessageBox.Show(sMessage, "UDM Profiler", emButtons, emIcon);
        }

        //yjk, 18.08.10
        public static DialogResult ShowPopup(IWin32Window owner,string sMessage, string sTitle, MessageBoxButtons emButtons, MessageBoxIcon emIcon)
        {
            return MessageBox.Show(owner, sMessage, sTitle, emButtons, emIcon);
        }


        private static StreamWriter m_StreamWrite = null;
        private static string m_sFileName = "";

        public static void MeesageLogCreate(string sfileName,string sLogPath, string sApp)
        {
            DateTime dtNow = DateTime.Now;
            string sFilePath = string.Format(@"{0}\{1}{3}_{2}.txt", sLogPath, sApp, dtNow.ToString("yyyyMMddHHmmss"), sfileName);
            //string sFilePath = string.Format(@"{0}\{1}LSErrorLog_{2}.txt", sLogPath, sApp, dtNow.ToString("yyyyMMddHHmmss"));
            if (Directory.Exists(sLogPath) == false)
                Directory.CreateDirectory(sLogPath);
            m_sFileName = sApp + sfileName + dtNow.ToString("yyyyMMddHHmmss") + ".txt";
            m_StreamWrite = new StreamWriter(sFilePath);
            m_StreamWrite.AutoFlush = true;
        }


        public static void WriteLog(string sSender, string sMessage)
        {
            //yjk, 18.05.31 - null 예외처리
            if (m_StreamWrite == null)
            {
                m_StreamWrite = new StreamWriter(m_sFileName);
                m_StreamWrite.AutoFlush = true;
            }

            string sMsg = string.Format("{0}, {1}, {2}", DateTime.Now.ToString("yyyyMMddHHmmsss.fff"), sSender, sMessage);
            m_StreamWrite.WriteLine(sMsg);
        }

        public static void WriteEndLog()
        {
            m_StreamWrite.WriteLine("정상 종료되었습니다.");
            m_StreamWrite.Flush();
            m_StreamWrite.Close();
            m_StreamWrite = null;
        }

        //yjk, 18.02.08 - Exception 발생 시 WriteEndLog 호출
        public static void WriteEndLog(string sExceptionMessage)
        {
            m_StreamWrite.WriteLine("Exception : " + sExceptionMessage);
            m_StreamWrite.Flush();
            m_StreamWrite.Close();
            m_StreamWrite.Dispose();
            m_StreamWrite = null;
        }
    }
}
