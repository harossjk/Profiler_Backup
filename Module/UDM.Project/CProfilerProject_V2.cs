// Decompiled with JetBrains decompiler
// Type: UDM.Project.CProfilerProject_V2
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using MsgPack.Serialization;
using System;
using UDM.Common;

namespace UDM.Project
{
    [Serializable]
    public class CProfilerProject_V2 : CProfilerProject
    {
        protected CMdcChartItemDetailS_V2 m_cMdcChartItemDetailS_V2 = new CMdcChartItemDetailS_V2();
        protected CMdcChartItemDetailAxis_V2 m_cMdcChartItemDetailAxis_V2 = new CMdcChartItemDetailAxis_V2();

        public CProfilerProject_V2()
        {
        }

        public CProfilerProject_V2(CProfilerProject cOldVersion)
        {
            this.CreateFrom(cOldVersion);
        }

        public new CMdcChartItemDetailS MdcChartItemDetailS
        {
            get
            {
                return this.m_cMdcChartItemDetailS;
            }
            set
            {
                this.m_cMdcChartItemDetailS = value;
            }
        }

        [MessagePackMember(27, Name = "MdcChartItemDetailS")]
        public CMdcChartItemDetailS_V2 MdcChartItemDetailS_V2
        {
            get
            {
                return this.m_cMdcChartItemDetailS_V2;
            }
            set
            {
                this.m_cMdcChartItemDetailS_V2 = value;
            }
        }

        [MessagePackMember(29, Name = "MdcChartItemDetailAxis")]
        public CMdcChartItemDetailAxis_V2 MdcChartItemDetailAxis_V2
        {
            get
            {
                return this.m_cMdcChartItemDetailAxis_V2;
            }
            set
            {
                this.m_cMdcChartItemDetailAxis_V2 = value;
            }
        }

        protected void CreateFrom(CProfilerProject cOldVersion)
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
        }
    }
}
