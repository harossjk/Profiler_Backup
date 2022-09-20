using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UDM.Common;

namespace UDMProfilerV3
{
    public class CUserTag :IDisposable
    {

        #region Member Variables

        private string m_sAddress = "";
        private string m_sDescription = "";
        private int m_iSize = 1;

        #endregion


        #region Initialize/Dispose

        public CUserTag()
        {

        }

        public CUserTag(string sAddress)
        {
            m_sAddress = sAddress;
        }

        public void Dispose()
        {

        }

        #endregion


        #region Public Properties

        public string Address
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }

        public int Size
        {
            get { return m_iSize; }
            set { m_iSize = value; }
        }

        #endregion


        #region Public Methods


        #endregion


        #region Private Methods


        #endregion
    }
}
