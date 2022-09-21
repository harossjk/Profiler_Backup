using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDM.DDEA
{
    public class CControlDDEA
    {
        #region Member Veriables

        protected string m_sName = "";
        protected int m_iProccessID = -1;
        protected CRegistryDDEA m_cRegDDEA = null;

        #endregion


        #region Initialize / Despose

        public CControlDDEA(string sName)
        {
            m_sName = sName;
            m_cRegDDEA = new CRegistryDDEA(sName, false);
        }

        #endregion


        #region Properties

        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public int ProcessID
        {
            get { return m_iProccessID; }
            set { m_iProccessID = value; }
        }

        public CRegistryDDEA RegistryDDEA
        {
            get { return m_cRegDDEA; }
            set { m_cRegDDEA = value; }
        }

        #endregion
    }
}
