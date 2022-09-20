using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.Common;
using UDM.DDEACommon;

namespace UDM.Project
{
    //yjk, 19.06.19 - 동작연계표 Class(CLogicChartDispItem_V2)를 새로 만듦
    [Serializable]
    public class CProfilerProject_V6 : CProfilerProject_V5
    {

        #region Variables

        //동작연계표 Item
        protected CLogicChartDispItemS_V2 m_cLogicDispItemS_V2 = null;

        #endregion


        #region Properties

        [MessagePackMember(46, Name = "LogicChartDispItemS_V2")]
        public CLogicChartDispItemS_V2 LogicChartDispItemS_V2
        {
            get { return m_cLogicDispItemS_V2; }
            set { m_cLogicDispItemS_V2 = value; }
        }

        #endregion


        #region Initailize

        public CProfilerProject_V6()
        {
            m_cLogicDispItemS_V2 = new CLogicChartDispItemS_V2();
        }

        public CProfilerProject_V6(CProfilerProject_V5 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Public Method

        public override void Dispose()
        {
            Clear();
        }

        public override void Compose()
        {
            if (m_cStepS != null)
            {
                m_cStepS.Compose(m_cTagS);
                m_cStepS.ComposeTagRoleS();
            }

            if (m_cFragmentPacketS != null)
                m_cFragmentPacketS.Compose(m_cStepS, m_cTagS);

            if (m_cNormalPacketS != null)
                m_cNormalPacketS.Compose(m_cStepS, m_cTagS);

            if (m_cFilterNormalPacketS != null)
                m_cFilterNormalPacketS.Compose(m_cStepS, m_cTagS);
        }

        public override void Clear()
        {
            if (m_cTagS != null)
                m_cTagS.Clear();

            if (m_cStepS != null)
                m_cStepS.Clear();

            if (m_cNormalPacketS != null)
                m_cNormalPacketS.Clear();

            if (m_cFragmentPacketS != null)
                m_cFragmentPacketS.Clear();

            if (m_cCycleStart != null)
                m_cCycleStart.Clear();

            if (m_cCycleEnd != null)
                m_cCycleEnd.Clear();

            if (m_lstMDCItem != null)
                m_lstMDCItem.Clear();

            if (m_lstMDCColumn != null)
                m_lstMDCColumn.Clear();

            if (m_cMDCTagS != null)
                m_cMDCTagS.Clear();

            m_cRecipe = new CTag();
            m_cGlassID = new CTag();
            m_cProcess = new CTag();
            m_cRefresh = new CTag();
            m_cTackTime = new CTag();

            if (m_cFilterNormalPacketS != null)
                m_cFilterNormalPacketS.Clear();

            //V6에서 추가
            m_cLogicDispItemS_V2 = new CLogicChartDispItemS_V2();
        }

        #endregion


        #region Protected Method

        protected void CreateFrom(CProfilerProject_V5 cOldVersion)
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

            if (m_cFilterNormalPacketS == null)
                m_cFilterNormalPacketS = new CPacketInfoS();

            //V6에서 추가된 변수
            m_cLogicDispItemS_V2 = new CLogicChartDispItemS_V2(cOldVersion.LogicChartDispItemS);
        }

        #endregion

    }
}
