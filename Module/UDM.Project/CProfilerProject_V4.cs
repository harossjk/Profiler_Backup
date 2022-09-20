// Decompiled with JetBrains decompiler
// Type: UDM.Project.CProfilerProject_V4
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using MsgPack.Serialization;
using System;
using UDM.Common;
using UDM.DDEACommon;
using UDM.LS;
using System.Collections.Generic;

namespace UDM.Project
{
    [Serializable]
    public class CProfilerProject_V4 : CProfilerProject_V3
    {
        protected CLsConfig m_cLsConfig = null;
        protected EMPlcMaker m_emPlcMaker = EMPlcMaker.MITSUBISHI;

        public CProfilerProject_V4()
        {
        }

        public CProfilerProject_V4(CProfilerProject_V3 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        [MessagePackMember(42, Name = "LSConfig")]
        public CLsConfig LSConfig
        {
            get
            {
                return m_cLsConfig;
            }
            set
            {
                m_cLsConfig = value;
            }
        }

        [MessagePackMember(43, Name = "PLCMaker")]
        public EMPlcMaker PLCMaker
        {
            get
            {
                return m_emPlcMaker;
            }
            set
            {
                m_emPlcMaker = value;
            }
        }

        protected void CreateFrom(CProfilerProject_V3 cOldVersion)
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
            m_cMdcChartItemDetailS_V2 = new CMdcChartItemDetailS_V2(cOldVersion.MdcChartItemDetailS);
            m_cMdcChartItemDetailAxis_V2 = new CMdcChartItemDetailAxis_V2();
            m_bAllowDuplicatedPacket = cOldVersion.AllowDuplicatedPacket;
            m_dicAddressLimit = cOldVersion.PLCAddressLimit;
            m_sFilterNormalCycleTagKey = "";
            m_iFilterNormalCycleTime = 120000;
            m_iFilterNormalCycleCount = 2;
            m_iFilterNormalMinimumLogCount = 2;
            m_sFilterNormalCycleStartKey = "";
            m_sFilterNormalCycleTriggerKey = "";
            m_iFilterNormalCycleStartValue = 1;
            m_iFilterNormalCycleTriggerValue = 1;
            m_bFilterNormalCycleTriggerOption = true;

            if (m_cLsConfig != null)
                m_emPlcMaker = EMPlcMaker.LS;
        }
    }
}
