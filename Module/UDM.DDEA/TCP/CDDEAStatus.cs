// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAStatus
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

namespace UDM.DDEA
{
  public class CDDEAStatus
  {
    protected string m_sCollectTime = "";
    protected string m_sCollectState = "NM";
    protected string m_sLinPCName = "";
    protected string m_sResponesCode = "";

    public string CollectTime
    {
      get
      {
        return this.m_sCollectTime;
      }
      set
      {
        this.m_sCollectTime = value;
      }
    }

    public string CollectState
    {
      get
      {
        return this.m_sCollectState;
      }
      set
      {
        this.m_sCollectState = value;
      }
    }

    public string LinePcName
    {
      get
      {
        return this.m_sLinPCName;
      }
      set
      {
        this.m_sLinPCName = value;
      }
    }

    public string ResponesCode
    {
      get
      {
        return this.m_sResponesCode;
      }
      set
      {
        this.m_sResponesCode = value;
      }
    }
  }
}
