using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.Common;
using MsgPack.Serialization;
using UDM.DDEACommon;
using UDM.LS;

namespace UDM.Project
{
    //yjk, 18.10.2 - FilterNormal PacketInfoS를 별개로 관리
    [Serializable]
    public class CProfilerProject_V5 : CProfilerProject_V4
    {

        #region Member Variables

        //yjk, 18.10.04 - 필터 수집 패킷
        protected CPacketInfoS m_cFilterNormalPacketS = new CPacketInfoS();

        //yjk, 18.10.12 - DDEA에서 LS 패킷 설정할 때 모드를 모르고 있기 때문에 모드를 가지고 있음
        protected EMCollectMode m_emMode = EMCollectMode.Wait;


        #endregion


        #region Properties

        [MessagePackMember(44, Name = "FilterNormalPacketS")]
        public CPacketInfoS FilterNormalPacketS
        {
            get { return m_cFilterNormalPacketS; }
            set { m_cFilterNormalPacketS = value; }
        }

        //yjk, 18.10.12 - CollectMode
        [MessagePackMember(45, Name = "CollectMode")]
        public EMCollectMode CollectMode
        {
            get { return m_emMode; }
            set { m_emMode = value; }
        }

        #endregion


        #region Initailize

        public CProfilerProject_V5()
        {
        }

        public CProfilerProject_V5(CProfilerProject_V4 cOldVersion)
        {
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Public Method

        //yjk, 18.08.03 - Melsec Filter Normal Mode
        public void CreateMSFilterNormalModePacketInfoS(List<CTag> lstCycleTag, List<CTag> lstTotalTag, int iMaxWord)
        {
            for (int index = 0; index < lstCycleTag.Count; ++index)
            {
                CTag ctag = lstCycleTag[index];
                if (!lstTotalTag.Contains(ctag))
                    lstTotalTag.Add(ctag);
            }

            lstTotalTag.Sort((IComparer<CTag>)new CTagComparerByAddress());
            m_cFilterNormalPacketS.Clear();

            try
            {
                if (CPacketHelper.GetWordSize(lstTotalTag, m_dicAddressLimit) > iMaxWord)
                {
                    int iCnt = 0;
                    CPacketInfo cpacketInfo = new CPacketInfo();
                    List<CTag> lstTag = new List<CTag>();

                    //최초 패킷에 사이클 관련 Tag 추가
                    if (lstCycleTag.Count > 0)
                    {
                        for (int index = 0; index < lstCycleTag.Count; ++index)
                        {
                            CTag ctag = lstCycleTag[index];
                            lstTag.Add(ctag);
                        }

                        iCnt = lstCycleTag.Count;
                    }

                    foreach (CTag ctag1 in lstTotalTag)
                    {
                        if (!lstCycleTag.Contains(ctag1))
                        {
                            lstTag.Add(ctag1);
                            ++iCnt;

                            if (iCnt >= iMaxWord)
                            {
                                iCnt = CPacketHelper.GetWordSize(lstTag, m_dicAddressLimit);
                                if (iCnt >= iMaxWord)
                                {
                                    foreach (CTag oValue in lstTag)
                                        cpacketInfo.RefTagS.Add(oValue.Key, oValue);

                                    m_cFilterNormalPacketS.Add(cpacketInfo);
                                    cpacketInfo = new CPacketInfo();

                                    lstTag = new List<CTag>();
                                    iCnt = 0;

                                    if (lstCycleTag.Count > 0)
                                    {
                                        //매 패킷마다 사이클 관련 Tag 추가
                                        for (int index = 0; index < lstCycleTag.Count; ++index)
                                        {
                                            CTag ctag2 = lstCycleTag[index];
                                            lstTag.Add(ctag2);
                                        }

                                        iCnt = lstCycleTag.Count;
                                    }
                                }
                            }
                        }
                    }

                    if (lstTag.Count <= lstCycleTag.Count)
                        return;

                    foreach (CTag oValue in lstTag)
                        cpacketInfo.RefTagS.Add(oValue.Key, oValue);

                    m_cFilterNormalPacketS.Add(cpacketInfo);
                }
                else
                {
                    CPacketInfo cpacketInfo = new CPacketInfo();

                    for (int index = 0; index < lstTotalTag.Count; ++index)
                    {
                        CTag oValue = lstTotalTag[index];
                        cpacketInfo.RefTagS.Add(oValue.Key, oValue);
                    }

                    m_cFilterNormalPacketS.Add(cpacketInfo);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        //yjk, 18.08.03 - LS Filter Normal Mode
        //yjk, 18.10.05 - FilterNormalPacketInfoS에 추가하는 것으로 수정
        public void CreateLSFilterNormalModePacketInfoS(List<CTag> lstCycleTag, List<CTag> lstTotalTag, int iMaxSize)
        {
            for (int index = 0; index < lstCycleTag.Count; ++index)
            {
                CTag ctag = lstCycleTag[index];
                if (!lstTotalTag.Contains(ctag))
                    lstTotalTag.Add(ctag);
            }

            lstTotalTag.Sort((IComparer<CTag>)new CTagComparerByAddress());
            m_cFilterNormalPacketS.Clear();

            try
            {


                if (CLSHelper.GetBufferSize(lstTotalTag) > iMaxSize)
                {
                    CPacketInfo cpacketInfo = new CPacketInfo();
                    List<CTag> lstTag = new List<CTag>();

                    if (lstCycleTag.Count > 0)
                    {
                        for (int index = 0; index < lstCycleTag.Count; ++index)
                        {
                            CTag ctag = lstCycleTag[index];
                            lstTag.Add(ctag);
                        }
                    }

                    foreach (CTag tag in lstTotalTag)
                    {
                        if (lstCycleTag.Count == 0 || !lstCycleTag.Exists(f => f.Key.Equals(tag.Key)))
                        {
                            lstTag.Add(tag);

                            if (CLSHelper.GetBufferSize(lstTag) >= iMaxSize)
                            {
                                foreach (CTag oValue in lstTag)
                                    cpacketInfo.RefTagS.Add(oValue.Key, oValue);

                                m_cFilterNormalPacketS.Add(cpacketInfo);

                                cpacketInfo = new CPacketInfo();
                                lstTag = new List<CTag>();

                                if (lstCycleTag.Count > 0)
                                {
                                    for (int i = 0; i < lstCycleTag.Count; ++i)
                                    {
                                        CTag cycleTag = lstCycleTag[i];
                                        lstTag.Add(cycleTag);
                                    }
                                }
                            }
                        }
                    }

                    if (lstTag.Count <= lstCycleTag.Count)
                        return;

                    foreach (CTag oValue in lstTag)
                        cpacketInfo.RefTagS.Add(oValue.Key, oValue);

                    m_cFilterNormalPacketS.Add(cpacketInfo);
                }
                else
                {
                    CPacketInfo cpacketInfo = new CPacketInfo();

                    for (int index = 0; index < lstTotalTag.Count; ++index)
                    {
                        CTag oValue = lstTotalTag[index];
                        cpacketInfo.RefTagS.Add(oValue.Key, oValue);
                    }

                    m_cFilterNormalPacketS.Add(cpacketInfo);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

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
        }

        #endregion


        #region Protected Method

        protected void CreateFrom(CProfilerProject_V4 cOldVersion)
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

            if (m_cFilterNormalPacketS == null)
                m_cFilterNormalPacketS = new CPacketInfoS();
        }

        #endregion

    }
}
