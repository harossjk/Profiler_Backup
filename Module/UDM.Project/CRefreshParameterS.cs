using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDM.Project
{
    [Serializable]
    public class CRefreshParameterS : List<CRefreshParameter>
    {
        #region Memeber Variables

        private string m_sLinkSide = "";

        #endregion


        #region Initialize/Dispose

        public CRefreshParameterS()
        {

        }

        public void Dispose()
        {

        }

        #endregion


        #region Public Properties

        public string LinkSide
        {
            get { return m_sLinkSide; }
            set { m_sLinkSide = value; }
        }
        
        #endregion


        #region Public Methods


        #endregion


        #region Private Methods


        #endregion
    }
}
