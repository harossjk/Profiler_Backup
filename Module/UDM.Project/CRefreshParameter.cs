using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDM.Project
{
    [Serializable]
    public class CRefreshParameter : IDisposable
    {
        #region Memeber Variables

        private string m_sCategory = "";
        
        private string m_sLinkDevice = "";
        private string m_sLinkPoints = "";
        private string m_sLinkStart = "";
        private string m_sLinkEnd = "";

        private string m_sPLCDevice = "";
        private string m_sPLCPoints = "";
        private string m_sPLCStart = "";
        private string m_sPLCEnd = "";

        #endregion


        #region Initialize/Dispose

        public CRefreshParameter()
        {

        }

        public void Dispose()
        {

        }

        #endregion


        #region Public Properties

        public string Category
        {
            get { return m_sCategory; }
            set { m_sCategory = value; }
        }

        public string LinkDevice
        {
            get { return m_sLinkDevice; }
            set { m_sLinkDevice = value; }
        }

        public string LinkPoints
        {
            get { return m_sLinkPoints; }
            set { m_sLinkPoints = value; }
        }

        public string LinkStart
        {
            get { return m_sLinkStart; }
            set { m_sLinkStart = value; }
        }

        public string LinkEnd
        {
            get { return m_sLinkEnd; }
            set { m_sLinkEnd = value; }
        }

        public string PLCDevice
        {
            get { return m_sPLCDevice; }
            set { m_sPLCDevice = value; }
        }

        public string PLCPoints
        {
            get { return m_sPLCPoints; }
            set { m_sPLCPoints = value; }
        }

        public string PLCStart
        {
            get { return m_sPLCStart; }
            set { m_sPLCStart = value; }
        }

        public string PLCEnd
        {
            get { return m_sPLCEnd; }
            set { m_sPLCEnd = value; }
        }

        #endregion


        #region Public Methods


        #endregion


        #region Private Methods


        #endregion
    }
}
