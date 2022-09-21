// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CNormalMode
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    [Serializable]
    public class CNormalMode
    {
        protected CDDEASymbolList m_cWordSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cBitSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cIndexSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cIncludeIndexSymbolList = new CDDEASymbolList();

        public CDDEASymbolList BitSymbolList
        {
            get
            {
                return this.m_cBitSymbolList;
            }
            set
            {
                this.m_cBitSymbolList = value;
            }
        }

        public CDDEASymbolList WordSymbolList
        {
            get
            {
                return this.m_cWordSymbolList;
            }
            set
            {
                this.m_cWordSymbolList = value;
            }
        }

        public CDDEASymbolList IndexSymbolList
        {
            get
            {
                return this.m_cIndexSymbolList;
            }
            set
            {
                this.m_cIndexSymbolList = value;
            }
        }

        public CDDEASymbolList IncludeIndexSymbolList
        {
            get
            {
                return this.m_cIncludeIndexSymbolList;
            }
            set
            {
                this.m_cIncludeIndexSymbolList = value;
            }
        }
    }
}
