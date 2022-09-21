// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAReadAddressData
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;

namespace UDM.DDEA
{
    public class CDDEAReadAddressData : IDisposable, ICloneable
    {
        protected string m_sAddress = string.Empty;
        protected int m_iAddressLength = 1;
        protected int m_iValue = -1;
        protected string m_sValue = "";
        protected int m_iDWordValue = -1;
        protected DateTime m_dtRisingTime = DateTime.MinValue;

        public void Dispose()
        {
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

        public int Value
        {
            get
            {
                return this.m_iValue;
            }
            set
            {
                this.m_iValue = value;
            }
        }

        public int DWordValue
        {
            get
            {
                return this.m_iDWordValue;
            }
            set
            {
                this.m_iDWordValue = value;
            }
        }

        public int AddressLength
        {
            get
            {
                return this.m_iAddressLength;
            }
            set
            {
                this.m_iAddressLength = value;
            }
        }

        public string SValue
        {
            get
            {
                return this.m_sValue;
            }
            set
            {
                this.m_sValue = value;
            }
        }

        public DateTime RisingTime
        {
            get
            {
                return this.m_dtRisingTime;
            }
            set
            {
                this.m_dtRisingTime = value;
            }
        }

        public object Clone()
        {
            CDDEAReadAddressData oData = new CDDEAReadAddressData();

            oData.Address = m_sAddress;
            oData.Value = m_iValue;
            oData.DWordValue = m_iDWordValue;
            oData.AddressLength = m_iAddressLength;
            oData.RisingTime = m_dtRisingTime;

            return oData;
        }
    }
}
