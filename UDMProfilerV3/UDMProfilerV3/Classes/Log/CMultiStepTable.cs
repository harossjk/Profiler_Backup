// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CMultiStepTable
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using UDM.Common;

namespace UDMProfilerV3
{
    public class CMultiStepTable : CStep, IDisposable
    {
        private string m_sFacility = string.Empty;
        private object m_oID = (object)0;

        public string Facility
        {
            get
            {
                return this.m_sFacility;
            }
            set
            {
                this.m_sFacility = value;
            }
        }

        public object ID
        {
            get
            {
                return this.m_oID;
            }
            set
            {
                this.m_oID = value;
            }
        }

        public CStep Step
        {
            get
            {
                if (this.CoilS == null)
                    return (CStep)null;
                CStep cstep = new CStep();
                cstep.CallControl = this.CallControl;
                cstep.CoilS = this.CoilS;
                cstep.ContactS = this.ContactS;
                cstep.ForNextControl = this.ForNextControl;
                cstep.Key = this.Key;
                cstep.MasterControl = this.MasterControl;
                cstep.Program = this.Program;
                cstep.RefTagS = this.RefTagS;
                cstep.Situation = this.Situation;
                cstep.StepIndex = this.StepIndex;
                return cstep;
            }
        }

        //yjk, 19.05.21 - 다중 로직 차트의 Step/접점 리스트에서 String으로 인식하게 하기 위해(CMultiStepTable)
        public string EnumToStringDataType
        {
            get
            {
                if (base.CoilS != null)
                    return Utils.GetEnumDescription(base.CoilS.DataType);
                else
                    return Utils.GetEnumDescription(EMDataType.None);
            }
        }
    }
}
