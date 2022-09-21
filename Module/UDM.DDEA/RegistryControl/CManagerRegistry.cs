// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CManagerRegistry
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using Microsoft.Win32;
using System.Collections.Generic;

namespace UDM.DDEA
{
    public class CManagerRegistry
    {
        protected RegistryKey m_regLineKey = (RegistryKey)null;
        protected RegistryKey m_regMachineListKey = (RegistryKey)null;
        protected string m_sLineName = "Empty";
        protected string m_sTcpMode = "S";
        protected string m_sConfigFileName = "Empty";
        protected string m_sConfigPath = "C:";
        protected Dictionary<string, CMachineRegistry> m_dicMachineList = new Dictionary<string, CMachineRegistry>();
        protected const string m_REGBASE = "Software\\DDEAManager\\";
        protected const string m_REGSUBLINE = "Line";
        protected const string m_REGSUBMACHINELIST = "MachineList";
        protected const string m_UPMFILEPATH = "UpmFilePath";
        protected const string m_CONFIGFILEPATH = "ConfigFilePath";
        protected const string m_COLLECTSTATE = "CollectState";
        protected const string m_LINENAME = "LineName";
        protected const string m_TCPMODE = "TCPMode";
        protected const string m_CONFIGFILE = "ConfigFileName";
        protected const string m_CONFIGPATH = "ConfigPath";

        /// <summary>
        /// 접근하여 Read만할 때
        /// </summary>
        public CManagerRegistry()
        {
            string sLinePath = m_REGBASE + m_REGSUBLINE;
            string sMachineListPath = m_REGBASE + m_REGSUBMACHINELIST;

            m_regLineKey = Registry.LocalMachine.OpenSubKey(sLinePath, true);
            if (m_regLineKey == null)
            {
                m_regLineKey = Registry.LocalMachine.CreateSubKey(sLinePath);
                m_regLineKey.SetValue(m_LINENAME, m_sLineName);
                m_regLineKey.SetValue(m_CONFIGPATH, m_sConfigPath);
                m_regLineKey.SetValue(m_CONFIGFILE, m_sConfigFileName);
                m_regLineKey.SetValue(m_TCPMODE, m_sTcpMode);
            }
            else
            {
                string[] saValue = m_regLineKey.GetValueNames();
                for (int i = 0; i < saValue.Length; i++)
                {
                    if (m_LINENAME == saValue[i])
                        m_sLineName = m_regLineKey.GetValue(saValue[i]) as string;
                    else if (m_CONFIGFILE == saValue[i])
                        m_sConfigFileName = m_regLineKey.GetValue(saValue[i]) as string;
                    else if (m_CONFIGPATH == saValue[i])
                        m_sConfigPath = m_regLineKey.GetValue(saValue[i]) as string;
                    else if (m_TCPMODE == saValue[i])
                        m_sTcpMode = m_regLineKey.GetValue(saValue[i]) as string;
                }
            }
            m_regMachineListKey = Registry.LocalMachine.OpenSubKey(sMachineListPath, true);
            if (m_regMachineListKey == null)
                m_regMachineListKey = Registry.LocalMachine.CreateSubKey(sMachineListPath);
            else
            {
                string[] saSubKey = m_regMachineListKey.GetSubKeyNames();
                m_dicMachineList.Clear();
                for (int i = 0; i < saSubKey.Length; i++)
                {
                    RegistryKey regKey = Registry.LocalMachine.OpenSubKey(sMachineListPath + "\\" + saSubKey[i], true);
                    if (regKey != null)
                    {
                        CMachineRegistry cMachineReg = new CMachineRegistry();
                        cMachineReg.RegKey = regKey;
                        string[] saValue = regKey.GetValueNames();
                        for (int k = 0; k < saValue.Length; k++)
                        {
                            if (saValue[k] == m_UPMFILEPATH)
                                cMachineReg.UpmFilePath = regKey.GetValue(saValue[k]) as string;
                            else if (saValue[k] == m_CONFIGFILEPATH)
                                cMachineReg.ConfigFilePath = regKey.GetValue(saValue[k]) as string;
                            else if (saValue[k] == m_COLLECTSTATE)
                                cMachineReg.CollectState = regKey.GetValue(saValue[k]) as string;
                        }
                        m_dicMachineList.Add(saSubKey[i], cMachineReg);

                    }
                }
            }
        }


        public RegistryKey LineRegKey
        {
            get { return m_regLineKey; }
            set { m_regLineKey = value; }
        }

        public string LineName
        {
            get
            {
                m_sLineName = m_regLineKey.GetValue(m_LINENAME) as string;
                return m_sLineName;
            }
        }

        public string TcpMode
        {
            get
            {
                m_sTcpMode = m_regLineKey.GetValue(m_TCPMODE) as string;
                return m_sTcpMode;
            }
        }

        public string ConfigFileName
        {
            get
            {
                m_sConfigFileName = m_regLineKey.GetValue(m_CONFIGFILE) as string;
                return m_sConfigFileName;
            }
        }

        public string ConfigPath
        {
            get
            {
                m_sConfigPath = m_regLineKey.GetValue(m_CONFIGPATH) as string;
                return m_sConfigPath;
            }
        }

        /// <summary>
        /// 파일경로와 이름을 조합.
        /// </summary>
        public string ConfigFilePath
        {
            get { return m_sConfigPath + "\\" + m_sConfigFileName; }
        }

        public Dictionary<string, CMachineRegistry> MachineRegKeyList
        {
            get { return m_dicMachineList; }
            set { m_dicMachineList = value; }
        }

        public void DeleteLineRegKey()
        {
            Registry.LocalMachine.DeleteSubKeyTree(m_REGBASE + m_REGSUBLINE);

            m_regLineKey = Registry.LocalMachine.CreateSubKey(m_REGBASE + m_REGSUBLINE);
            m_regLineKey.SetValue(m_LINENAME, m_sLineName);
            m_regLineKey.SetValue(m_CONFIGPATH, m_sConfigPath);
            m_regLineKey.SetValue(m_CONFIGFILE, m_sConfigFileName);
            m_regLineKey.SetValue(m_TCPMODE, m_sTcpMode);
        }

        public string GetLineName()
        {
            m_sLineName = m_regLineKey.GetValue(m_LINENAME) as string;
            return m_sLineName;
        }

        public void SetLineName(string sName)
        {
            if (m_regLineKey != null)
            {
                m_sLineName = sName;
                m_regLineKey.SetValue(m_LINENAME, sName);
            }
        }

        public string GetConfigFileName()
        {
            m_sConfigFileName = m_regLineKey.GetValue(m_CONFIGFILE) as string;
            return m_sConfigFileName;
        }

        public void SetConfigFileName(string sFileName)
        {
            if (m_regLineKey != null)
            {
                m_sConfigFileName = sFileName;
                m_regLineKey.SetValue(m_CONFIGFILE, sFileName);
            }
        }

        public string GetConfigPath()
        {
            m_sConfigPath = m_regLineKey.GetValue(m_CONFIGPATH) as string;
            return m_sConfigPath;
        }

        public void SetConfigPath(string sPath)
        {
            if (m_regLineKey != null)
            {
                m_sConfigPath = sPath;
                m_regLineKey.SetValue(m_CONFIGPATH, sPath);
            }
        }

        public void SetTcpMode(string sMode)
        {
            if (m_regLineKey != null)
            {
                m_sTcpMode = sMode;
                m_regLineKey.SetValue(m_TCPMODE, sMode);
            }
        }
    }
}
