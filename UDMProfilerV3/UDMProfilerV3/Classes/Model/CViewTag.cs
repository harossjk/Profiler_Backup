// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CViewTag
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using UDM.Common;

namespace UDMProfilerV3
{
    public class CViewTag : IDisposable
    {
        protected string m_sKey = "";
        protected string m_sAddress = "";
        protected string m_sDescription = "";
        protected int m_iSize = 1;
        protected EMDataType m_emDataType = EMDataType.None;
        protected string m_sProgram = "";
        protected string m_sLinkAddress = "";
        protected string m_sCreator = "System";
        protected CTag m_cTag = null;

        public CViewTag()
        {
        }

        public CViewTag(CTag cTag)
        {
            this.SetTag(cTag);
        }

        public void Dispose()
        {
            this.SetTag(null);
        }

        public string Key
        {
            get
            {
                return this.m_sKey;
            }
            set
            {
                this.m_sKey = value;
            }
        }

        public string Address
        {
            get
            {
                return this.m_sAddress;
            }
            set
            {
                this.m_sAddress = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_sDescription;
            }
            set
            {
                this.m_sDescription = value;
            }
        }

        public int Size
        {
            get
            {
                return this.m_iSize;
            }
            set
            {
                this.m_iSize = value;
            }
        }

        public EMDataType DataType
        {
            get
            {
                return this.m_emDataType;
            }
            set
            {
                this.m_emDataType = value;
            }
        }

        public string Program
        {
            get
            {
                return this.m_sProgram;
            }
            set
            {
                this.m_sProgram = value;
            }
        }

        public string LinkAddress
        {
            get
            {
                return this.m_sLinkAddress;
            }
            set
            {
                this.m_sLinkAddress = value;
            }
        }

        public string Creator
        {
            get
            {
                return this.m_sCreator;
            }
            set
            {
                this.m_sCreator = value;
            }
        }

        public CTag Tag
        {
            get
            {
                return this.m_cTag;
            }
            set
            {
                this.SetTag(value);
            }
        }

        public virtual void Apply()
        {
            if (this.m_cTag == null)
                return;

            this.m_cTag.Description = this.m_sDescription;
        }

        protected void SetTag(CTag cTag)
        {
            if (cTag == null)
            {
                this.m_sKey = "";
                this.m_sAddress = "";
                this.m_sDescription = "";
                this.m_iSize = 0;
                this.m_emDataType = EMDataType.None;
                this.m_sProgram = "";
                this.m_sLinkAddress = "";
                this.m_sCreator = "System";
            }
            else
            {
                this.m_sKey = cTag.Key;
                this.m_sAddress = cTag.Address;
                this.m_sDescription = cTag.Description;
                this.m_iSize = cTag.Size;
                this.m_emDataType = cTag.DataType;
                this.m_sProgram = cTag.Program;
                this.m_sLinkAddress = cTag.LinkAddress;
                this.m_sCreator = cTag.Creator;
            }

            if (this.m_cTag == cTag)
                return;

            this.m_cTag = cTag;
        }
    }
}
