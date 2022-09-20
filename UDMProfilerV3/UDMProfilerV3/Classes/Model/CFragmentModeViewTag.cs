using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UDM.Common;

namespace UDMProfilerV3
{
    public class CFragmentModeViewTag : IViewTag
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

        private bool m_bStandardMode = false;
        private bool m_bFragmentMode = false;
        private bool m_bStandardable = false;
        private bool m_bStandardCollectable = false;
        private int m_iStandardOrder = 0;

        protected CTag m_cTag = null;

        #endregion


        #region Initialize/Dispose

        public CFragmentModeViewTag()
        {
        }

        public CFragmentModeViewTag(CTag cTag)
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

        public bool IsStandardMode
        {
            get { return m_bStandardMode; }
            set { m_bStandardMode = value; }
        }

        public bool IsFragmentMode
        {
            get { return m_bFragmentMode; }
            set { m_bFragmentMode = value; }
        }

        public bool IsStandardable
        {
            get { return m_bStandardable; }
            set { m_bStandardable = value; }
        }

        public bool IsStandardCollectable
        {
            get { return m_bStandardCollectable; }
            set { m_bStandardCollectable = value; }
        }

        public int StandardOrder
        {
            get { return m_iStandardOrder; }
            set
            {
                m_iStandardOrder = value;
            }
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

        public new void Apply()
        {
            if (m_cTag == null)
                return;

            m_cTag.Description = m_sDescription;
            m_cTag.IsStandardMode = m_bStandardMode;
            m_cTag.IsFragmentMode = m_bFragmentMode;
            m_cTag.IsStandardable = m_bStandardable;
            m_cTag.IsStandardCollectable = m_bStandardCollectable;
            m_cTag.StandardOrder = m_iStandardOrder;
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

                m_bStandardMode = false;
                m_bFragmentMode = false;
                m_bStandardable = false;
                m_bStandardCollectable = false;
                m_iStandardOrder = 0;
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

                m_bStandardMode = cTag.IsStandardMode;
                m_bFragmentMode = cTag.IsFragmentMode;
                m_bStandardable = cTag.IsStandardable;
                m_bStandardCollectable = cTag.IsStandardCollectable;
                m_iStandardOrder = cTag.StandardOrder;
            }

            if (m_cTag != cTag)
                m_cTag = cTag;
        }

        #endregion
    }
}
