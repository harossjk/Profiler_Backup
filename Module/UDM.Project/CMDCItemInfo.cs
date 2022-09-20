using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;

using UDM.Common;

namespace UDM.Project
{
    [Serializable]
    public class CMDCItemInfo
    {
        #region Member Variables

        private string m_sKey = "";
        private string m_sAddress = "";
        private string m_sDescription = "";
        private string m_sAxis = "";
        private EMDataType m_emDataType = EMDataType.Bool;

        private List<CMDCSubItemInfo> m_lstSubItem = new List<CMDCSubItemInfo>();

        #endregion


        #region Initialize/Dispose

        public CMDCItemInfo()
        {

        }

        #endregion


        #region Pubilc Properties

        public string Key
        {
            get { return m_sKey; }
            set { m_sKey = value; }
        }

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

        public string Axis
        {
            get { return m_sAxis; }
            set { m_sAxis = value; }
        }

        public EMDataType DataType
        {
            get { return m_emDataType; }
            set { m_emDataType = value; }
        }

        public List<CMDCSubItemInfo> SubItemList
        {
            get { return m_lstSubItem; }
            set { m_lstSubItem = value; }
        }

        #endregion


        #region Public Mehtods

        public CMDCItemInfo Clone()
        {
            CMDCItemInfo cItem = new CMDCItemInfo();

            cItem.Key = m_sKey;
            cItem.Address = m_sAddress;
            cItem.DataType = m_emDataType;
            cItem.Description = m_sDescription;
            cItem.Axis = m_sAxis;

            CMDCSubItemInfo cSubItem;
            for (int i = 0; i < m_lstSubItem.Count; i++)
            {
                cSubItem = m_lstSubItem[i].Clone();
                cItem.SubItemList.Add(cSubItem);
            }

            return cItem;
        }

        #endregion


        #region Private Methods


        #endregion

    }
}
