// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CMachineRegistry
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using Microsoft.Win32;
using System.Diagnostics;

namespace UDM.DDEA
{
    public class CMachineRegistry
    {
        protected RegistryKey m_regKey = (RegistryKey)null;
        protected Process m_ProcessDDEA = (Process)null;
        protected bool m_bEmbeddedFlag = false;
        protected string m_sUpmPath = "Empty";
        protected string m_sConfigPath = "Empty";
        protected string m_sParamPath = "Empty";
        protected string m_sCollectState = "NM";
        protected string m_sRegPath = "";
        protected string m_sConfigFileChange = "N";
        protected string m_sUpmFileChange = "N";
        protected string m_sParamFileChange = "N";
        protected string m_sBlockNumber = "0";
        protected string m_sBlockCount = "0";
        protected string m_sCycleNumber = "0";
        protected string m_sCycleCount = "0";
        protected string m_sComplateTime = "없음"; 
        protected string m_sStartTime = "없음";
        protected string m_sRunState = "Off";
        protected string m_sCollectSpeed = "0";
        protected string m_sCycleState = "";
        protected string m_sDDEACollectState = "NM";
        protected string m_sModeState = "대기";
        protected string m_sDDEAControl = "Stop";
        protected string m_sDDEAHeart = "0";
        protected string m_sResponseCode = "000000";
        protected string m_sMachineDescription = "";
        protected string m_sVersion = "";

        public RegistryKey RegKey
        {
            get
            {
                return this.m_regKey;
            }
            set
            {
                this.m_regKey = value;
            }
        }

        public bool EmbeddedFlag
        {
            get
            {
                return this.m_bEmbeddedFlag;
            }
            set
            {
                this.m_bEmbeddedFlag = value;
            }
        }

        public string MachineDescription
        {
            get
            {
                return this.m_sMachineDescription;
            }
            set
            {
                this.m_sMachineDescription = value;
            }
        }

        public string UpmFilePath
        {
            get
            {
                this.m_sUpmPath = this.m_regKey.GetValue("UpmFilePath") as string;
                return this.m_sUpmPath;
            }
            set
            {
                this.m_sUpmPath = value;
            }
        }

        public string ParamFilePath
        {
            get
            {
                this.m_sParamPath = this.m_regKey.GetValue("ParamFilePath") as string;
                return this.m_sParamPath;
            }
            set
            {
                this.m_sParamPath = value;
            }
        }

        public string ConfigFilePath
        {
            get
            {
                this.m_sConfigPath = this.m_regKey.GetValue("ConfigFilePath") as string;
                return this.m_sConfigPath;
            }
            set
            {
                this.m_sConfigPath = value;
            }
        }

        public string CollectState
        {
            get
            {
                this.m_sCollectState = this.m_regKey.GetValue("CollectState") as string;
                return this.m_sCollectState;
            }
            set
            {
                this.m_sCollectState = value;
                if (this.m_regKey == null)
                    return;
                this.m_regKey.SetValue("CollectState", (object)this.m_sCollectState);
            }
        }

        public string DDEACollectState
        {
            get
            {
                this.m_sDDEACollectState = this.m_regKey.GetValue("DDEACollectState") as string;
                return this.m_sDDEACollectState;
            }
            set
            {
                this.m_sDDEACollectState = value;
                if (this.m_regKey == null)
                    return;
                this.m_regKey.SetValue("DDEACollectState", (object)this.m_sDDEACollectState);
            }
        }

        public string RegPath
        {
            get
            {
                return this.m_sRegPath;
            }
            set
            {
                this.m_sRegPath = value;
            }
        }

        public Process DDEAProcess
        {
            get
            {
                return this.m_ProcessDDEA;
            }
            set
            {
                this.m_ProcessDDEA = value;
            }
        }

        public string ConfigFileChanged
        {
            get
            {
                this.m_sConfigFileChange = this.m_regKey.GetValue("ConfigFileChanged") as string;
                return this.m_sConfigFileChange;
            }
            set
            {
                this.m_sConfigFileChange = value;
            }
        }

        public string UpmFileChanged
        {
            get
            {
                this.m_sUpmFileChange = this.m_regKey.GetValue("UpmFileChanged") as string;
                return this.m_sUpmFileChange;
            }
            set
            {
                this.m_sUpmFileChange = value;
            }
        }

        public string ParamFileChanged
        {
            get
            {
                this.m_sParamFileChange = this.m_regKey.GetValue("ParamFileChanged") as string;
                return this.m_sParamFileChange;
            }
            set
            {
                this.m_sParamFileChange = value;
            }
        }

        public string BlockNumber
        {
            get
            {
                this.m_sBlockNumber = this.m_regKey.GetValue("BlockNumber") as string;
                return this.m_sBlockNumber;
            }
            set
            {
                this.m_sBlockNumber = value;
            }
        }

