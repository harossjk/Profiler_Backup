// Decompiled with JetBrains decompiler
// Type: UDM.Project.CProfilerProject_V3
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using MsgPack.Serialization;
using System;
using UDM.Common;

namespace UDM.Project
{
    [Serializable]
    public class CProfilerProject_V3 : CProfilerProject_V2
    {
        protected int m_iFilterNormalCycleTime = 120000;
        protected int m_iFilterNormalCycleCount = 3;
        protected int m_iFilterNormalMinimumLogCount = 3;
        protected string m_sFilterNormalCycleTagKey = "";
        protected string m_sLogFileName = "";
        protected string m_sFilterNormalCycleStartKey = "";
        protected string m_sFilterNormalCycleTriggerKey = "";
        protected int m_iFilterNormalCycleStartValue = 1;
        protected int m_iFilterNormalCycleTriggerValue = 1;
        protected bool m_bFilterNormalCycleTriggerOption = true;

        public CProfilerProject_V3()
        {
        }

        public CProfilerProject_V3(CProfilerProject_V2 cOldVersion)
        {
            this.CreateFrom(cOldVersion);
        }

        [MessagePackMember(30, Name = "FilterNormalCycleTime")]
        public int FilterNormalCycleTime
        {
            get
            {
                return this.m_iFilterNormalCycleTime;
            }
            set
            {
                this.m_iFilterNormalCycleTime = value;
            }
        }

        [MessagePackMember(31, Name = "FilterNormalCycleCount")]
        public int FilterNormalCycleCount
        {
            get
            {
                return this.m_iFilterNormalCycleCount;
            }
            set
            {
                this.m_iFilterNormalCycleCount = value;
            }
        }

        [MessagePackMember(32, Name = "FilterNormalMinimumLogCount")]
        public int FilterNormalMinimumLogCount
        {
            get
            {
                return this.m_iFilterNormalMinimumLogCount;
            }
            set
            {
                this.m_iFilterNormalMinimumLogCount = value;
            }
        }

        [MessagePackMember(33, Name = "FilterNormalCycleTagKey")]
        public string FilterNormalCycleTagKey
        {
            get
            {
                return this.m_sFilterNormalCycleTagKey;
            }
            set
            {
                this.m_sFilterNormalCycleTagKey = value;
            }
        }

        [MessagePackMember(34, Name = "LogFileName")]
        public string LogFileName
        {
            get
            {
                return this.m_sLogFileName;
            }
            set
            {
                this.m_sLogFileName = value;
            }
        }

        [MessagePackMember(35, Name = "FilterNormalCycleStartKey")]
        public string FilterNormalCycleStartKey
        {
            get
            {
                return this.m_sFilterNormalCycleStartKey;
            }
            set
            {
                this.m_sFilterNormalCycleStartKey = value;
            }
        }

        [MessagePackMember(36, Name = "FilterNormalCycleTriggerKey")]
        public string FilterNormalCycleTriggerKey
        {
            get
            {
                return this.m_sFilterNormalCycleTriggerKey;
            }
            set
            {
                this.m_sFilterNormalCycleTriggerKey = value;
            }
        }

        [MessagePackMember(37, Name = "FilterNormalCycleStartValue")]
        public int FilterNormalCycleStartValue
        {
            get
            {
                return this.m_iFilterNormalCycleStartValue;
            }
            set
            {
                this.m_iFilterNormalCycleStartValue = value;
            }
        }

        [MessagePackMember(38, Name = "FilterNormalCycleTriggerValue")]
        public int FilterNormalCycleTriggerValue
        {
            get
            {
                return this.m_iFilterNormalCycleTriggerValue;
            }
            set
            {
                this.m_iFilterNormalCycleTriggerValue = value;
            }
        }

        [MessagePackMember(39, Name = "FilterNormalCycleTriggerOption")]
        public bool FilterNormalCycleTriggerOption
        {
            get
            {
                return this.m_bFilterNormalCycleTriggerOption;
            }
            set
            {
                this.m_bFilterNormalCycleTriggerOption = value;
            }
        }

        protected void CreateFrom(CProfilerProject_V2 cOldVersion)
        {
            this.m_sName = cOldVersion.Name;
            this.m_cTagS = cOldVersion.TagS;
            this.m_cStepS = cOldVersion.StepS;
            this.m_cCycleStart = cOldVersion.CycleStart;
            this.m_cCycleEnd = cOldVersion.CycleEnd;
            this.m_cCycleTrigger = cOldVersion.CycleTrigger;
            this.m_iMaxCycleTime = cOldVersion.MaxCycleTime;
            this.m_iMinCycleTime = cOldVersion.MinCycleTime;
            this.m_iCycleCount = cOldVersion.CycleCount;
            this.m_cRecipe = cOldVersion.RecipeTag;
            this.m_cGlassID = cOldVersion.GlassIDTag;
            this.m_cProcess = cOldVersion.ProcessTag;
            this.m_cTackTime = cOldVersion.TackTimeTag;
            this.m_cRefresh = cOldVersion.RefreshTag;
            this.m_cMDCTagS = cOldVersion.MDCTagInfoS;
            this.m_lstMDCColumn = cOldVersion.MDCColumnList;
            this.m_lstMDCItem = cOldVersion.MDCItemList;
            this.m_cMotionOption = cOldVersion.MotionOption;
            this.m_cFilterOption = cOldVersion.FilterOption;
            this.m_cNormalPacketS = cOldVersion.NormalPacketS;
            this.m_cFragmentPacketS = cOldVersion.FragmentPacketS;
            this.m_cStandardPacketS = cOldVersion.StandardPacketS;
            this.m_sStandardRecipe = cOldVersion.StandardRecipe;
            this.m_iStandardCycleCount = cOldVersion.StandardCycleCount;
            this.m_bRecipeFixedCollected = cOldVersion.IsRecipeFixedCollected;
            this.m_cLogicChartDispItemS = cOldVersion.LogicChartDispItemS;
            this.m_cMdcChartDispItemS = cOldVersion.MdcChartDispItemS;
            this.m_cMdcChartItemDetailS_V2 = new CMdcChartItemDetailS_V2(cOldVersion.MdcChartItemDetailS);
            this.m_cMdcChartItemDetailAxis_V2 = new CMdcChartItemDetailAxis_V2();
            this.m_bAllowDuplicatedPacket = cOldVersion.AllowDuplicatedPacket;
            this.m_dicAddressLimit = cOldVersion.PLCAddressLimit;
            this.m_sFilterNormalCycleTagKey = "";
            this.m_iFilterNormalCycleTime = 120000;
            this.m_iFilterNormalCycleCount = 2;
            this.m_iFilterNormalMinimumLogCount = 2;
            this.m_sFilterNormalCycleStartKey = "";
            this.m_sFilterNormalCycleTriggerKey = "";
            this.m_iFilterNormalCycleStartValue = 1;
            this.m_iFilterNormalCycleTriggerValue = 1;
            this.m_bFilterNormalCycleTriggerOption = true;
        }
    }
}
