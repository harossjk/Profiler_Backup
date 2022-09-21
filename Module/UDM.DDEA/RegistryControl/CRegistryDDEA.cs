// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CRegistryDDEA
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using Microsoft.Win32;
using System;

namespace UDM.DDEA
{
    public class CRegistryDDEA
    {
        protected RegistryKey m_RegKey = (RegistryKey)null;
        protected string m_sRegKeyName = "";
        protected string m_sRunStateOrder = "Stop";
        protected string m_sRunStateDDEA = "Stop";
        protected string m_sModeStatus = "전체수집";
        protected string m_sCycleStatus = "Active";
        protected string m_sCurrentBlockNumber = "0";
        protected string m_sCurrentCycleNumber = "0";
        protected string m_sComplateTime = "없음";
        protected string m_sUpmFileName = "";
        protected string m_sLogPath = "";
        protected string m_sNormalDatPath = "";
        protected string m_sFragDatPath = "";
        protected string m_sBasePath = "";
        protected const int m_ITEMCOUNT = 8;
        protected const string m_REGBASE = "Software\\UDMTek\\DDEA\\";

        public CRegistryDDEA(string sName, bool bOpen)
        {
            this.m_sRegKeyName = sName;
            if (!bOpen)
            {
                this.CreateKey(sName);
            }
            else
            {
                this.OpenKey(this.RegistryFullPath);
                this.ReadKeyValue();
            }
        }

        public CRegistryDDEA(string sLineName)
        {
            this.m_sBasePath = "Software\\DDEAManager\\" + sLineName;
            this.m_RegKey = Registry.LocalMachine.OpenSubKey(this.m_sBasePath, true);
            if (this.m_RegKey != null)
                return;
            this.m_RegKey = Registry.LocalMachine.CreateSubKey(this.m_sBasePath);
        }

        public RegistryKey RegistryKey
        {
            get
            {
                return this.m_RegKey;
            }
            set
            {
                this.m_RegKey = value;
            }
        }

        public string RegistryKeyName
        {
            get
            {
                return this.m_sRegKeyName;
            }
            set
            {
                this.m_sRegKeyName = value;
            }
        }

        public string RegistryFullPath
        {
            get
            {
                return "Software\\UDMTek\\DDEA\\" + this.m_sRegKeyName;
            }
        }

        public string UpmFileName
        {
            get
            {
                return this.m_RegKey.GetValue("UPMFileName") as string;
            }
            set
            {
                this.m_RegKey.SetValue("UPMFileName", (object)value);
            }
        }

        public string ModeStatus
        {
            get
            {
                return this.m_RegKey.GetValue("ModeStatus") as string;
            }
            set
            {
                this.m_RegKey.SetValue("ModeStatus", (object)value);
            }
        }

        public string LogPath
        {
            get
            {
                return this.m_RegKey.GetValue("LogPath") as string;
            }
            set
            {
                this.m_RegKey.SetValue("LogPath", (object)value);
            }
        }

        public string NormalDatPath
        {
            get
            {
                return this.m_RegKey.GetValue("NormalDatPath") as string;
            }
            set
            {
                this.m_RegKey.SetValue("NormalDatPath", (object)value);
            }
        }

        public string FragDatPath
        {
            get
            {
                return this.m_RegKey.GetValue("FragDatPath") as string;
            }
            set
            {
                this.m_RegKey.SetValue("FragDatPath", (object)value);
            }
        }

        public string RunStatueOrder
        {
            get
            {
                return this.m_RegKey.GetValue("RunStateOrder") as string;
            }
            set
            {
                this.m_RegKey.SetValue("RunStateOrder", (object)value);
            }
        }

        public string RunStatueDDEA
        {
            get
            {
                return this.m_RegKey.GetValue("RunStateDDEA") as string;
            }
            set
            {
                this.m_RegKey.SetValue("RunStateDDEA", (object)value);
            }
        }

        public string CycleStatus
        {
            get
            {
                return this.m_RegKey.GetValue("CycleStatus") as string;
            }
            set
            {
                this.m_RegKey.SetValue("CycleStatus", (object)value);
            }
        }

        public string CurrentBlockNumber
        {
            get
            {
                return this.m_RegKey.GetValue("BlockCount") as string;
            }
            set
            {
                this.m_RegKey.SetValue("BlockCount", (object)value);
            }
        }

        public string CurrentCycleNumber
        {
            get
            {
                return this.m_RegKey.GetValue("CycleCount") as string;
            }
            set
            {
                this.m_RegKey.SetValue("CycleCount", (object)value);
            }
        }

        public string ComplateTime
        {
            get
            {
                return this.m_RegKey.GetValue("ComplateTime") as string;
            }
            set
            {
                this.m_RegKey.SetValue("ComplateTime", (object)value);
            }
        }

        public string DDEAOpen
        {
            get
            {
                return this.m_RegKey.GetValue("DDEAOpen") as string;
            }
            set
            {
                this.m_RegKey.SetValue("DDEAOpen", (object)value);
            }
        }

        public void CreateKey(string sKey)
        {
            string str = "Software\\UDMTek\\DDEA\\" + sKey;
            this.m_sRegKeyName = sKey;
            this.m_RegKey = Registry.LocalMachine.OpenSubKey(str, true);
            if (this.m_RegKey != null)
                return;
            this.m_RegKey = Registry.LocalMachine.CreateSubKey(str);
        }

        public void DeleteKey()
        {
            string subkey = "Software\\UDMTek\\DDEA\\" + this.m_sRegKeyName;
            Registry.LocalMachine.DeleteSubKey(subkey);
        }

        public void DeleteTree(string sBase)
        {
            Registry.LocalMachine.DeleteSubKeyTree(sBase);
        }

        public bool OpenKey(string sRegKeyFullPath)
        {
            this.m_RegKey = Registry.LocalMachine.OpenSubKey(sRegKeyFullPath);
            return this.m_RegKey != null;
        }

        public bool OpenKey()
        {
            this.m_RegKey = Registry.LocalMachine.OpenSubKey(this.RegistryFullPath);
            return this.m_RegKey != null;
        }

        public void ReadKeyValue()
        {
            try
            {
                this.m_sRunStateOrder = this.m_RegKey.GetValue("RunStateOrder") as string;
                this.m_sRunStateDDEA = this.m_RegKey.GetValue("RunStateDDEA") as string;
                this.m_sModeStatus = this.m_RegKey.GetValue("ModeStatus") as string;
                this.m_sCycleStatus = this.m_RegKey.GetValue("CycleStatus") as string;
                this.m_sCurrentBlockNumber = this.m_RegKey.GetValue("BlockCount") as string;
                this.m_sCurrentCycleNumber = this.m_RegKey.GetValue("CycleCount") as string;
                this.m_sComplateTime = this.m_RegKey.GetValue("ComplateTime") as string;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SetRunStateOrder(string sSource)
        {
        }
    }
}
