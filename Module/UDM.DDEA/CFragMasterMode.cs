using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.DDEACommon;
using UDM.Common;

namespace UDM.DDEA
{
    [Serializable]
    public class CFragMasterMode
    {
        #region Member Variables
        protected CDDEACycleSymbolS m_cCycleConditionS = new CDDEACycleSymbolS();
        protected CDDEASymbolS m_cCycleSymbolS = new CDDEASymbolS();
        protected CDDEASymbol m_cSwitchingSymbol = new CDDEASymbol();
        protected int m_iSwitchingCount = 0;
        protected int m_iSwitchingValue = 0;
        protected int m_iCurrentCount = 0;

        protected CDDEASymbolList m_cBitSymbolList = new CDDEASymbolList();

        #endregion

        
        #region Initialize

        public CFragMasterMode()
        {

        }

        #endregion


        #region Properties

        public CDDEACycleSymbolS CycleConditionSymbolS
        {
            get { return m_cCycleConditionS; }
            set { m_cCycleConditionS = value; }
        }

        public CDDEASymbolS CycleSymbolS
        {
            get { return m_cCycleSymbolS; }
            set { m_cCycleSymbolS = value; }
        }

        public CDDEASymbol SwitchingSymbol
        {
            get { return m_cSwitchingSymbol; }
            set { m_cSwitchingSymbol = value; }
        }

        public int SwitchingCount
        {
            get { return m_iSwitchingCount; }
            set { m_iSwitchingCount = value; }
        }

        public int SwitchingValue
        {
            get { return m_iSwitchingValue; }
            set { m_iSwitchingValue = value; }
        }

        public int SwitchCurrentCount
        {
            get { return m_iCurrentCount; }
            set { m_iCurrentCount = value; }
        }

        public CDDEASymbolList BitSymbolList
        {
            get { return m_cBitSymbolList; }
            set { m_cBitSymbolList = value; }
        }

        #endregion


        #region Public Method

        public void SetDDEACycle(CConditionS cStartCondition, CConditionS cEndCondition, CConditionS cTriggerCondition, CDDEASymbolList lstSymbol)
        {
            List<string> lstCycleTagKey = GetCycleAddressList(cStartCondition, cEndCondition, cTriggerCondition);
            for (int i = 0; i < lstCycleTagKey.Count; i++)
            {
                CDDEASymbol cFind = lstSymbol.Find(b => b.Key == lstCycleTagKey[i]);
                if(cFind != null)
                {
                    if (m_cCycleSymbolS.ContainsKey(cFind.Key) == false)
                        m_cCycleSymbolS.Add(cFind.Key, cFind);
                }
            }

            foreach (CCondition condi in cStartCondition)
            {
                CDDEACycleSymbol cStartCycle = new CDDEACycleSymbol();
                cStartCycle.Condition = condi.TargetValue;

                foreach (var who in m_cCycleSymbolS)
                {
                    if (who.Value.Address == condi.Address)
                        cStartCycle.Symbol = who.Value;
                }

                m_cCycleConditionS.StartCycleList.Add(cStartCycle);
            }

            foreach (CCondition condi in cEndCondition)
            {
                CDDEACycleSymbol cEndCycle = new CDDEACycleSymbol();
                cEndCycle.Condition = condi.TargetValue;
                foreach (var who in m_cCycleSymbolS)
                {
                    if (who.Value.Address == condi.Address)
                        cEndCycle.Symbol = who.Value;
                }
                m_cCycleConditionS.EndCycleList.Add(cEndCycle);
            }

            foreach (CCondition condi in cTriggerCondition)
            {
                CDDEACycleSymbol cTriggerCycle = new CDDEACycleSymbol();
                cTriggerCycle.Condition = condi.TargetValue;
                foreach (var who in m_cCycleSymbolS)
                {
                    if (who.Value.Address == condi.Address)
                        cTriggerCycle.Symbol = who.Value;
                }
                m_cCycleConditionS.TriggerCycleList.Add(cTriggerCycle);
            }
        }

        #endregion


        #region Protected Method

        protected List<string> GetCycleAddressList(CConditionS cStartCond, CConditionS cEndCond, CConditionS cTriggerCond)
        {
            List<string> lstResult = new List<string>();

            foreach (CCondition cond in cStartCond)
            {
                if (lstResult.Contains(cond.Key) == false)
                    lstResult.Add(cond.Key);
            }
            foreach (CCondition cond in cEndCond)
            {
                if (lstResult.Contains(cond.Key) == false)
                    lstResult.Add(cond.Key);
            }
            foreach (CCondition cond in cTriggerCond)
            {
                if (lstResult.Contains(cond.Key) == false)
                    lstResult.Add(cond.Key);
            }

            return lstResult;
        }

        protected List<CTag> GetCycleTagList(List<string> lstAddress, CTagS cAllTagS)
        {
            List<CTag> lstResult = new List<CTag>();

            for (int i = 0; i < lstAddress.Count; i++)
            {
                foreach (var who in cAllTagS)
                {
                    if (who.Value.Address == lstAddress[i])
                    {
                        if (lstResult.Contains(who.Value) == false)
                            lstResult.Add(who.Value);
                        break;
                    }
                }
            }

            return lstResult;
        }

        protected CDDEASymbolS ChangeTagSToDDEASymbolS(List<CTag> lstTag)
        {
            CDDEASymbolS cSymbolS = new CDDEASymbolS();

            foreach (CTag sym in lstTag)
            {
                CDDEASymbol cSymbol = new CDDEASymbol(sym.Key, false);
                cSymbol.CreateMelsecDDEASymbol(sym.Address);
                cSymbol.AddressCount = sym.Size;
                if (cSymbolS.ContainsKey(cSymbol.Key) == false)
                    cSymbolS.AddSymbol(cSymbol);

                //Length가 1이상 일경우 새로운Word를 생성한다 단, 새로 생성된 접점은 Length가 0이다.
                if (cSymbol.DataType == EMDataType.Word && sym.Size > 1)
                {
                    cSymbolS.CreateWordLength(cSymbol);
                }
            }

            return cSymbolS;
        }

        #endregion

    }
}
