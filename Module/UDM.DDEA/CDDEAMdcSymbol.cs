using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UDM.DDEACommon;

namespace UDM.DDEA
{
    public class CDDEAMdcSymbol : CDDEASymbol
    {
        #region Member Variables

        protected List<string> m_lstPcode = new List<string>();
        protected List<string> m_lstDName = new List<string>();

        #endregion


        #region Initialize

        public CDDEAMdcSymbol()
        {


        }

        public CDDEAMdcSymbol(string sKey, bool bCreate)
        {
            if (bCreate)
                m_sKey = CreateKey(sKey);
            else
                m_sKey = sKey;
        }

        #endregion


        #region Properties

        public List<string> PCodeList
        {
            get { return m_lstPcode; }
            set { m_lstPcode = value; }
        }

        public List<string> DNameList
        {
            get { return m_lstDName; }
            set { m_lstDName = value; }
        }

        #endregion


        #region Public Method

        #endregion
    }
}
