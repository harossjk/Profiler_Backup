using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UDM.Common;

namespace UDMProfilerV3
{
    public class CTagTableViewTag :IViewTag
    {

        #region Member Variables        

        protected string m_sKey = "";
        protected string m_sAddress = "";
        protected string m_sDescription = "";
        protected int m_iSize = 1;
        protected EMDataType m_emDataType = EMDataType.None;
        protected string m_sProgram = "";
        protected string m_sLinkAddress = "";
        protected string m_sCreator = "System";

        protected CTag m_cTag = null;

        #endregion


        #region Initialize/Dispose

        public CTagTableViewTag()
        {

        }

        public CTagTableViewTag(CTag cTag)
        {
            SetTag(cTag);
        }

        public void Dispose()
        {
            SetTag(null);
        }

        #endregion


        #region Public Properties

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

        public int Size
        {
            get { return m_iSize; }
            set { m_iSize = value; }
        }

        public EMDataType DataType
        {
            get { return m_emDataType; }
            set { m_emDataType = value; }
        }

        public string Program
        {
            get { return m_sProgram; }
            set { m_sProgram = value; }
        }

        public string LinkAddress
        {
            get { return m_sLinkAddress; }
            set { m_sLinkAddress = value; }
        }

        public string Creator
        {
            get { return m_sCreator; }
            set { m_sCreator = value; }
        }

        public CTag Tag
        {
            get { return m_cTag; }
            set { SetTag(value); }
        }

        //yjk, 19.05.15 - Filter에서 String으로 인식하게 하기 위해
        public string EnumToStringDataType
        {
            get { return Utils.GetEnumDescription(m_emDataType); }
        }


        #endregion


        #region Public Methods

        public void Apply()
        {
            if (m_cTag == null)
                return;

            m_cTag.Description = m_sDescription;
            m_cTag.DataType = m_emDataType;
            m_cTag.Size = m_iSize;
        }

        #endregion


        #region Private Methods

        protected void SetTag(CTag cTag)
        {
            if (cTag == null)
            {
                m_sKey = "";
                m_sAddress = "";
                m_sDescription = "";
                m_iSize = 0;
                m_emDataType = EMDataType.None;
                m_sProgram = "";
                m_sLinkAddress = "";
                m_sCreator = "System";
            }
            else
            {
                m_sKey = cTag.Key;
                m_sAddress = cTag.Address;
                m_sDescription = cTag.Description;
                m_iSize = cTag.Size;
                m_emDataType = cTag.DataType;
                m_sProgram = cTag.Program;
                m_sLinkAddress = cTag.LinkAddress;
                m_sCreator = cTag.Creator;
            }

            if (m_cTag != cTag)
                m_cTag = cTag;
        }

        #endregion
    }
}