        public string BlockCount
        {
            get
            {
                this.m_sBlockCount = this.m_regKey.GetValue("BlockCount") as string;
                return this.m_sBlockCount;
            }
            set
            {
                this.m_sBlockCount = value;
            }
        }

        public string CycleNumber
        {
            get
            {
                this.m_sCycleNumber = this.m_regKey.GetValue("CycleNumber") as string;
                return this.m_sCycleNumber;
            }
            set
            {
                this.m_sCycleNumber = value;
            }
        }

        public string CycleCount
        {
            get
            {
                this.m_sCycleCount = this.m_regKey.GetValue("CycleCount") as string;
                return this.m_sCycleCount;
            }
            set
            {
                this.m_sCycleCount = value;
            }
        }

        public string FragCompTime
        {
            get
            {
                this.m_sComplateTime = this.m_regKey.GetValue("ComlateTime") as string;
                return this.m_sComplateTime;
            }
            set
            {
                this.m_sComplateTime = value;
            }
        }

        public string FragStartTime
        {
            get
            {
                this.m_sStartTime = this.m_regKey.GetValue("StartTime") as string;
                return this.m_sStartTime;
            }
            set
            {
                this.m_sStartTime = value;
            }
        }

        public string RunState
        {
            get
            {
                this.m_sRunState = this.m_regKey.GetValue("RunState") as string;
                return this.m_sRunState;
            }
            set
            {
                this.m_sRunState = value;
            }
        }

        public string CollectSpeed
        {
            get
            {
                this.m_sCollectSpeed = this.m_regKey.GetValue("CollectSpeed") as string;
                return this.m_sCollectSpeed;
            }
            set
            {
                this.m_sCollectSpeed = value;
            }
        }

        public string CycleState
        {
            get
            {
                this.m_sCycleState = this.m_regKey.GetValue("CycleActive") as string;
                return this.m_sCycleState;
            }
            set
            {
                this.m_sCycleState = value;
            }
        }

        public string ModeState
        {
            get
            {
                this.m_sModeState = this.m_regKey.GetValue("ModeState") as string;
                return this.m_sModeState;
            }
            set
            {
                this.m_sModeState = value;
            }
        }

        public string DDEAControl
        {
            get
            {
                this.m_sDDEAControl = this.m_regKey.GetValue("DDEAControl") as string;
                return this.m_sDDEAControl;
            }
            set
            {
                this.m_sDDEAControl = value;
            }
        }

        public string DDEAHeart
        {
            get
            {
                this.m_sDDEAHeart = this.m_regKey.GetValue("DDEAHeart") as string;
                return this.m_sDDEAHeart;
            }
            set
            {
                this.m_sDDEAHeart = value;
            }
        }

        public string ResponseCode
        {
            get
            {
                this.m_sResponseCode = this.m_regKey.GetValue("ResponseCode") as string;
                return this.m_sResponseCode;
            }
            set
            {
                this.m_sResponseCode = value;
            }
        }

        public string Version
        {
            get
            {
                this.m_sVersion = this.m_regKey.GetValue("Version") as string;
                return this.m_sVersion;
            }
            set
            {
                this.m_sVersion = value;
            }
        }

        public void SetUpmFilePath(string sPath)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("UpmFilePath", (object)sPath);
        }

        public void SetParamFilePath(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("ParamFilePath", (object)Code);
        }

        public void SetConfigFilePath(string sPath)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("ConfigFilePath", (object)sPath);
        }

        public void SetUpmFileChanged(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("UpmFileChanged", (object)Code);
        }

        public void SetParamFileChanged(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("ParamFileChanged", (object)Code);
        }

        public void SetConfigFileChanged(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("ConfigFileChanged", (object)Code);
        }

        public void SetBlockNumber(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("BlockNumber", (object)Code);
        }

        public void SetBlockCount(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("BlockCount", (object)Code);
        }

        public void SetCycleNumber(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("CycleNumber", (object)Code);
        }

        public void SetCycleCount(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("CycleCount", (object)Code);
        }

        public void SetFragCompTime(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("ComlateTime", (object)Code);
        }

        public void SetFragStartTime(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("StartTime", (object)Code);
        }

        public void SetRunState(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("RunState", (object)Code);
        }

        public void SetCollectSpeed(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("CollectSpeed", (object)Code);
        }

        public void SetCycleState(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("CycleActive", (object)Code);
        }

        public void SetModeState(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("ModeState", (object)Code);
        }

        public void SetDDEAControl(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("DDEAControl", (object)Code);
        }

        public void SetDDEAHeart(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("DDEAHeart", (object)Code);
        }

        public void SetResponseCode(string Code)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("ResponseCode", (object)Code);
        }

        public void SetVersion(string sVersion)
        {
            if (this.m_regKey == null)
                return;
            this.m_regKey.SetValue("Version", (object)sVersion);
        }
    }
}
