// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CNormalModeViewTag
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using UDM.Common;

namespace UDMProfilerV3
{
    public class CFilterNormalModeViewTag : IViewTag, IDisposable
    {
        protected string m_sKey = "";
        protected string m_sAddress = "";
        protected string m_sDescription = "";
        protected int m_iSize = 1;
        protected EMDataType m_emDataType = EMDataType.None;
        protected string m_sProgram = "";
        protected string m_sLinkAddress = "";
        protected string m_sCreator = "System";
        protected bool m_bFilterNormalMode = false;
        protected int m_iTraceDepth = -1;
        protected int m_iRemainDepth = -1;
        protected CTag m_cTag = (CTag)null;

        //jjk, 20.11.26 Coil 
        protected bool m_bIsCoil = false;

        public CFilterNormalModeViewTag()
        {
        }

        public CFilterNormalModeViewTag(CTag cTag)
        {
            SetTag(cTag);
        }

        public void Dispose()
        {
            SetTag(null);
        }

        public bool IsCoil
        {
            get { return m_bIsCoil; }
            set { m_bIsCoil = value; }
        }

        public string Key
        {
            get
            {
                return m_sKey;
            }
            set
            {
                m_sKey = value;
            }
        }

        public string Address
        {
            get
            {
                return m_sAddress;
            }
            set
            {
                m_sAddress = value;
            }
        }

        public string Description
        {
            get
            {
                return m_sDescription;
            }
            set
            {
                m_sDescription = value;
            }
        }

        public int Size
        {
            get
            {
                return m_iSize;
            }
            set
            {
                m_iSize = value;
            }
        }

        public EMDataType DataType
        {
            get
            {
                return m_emDataType;
            }
            set
            {
                m_emDataType = value;
            }
        }

        public string Program
        {
            get
            {
                return m_sProgram;
            }
            set
            {
                m_sProgram = value;
            }
        }

        public string LinkAddress
        {
            get
            {
                return m_sLinkAddress;
            }
            set
            {
                m_sLinkAddress = value;
            }
        }

        public string Creator
        {
            get
            {
                return m_sCreator;
            }
            set
            {
                m_sCreator = value;
            }
        }

        public bool IsFilterNormalMode
        {
            get
            {
                return m_bFilterNormalMode;
            }
            set
            {
                m_bFilterNormalMode = value;
            }
        }

        public int TraceDepth
        {
            get
            {
                return m_iTraceDepth;
            }
            set
            {
                m_iTraceDepth = value;
            }
        }

        public int RemainDepth
        {
            get
            {
                return m_iRemainDepth;
            }
            set
            {
                m_iRemainDepth = value;
            }
        }

        public CTag Tag
        {
            get
            {
                return m_cTag;
            }
            set
            {
                SetTag(value);
            }
        }

        public void Apply()
        {
            if (m_cTag == null)
                return;

            m_cTag.Description = m_sDescription;
            m_cTag.IsFilterNormalMode = m_bFilterNormalMode;
            m_cTag.TraceDepth = m_iTraceDepth;
        }

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
                m_bFilterNormalMode = false;
                m_iTraceDepth = -1;
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
                m_bFilterNormalMode = cTag.IsFilterNormalMode;
                m_iTraceDepth = cTag.TraceDepth;
            }

            if (m_cTag == cTag)
                return;

            m_cTag = cTag;
        }

        //yjk, 19.05.15 - Filter에서 String으로 인식하게 하기 위해
        public string EnumToStringDataType
        {
            get { return Utils.GetEnumDescription(m_emDataType); }
        }
    }
}
