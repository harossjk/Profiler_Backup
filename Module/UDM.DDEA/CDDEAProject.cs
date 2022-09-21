// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAProject
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UDM.Common;
using UDM.DDEACommon;
using UDM.General.Serialize;

namespace UDM.DDEA
{
    [DataContract]
    [Serializable]
    public class CDDEAProject
    {
        protected string m_sPath = string.Empty;
        protected string m_sName = string.Empty;
        protected string m_sMachineName = string.Empty;
        protected string m_sMachineDescription = string.Empty;
        protected string m_sLogSavePath = string.Empty;
        protected int m_iCycleCount = 2;
        protected int m_iStartBlock = 1;
        protected int m_iLogSaveTime = 10;
        protected int m_iParamReadTime = 4;
        protected int m_iMasterRecipeValue = 0;
        protected bool m_bServerRunFlag = false;
        protected bool m_bParaFileChagne = false;
        protected CDDEASymbolList m_cRecipeSymbolList = new CDDEASymbolList();
        protected CDDEAConfigMS m_cDDEAConfig = new CDDEAConfigMS();
        protected List<CNormalMode> m_cNormalBundleList = new List<CNormalMode>();
        protected List<CLOBMode> m_cLOBBundleList = new List<CLOBMode>();
        protected List<CFragMode> m_cFragBundleList = new List<CFragMode>();
        protected List<CFragMasterMode> m_cFragMasterBundleList = new List<CFragMasterMode>();
        protected CParameterSymbolS m_cParamSymbolS = new CParameterSymbolS();
        protected EMCollectMode m_emCollectMode = EMCollectMode.Normal;
        protected EMConnectAppType m_emConnectAppType = EMConnectAppType.None;
        protected Dictionary<string, int> m_dicHeaderSize = new Dictionary<string, int>();
        protected List<string> m_lstFailCollectAddress = new List<string>();
        protected Dictionary<string, int> m_dicParameterValue = (Dictionary<string, int>)null;

        public CDDEAProject(string sName)
        {
            this.m_sName = sName;
        }

        public CDDEAProject()
        {
            this.m_sName = "None";
        }

        [DataMember]
        public string Name
        {
            get
            {
                return this.m_sName;
            }
            set
            {
                this.m_sName = value;
            }
        }

        [DataMember]
        public string Path
        {
            get
            {
                return this.m_sPath;
            }
            set
            {
                this.m_sPath = value;
            }
        }

        [DataMember]
        public string MachineName
        {
            get
            {
                return this.m_sMachineName;
            }
            set
            {
                this.m_sMachineName = value;
            }
        }

        [DataMember]
        public string MachineDescription
        {
            get
            {
                return this.m_sMachineDescription;
            }
            set
            {
                this.m_sMachineDescription = value;
            }
        }

        [DataMember]
        public string LogSavePath
        {
            get
            {
                return this.m_sLogSavePath;
            }
            set
            {
                this.m_sLogSavePath = value;
            }
        }

        [DataMember]
        public int CycleCount
        {
            get
            {
                return this.m_iCycleCount;
            }
            set
            {
                this.m_iCycleCount = value;
            }
        }

        [DataMember]
        public int StartBlock
        {
            get
            {
                return this.m_iStartBlock;
            }
            set
            {
                this.m_iStartBlock = value;
            }
        }

        [DataMember]
        public int LogSaveTime
        {
            get
            {
                return this.m_iLogSaveTime;
            }
            set
            {
                this.m_iLogSaveTime = value;
            }
        }

        [DataMember]
        public int ParamReadTime
        {
            get
            {
                return this.m_iParamReadTime;
            }
            set
            {
                this.m_iParamReadTime = value;
            }
        }

        [DataMember]
        public int MasterRecipeValue
        {
            get
            {
                return this.m_iMasterRecipeValue;
            }
            set
            {
                this.m_iMasterRecipeValue = value;
            }
        }

        [DataMember]
        public CDDEASymbolList RecipeSymbolList
        {
            get
            {
                return this.m_cRecipeSymbolList;
            }
            set
            {
                this.m_cRecipeSymbolList = value;
            }
        }

        [DataMember]
        public List<CFragMode> FragBundleList
        {
            get
            {
                return this.m_cFragBundleList;
            }
            set
            {
                this.m_cFragBundleList = value;
            }
        }

        [DataMember]
        public List<CFragMasterMode> FragMasterBundleList
        {
            get
            {
                return this.m_cFragMasterBundleList;
            }
            set
            {
                this.m_cFragMasterBundleList = value;
            }
        }

        [DataMember]
        public List<CNormalMode> NormalBundleList
        {
            get
            {
                return this.m_cNormalBundleList;
            }
            set
            {
                this.m_cNormalBundleList = value;
            }
        }

        [DataMember]
        public List<CLOBMode> LOBBundleList
        {
            get
            {
                return this.m_cLOBBundleList;
            }
            set
            {
                this.m_cLOBBundleList = value;
            }
        }

        [DataMember]
        public CDDEAConfigMS Config
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
        public EMCollectMode CollectMode
        {
            get
            {
                return this.m_emCollectMode;
            }
            set
            {
                this.m_emCollectMode = value;
            }
        }

        [DataMember]
        public EMConnectAppType ConnectApp
        {
            get
            {
                return this.m_emConnectAppType;
            }
            set
            {
                this.m_emConnectAppType = value;
            }
        }

        [DataMember]
        public Dictionary<string, int> HeaderSize
        {
            get
            {
                return this.m_dicHeaderSize;
            }
            set
            {
                this.m_dicHeaderSize = value;
            }
        }

        [DataMember]
        public bool ServerRunFlag
        {
            get
            {
                return this.m_bServerRunFlag;
            }
            set
            {
                this.m_bServerRunFlag = value;
            }
        }

        [DataMember]
        public List<string> FailAddressList
        {
            get
            {
                return this.m_lstFailCollectAddress;
            }
            set
            {
                this.m_lstFailCollectAddress = value;
            }
        }

        [DataMember]
        public CParameterSymbolS ParamSymbolS
        {
            get
            {
                return this.m_cParamSymbolS;
            }
            set
            {
                this.m_cParamSymbolS = value;
            }
        }

        [DataMember]
        public bool ParaFileChange
        {
            get
            {
                return this.m_bParaFileChagne;
            }
            set
            {
                this.m_bParaFileChagne = value;
            }
        }

        [DataMember]
        public Dictionary<string, int> DeviceParameterSize
        {
            get
            {
                return this.m_dicParameterValue;
            }
            set
            {
                this.m_dicParameterValue = value;
            }
        }

        public bool IsRecipeSymbolS
        {
            get
            {
                return this.m_cRecipeSymbolList.Count > 0;
            }
        }

