using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;

namespace UDM.Project
{
    [Serializable]
    public class CMDCSubItemInfo
    {

        #region Member Variables

        private string m_sKey = "";
        private string m_sCode = "";
        private string m_sAddress = "";
        private string m_sParent = "";
        private int m_iSize = 1;


        #endregion


        #region Initialize/Dispose

        public CMDCSubItemInfo()
        {

        }

        #endregion


        #region Pubilc Properties

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

        [MessagePackMember(1)]
        public string Code
        {
            get { return m_sCode; }
            set { m_sCode = value; }
        }

        [MessagePackMember(4)]
        public string Parent
        {
            get { return m_sParent; }
            set { m_sParent = value; }
        }

        [MessagePackMember(3)]
        public int Size
        {
            get { return m_iSize; }
            set { m_iSize = value; }
        }

        #endregion


        #region Public Mehtods

        public CMDCSubItemInfo Clone()
        {
            CMDCSubItemInfo cSubItem = new CMDCSubItemInfo();

            cSubItem.Key = m_sKey;
            cSubItem.Code = m_sCode;
            cSubItem.Address = m_sAddress;
            cSubItem.Parent = m_sParent;
            cSubItem.Size = m_iSize;

            return cSubItem;
        }

        #endregion


        #region Private Methods


        #endregion

    }
}
