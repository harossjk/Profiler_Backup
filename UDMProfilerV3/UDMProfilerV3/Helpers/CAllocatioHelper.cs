// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CAllocatioHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using UDM.Project;

namespace UDMProfilerV3
{
    public static class CAllocatioHelper
    {
        private static bool m_bTestMode = false;
        private static string m_sDirectory = Application.StartupPath + "\\Allocation";
        private static string m_sFilePath = "";
        private static string m_sFileExtention = ".all";
        private static int m_iPort = 10000;

        public static bool IsTestMode
        {
            get
            {
                return CAllocatioHelper.m_bTestMode;
            }
            set
            {
                CAllocatioHelper.m_bTestMode = value;
            }
        }

        public static int Port
        {
            get
            {
                return CAllocatioHelper.m_iPort;
            }
        }

        public static string Path
        {
            get
            {
                return CAllocatioHelper.m_sDirectory;
            }
        }

        public static void Initialize(bool bCloseAllProcess)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(CAllocatioHelper.m_sDirectory);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
            if (!bCloseAllProcess)
            {
                if (CAllocatioHelper.m_bTestMode || Process.GetProcessesByName(CAssemblyHelper.ProfilerProcessName).Length != 0)
                    return;
                Process[] processesByName = Process.GetProcessesByName(CAssemblyHelper.DDEAProcessName);
                if (processesByName != null && processesByName.Length > 0)
                {
                    foreach (Process process in processesByName)
                    {
                        //yjk, 18.10.02 - Try~Catch
                        try
                        {
                            process.Kill();
                        }
                        catch (Exception ex)
                        {
                            ex.Data.Clear();
                        }
                    }
                }
                CAllocatioHelper.ClearDirectory(CAllocatioHelper.m_sDirectory);
            }
            else
            {
                Process[] processesByName1 = Process.GetProcessesByName(CAssemblyHelper.DDEAProcessName);
                if (processesByName1 != null && processesByName1.Length > 0)
                {
                    foreach (Process process in processesByName1)
                    {
                        //yjk, 18.10.02 - Try~Catch
                        try
                        {
                            process.Kill();
                        }
                        catch (Exception ex)
                        {
                            ex.Data.Clear();
                        }
                    }
                }
                Process[] processesByName2 = Process.GetProcessesByName(CAssemblyHelper.ProfilerProcessName);
                if (processesByName2 != null && processesByName2.Length > 0)
                {
                    foreach (Process process in processesByName2)
                    {
                        //yjk, 18.10.02 - Try~Catch
                        try
                        {
                            process.Kill();
                        }
                        catch (Exception ex)
                        {
                            ex.Data.Clear();
                        }
                    }
                }
                CAllocatioHelper.ClearDirectory(CAllocatioHelper.m_sDirectory);
            }
        }

        public static bool AllocatePort()
        {
            bool flag = false;
            DirectoryInfo directoryInfo = new DirectoryInfo(CAllocatioHelper.m_sDirectory);
            try
            {
                FileInfo[] files = directoryInfo.GetFiles();
                if (files == null || files.Length == 0)
                {
                    CAllocatioHelper.m_sFilePath = CAllocatioHelper.CreateFile(CAllocatioHelper.m_sDirectory, CAllocatioHelper.m_iPort);
                }
                else
                {
                    int num = CAllocatioHelper.m_iPort;
                    int result = 0;
                    foreach (FileInfo fileInfo in files)
                    {
                        if (fileInfo.Extension == CAllocatioHelper.m_sFileExtention)
                        {
                            if (int.TryParse(fileInfo.Name.Split('.')[0], out result) && result > num)
                                num = result;
                        }
                    }
                    CAllocatioHelper.m_iPort = num + 1;
                    CAllocatioHelper.m_sFilePath = CAllocatioHelper.CreateFile(CAllocatioHelper.m_sDirectory, CAllocatioHelper.m_iPort);
                }
                if (CAllocatioHelper.m_sFilePath != "")
                    flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return flag;
        }

        public static bool ReleasePort()
        {
            if (CAllocatioHelper.m_sFilePath == "")
                return false;
            bool flag = false;
            try
            {
                if (File.Exists(CAllocatioHelper.m_sFilePath))
                {
                    File.Delete(CAllocatioHelper.m_sFilePath);
                    CAllocatioHelper.m_sFilePath = "";
                }
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return flag;
        }

        public static string AllocateProject(CProfilerProject_V8 cProject)
        {
            bool bSave = false;
            string sPath = CAllocatioHelper.Path + "\\ForDDEA_" + CAllocatioHelper.Port.ToString() + ".upm";
            
            CProfilerProjectManager pManager = new CProfilerProjectManager();
            pManager.Project = cProject;
            bSave = pManager.Save(sPath);

            if (!bSave)
                sPath = "";

            return sPath;
        }

        public static void ReleaseProject()
        {   
            try
            {
                string path = CAllocatioHelper.Path + "\\ForDDEA_" + CAllocatioHelper.Port.ToString() + ".upm";
                if (!File.Exists(path))
                    return;
                File.Delete(path);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private static string CreateFile(string sDirectory, int iPort)
        {
            string path = CAllocatioHelper.m_sDirectory + "\\" + CAllocatioHelper.m_iPort.ToString() + CAllocatioHelper.m_sFileExtention;
            FileStream fileStream = (FileStream)null;
            try
            {
                fileStream = new FileStream(path, FileMode.Create);
                fileStream.WriteByte((byte)0);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                path = "";
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            return path;
        }

        private static bool ClearDirectory(string sPath)
        {
            bool flag = true;
            DirectoryInfo directoryInfo = new DirectoryInfo(sPath);
            try
            {
                foreach (FileSystemInfo file in directoryInfo.GetFiles())
                    file.Delete();
                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    CAllocatioHelper.ClearDirectory(directory.FullName);
                    directory.Delete();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                flag = false;
            }
            return flag;
        }
    }
}
