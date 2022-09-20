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
    [Serializable]
    public class CProfilerProject_V8 : CProfilerProject_V7
    {
        #region Variables

        protected CStepS m_cAutoStepS = new CStepS();
        protected List<CTag> m_cAutoTagS = new List<CTag>(); //중복된 순서대로 저장하기위해 List로 저장

        #endregion

        #region Properties
        [MessagePackMember(48, Name = "AutoStepS")]
        public CStepS AutoStepS
        {
            get { return m_cAutoStepS; }
            set { m_cAutoStepS = value; }
        }

        [MessagePackMember(49, Name = "AutoTagS")]
        public List<CTag> AutoTagS
        {
            get { return m_cAutoTagS; }
            set { m_cAutoTagS = value; }
        }

        #endregion

        #region Initailize
        public CProfilerProject_V8()
        {
  
        }

        public CProfilerProject_V8(CProfilerProject_V7 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion

        #region Public Method

        #endregion

        #region Protected Method
      
        protected void CreateFrom(CProfilerProject_V7 cOldVersion)
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

            //jjk, 20.07.14 - logicChartdispItemS 0보다 크면 과거 버전에서 V2동작연계표로 올려주고 그것이 아니라면 그대로 전달.
            if (cOldVersion.LogicChartDispItemS.Count > 0)
            {
                //V6에서 추가된 변수
                m_cLogicDispItemS_V2 = new CLogicChartDispItemS_V2(cOldVersion.LogicChartDispItemS);
            }
            else if (cOldVersion.LogicChartDispItemS_V2.Count > 0)
            {
                m_cLogicDispItemS_V2 = cOldVersion.LogicChartDispItemS_V2;
            }

            //yjk, 20.02.03
            if (m_cParameterPacketS == null)
                m_cParameterPacketS = new CPacketInfoS();

            //jjk, 21.04.23 - Auto Step , Tag 생성
            if (m_cAutoTagS == null)
                m_cAutoTagS = new List<CTag>();
            if (m_cAutoStepS == null)
                m_cAutoStepS = new CStepS();
        }
        #endregion

    }
}
