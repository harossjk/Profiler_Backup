// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEACycleSymbol
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using UDM.DDEACommon;

namespace UDM.DDEA
{
  [Serializable]
  public class CDDEACycleSymbol
  {
    protected CDDEASymbol m_cSymbiol = (CDDEASymbol) null;
    protected int m_iCondition = 1;
    protected bool m_bConditionComp = false;
    protected bool m_bConditionChange = false;

    public CDDEASymbol Symbol
    {
      get
      {
        return this.m_cSymbiol;
      }
      set
      {
        this.m_cSymbiol = value;
      }
    }

    public int Condition
    {
      get
      {
        return this.m_iCondition;
      }
      set
      {
        this.m_iCondition = value;
      }
    }

    public bool ConditionComp
    {
      get
      {
        return this.m_bConditionComp;
      }
      set
      {
        this.m_bConditionComp = value;
      }
    }

    public bool ConditionChange
    {
      get
      {
        return this.m_bConditionChange;
      }
      set
      {
        this.m_bConditionChange = value;
      }
    }
  }
}
