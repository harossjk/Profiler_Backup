// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CLOBMode
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    [Serializable]
    public class CLOBMode
    {
        protected CDDEASymbolList m_cGlassIDSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cProcessIDSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cBitSymbolList = new CDDEASymbolList();
        protected CDDEAMdcSymbolList m_cMdcSymbolList = new CDDEAMdcSymbolList();
        protected CDDEASymbolList m_cWordSymbolList = new CDDEASymbolList();
        protected CDDEASymbol m_cProcessSymbol;
        protected CDDEASymbol m_cTactTimeSymbol;
        protected CDDEASymbol m_cRefreshSymbol;

        public CDDEASymbol ProcessSymbol
        {
            get
            {
                return this.m_cProcessSymbol;
            }
            set
            {
                this.m_cProcessSymbol = value;
            }
        }

        public CDDEASymbol TactTimeSymbol
        {
            get
            {
                return this.m_cTactTimeSymbol;
            }
            set
            {
                this.m_cTactTimeSymbol = value;
            }
        }

        public CDDEASymbol RefreshSymbol
        {
            get
            {
                return this.m_cRefreshSymbol;
            }
            set
            {
                this.m_cRefreshSymbol = value;
            }
        }

        public CDDEASymbolList GlassIDSymbolList
        {
            get
            {
                return this.m_cGlassIDSymbolList;
            }
            set
            {
                this.m_cGlassIDSymbolList = value;
            }
        }

        public CDDEASymbolList ProcessIDSymbolList
        {
            get
            {
                return this.m_cProcessIDSymbolList;
            }
            set
            {
                this.m_cProcessIDSymbolList = value;
            }
        }

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

        public CDDEAMdcSymbolList MDCSymbolList
        {
            get
            {
                return this.m_cMdcSymbolList;
            }
            set
            {
                this.m_cMdcSymbolList = value;
            }
        }
    }
}
