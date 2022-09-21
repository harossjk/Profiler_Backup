using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UDM.Common;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    [Serializable]
    public class CDDEACycleSymbolS
    {
        #region Member Veriables

        protected List<CDDEACycleSymbol> m_cStartCycleList = new List<CDDEACycleSymbol>();
        protected List<CDDEACycleSymbol> m_cEndCycleList = new List<CDDEACycleSymbol>();
        protected List<CDDEACycleSymbol> m_cTriggerCycleList = new List<CDDEACycleSymbol>();

        protected int m_iRecipeValue = -1;

        protected int m_iCycleMinTimeMS = 0;
        protected int m_iCycleMaxTimeMS = 0;

        protected bool m_bStartConditionComp = false;
        protected bool m_bEndConditionComp = false;

        #endregion


        #region Initialize

        public CDDEACycleSymbolS()
        {

        }

        #endregion


        #region Properties

        public List<CDDEACycleSymbol> StartCycleList
        {
            get { return m_cStartCycleList; }
            set { m_cStartCycleList = value; }
        }

        public List<CDDEACycleSymbol> EndCycleList
        {
            get { return m_cEndCycleList; }
            set { m_cEndCycleList = value; }
        }

        public List<CDDEACycleSymbol> TriggerCycleList
        {
            get { return m_cTriggerCycleList; }
            set { m_cTriggerCycleList = value; }
        }


        public int CycleMinTimeMs
        {
            get { return m_iCycleMinTimeMS; }
            set { m_iCycleMinTimeMS = value; }
        }

        public int CycleMaxTimeMs
        {
            get { return m_iCycleMaxTimeMS; }
            set { m_iCycleMaxTimeMS = value; }
        }

        public int RecipeValue
        {
            get { return m_iRecipeValue; }
            set { m_iRecipeValue = value; }
        }

        public int Count
        {
            get { return m_cStartCycleList.Count + m_cEndCycleList.Count; }
        }

        public bool StartConditionComplate
        {
            get { return m_bStartConditionComp; }
            set { m_bStartConditionComp = value; }
        }

        public bool EndConditionComplate
        {
            get { return m_bEndConditionComp; }
            set { m_bEndConditionComp = value; }
        }

        #endregion


        #region Public Method


        public void ClearComplateFlag()
        {
            foreach (CDDEACycleSymbol sym in m_cStartCycleList)
            {
                sym.ConditionComp = false;
                sym.ConditionChange = false;
            }

            foreach (CDDEACycleSymbol sym in m_cTriggerCycleList)
            {
                sym.ConditionComp = false;
                sym.ConditionChange = false;
            }

            foreach (CDDEACycleSymbol sym in m_cEndCycleList)
            {
                sym.ConditionComp = false;
                sym.ConditionChange = false;
            }
            m_bStartConditionComp = false;
            m_bEndConditionComp = false;
        }
        #endregion

    }
}
