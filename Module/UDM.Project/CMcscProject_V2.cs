// Decompiled with JetBrains decompiler
// Type: UDM.Project.CMcscProject_V2
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace UDM.Project
{
    [Serializable]
    public class CMcscProject_V2 : CMcscProject
    {
        protected List<string> m_lstStepAddressFilter = new List<string>();
        protected List<string> m_lstStepDescriptionFitler = new List<string>();
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
        protected bool m_bFilterNormalMode = false;
        private Dictionary<string, int> m_dicTraceDepth = new Dictionary<string, int>();

        public CMcscProject_V2()
        {
        }

        public CMcscProject_V2(CMcscProject cOldVersion)
        {
            this.CreateFrom(cOldVersion);
        }

        [MessagePackMember(28, Name = "StepAddressFilter")]
        public List<string> StepAddressFilterList
        {
            get
            {
                return this.m_lstStepAddressFilter;
            }
            set
            {
                this.m_lstStepAddressFilter = value;
            }
        }

        [MessagePackMember(29, Name = "StepDescriptionFilter")]
        public List<string> StepDescriptionFilterList
        {
            get
            {
                return this.m_lstStepDescriptionFitler;
            }
            set
            {
                this.m_lstStepDescriptionFitler = value;
            }
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

        [MessagePackMember(40, Name = "FilterNormalMode")]
        public bool IsFilterNormalMode
        {
            get
            {
                return this.m_bFilterNormalMode;
            }
            set
            {
                this.m_bFilterNormalMode = value;
            }
        }

        [MessagePackMember(41, Name = "TraceDepthList")]
        public Dictionary<string, int> TraceDepthList
        {
            get
            {
                return this.m_dicTraceDepth;
            }
            set
            {
                this.m_dicTraceDepth = value;
            }
        }

        protected void CreateFrom(CMcscProject cOldVersion)
        {
            m_sName = cOldVersion.Name;
            m_cTagS = cOldVersion.TagS;
            m_cStepS = cOldVersion.StepS;
            m_cCycleStart = cOldVersion.CycleStart;
            m_cCycleEnd = cOldVersion.CycleEnd;
            m_cCycleTrigger = cOldVersion.CycleTrigger;
            m_iMaxCycleTime = cOldVersion.MaxCycleTime;
            m_iMinCycleTime = cOldVersion.MinCycleTime;
            m_iCycleCount = cOldVersion.CycleCount;
            m_cRecipe = cOldVersion.RecipeTag;
            m_cGlassID = cOldVersion.GlassIDTag;
            m_cProcess = cOldVersion.ProcessTag;
            m_cTackTime = cOldVersion.TackTimeTag;
            m_cRefresh = cOldVersion.RefreshTag;
            m_cMDCTagS = cOldVersion.MDCTagInfoS;
            m_lstMDCColumn = cOldVersion.MDCColumnList;
            m_lstMDCItem = cOldVersion.MDCItemList;
            m_cMotionOption = cOldVersion.MotionOption;
            m_cFilterOption = cOldVersion.FilterOption;
            m_cNormalPacketS = cOldVersion.NormalPacketS;
            m_cFragmentPacketS = cOldVersion.FragmentPacketS;
            m_cStandardPacketS = cOldVersion.StandardPacketS;
            m_sStandardRecipe = cOldVersion.StandardRecipe;
            m_iStandardCycleCount = cOldVersion.StandardCycleCount;
            m_bRecipeFixedCollected = cOldVersion.IsRecipeFixedCollected;
            m_cLogicChartDispItemS = cOldVersion.LogicChartDispItemS;
            m_cMdcChartDispItemS = cOldVersion.MdcChartDispItemS;

            //yjk, 18.11.19 - 할당변수가 없어서 추가
            m_cMdcChartItemDetailS = cOldVersion.MdcChartItemDetailS;

            m_lstStepAddressFilter = new List<string>();
            m_lstStepDescriptionFitler = new List<string>();
            m_dicTraceDepth = new Dictionary<string, int>();
            m_sFilterNormalCycleTagKey = "";
            m_iFilterNormalCycleTime = 120000;
            m_iFilterNormalCycleCount = 2;
            m_iFilterNormalMinimumLogCount = 2;
            m_sFilterNormalCycleStartKey = "";
            m_sFilterNormalCycleTriggerKey = "";
            m_iFilterNormalCycleStartValue = 1;
            m_iFilterNormalCycleTriggerValue = 1;
            m_bFilterNormalCycleTriggerOption = true;
            m_bFilterNormalMode = false;
        }
    }
}
