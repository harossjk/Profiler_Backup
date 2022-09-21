// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAProject_V2
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.IO;
using System.Runtime.Serialization;
using UDM.General.Serialize;

namespace UDM.DDEA
{
  [DataContract]
  [Serializable]
  public class CDDEAProject_V2 : CDDEAProject
  {
    public CDDEAProject_V2(string sName)
      : base(sName)
    {
      this.m_cDDEAConfig = (CDDEAConfigMS) new CDDEAConfigMS_V2();
    }

    public CDDEAProject_V2()
    {
      this.m_sName = "None";
    }

    public CDDEAProject_V2(CDDEAProject cOldVersion)
    {
      this.CreateFrom(cOldVersion);
    }

    public new CDDEAConfigMS Config
    {
      get
      {
        return this.m_cDDEAConfig;
      }
      set
      {
        this.m_cDDEAConfig = value;
      }
    }

    [DataMember]
    public CDDEAConfigMS_V2 Config_V2
    {
      get
      {
        return (CDDEAConfigMS_V2) this.m_cDDEAConfig;
      }
      set
      {
        this.m_cDDEAConfig = (CDDEAConfigMS) value;
      }
    }

    public new bool Open(string sPath)
    {
      bool flag = true;
      if (!new FileInfo(sPath).Exists)
        return false;
      this.m_sPath = sPath;
      this.Clear();
      CNetSerializer cnetSerializer = new CNetSerializer();
      object obj = cnetSerializer.Read(sPath);
      if (obj != null)
      {
        CDDEAProject_V2 cddeaProjectV2 = (CDDEAProject_V2) obj;
        this.m_sName = cddeaProjectV2.Name;
        this.m_sPath = cddeaProjectV2.Path;
        this.m_cDDEAConfig = cddeaProjectV2.Config;
        this.m_iCycleCount = cddeaProjectV2.CycleCount;
        this.m_cRecipeSymbolList = cddeaProjectV2.RecipeSymbolList;
        this.m_cFragBundleList = cddeaProjectV2.FragBundleList;
        this.m_cFragMasterBundleList = cddeaProjectV2.FragMasterBundleList;
        this.m_cNormalBundleList = cddeaProjectV2.NormalBundleList;
        this.m_cLOBBundleList = cddeaProjectV2.LOBBundleList;
        this.m_iStartBlock = cddeaProjectV2.StartBlock;
        this.m_emCollectMode = cddeaProjectV2.CollectMode;
        this.m_emConnectAppType = cddeaProjectV2.ConnectApp;
        this.m_iLogSaveTime = cddeaProjectV2.LogSaveTime;
        this.m_iMasterRecipeValue = cddeaProjectV2.MasterRecipeValue;
      }
      else
        flag = false;
      cnetSerializer.Dispose();
      return flag;
    }

    public new bool Save(string sPath)
    {
      this.m_sPath = sPath;
      CDDEAProject_V2 cddeaProjectV2 = new CDDEAProject_V2(this.m_sName);
      cddeaProjectV2.Path = sPath;
      cddeaProjectV2.Config = this.m_cDDEAConfig;
      cddeaProjectV2.CycleCount = this.m_iCycleCount;
      cddeaProjectV2.RecipeSymbolList = this.m_cRecipeSymbolList;
      cddeaProjectV2.FragBundleList = this.m_cFragBundleList;
      cddeaProjectV2.FragMasterBundleList = this.m_cFragMasterBundleList;
      cddeaProjectV2.NormalBundleList = this.m_cNormalBundleList;
      cddeaProjectV2.LOBBundleList = this.m_cLOBBundleList;
      cddeaProjectV2.StartBlock = this.m_iStartBlock;
      cddeaProjectV2.CollectMode = this.m_emCollectMode;
      cddeaProjectV2.ConnectApp = this.m_emConnectAppType;
      cddeaProjectV2.LogSaveTime = this.m_iLogSaveTime;
      cddeaProjectV2.MasterRecipeValue = this.m_iMasterRecipeValue;
      CNetSerializer cnetSerializer = new CNetSerializer();
      bool flag = cnetSerializer.Write(sPath, (object) cddeaProjectV2);
      cnetSerializer.Dispose();
      return flag;
    }

    protected void CreateFrom(CDDEAProject cOldVersion)
    {
      this.Name = cOldVersion.Name;
      this.Path = cOldVersion.Path;
      this.MachineName = cOldVersion.MachineName;
      this.MachineDescription = cOldVersion.MachineDescription;
      this.LogSavePath = cOldVersion.LogSavePath;
      this.CycleCount = cOldVersion.CycleCount;
      this.StartBlock = cOldVersion.StartBlock;
      this.LogSaveTime = cOldVersion.LogSaveTime;
      this.ParamReadTime = cOldVersion.ParamReadTime;
      this.MasterRecipeValue = cOldVersion.MasterRecipeValue;
      this.RecipeSymbolList = cOldVersion.RecipeSymbolList;
      this.FragBundleList = cOldVersion.FragBundleList;
      this.FragMasterBundleList = cOldVersion.FragMasterBundleList;
      this.NormalBundleList = cOldVersion.NormalBundleList;
      this.LOBBundleList = cOldVersion.LOBBundleList;
      this.Config = (CDDEAConfigMS) new CDDEAConfigMS_V2(cOldVersion.Config);
      this.CollectMode = cOldVersion.CollectMode;
      this.ConnectApp = cOldVersion.ConnectApp;
      this.HeaderSize = cOldVersion.HeaderSize;
      this.ServerRunFlag = cOldVersion.ServerRunFlag;
      this.FailAddressList = cOldVersion.FailAddressList;
      this.ParamSymbolS = cOldVersion.ParamSymbolS;
      this.ParaFileChange = cOldVersion.ParaFileChange;
      this.DeviceParameterSize = cOldVersion.DeviceParameterSize;
    }
  }
}
