using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;

namespace UDM.Project
{
    [Serializable]
    public class CMDCTagInfo
    {

        #region Member Varialbes

        private string m_sKey = "";
        private string m_sAddress = "";
        private int m_iSize = 1;
        private List<string> m_lstCode = new List<string>();
        private List<string> m_lstParent = new List<string>();

        #endregion


        #region Inialize/Dispose

        #endregion


        #region Public Properties

        [MessagePackMember(2)]
        public string Key
        {
            get { return m_sKey; }
            set { m_sKey = value; }
        }

        [MessagePackMember(0)]
        public string Address
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }

        [MessagePackMember(3)]
        public int Size
        {
            get { return m_iSize; }
            set { m_iSize = value; }
        }

        [MessagePackMember(1)]
        public List<string> CodeList
        {
            get { return m_lstCode; }
            set { m_lstCode = value; }
        }

        [MessagePackMember(4)]
        public List<string> ParentList
        {
            get { return m_lstParent; }
            set { m_lstParent = value; }
        }

        #endregion
    }
}