        public bool Open(string sPath)
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
                CDDEAProject cddeaProject = (CDDEAProject)obj;
                this.m_sName = cddeaProject.Name;
                this.m_sPath = cddeaProject.Path;
                this.m_cDDEAConfig = cddeaProject.Config;
                this.m_iCycleCount = cddeaProject.CycleCount;
                this.m_cRecipeSymbolList = cddeaProject.RecipeSymbolList;
                this.m_cFragBundleList = cddeaProject.FragBundleList;
                this.m_cFragMasterBundleList = cddeaProject.FragMasterBundleList;
                this.m_cNormalBundleList = cddeaProject.NormalBundleList;
                this.m_cLOBBundleList = cddeaProject.LOBBundleList;
                this.m_iStartBlock = cddeaProject.StartBlock;
                this.m_emCollectMode = cddeaProject.CollectMode;
                this.m_emConnectAppType = cddeaProject.ConnectApp;
                this.m_iLogSaveTime = cddeaProject.LogSaveTime;
                this.m_iMasterRecipeValue = cddeaProject.MasterRecipeValue;
            }
            else
                flag = false;
            cnetSerializer.Dispose();
            return flag;
        }

        public bool Save(string sPath)
        {
            this.m_sPath = sPath;
            CDDEAProject cddeaProject = new CDDEAProject(this.m_sName);
            cddeaProject.Path = sPath;
            cddeaProject.Config = this.m_cDDEAConfig;
            cddeaProject.CycleCount = this.m_iCycleCount;
            cddeaProject.RecipeSymbolList = this.m_cRecipeSymbolList;
            cddeaProject.FragBundleList = this.m_cFragBundleList;
            cddeaProject.FragMasterBundleList = this.m_cFragMasterBundleList;
            cddeaProject.NormalBundleList = this.m_cNormalBundleList;
            cddeaProject.LOBBundleList = this.m_cLOBBundleList;
            cddeaProject.StartBlock = this.m_iStartBlock;
            cddeaProject.CollectMode = this.m_emCollectMode;
            cddeaProject.ConnectApp = this.m_emConnectAppType;
            cddeaProject.LogSaveTime = this.m_iLogSaveTime;
            cddeaProject.MasterRecipeValue = this.m_iMasterRecipeValue;
            CNetSerializer cnetSerializer = new CNetSerializer();
            bool flag = cnetSerializer.Write(sPath, (object)cddeaProject);
            cnetSerializer.Dispose();
            return flag;
        }

        public void Clear()
        {
            GC.Collect();
        }

        public void SetDDEARecipeSymbolS(CTag cTag)
        {
            if (cTag.Address == "")
                return;
            this.m_cRecipeSymbolList = this.ChangeTagToDDEASymbolList(cTag);
        }

        public void SetLOB(CTag cGlassIDTag, CTag cProcessTag, CTag cRefreshTag, CTag cTactTimeTag)
        {
            if (this.m_cLOBBundleList.Count == 0)
                this.m_cLOBBundleList.Add(new CLOBMode());
            if (cGlassIDTag.Address != "")
                this.m_cLOBBundleList[0].GlassIDSymbolList = this.ChangeTagToDDEASymbolList(cGlassIDTag);
            if (cProcessTag.Address != "")
                this.m_cLOBBundleList[0].ProcessSymbol = this.ChangeTagToDDEASymbol(cProcessTag);
            if (cProcessTag.Address != "")
                this.m_cLOBBundleList[0].ProcessIDSymbolList = this.ChangeTagToDDEASymbolList(cProcessTag);
            if (cRefreshTag.Address != "")
                this.m_cLOBBundleList[0].RefreshSymbol = this.ChangeTagToDDEASymbol(cRefreshTag);
            if (!(cTactTimeTag.Address != ""))
                return;
            this.m_cLOBBundleList[0].TactTimeSymbol = this.ChangeTagToDDEASymbol(cTactTimeTag);
        }

        public void SetLobBundle(CTagS cAllTagS)
        {
            CDDEASymbolS cSymbolS = this.SetLOBSelectedSymbolS(cAllTagS);
            List<CDDEASymbol> cddeaSymbolList1 = this.ChangeFromSymbolSToListSymbol(cSymbolS);
            bool flag = false;
            if (this.m_cLOBBundleList[0].RefreshSymbol != null)
            {
                if (!cddeaSymbolList1.Contains(this.m_cLOBBundleList[0].RefreshSymbol))
                    cddeaSymbolList1.Add(this.m_cLOBBundleList[0].RefreshSymbol);
                else
                    flag = true;
            }
            this.GetWordSize(cSymbolS);
            List<CDDEASymbol> cddeaSymbolList2 = (List<CDDEASymbol>)null;
            List<string> stringList = new List<string>();
            using (List<CDDEASymbol>.Enumerator enumerator = cddeaSymbolList1.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CDDEASymbol sym = enumerator.Current;
                    if (this.m_lstFailCollectAddress.Contains(sym.Address))
                        sym.CollectUse = false;
                    else if (sym.DataType == EMDataType.Bool)
                    {
                        if (!stringList.Contains("B_" + sym.AddressHeader))
                        {
                            stringList.Add("B_" + sym.AddressHeader);
                            cddeaSymbolList2 = new List<CDDEASymbol>();
                            List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool && b.CollectUse));
                            all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                            List<CDDEASymbol> lstAddSymbol = sym.AddressMinor == -1 ? this.InsertBitSymbol(all) : this.InsertWordDotSymbol(all);
                            if (this.m_cLOBBundleList[0].RefreshSymbol != null)
                            {
                                if (!flag)
                                {
                                    CDDEASymbol cddeaSymbol = (CDDEASymbol)this.m_cLOBBundleList[0].RefreshSymbol.Clone();
                                    lstAddSymbol.Remove(this.m_cLOBBundleList[0].RefreshSymbol);
                                }
                                else
                                    this.m_cLOBBundleList[0].RefreshSymbol = lstAddSymbol.Find((Predicate<CDDEASymbol>)(b => b.Address == this.m_cLOBBundleList[0].RefreshSymbol.Address));
                            }
                            this.m_cLOBBundleList[0].BitSymbolList.AddSymbolList(lstAddSymbol);
                        }
                    }
                    else if (sym.DataType == EMDataType.Word)
                    {
                        if (!stringList.Contains("W_" + sym.AddressHeader))
                        {
                            stringList.Add("W_" + sym.AddressHeader);
                            cddeaSymbolList2 = new List<CDDEASymbol>();
                            List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word && (b.IndexAddressNumber == -1 && b.IndexType != EMIndexTypeMS.IncludeAddress) && b.CollectUse));
                            all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                            for (int index = 0; index < all.Count; ++index)
                                all[index].BaseAddress = all[index].Address;
                            this.m_cLOBBundleList[0].WordSymbolList.AddSymbolList(all);
                        }
                    }
                    else if (sym.DataType == EMDataType.DWord && !stringList.Contains("DW_" + sym.AddressHeader))
                    {
                        stringList.Add("DW_" + sym.AddressHeader);
                        cddeaSymbolList2 = new List<CDDEASymbol>();
                        List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord && b.CollectUse));
                        all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                        this.m_cLOBBundleList[0].WordSymbolList.AddSymbolList(this.InsertDWordSymbol(all));
                    }
                }
            }
        }

        public void SetLOBMdcSymbolS(CDDEAMdcSymbolList cSymbolList)
        {
            this.m_cLOBBundleList[0].MDCSymbolList = cSymbolList;
        }

        public void SetNormalBundleList(CPacketInfoS cPacketInfoS, CTagS cAllTags)
        {
            foreach (CPacketInfo cpacketInfo in (List<CPacketInfo>)cPacketInfoS)
            {
                CNormalMode cnormalMode = new CNormalMode();
                List<CDDEASymbol> collectDdeaSymbolList = (List<CDDEASymbol>)this.GetCollectDDEASymbolList(cpacketInfo.RefTagS, cAllTags);
                CNormalMode normalBundle = this.CreateNormalBundle(collectDdeaSymbolList);
                foreach (CDDEASymbol cddeaSymbol in collectDdeaSymbolList)
                {
                    if (cddeaSymbol.BaseAddress == "" && cddeaSymbol.CollectUse)
                        Console.WriteLine(cddeaSymbol.Address + " 접점의 BaseAddress가 없다.");
                }
                this.m_cNormalBundleList.Add(normalBundle);
            }
        }

        public void SetNormalBundleList(CDDEASymbolList cTotalSymbolList, int iMaxWord)
        {
            List<CDDEASymbolList> packetSymbolList = this.GetPacketSymbolList(cTotalSymbolList, iMaxWord);
            if (packetSymbolList.Count == 0)
                return;
            for (int index = 0; index < packetSymbolList.Count; ++index)
                this.m_cNormalBundleList.Add(this.CreateNormalBundle((List<CDDEASymbol>)packetSymbolList[index]));
            packetSymbolList.Clear();
        }

        public void SetFragBundleList(CConditionS cCycleStart, CConditionS cCycleEnd, CConditionS cCycleTrigger, CPacketInfoS cPacketInfoS, CTagS cAllTags)
        {
            foreach (CPacketInfo cpacketInfo in (List<CPacketInfo>)cPacketInfoS)
            {
                CDDEASymbolList collectDdeaSymbolList = this.GetCollectDDEASymbolList(cpacketInfo.RefTagS, cAllTags);
                CFragMode cFragMode = new CFragMode();
                this.AddCycleSymbol(cCycleStart, collectDdeaSymbolList);
                this.AddCycleSymbol(cCycleEnd, collectDdeaSymbolList);
                this.AddCycleSymbol(cCycleTrigger, collectDdeaSymbolList);
                this.CreateFragBundle(cFragMode, collectDdeaSymbolList);
                cFragMode.SetDDEACycle(cCycleStart, cCycleEnd, cCycleTrigger, collectDdeaSymbolList);
                foreach (CDDEASymbol cddeaSymbol in (List<CDDEASymbol>)collectDdeaSymbolList)
                {
                    if (cddeaSymbol.BaseAddress == "" && cddeaSymbol.CollectUse)
                        Console.WriteLine(cddeaSymbol.Address + " 접점의 BaseAddress가 없다.");
                    if (cFragMode.CycleSymbolS.ContainsKey(cddeaSymbol.Key))
                        cFragMode.CycleSymbolS[cddeaSymbol.Key] = cddeaSymbol;
                }
                this.m_cFragBundleList.Add(cFragMode);
            }
        }

        public void SetFragMasterBundleList(CConditionS cCycleStart, CConditionS cCycleEnd, CConditionS cCycleTrigger, CPacketInfoS cPacketInfoS, CTagS cAllTags)
        {
            foreach (CPacketInfo cpacketInfo in (List<CPacketInfo>)cPacketInfoS)
            {
                CDDEASymbolList collectDdeaSymbolList = this.GetCollectDDEASymbolList(cpacketInfo.RefTagS, cAllTags);
                CFragMasterMode cFragMasterMode = new CFragMasterMode();
                this.AddCycleSymbol(cCycleStart, collectDdeaSymbolList);
                this.AddCycleSymbol(cCycleEnd, collectDdeaSymbolList);
                this.AddCycleSymbol(cCycleTrigger, collectDdeaSymbolList);
                this.CreateFragMasterBundle(cFragMasterMode, collectDdeaSymbolList);
                string sSwitchKey = cpacketInfo.SwitchCondition.TagKey;
                cFragMasterMode.SwitchingCount = cpacketInfo.SwitchCondition.TagCount;
                cFragMasterMode.SwitchingValue = cpacketInfo.SwitchCondition.TagValue;
                CDDEASymbol cddeaSymbol1 = cFragMasterMode.BitSymbolList.Find((Predicate<CDDEASymbol>)(b => b.Key == sSwitchKey));
                if (cddeaSymbol1 != null)
                    cFragMasterMode.SwitchingSymbol = cddeaSymbol1;
                else
                    Console.WriteLine(sSwitchKey + "Bit 접점내에 같은 Key가 없습니다.");
                cFragMasterMode.SetDDEACycle(cCycleStart, cCycleEnd, cCycleTrigger, collectDdeaSymbolList);
                foreach (CDDEASymbol cddeaSymbol2 in (List<CDDEASymbol>)collectDdeaSymbolList)
                {
                    if (cddeaSymbol2.BaseAddress == "" && cddeaSymbol2.CollectUse)
                        Console.WriteLine(cddeaSymbol2.Address + " 접점의 BaseAddress가 없다.");
                    if (cFragMasterMode.CycleSymbolS.ContainsKey(cddeaSymbol2.Key))
                        cFragMasterMode.CycleSymbolS[cddeaSymbol2.Key].BaseAddress = cddeaSymbol2.BaseAddress;
                }
                this.m_cFragMasterBundleList.Add(cFragMasterMode);
            }
        }

        public void SetFragCycleInfo(int iMinCycleTime, int iMaxCycleTime)
        {
            foreach (CFragMode cFragBundle in this.m_cFragBundleList)
            {
                cFragBundle.CycleConditionSymbolS.CycleMaxTimeMs = iMaxCycleTime;
                cFragBundle.CycleConditionSymbolS.CycleMinTimeMs = iMinCycleTime;
            }
        }

        public void SetFragMasterCycleInfo(int iMinCycleTime, int iMaxCycleTime)
        {
            foreach (CFragMasterMode fragMasterBundle in this.m_cFragMasterBundleList)
            {
                fragMasterBundle.CycleConditionSymbolS.CycleMaxTimeMs = iMaxCycleTime;
                fragMasterBundle.CycleConditionSymbolS.CycleMinTimeMs = iMinCycleTime;
            }
        }

        protected void AddCycleSymbol(CConditionS cCycleCondition, CDDEASymbolList cSymbolList)
        {
            using (List<CCondition>.Enumerator enumerator = cCycleCondition.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CCondition condi = enumerator.Current;
                    if (cSymbolList.Find((Predicate<CDDEASymbol>)(b => b.Key == condi.Key)) == null)
                    {
                        CDDEASymbol cSymbol = new CDDEASymbol(condi.Key, false);
                        cSymbol.CreateMelsecDDEASymbol(condi.Address);
                        cSymbolList.AddSymbol(cSymbol);
                    }
                }
            }
        }

        protected CNormalMode CreateNormalBundle(List<CDDEASymbol> clstDDESymbol)
        {
            List<string> stringList = new List<string>();
            List<CDDEASymbol> cddeaSymbolList = (List<CDDEASymbol>)null;
            CNormalMode cnormalMode = new CNormalMode();

            List<CDDEASymbol> lstAddSymbol1 = this.InsertIncludeIndexSymbol(clstDDESymbol);
            cnormalMode.IncludeIndexSymbolList.AddSymbolList(lstAddSymbol1);

            List<CDDEASymbol> lstAddSymbol2 = this.InsertIndexSymbol(clstDDESymbol);
            cnormalMode.IndexSymbolList.AddSymbolList(lstAddSymbol2);

            foreach (CDDEASymbol cddeaSymbol in clstDDESymbol)
            {
                if (this.m_lstFailCollectAddress.Contains(cddeaSymbol.Address))
                    cddeaSymbol.CollectUse = false;
            }
            using (List<CDDEASymbol>.Enumerator enumerator = clstDDESymbol.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CDDEASymbol sym = enumerator.Current;
                    if (sym.CollectUse && sym.IndexAddressNumber == -1 && sym.IndexType != EMIndexTypeMS.IncludeAddress)
                    {
                        if (sym.DataType == EMDataType.Bool)
                        {
                            if (!stringList.Contains("B_" + sym.AddressHeader))
                            {
                                stringList.Add("B_" + sym.AddressHeader);
                                cddeaSymbolList = new List<CDDEASymbol>();
                                List<CDDEASymbol> all = clstDDESymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool && b.CollectUse));
                                all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                                List<CDDEASymbol> lstAddSymbol3 = sym.AddressMinor == -1 ? this.InsertBitSymbol(all) : this.InsertWordDotSymbol(all);
                                cnormalMode.BitSymbolList.AddSymbolList(lstAddSymbol3);
                            }
                        }
                        else if (sym.DataType == EMDataType.Word)
                        {
                            if (!stringList.Contains("W_" + sym.AddressHeader))
                            {
                                stringList.Add("W_" + sym.AddressHeader);
                                cddeaSymbolList = new List<CDDEASymbol>();
                                List<CDDEASymbol> all = clstDDESymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word && (b.IndexAddressNumber == -1 && b.IndexType != EMIndexTypeMS.IncludeAddress) && b.CollectUse));
                                all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                                for (int index = 0; index < all.Count; ++index)
                                    all[index].BaseAddress = all[index].Address;
                                cnormalMode.WordSymbolList.AddSymbolList(all);
                            }
                        }
                        else if (sym.DataType == EMDataType.DWord && !stringList.Contains("DW_" + sym.AddressHeader))
                        {
                            stringList.Add("DW_" + sym.AddressHeader);
                            cddeaSymbolList = new List<CDDEASymbol>();
                            List<CDDEASymbol> all = clstDDESymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord && b.CollectUse));
                            all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                            List<CDDEASymbol> lstAddSymbol3 = this.InsertDWordSymbol(all);
                            cnormalMode.WordSymbolList.AddSymbolList(lstAddSymbol3);
                        }
                    }
                }
            }
            return cnormalMode;
        }

        protected CFragMode CreateFragBundle(CFragMode cFragMode, CDDEASymbolList clstDDESymbol)
        {
            List<string> stringList = new List<string>();
            List<CDDEASymbol> cddeaSymbolList = (List<CDDEASymbol>)null;
            List<CDDEASymbol> lstAddSymbol1 = this.InsertIncludeIndexSymbol((List<CDDEASymbol>)clstDDESymbol);
            cFragMode.IncludeIndexSymbolList.AddSymbolList(lstAddSymbol1);
            List<CDDEASymbol> lstAddSymbol2 = this.InsertIndexSymbol((List<CDDEASymbol>)clstDDESymbol);
            cFragMode.IndexSymbolList.AddSymbolList(lstAddSymbol2);
            foreach (CDDEASymbol cddeaSymbol in (List<CDDEASymbol>)clstDDESymbol)
            {
                if (this.m_lstFailCollectAddress.Contains(cddeaSymbol.Address))
                    cddeaSymbol.CollectUse = false;
            }
            using (List<CDDEASymbol>.Enumerator enumerator = clstDDESymbol.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CDDEASymbol sym = enumerator.Current;
                    if (sym.CollectUse && sym.IndexAddressNumber == -1 && sym.IndexType != EMIndexTypeMS.IncludeAddress)
                    {
                        if (sym.DataType == EMDataType.Bool)
                        {
                            if (!stringList.Contains("B_" + sym.AddressHeader))
                            {
                                stringList.Add("B_" + sym.AddressHeader);
                                cddeaSymbolList = new List<CDDEASymbol>();
                                List<CDDEASymbol> all = clstDDESymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool && b.CollectUse));
                                all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                                List<CDDEASymbol> lstAddSymbol3 = sym.AddressMinor == -1 ? this.InsertBitSymbol(all) : this.InsertWordDotSymbol(all);
                                cFragMode.BitSymbolList.AddSymbolList(lstAddSymbol3);
                            }
                        }
                        else if (sym.DataType == EMDataType.Word)
                        {
                            if (!stringList.Contains("W_" + sym.AddressHeader))
                            {
                                stringList.Add("W_" + sym.AddressHeader);
                                cddeaSymbolList = new List<CDDEASymbol>();
                                List<CDDEASymbol> all = clstDDESymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word && (b.IndexAddressNumber == -1 && b.IndexType != EMIndexTypeMS.IncludeAddress) && b.CollectUse));
                                all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                                cFragMode.WordSymbolList.AddSymbolList(all);
                            }
                        }
                        else if (sym.DataType == EMDataType.DWord && !stringList.Contains("DW_" + sym.AddressHeader))
                        {
                            stringList.Add("DW_" + sym.AddressHeader);
                            cddeaSymbolList = new List<CDDEASymbol>();
                            List<CDDEASymbol> all = clstDDESymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord && b.CollectUse));
                            all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                            List<CDDEASymbol> lstAddSymbol3 = this.InsertDWordSymbol(all);
                            cFragMode.WordSymbolList.AddSymbolList(lstAddSymbol3);
                        }
                    }
                }
            }
            this.SameSymbolFilter(cFragMode);
            return cFragMode;
        }

        protected CFragMasterMode CreateFragMasterBundle(CFragMasterMode cFragMasterMode, CDDEASymbolList clstDDESymbol)
        {
            List<string> stringList = new List<string>();
            List<CDDEASymbol> cddeaSymbolList = (List<CDDEASymbol>)null;
            foreach (CDDEASymbol cddeaSymbol in (List<CDDEASymbol>)clstDDESymbol)
            {
                if (this.m_lstFailCollectAddress.Contains(cddeaSymbol.Address))
                    cddeaSymbol.CollectUse = false;
            }
            using (List<CDDEASymbol>.Enumerator enumerator1 = clstDDESymbol.GetEnumerator())
            {
                while (enumerator1.MoveNext())
                {
                    CDDEASymbol sym = enumerator1.Current;
                    if (sym.CollectUse)
                    {
                        if (sym.DataType != EMDataType.Bool)
                            Console.WriteLine("비트 접점이 아닙니다. - " + sym.Address);
                        else if (sym.DataType == EMDataType.Bool && !stringList.Contains("B_" + sym.AddressHeader))
                        {
                            stringList.Add("B_" + sym.AddressHeader);
                            cddeaSymbolList = new List<CDDEASymbol>();
                            List<CDDEASymbol> all1 = clstDDESymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool && b.CollectUse));
                            all1.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                            List<CDDEASymbol> lstAddSymbol = sym.AddressMinor == -1 ? this.InsertBitSymbol(all1) : this.InsertWordDotSymbol(all1);
                            using (Dictionary<string, CDDEASymbol>.Enumerator enumerator2 = cFragMasterMode.CycleSymbolS.GetEnumerator())
                            {
                                while (enumerator2.MoveNext())
                                {
                                    KeyValuePair<string, CDDEASymbol> who = enumerator2.Current;
                                    List<CDDEASymbol> all2 = lstAddSymbol.FindAll((Predicate<CDDEASymbol>)(b => b.Address == who.Value.Address));
                                    if (all2.Count > 1)
                                        lstAddSymbol.Remove(all2[1]);
                                }
                            }
                            cFragMasterMode.BitSymbolList.AddSymbolList(lstAddSymbol);
                        }
                    }
                }
            }
            return cFragMasterMode;
        }

        protected void SameSymbolFilter(CFragMode cFrag)
        {
            Dictionary<string, CDDEASymbol> dictionary = new Dictionary<string, CDDEASymbol>();
            int num1 = 0;
            foreach (CDDEASymbol bitSymbol in (List<CDDEASymbol>)cFrag.BitSymbolList)
            {
                if (!dictionary.ContainsKey(bitSymbol.Key))
                {
                    dictionary.Add(bitSymbol.Key, bitSymbol);
                }
                else
                {
                    Console.WriteLine(bitSymbol.Key + " 중복");
                    ++num1;
                }
            }
            if (num1 > 0)
            {
                cFrag.BitSymbolList.Clear();
                foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in dictionary)
                    cFrag.BitSymbolList.Add(keyValuePair.Value);
            }
            dictionary.Clear();
            int num2 = 0;
            foreach (CDDEASymbol wordSymbol in (List<CDDEASymbol>)cFrag.WordSymbolList)
            {
                if (!dictionary.ContainsKey(wordSymbol.Key))
                {
                    dictionary.Add(wordSymbol.Key, wordSymbol);
                }
                else
                {
                    Console.WriteLine(wordSymbol.Key + " 중복");
                    ++num2;
                }
            }
            if (num2 <= 0)
                return;
            cFrag.WordSymbolList.Clear();
            foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in dictionary)
                cFrag.WordSymbolList.Add(keyValuePair.Value);
        }

        protected List<CLOBMode> CreateLOBBundleList(CDDEASymbolS cMDCSymbolS, CDDEASymbolList cBitSymbolList)
        {
            List<CLOBMode> clobModeList = new List<CLOBMode>();
            CLOBMode clobMode = new CLOBMode();
            CDDEASymbolS cSymbolS = new CDDEASymbolS();
            cSymbolS.AddSymbolList((List<CDDEASymbol>)this.m_cLOBBundleList[0].GlassIDSymbolList);
            cSymbolS.AddSymbolList((List<CDDEASymbol>)this.m_cLOBBundleList[0].ProcessIDSymbolList);
            cSymbolS.AddSymbol(this.m_cLOBBundleList[0].ProcessSymbol);
            cSymbolS.AddSymbol(this.m_cLOBBundleList[0].RefreshSymbol);
            cSymbolS.AddSymbol(this.m_cLOBBundleList[0].TactTimeSymbol);
            int num1 = cSymbolS.Count;
            int num2 = 94;
            CDDEASymbolS cddeaSymbolS = new CDDEASymbolS();
            foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in (Dictionary<string, CDDEASymbol>)cMDCSymbolS)
            {
                cSymbolS.AddSymbol(keyValuePair.Value);
                cddeaSymbolS.AddSymbol(keyValuePair.Value);
                int wordSize = this.GetWordSize(cSymbolS);
                if (num2 >= wordSize)
                {
                    cddeaSymbolS = new CDDEASymbolS();
                    cSymbolS = new CDDEASymbolS();
                    cSymbolS.AddSymbolList((List<CDDEASymbol>)this.m_cLOBBundleList[0].GlassIDSymbolList);
                    cSymbolS.AddSymbolList((List<CDDEASymbol>)this.m_cLOBBundleList[0].ProcessIDSymbolList);
                    cSymbolS.AddSymbol(this.m_cLOBBundleList[0].ProcessSymbol);
                    cSymbolS.AddSymbol(this.m_cLOBBundleList[0].RefreshSymbol);
                    cSymbolS.AddSymbol(this.m_cLOBBundleList[0].TactTimeSymbol);
                    clobMode.GlassIDSymbolList = this.m_cLOBBundleList[0].GlassIDSymbolList;
                    clobMode.ProcessIDSymbolList = this.m_cLOBBundleList[0].ProcessIDSymbolList;
                    clobMode.ProcessSymbol = this.m_cLOBBundleList[0].ProcessSymbol;
                    clobMode.RefreshSymbol = this.m_cLOBBundleList[0].RefreshSymbol;
                    clobMode.TactTimeSymbol = this.m_cLOBBundleList[0].TactTimeSymbol;
                    clobModeList.Add(clobMode);
                    clobMode = new CLOBMode();
                }
            }
            if (cddeaSymbolS.Count <= 0)
                ;
            num1 = this.GetWordSize(cSymbolS);
            return clobModeList;
        }

        protected List<CDDEASymbol> InsertIncludeIndexSymbol(List<CDDEASymbol> lstSymbol)
        {
            List<CDDEASymbol> all = lstSymbol.FindAll((Predicate<CDDEASymbol>)(b => b.IndexAddressNumber != -1));
            List<CDDEASymbol> cddeaSymbolList = new List<CDDEASymbol>();
            foreach (CDDEASymbol cddeaSymbol1 in all)
            {
                cddeaSymbol1.BaseAddress = cddeaSymbol1.Address;
                string sIndexAddress = cddeaSymbol1.IndexHeader + cddeaSymbol1.IndexAddressNumber.ToString();
                CDDEASymbol cddeaSymbol2 = lstSymbol.Find((Predicate<CDDEASymbol>)(b => b.Address == sIndexAddress));
                if (cddeaSymbol2 != null)
                    cddeaSymbol1.IndexKey = cddeaSymbol2.Key;
                cddeaSymbolList.Add(cddeaSymbol1);
            }
            return cddeaSymbolList;
        }

        protected List<CDDEASymbol> InsertIndexSymbol(List<CDDEASymbol> lstSymbol)
        {
            List<CDDEASymbol> all = lstSymbol.FindAll((Predicate<CDDEASymbol>)(b => b.IndexAddressNumber != -1));
            List<CDDEASymbol> cddeaSymbolList = new List<CDDEASymbol>();
            foreach (CDDEASymbol cSymbol in all)
            {
                CDDEASymbol cddeaSymbol1 = this.AddIndexSymbol(cSymbol);
                string sIndexAddress = cSymbol.IndexHeader + cSymbol.IndexAddressNumber.ToString();
                CDDEASymbol cddeaSymbol2 = lstSymbol.Find((Predicate<CDDEASymbol>)(b => b.Address == sIndexAddress));
                if (cddeaSymbol2 != null)
                    cSymbol.IndexKey = cddeaSymbol2.Key;
                cddeaSymbolList.Add(cddeaSymbol1);
            }
            return cddeaSymbolList;
        }

        protected CDDEASymbol AddIndexSymbol(CDDEASymbol cSymbol)
        {
            string sAddress = cSymbol.IndexHeader + cSymbol.IndexAddressNumber.ToString();
            CDDEASymbol cddeaSymbol = new CDDEASymbol("[Created]" + sAddress, true);
            cddeaSymbol.CreateMelsecDDEASymbol(sAddress);
            cddeaSymbol.IndexType = EMIndexTypeMS.CreateIndex;
            cddeaSymbol.BaseAddress = sAddress;
            return cddeaSymbol;
        }

        /// <summary>
        /// Word심볼에Dot가 있는 것에 한해서 32bit단위로 최적 묶기
        /// </summary>
        /// <param name="lstSource"></param>
        /// <returns></returns>
        protected List<CDDEASymbol> InsertWordDotSymbol(List<CDDEASymbol> lstSource)
        {
            int iLeaderAddress = 0;
            List<int> lstSym = new List<int>();
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();
            List<CDDEASymbol> lstSub = null;
            foreach (CDDEASymbol sym in lstSource)
            {
                if (m_lstFailCollectAddress.Contains(sym.Address))
                {
                    sym.CollectUse = false;
                    continue;
                }

                iLeaderAddress = sym.AddressMajor;
                if (!lstSym.Contains(iLeaderAddress))
                {
                    lstSym.Add(iLeaderAddress);
                    lstSub = new List<CDDEASymbol>();
                    lstSub = lstSource.FindAll(b => b.AddressMajor == sym.AddressMajor);
                    lstSub.Sort(new CSymbolMinorComparer());

                    foreach (CDDEASymbol sub in lstSub)
                    {
                        sub.Mask = (UInt32)(0x01 << sub.AddressMinor);
                        sub.BaseAddress = sub.AddressHeader + sub.AddressHeadRemainder;
                        lstResult.Add(sub);
                    }
                }
            }

            return lstResult;
        }

        /// <summary>
        /// Bit심볼에 한해서 32bit단위로 최적 묶기
        /// </summary>
        /// <param name="lstSource"></param>
        /// <returns></returns>
        protected List<CDDEASymbol> InsertBitSymbol(List<CDDEASymbol> lstSource)
        {
            int iLeaderAddress = 0;
            int iLastSymbolMajor = -1;
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();
            foreach (CDDEASymbol sym in lstSource)
            {
                if (iLastSymbolMajor >= sym.AddressMajor)
                    continue;

                int iLoofCount = 0;
                bool bPass = false;
                if (m_dicParameterValue != null)
                {
                    if (m_dicParameterValue.ContainsKey(sym.AddressHeader))
                    {
                        int iSize = 0;
                        int iMaxSize = m_dicParameterValue[sym.AddressHeader];
                        //kch@udmtek, 17.05.19
                        if (iMaxSize <= 32 && iMaxSize > 0)
                            iMaxSize = 8192;

                        if (iMaxSize > 0)
                        {
                            iSize = iMaxSize - 31;
                        }
                        else
                        {
                            CDDEASymbol cLastSymbol;
                            for (int i = lstSource.Count - 1; i > -1; i--)
                            {
                                cLastSymbol = lstSource[i];
                                if (cLastSymbol.CollectUse)
                                {
                                    iSize = cLastSymbol.AddressMajor - 31;
                                    iMaxSize = cLastSymbol.AddressMajor + 1;
                                    break;
                                }
                            }
                        }

                        if (iMaxSize < sym.AddressMajor)
                        {
                            sym.CollectUse = false;
                            continue;
                        }

                        if (iSize < sym.AddressMajor)
                        {
                            //끝부분에 가까워져 K8로 묶을 수 없음.
                            bPass = true;
                            string sAddress = sym.AddressHeader + iSize.ToString();
                            if (sym.CheckAddressHexa(sAddress))
                            {
                                string sHexa = string.Format("{0:x}", iSize);
                                sAddress = sym.AddressHeader + sHexa.ToUpper();
                            }
                            foreach (CDDEASymbol sub in lstSource)
                            {
                                if (((iSize + 31) >= sub.AddressMajor) && (iSize <= sub.AddressMajor))
                                {
                                    int isum = sub.AddressMajor - iSize;
                                    sub.Mask = (UInt32)(0x01 << isum);
                                    sub.BaseAddress = sAddress;
                                    lstResult.Add(sub);
                                    iLastSymbolMajor = sub.AddressMajor;
                                }
                                if (iLoofCount > 31)
                                {
                                    break;
                                }
                                if (iSize <= sub.AddressMajor)
                                    iLoofCount++;
                            }
                        }
                    }
                }

                if (bPass == false)
                {
                    iLoofCount = 0;
                    iLeaderAddress = sym.AddressMajor;
                    foreach (CDDEASymbol sub in lstSource)
                    {
                        if (((iLeaderAddress + 31) >= sub.AddressMajor) && (iLeaderAddress <= sub.AddressMajor))
                        {
                            int isum = sub.AddressMajor - iLeaderAddress;
                            sub.Mask = (UInt32)(0x01 << isum);
                            sub.BaseAddress = sym.Address;
                            lstResult.Add(sub);
                            iLastSymbolMajor = sub.AddressMajor;
                        }

                        if (iLoofCount > 31)
                            break;

                        if (iLeaderAddress <= sub.AddressMajor)
                            iLoofCount++;
                    }
                }
            }
            return lstResult;
        }

        protected List<CDDEASymbol> InsertDWordSymbol(List<CDDEASymbol> lstSource)
        {
            List<CDDEASymbol> cddeaSymbolList = new List<CDDEASymbol>();
            foreach (CDDEASymbol cddeaSymbol in lstSource)
            {
                cddeaSymbol.Mask = this.MaskValueExtraction(cddeaSymbol.BitCollectNumber);
                cddeaSymbol.BaseAddress = cddeaSymbol.Address;
                cddeaSymbolList.Add(cddeaSymbol);
            }
            return cddeaSymbolList;
        }

        /// <summary>
        /// DWord Mask 값 찾기 K8 == 0xFFFFFFFF(32Bit) K1 == 0xF
        /// </summary>
        /// <param name="sSource">ex)K8</param>
        /// <returns></returns>
        protected UInt32 MaskValueExtraction(int sSource)
        {
            UInt32 iResult = 0;
            switch (sSource)
            {
                case 8:
                    iResult = 0xFFFFFFFF;
                    break;
                case 7:
                    iResult = 0x0FFFFFFF;
                    break;
                case 6:
                    iResult = 0x00FFFFFF;
                    break;
                case 5:
                    iResult = 0x000FFFFF;
                    break;
                case 4:
                    iResult = 0x0000FFFF;
                    break;
                case 3:
                    iResult = 0x00000FFF;
                    break;
                case 2:
                    iResult = 0x000000FF;
                    break;
                case 1:
                    iResult = 0x0000000F;
                    break;
            }

            return iResult;
        }

        protected CDDEASymbolS SetLOBSelectedSymbolS(CTagS cAllTagS)
        {
            CDDEASymbolS cddeaSymbolS = new CDDEASymbolS();
            foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>)cAllTagS)
            {
                if (keyValuePair.Value.IsLOBMode)
                {
                    CDDEASymbol cSymbol = new CDDEASymbol(keyValuePair.Value.Key, false);
                    cSymbol.CreateMelsecDDEASymbol(keyValuePair.Value.Address);
                    cSymbol.AddressCount = keyValuePair.Value.Size;
                    cddeaSymbolS.AddSymbol(cSymbol);
                }
            }
            return cddeaSymbolS;
        }

        protected CDDEASymbolList GetCollectDDEASymbolList(CRefTagS cCollectTagS, CTagS cAllTagS)
        {
            CDDEASymbolList cddeaSymbolList = new CDDEASymbolList();
            foreach (string key in cCollectTagS.KeyList)
            {
                if (cAllTagS.ContainsKey(key))
                {
                    CDDEASymbol cSymbol = new CDDEASymbol(cAllTagS[key]);
                    cddeaSymbolList.Add(cSymbol);
                    cddeaSymbolList.CreateWordLength(cSymbol);
                }
            }
            return cddeaSymbolList;
        }

        public CDDEASymbolS ChangeTagSToDDEASymbolS(List<CTag> lstTag)
        {
            CDDEASymbolS cddeaSymbolS = new CDDEASymbolS();
            foreach (CTag ctag in lstTag)
            {
                try
                {
                    CDDEASymbol cSymbol = new CDDEASymbol(ctag.Key, false);
                    cSymbol.CreateMelsecDDEASymbol(ctag.Address);
                    cSymbol.AddressCount = ctag.Size;
                    if (!cddeaSymbolS.ContainsKey(cSymbol.Key))
                        cddeaSymbolS.AddSymbol(cSymbol);
                    if (cSymbol.DataType == EMDataType.Word && ctag.Size > 1)
                        cddeaSymbolS.CreateWordLength(cSymbol);
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
            }
            return cddeaSymbolS;
        }

        protected CDDEASymbolS ChangeTagToDDEASymbolS(CTag cTag)
        {
            CDDEASymbolS cddeaSymbolS = new CDDEASymbolS();
            CDDEASymbol cSymbol = new CDDEASymbol(cTag.Key, false);
            cSymbol.CreateMelsecDDEASymbol(cTag.Address);
            cSymbol.AddressCount = cTag.Size;
            cSymbol.BaseAddress = cSymbol.Address;
            if (!cddeaSymbolS.ContainsKey(cSymbol.Key))
                cddeaSymbolS.AddSymbol(cSymbol);
            if (cSymbol.DataType == EMDataType.Word && cTag.Size > 1)
                cddeaSymbolS.CreateWordLength(cSymbol);
            return cddeaSymbolS;
        }

        protected CDDEASymbolList ChangeTagToDDEASymbolList(CTag cTag)
        {
            CDDEASymbolList cddeaSymbolList = new CDDEASymbolList();
            CDDEASymbol cSymbol = new CDDEASymbol(cTag.Key, false);
            cSymbol.CreateMelsecDDEASymbol(cTag.Address);
            cSymbol.AddressCount = cTag.Size;
            cSymbol.BaseAddress = cSymbol.Address;
            if (!cddeaSymbolList.Contains(cSymbol))
                cddeaSymbolList.AddSymbol(cSymbol);
            if (cSymbol.DataType == EMDataType.Word && cTag.Size > 1)
                cddeaSymbolList.CreateWordLength(cSymbol);
            return cddeaSymbolList;
        }

        protected CDDEASymbol ChangeTagToDDEASymbol(CTag cTag)
        {
            CDDEASymbol cddeaSymbol = new CDDEASymbol(cTag.Key, false);
            cddeaSymbol.CreateMelsecDDEASymbol(cTag.Address);
            cddeaSymbol.AddressCount = cTag.Size;
            cddeaSymbol.BaseAddress = cTag.Address;
            return cddeaSymbol;
        }

        private List<CDDEASymbolList> GetPacketSymbolList(CDDEASymbolList cTotalSymbolList, int iMaxWord)
        {
            List<CDDEASymbolList> cddeaSymbolListList = new List<CDDEASymbolList>();
            try
            {
                cTotalSymbolList.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                if (CPacketHelper.GetWordSize(cTotalSymbolList, (Dictionary<string, int>)null) > iMaxWord)
                {
                    CDDEASymbolList lstSymbol = new CDDEASymbolList();
                    int num = 0;
                    foreach (CDDEASymbol cTotalSymbol in (List<CDDEASymbol>)cTotalSymbolList)
                    {
                        if (num >= iMaxWord)
                        {
                            num = CPacketHelper.GetWordSize(lstSymbol, (Dictionary<string, int>)null);
                            if (num >= iMaxWord)
                            {
                                cddeaSymbolListList.Add(lstSymbol);
                                lstSymbol = new CDDEASymbolList();
                                num = 0;
                            }
                            lstSymbol.AddSymbol(cTotalSymbol);
                            lstSymbol.CreateWordLength(cTotalSymbol);
                            ++num;
                        }
                        else
                        {
                            lstSymbol.AddSymbol(cTotalSymbol);
                            lstSymbol.CreateWordLength(cTotalSymbol);
                            ++num;
                        }
                    }
                    if (num > 0)
                        cddeaSymbolListList.Add(lstSymbol);
                }
                else
                {
                    CDDEASymbolList cddeaSymbolList = new CDDEASymbolList();
                    foreach (CDDEASymbol cTotalSymbol in (List<CDDEASymbol>)cTotalSymbolList)
                    {
                        cddeaSymbolList.AddSymbol(cTotalSymbol);
                        cddeaSymbolList.CreateWordLength(cTotalSymbol);
                    }
                    cddeaSymbolListList.Add(cddeaSymbolList);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return cddeaSymbolListList;
        }

        protected int GetWordSize(CDDEASymbolS cSymbolS)
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            if (cSymbolS == null)
                return -1;
            List<CDDEASymbol> cddeaSymbolList1 = this.ChangeFromSymbolSToListSymbol(cSymbolS);
            foreach (CDDEASymbol cddeaSymbol1 in cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>)(a => a.IndexAddressNumber != -1)))
            {
                string str = cddeaSymbol1.IndexHeader + cddeaSymbol1.IndexAddressNumber.ToString();
                CDDEASymbol cddeaSymbol2 = new CDDEASymbol(str, true);
                cddeaSymbol2.CreateMelsecDDEASymbol(str);
                cddeaSymbol2.IndexType = EMIndexTypeMS.CreateIndex;
                if (!cSymbolS.ContainsKey(cddeaSymbol2.Key))
                    ++num4;
            }
            List<string> stringList = new List<string>();
            CTagS ctagS = new CTagS();
            List<CDDEASymbol> cddeaSymbolList2 = new List<CDDEASymbol>();
            using (List<CDDEASymbol>.Enumerator enumerator = cddeaSymbolList1.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CDDEASymbol sym = enumerator.Current;
                    if (sym.DataType == EMDataType.Bool)
                    {
                        if (!stringList.Contains("B_" + sym.AddressHeader))
                        {
                            stringList.Add("B_" + sym.AddressHeader);
                            cddeaSymbolList2 = new List<CDDEASymbol>();
                            List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool));
                            all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                            if (sym.AddressMinor != -1)
                                num1 += this.GetWordCountFromWordDot(all);
                            else
                                num1 += this.GetWordCountFromBit(all);
                        }
                    }
                    else if (sym.DataType == EMDataType.Word)
                    {
                        if (!stringList.Contains("W_" + sym.AddressHeader))
                        {
                            stringList.Add("W_" + sym.AddressHeader);
                            cddeaSymbolList2 = new List<CDDEASymbol>();
                            List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word));
                            all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                            num2 += all.Count;
                        }
                    }
                    else if (sym.DataType == EMDataType.DWord && !stringList.Contains("DW_" + sym.AddressHeader))
                    {
                        stringList.Add("DW_" + sym.AddressHeader);
                        cddeaSymbolList2 = new List<CDDEASymbol>();
                        List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>)(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord));
                        all.Sort((IComparer<CDDEASymbol>)new CSymbolComparer());
                        num3 += all.Count;
                    }
                }
            }
            return num1 + num2 + num3 + num4;
        }

        protected int GetWordCountFromWordDot(List<CDDEASymbol> lstSymbol)
        {
            int num = 0;
            List<int> intList = new List<int>();
            List<CDDEASymbol> cddeaSymbolList = (List<CDDEASymbol>)null;
            using (List<CDDEASymbol>.Enumerator enumerator = lstSymbol.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CDDEASymbol sym = enumerator.Current;
                    int addressMajor = sym.AddressMajor;
                    if (!intList.Contains(addressMajor))
                    {
                        intList.Add(addressMajor);
                        cddeaSymbolList = new List<CDDEASymbol>();
                        List<CDDEASymbol> all = lstSymbol.FindAll((Predicate<CDDEASymbol>)(b => b.AddressMajor == sym.AddressMajor));
                        all.Sort((IComparer<CDDEASymbol>)new CSymbolMinorComparer());
                        if (all.Count > 0)
                            ++num;
                    }
                }
            }
            return num;
        }

        protected int GetWordCountFromBit(List<CDDEASymbol> lstSymbol)
        {
            int num1 = 0;
            int num2 = -1;
            foreach (CDDEASymbol cddeaSymbol1 in lstSymbol)
            {
                if (num2 < cddeaSymbol1.AddressMajor)
                {
                    int addressMajor = cddeaSymbol1.AddressMajor;
                    int num3 = 0;
                    foreach (CDDEASymbol cddeaSymbol2 in lstSymbol)
                    {
                        if (addressMajor + 31 >= cddeaSymbol2.AddressMajor && addressMajor <= cddeaSymbol2.AddressMajor)
                            num2 = cddeaSymbol2.AddressMajor;
                        if (num3 <= 31)
                        {
                            if (addressMajor <= cddeaSymbol2.AddressMajor)
                                ++num3;
                        }
                        else
                            break;
                    }
                    if (num3 > 0)
                        ++num1;
                }
            }
            return num1;
        }

        protected List<CDDEASymbol> ChangeFromSymbolSToListSymbol(CDDEASymbolS cSymbolS)
        {
            List<CDDEASymbol> cddeaSymbolList = new List<CDDEASymbol>();
            foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in (Dictionary<string, CDDEASymbol>)cSymbolS)
                cddeaSymbolList.Add(keyValuePair.Value);
            return cddeaSymbolList;
        }
    }
}
