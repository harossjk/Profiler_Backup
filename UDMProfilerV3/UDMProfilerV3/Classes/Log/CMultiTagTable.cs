// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CMultiTagTable
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using UDM.Common;

namespace UDMProfilerV3
{
    public class CMultiTagTable : CTag, IDisposable
    {
        private string m_sFacility = string.Empty;
        private object m_oID = (object)0;

        public string Facility
        {
            get { return this.m_sFacility; }
            set { this.m_sFacility = value; }
        }

        public object ID
        {
            get { return this.m_oID; }
            set { this.m_oID = value; }
        }

        public CTag Tag
        {
            get
            {
                if (string.IsNullOrEmpty(this.Address))
                    return (CTag)null;

                return new CTag()
                {
                    Address = this.Address,
                    AddressType = this.AddressType,
                    Creator = this.Creator,
                    DataType = this.DataType,
                    Description = this.Description,
                    FeatureType = this.FeatureType,
                    IsCollectable = this.IsCollectable,
                    IsFragmentMode = this.IsFragmentMode,
                    IsLOBMode = this.IsLOBMode,
                    IsMDCItem = this.IsMDCItem,
                    IsNormalMode = this.IsNormalMode,
                    IsStandardable = this.IsStandardable,
                    IsStandardCollectable = this.IsStandardCollectable,
                    IsStandardMode = this.IsStandardMode,
                    Key = this.Key,
                    LinkAddress = this.LinkAddress,
                    LogCount = this.LogCount,
                    Note = this.Note,
                    Program = this.Program,
                    Size = this.Size,
                    StandardOrder = this.StandardOrder,
                    StepRoleS = this.StepRoleS,
                    TraceDepth = this.TraceDepth,
                    //jjk, 22.06.07
                    PLCMaker = this.PLCMaker
                };
            }
        }

        //yjk, 19.05.21 - 다중 로직 차트의 Step/접점 리스트에서 String으로 인식하게 하기 위해(CMultiTagTable)
        public string EnumToStringDataType
        {
            get { return Utils.GetEnumDescription(base.DataType); }
        }

        public void Dispose()
        {
        }
    }
}
