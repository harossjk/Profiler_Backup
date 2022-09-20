// Decompiled with JetBrains decompiler
// Type: UDM.Project.CMcscProject
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UDM.Common;
using UDM.DDEACommon;
using UDM.Log;

namespace UDM.Project
{
  [Serializable]
  public class CMcscProject : IDisposable
  {
    protected string m_sName = "";
    protected CTagS m_cTagS = new CTagS();
    protected CStepS m_cStepS = new CStepS();
    protected CConditionS m_cCycleStart = new CConditionS();
    protected CConditionS m_cCycleEnd = new CConditionS();
    protected CConditionS m_cCycleTrigger = new CConditionS();
    protected int m_iMaxCycleTime = 1000000;
    protected int m_iMinCycleTime = 0;
    protected int m_iCycleCount = 1;
    protected CTag m_cRecipe = new CTag();
    protected CTag m_cGlassID = new CTag();
    protected CTag m_cProcess = new CTag();
    protected CTag m_cTackTime = new CTag();
    protected CTag m_cRefresh = new CTag();
    protected CMDCTagInfoS m_cMDCTagS = new CMDCTagInfoS();
    protected List<CMDCColumnInfo> m_lstMDCColumn = new List<CMDCColumnInfo>();
    protected List<CMDCItemInfo> m_lstMDCItem = new List<CMDCItemInfo>();
    protected CMotionOption m_cMotionOption = new CMotionOption();
    protected CFilterOption m_cFilterOption = new CFilterOption();
    protected CPacketInfoS m_cNormalPacketS = new CPacketInfoS();
    protected CPacketInfoS m_cFragmentPacketS = new CPacketInfoS();
    protected CPacketInfoS m_cStandardPacketS = new CPacketInfoS();
    protected string m_sStandardRecipe = "";
    protected int m_iStandardCycleCount = 3;
    protected bool m_bRecipeFixedCollected = false;
    protected CLogicChartDispItemS m_cLogicChartDispItemS = new CLogicChartDispItemS();
    protected CMdcChartDispItems m_cMdcChartDispItemS = new CMdcChartDispItems();
    protected CMdcChartItemDetailS m_cMdcChartItemDetailS = new CMdcChartItemDetailS();
    [NonSerialized]
    protected int m_iOverCount = 0;
    [NonSerialized]
    protected string m_sRenamemingName = string.Empty;
    protected string m_Facility;

    public void Dispose()
    {
      this.Clear();
    }

    [MessagePackMember(0, Name = "Name")]
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

    [MessagePackMember(1, Name = "TagS")]
    public CTagS TagS
    {
      get
      {
        return this.m_cTagS;
      }
      set
      {
        this.m_cTagS = value;
      }
    }

    [MessagePackMember(2, Name = "StepS")]
    public CStepS StepS
    {
      get
      {
        return this.m_cStepS;
      }
      set
      {
        this.m_cStepS = value;
      }
    }

    [MessagePackMember(3, Name = "CycleStart")]
    public CConditionS CycleStart
    {
      get
      {
        return this.m_cCycleStart;
      }
      set
      {
        this.m_cCycleStart = value;
      }
    }

    [MessagePackMember(4, Name = "CycleEnd")]
    public CConditionS CycleEnd
    {
      get
      {
        return this.m_cCycleEnd;
      }
      set
      {
        this.m_cCycleEnd = value;
      }
    }

    [MessagePackMember(5, Name = "CycleTrigger")]
    public CConditionS CycleTrigger
    {
      get
      {
        return this.m_cCycleTrigger;
      }
      set
      {
        this.m_cCycleTrigger = value;
      }
    }

    [MessagePackMember(6, Name = "MaxCycleTime")]
    public int MaxCycleTime
    {
      get
      {
        return this.m_iMaxCycleTime;
      }
      set
      {
        this.m_iMaxCycleTime = value;
      }
    }

    [MessagePackMember(7, Name = "MinCycleTime")]
    public int MinCycleTime
    {
      get
      {
        return this.m_iMinCycleTime;
      }
      set
      {
        this.m_iMinCycleTime = value;
      }
    }

    [MessagePackMember(8, Name = "CycleCount")]
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

    [MessagePackMember(9, Name = "Recipe")]
    public CTag RecipeTag
    {
      get
      {
        return this.m_cRecipe;
      }
      set
      {
        this.m_cRecipe = value;
      }
    }

    [MessagePackMember(10, Name = "GlassID")]
    public CTag GlassIDTag
    {
      get
      {
        return this.m_cGlassID;
      }
      set
      {
        this.m_cGlassID = value;
      }
    }

    [MessagePackMember(11, Name = "Process")]
    public CTag ProcessTag
    {
      get
      {
        return this.m_cProcess;
      }
      set
      {
        this.m_cProcess = value;
      }
    }

    [MessagePackMember(12, Name = "TackTimeTag")]
    public CTag TackTimeTag
    {
      get
      {
        return this.m_cTackTime;
      }
      set
      {
        this.m_cTackTime = value;
      }
    }

    [MessagePackMember(13, Name = "RefreshTag")]
    public CTag RefreshTag
    {
      get
      {
        return this.m_cRefresh;
      }
      set
      {
        this.m_cRefresh = value;
      }
    }

    [MessagePackMember(14, Name = "MDCTagInfoS")]
    public CMDCTagInfoS MDCTagInfoS
    {
      get
      {
        return this.m_cMDCTagS;
      }
      set
      {
        this.m_cMDCTagS = value;
      }
    }

    [MessagePackMember(15, Name = "MDCColumnList")]
    public List<CMDCColumnInfo> MDCColumnList
    {
      get
      {
        return this.m_lstMDCColumn;
      }
      set
      {
        this.m_lstMDCColumn = value;
      }
    }

    [MessagePackMember(16, Name = "MDCItemList")]
    public List<CMDCItemInfo> MDCItemList
    {
      get
      {
        return this.m_lstMDCItem;
      }
      set
      {
        this.m_lstMDCItem = value;
      }
    }

    [MessagePackMember(17, Name = "MotionOption")]
    public CMotionOption MotionOption
    {
      get
      {
        return this.m_cMotionOption;
      }
      set
      {
        this.m_cMotionOption = value;
      }
    }

    [MessagePackMember(18, Name = "FilterOption")]
    public CFilterOption FilterOption
    {
      get
      {
        return this.m_cFilterOption;
      }
      set
      {
        this.m_cFilterOption = value;
      }
    }

    [MessagePackMember(19, Name = "NormalPacketS")]
    public CPacketInfoS NormalPacketS
    {
      get
      {
        return this.m_cNormalPacketS;
      }
      set
      {
        this.m_cNormalPacketS = value;
      }
    }

    [MessagePackMember(20, Name = "FragmentPacketS")]
    public CPacketInfoS FragmentPacketS
    {
      get
      {
        return this.m_cFragmentPacketS;
      }
      set
      {
        this.m_cFragmentPacketS = value;
      }
    }

    [MessagePackMember(21, Name = "StandardPacketS")]
    public CPacketInfoS StandardPacketS
    {
      get
      {
        return this.m_cStandardPacketS;
      }
      set
      {
        this.m_cStandardPacketS = value;
      }
    }

    [MessagePackMember(22, Name = "StandardRecipe")]
    public string StandardRecipe
    {
      get
      {
        return this.m_sStandardRecipe;
      }
      set
      {
        this.m_sStandardRecipe = value;
      }
    }

    [MessagePackMember(23, Name = "StandardCycleCount")]
    public int StandardCycleCount
    {
      get
      {
        return this.m_iStandardCycleCount;
      }
      set
      {
        this.m_iStandardCycleCount = value;
      }
    }

    [MessagePackMember(24, Name = "RecipeFixedCollected")]
    public bool IsRecipeFixedCollected
    {
      get
      {
        return this.m_bRecipeFixedCollected;
      }
      set
      {
        this.m_bRecipeFixedCollected = value;
      }
    }

    [MessagePackMember(25, Name = "LogicChartDispItemS")]
    public CLogicChartDispItemS LogicChartDispItemS
    {
      get
      {
        return this.m_cLogicChartDispItemS;
      }
      set
      {
        this.m_cLogicChartDispItemS = value;
      }
    }

    [MessagePackMember(26, Name = "MdcChartDispItemS")]
    public CMdcChartDispItems MdcChartDispItemS
    {
      get
      {
        return this.m_cMdcChartDispItemS;
      }
      set
      {
        this.m_cMdcChartDispItemS = value;
      }
    }

    [MessagePackMember(27, Name = "MdcChartItemDetailS")]
    public CMdcChartItemDetailS MdcChartItemDetailS
    {
      get
      {
        return this.m_cMdcChartItemDetailS;
      }
      set
      {
        this.m_cMdcChartItemDetailS = value;
      }
    }

    public string Facility
    {
      get
      {
        return this.m_Facility;
      }
      set
      {
        this.m_Facility = value;
        if (!string.IsNullOrEmpty(this.m_sRenamemingName))
          return;
        this.m_sRenamemingName = this.m_Facility;
      }
    }

    public string RenamingName
    {
      get
      {
        return this.m_sRenamemingName;
      }
      set
      {
        this.m_sRenamemingName = value;
      }
    }

    public void Compose()
    {
      this.m_cStepS.Compose(this.m_cTagS);
      this.m_cFragmentPacketS.Compose(this.m_cStepS, this.m_cTagS);
      this.m_cNormalPacketS.Compose(this.m_cStepS, this.m_cTagS);
    }

    public void Clear()
    {
      if (this.m_cTagS != null)
        this.m_cTagS.Clear();
      if (this.m_cStepS != null)
        this.m_cStepS.Clear();
      if (this.m_cNormalPacketS != null)
        this.m_cNormalPacketS.Clear();
      if (this.m_cFragmentPacketS != null)
        this.m_cFragmentPacketS.Clear();
      if (this.m_cFilterOption != null)
        this.m_cFilterOption.Clear();
      if (this.m_cCycleStart != null)
        this.m_cCycleStart.Clear();
      if (this.m_cCycleEnd != null)
        this.m_cCycleEnd.Clear();
      if (this.m_lstMDCItem != null)
        this.m_lstMDCItem.Clear();
      if (this.m_lstMDCColumn != null)
        this.m_lstMDCColumn.Clear();
      if (this.m_cMDCTagS != null)
        this.m_cMDCTagS.Clear();
      this.m_cRecipe = new CTag();
      this.m_cGlassID = new CTag();
      this.m_cProcess = new CTag();
      this.m_cRefresh = new CTag();
      this.m_cTackTime = new CTag();
    }

    public List<CTag> GetTagList(CStep cStep)
    {
      List<CTag> ctagList = new List<CTag>();
      for (int index = 0; index < cStep.RefTagS.Count; ++index)
      {
        CTag ctag = cStep.RefTagS[index];
        ctagList.Add(ctag);
      }
      return ctagList;
    }

    public List<CTag> GetTagList(CStep cStep, EMCollectModeType emModeType)
    {
      List<CTag> ctagList = new List<CTag>();
      for (int index = 0; index < cStep.RefTagS.Count; ++index)
      {
        CTag ctag = cStep.RefTagS[index];
        switch (emModeType)
        {
          case EMCollectModeType.Normal:
            if (ctag.IsNormalMode)
            {
              ctagList.Add(ctag);
              break;
            }
            break;
          case EMCollectModeType.Fragment:
            if (ctag.IsFragmentMode)
            {
              ctagList.Add(ctag);
              break;
            }
            break;
          case EMCollectModeType.LOB:
            if (ctag.IsLOBMode)
            {
              ctagList.Add(ctag);
              break;
            }
            break;
          case EMCollectModeType.StandardTag:
            if (ctag.IsLOBMode)
            {
              ctagList.Add(ctag);
              break;
            }
            break;
          default:
            ctagList.Add(ctag);
            break;
        }
      }
      return ctagList;
    }

    public List<CTag> GetTagList(List<string> lstAddress)
    {
      List<CTag> ctagList = new List<CTag>();
      for (int index = 0; index < this.m_cTagS.Count; ++index)
      {
        CTag ctag = this.m_cTagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
        if (lstAddress.Contains(ctag.Address) && !ctagList.Contains(ctag))
          ctagList.Add(ctag);
      }
      return ctagList;
    }

    public List<CTag> GetNormalModeTagList()
    {
      List<CTag> ctagList = new List<CTag>();
      for (int index = 0; index < this.m_cTagS.Count; ++index)
      {
        CTag ctag = this.m_cTagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
        if (ctag.IsNormalMode)
          ctagList.Add(ctag);
      }
      return ctagList;
    }

    public List<CTag> GetStandardTagList()
    {
      return this.m_cTagS.Select<KeyValuePair<string, CTag>, CTag>((Func<KeyValuePair<string, CTag>, CTag>) (x => x.Value)).Where<CTag>((Func<CTag, bool>) (x => x.IsStandardable)).ToList<CTag>();
    }

    public List<CTag> GetBitCoilTagList(CStep cStep)
    {
      List<CTag> ctagList = new List<CTag>();
      for (int index = 0; index < cStep.CoilS.Count; ++index)
      {
        CCoil ccoil = cStep.CoilS[index];
        if (ccoil.RefTagS.Count == 1)
        {
          CTag ctag = ccoil.RefTagS[0];
          if (ctag.DataType == EMDataType.Bool)
            ctagList.Add(ctag);
        }
      }
      return ctagList;
    }

    public List<CTag> GetCycleTagList()
    {
      List<string> cycleAddressList = this.GetCycleAddressList(this.m_cCycleStart, this.m_cCycleEnd);
      List<CTag> ctagList = new List<CTag>();
      for (int index = 0; index < cycleAddressList.Count; ++index)
      {
        foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>) this.m_cTagS)
        {
          if (keyValuePair.Value.Address == cycleAddressList[index])
          {
            if (!ctagList.Contains(keyValuePair.Value))
            {
              ctagList.Add(keyValuePair.Value);
              break;
            }
            break;
          }
        }
      }
      return ctagList;
    }

    public List<CStep> GetStepList(string sKey)
    {
      List<CStep> cstepList = new List<CStep>();
      for (int index = 0; index < this.m_cStepS.Count; ++index)
      {
        CStep cstep = this.m_cStepS.ElementAt<KeyValuePair<string, CStep>>(index).Value;
        if (cstep.RefTagS.KeyList.Contains(sKey))
          cstepList.Add(cstep);
      }
      return cstepList;
    }

    public List<CStep> GetCoilStepList(CTag cTag)
    {
      List<CStep> cstepList = new List<CStep>();
      for (int index = 0; index < cTag.StepRoleS.Count; ++index)
      {
        if (cTag.StepRoleS[index].RoleType == EMTagRoleType.Coil || cTag.StepRoleS[index].RoleType == EMTagRoleType.Both)
        {
          string step = cTag.StepRoleS[index].Step;
          if (this.m_cStepS.ContainsKey(step))
          {
            CStep cstep = this.m_cStepS[step];
            if (!cstepList.Contains(cstep))
              cstepList.Add(cstep);
          }
        }
      }
      for (int index1 = 0; index1 < this.m_cStepS.Count; ++index1)
      {
        CStep cstep = this.m_cStepS.ElementAt<KeyValuePair<string, CStep>>(index1).Value;
        if (!cstepList.Contains(cstep) && cstep.CoilS.Count > 0)
        {
          CCoil ccoil = cstep.CoilS[0];
          for (int index2 = 0; index2 < ccoil.RefTagS.Count; ++index2)
          {
            if (ccoil.RefTagS[index2].Address.StartsWith(cTag.Address + "Z"))
            {
              cstepList.Add(cstep);
              break;
            }
          }
        }
      }
      return cstepList;
    }

    public List<CStep> GetCoilStepList(List<CTag> lstTag)
    {
      List<CStep> cstepList1 = new List<CStep>();
      List<CStep> cstepList2 = new List<CStep>();
      for (int index1 = 0; index1 < lstTag.Count; ++index1)
      {
        List<CStep> coilStepList = this.GetCoilStepList(lstTag[index1]);
        for (int index2 = 0; index2 < coilStepList.Count; ++index2)
        {
          CStep cstep = coilStepList[index2];
          if (!cstepList1.Contains(cstep))
            cstepList1.Add(cstep);
        }
        coilStepList.Clear();
      }
      return cstepList1;
    }

    public List<CStep> GetSubCoilStepList(List<CTag> lstTag)
    {
      List<CStep> cstepList = new List<CStep>();
      for (int index1 = 0; index1 < lstTag.Count; ++index1)
      {
        CTag ctag = lstTag[index1];
        for (int index2 = 0; index2 < ctag.StepRoleS.Count; ++index2)
        {
          if (ctag.StepRoleS[index2].RoleType == EMTagRoleType.Coil || ctag.StepRoleS[index2].RoleType == EMTagRoleType.Both)
          {
            string step = ctag.StepRoleS[index2].Step;
            if (this.m_cStepS.ContainsKey(step))
            {
              CStep cstep = this.m_cStepS[step];
              if (!cstepList.Contains(cstep))
                cstepList.Add(cstep);
            }
          }
        }
      }
      return cstepList;
    }

    public List<CStep> GetSubCoilStepList(CTag cTag)
    {
      List<CStep> cstepList = new List<CStep>();
      for (int index = 0; index < cTag.StepRoleS.Count; ++index)
      {
        if (cTag.StepRoleS[index].RoleType == EMTagRoleType.Coil || cTag.StepRoleS[index].RoleType == EMTagRoleType.Both)
        {
          string step = cTag.StepRoleS[index].Step;
          if (this.m_cStepS.ContainsKey(step))
          {
            CStep cstep = this.m_cStepS[step];
            if (!cstepList.Contains(cstep))
              cstepList.Add(cstep);
          }
        }
      }
      return cstepList;
    }

    public List<CStep> GetEndCoilStepList()
    {
      List<CStep> cstepList = new List<CStep>();
      for (int index = 0; index < this.m_cStepS.Count; ++index)
      {
        CStep cstep = this.m_cStepS.ElementAt<KeyValuePair<string, CStep>>(index).Value;
        if (cstep.CoilS.Count == 1)
        {
          CCoil ccoil = cstep.CoilS[0];
          if (ccoil.ContentS.Count == 1)
          {
            CTag tag = ccoil.ContentS[0].Tag;
            if (tag != null && tag.DataType == EMDataType.Bool && this.IsEndCoilTag(tag))
              cstepList.Add(cstep);
          }
        }
      }
      return cstepList;
    }

    public List<CTagStepPair> GetPairList(CStep cStep)
    {
      List<CTagStepPair> ctagStepPairList = new List<CTagStepPair>();
      for (int index = 0; index < cStep.RefTagS.Count; ++index)
        ctagStepPairList.Add(new CTagStepPair()
        {
          Tag = cStep.RefTagS[index],
          Step = cStep
        });
      return ctagStepPairList;
    }

    public List<CTagStepPair> GetCoilPairList(CStep cStep)
    {
      List<CTagStepPair> ctagStepPairList = new List<CTagStepPair>();
      for (int index1 = 0; index1 < cStep.CoilS.Count; ++index1)
      {
        CCoil ccoil = cStep.CoilS[index1];
        for (int index2 = 0; index2 < ccoil.RefTagS.Count; ++index2)
          ctagStepPairList.Add(new CTagStepPair()
          {
            Tag = ccoil.RefTagS[index2],
            Step = cStep
          });
      }
      return ctagStepPairList;
    }

    public List<CTagStepPair> GetCoilPairList(List<CStep> lstStep)
    {
      List<CTagStepPair> ctagStepPairList = new List<CTagStepPair>();
      for (int index = 0; index < lstStep.Count; ++index)
      {
        List<CTagStepPair> coilPairList = this.GetCoilPairList(lstStep[index]);
        if (coilPairList.Count > 0)
          ctagStepPairList.AddRange((IEnumerable<CTagStepPair>) coilPairList);
        coilPairList.Clear();
      }
      return ctagStepPairList;
    }

    public List<CTagStepPair> GetBitPairList()
    {
      List<CTagStepPair> ctagStepPairList1 = new List<CTagStepPair>();
      List<CTag> ctagList = new List<CTag>();
      List<CTagStepPair> ctagStepPairList2 = (List<CTagStepPair>) null;
      for (int index1 = 0; index1 < this.m_cStepS.Count; ++index1)
      {
        List<CTagStepPair> bitPairList = this.GetBitPairList(this.m_cStepS.ElementAt<KeyValuePair<string, CStep>>(index1).Value);
        for (int index2 = 0; index2 < bitPairList.Count; ++index2)
        {
          CTagStepPair ctagStepPair = bitPairList[index2];
          if (!ctagList.Contains(ctagStepPair.Tag))
          {
            ctagStepPairList1.Add(ctagStepPair);
            ctagList.Add(ctagStepPair.Tag);
          }
        }
        bitPairList.Clear();
        ctagStepPairList2 = (List<CTagStepPair>) null;
      }
      return ctagStepPairList1;
    }

    public List<CTagStepPair> GetBitPairList(CStep cStep)
    {
      List<CTagStepPair> ctagStepPairList = new List<CTagStepPair>();
      for (int index = 0; index < cStep.RefTagS.Count; ++index)
      {
        if (cStep.RefTagS[index].DataType == EMDataType.Bool)
          ctagStepPairList.Add(new CTagStepPair()
          {
            Tag = cStep.RefTagS[index],
            Step = cStep
          });
      }
      return ctagStepPairList;
    }

    public List<CTagStepPair> GetBitCoilPairList(List<CStep> lstStep)
    {
      List<CTagStepPair> ctagStepPairList = new List<CTagStepPair>();
      List<CTag> ctagList1 = new List<CTag>();
      List<CTag> ctagList2 = (List<CTag>) null;
      for (int index1 = 0; index1 < lstStep.Count; ++index1)
      {
        CStep cStep = lstStep[index1];
        List<CTag> bitCoilTagList = this.GetBitCoilTagList(cStep);
        for (int index2 = 0; index2 < bitCoilTagList.Count; ++index2)
        {
          CTag ctag = bitCoilTagList[index2];
          if (!ctagList1.Contains(ctag))
          {
            ctagStepPairList.Add(new CTagStepPair()
            {
              Tag = ctag,
              Step = cStep
            });
            ctagList1.Add(ctag);
          }
        }
        bitCoilTagList.Clear();
        ctagList2 = (List<CTag>) null;
      }
      ctagList1.Clear();
      return ctagStepPairList;
    }

    public bool AddStepListToMaxWordSize(List<CStep> lstStep, List<CStep> lstTotalStep, List<CTag> lstTotalTag, EMCollectModeType emModeType, int iMaxWordSize, out List<CTag> lstAddTag)
    {
      lstAddTag = new List<CTag>();
      int wordSize = CPacketHelper.GetWordSize(lstTotalTag, (Dictionary<string, int>) null);
      int num = 0;
      if (wordSize >= iMaxWordSize)
        return true;
      List<CTag> ctagList = (List<CTag>) null;
      bool flag = false;
      for (int index1 = 0; index1 < lstStep.Count; ++index1)
      {
        CStep cStep = lstStep[index1];
        if (!lstTotalStep.Contains(cStep))
        {
          List<CTag> tagList = this.GetTagList(cStep, emModeType);
          for (int index2 = 0; index2 < tagList.Count; ++index2)
          {
            if (lstTotalTag.Contains(tagList[index2]))
            {
              tagList.RemoveAt(index2);
              --index2;
            }
          }
          num += this.GetVirtualWordSize(tagList);
          if (iMaxWordSize > wordSize + num)
          {
            lstTotalStep.Add(cStep);
            lstTotalTag.AddRange((IEnumerable<CTag>) tagList);
            lstAddTag.AddRange((IEnumerable<CTag>) tagList);
          }
          else
          {
            lstTotalTag.AddRange((IEnumerable<CTag>) tagList);
            wordSize = CPacketHelper.GetWordSize(lstTotalTag, (Dictionary<string, int>) null);
            if (wordSize > iMaxWordSize)
            {
              int count = lstTotalTag.Count;
              lstTotalTag.RemoveRange(count - tagList.Count, tagList.Count);
              flag = true;
              break;
            }
            lstTotalStep.Add(cStep);
            lstAddTag.AddRange((IEnumerable<CTag>) tagList);
            num = 0;
          }
          tagList.Clear();
          ctagList = (List<CTag>) null;
        }
      }
      return flag;
    }

    public CPacketInfo CreatePacketInfoFullDepth(List<CTag> lstHeaderTag, CTag cBaseTag, EMCollectModeType emModeType, int iMaxWordSize)
    {
      CPacketInfo cpacketInfo = new CPacketInfo();
      cpacketInfo.BaseTagKey = cBaseTag.Key;
      List<CStep> lstTotalStep = new List<CStep>();
      List<CTag> ctagList = new List<CTag>();
      List<CTag> lstAddTag = new List<CTag>();
      bool flag = false;
      int num = 0;
      ctagList.AddRange((IEnumerable<CTag>) lstHeaderTag);
      if (!ctagList.Contains(cBaseTag))
        ctagList.Add(cBaseTag);
      List<CStep> lstStep = this.GetCoilStepList(cBaseTag);
      if (lstStep.Count == 0)
      {
        for (int index = 0; index < ctagList.Count; ++index)
          cpacketInfo.RefTagS.Add(ctagList[index].Key, ctagList[index]);
        return cpacketInfo;
      }
      num = CPacketHelper.GetWordSize(ctagList, (Dictionary<string, int>) null);
      while (!flag)
      {
        flag = this.AddStepListToMaxWordSize(lstStep, lstTotalStep, ctagList, emModeType, iMaxWordSize, out lstAddTag);
        if (!flag && lstAddTag.Count > 0)
        {
          lstStep = this.GetSubCoilStepList(lstAddTag);
          for (int index = 0; index < lstStep.Count; ++index)
          {
            if (lstTotalStep.Contains(lstStep[index]))
            {
              lstStep.RemoveAt(index);
              --index;
            }
          }
          lstAddTag.Clear();
          lstAddTag = (List<CTag>) null;
          if (lstStep.Count == 0)
            break;
        }
        else
          break;
      }
      for (int index = 0; index < lstTotalStep.Count; ++index)
      {
        CStep oValue = lstTotalStep[index];
        cpacketInfo.RefStepS.Add(oValue.Key, oValue);
      }
      for (int index = 0; index < ctagList.Count; ++index)
        cpacketInfo.RefTagS.Add(ctagList[index].Key, ctagList[index]);
      lstTotalStep.Clear();
      ctagList.Clear();
      return cpacketInfo;
    }

    public void CreateNormalModePacketInfoS(List<CTag> lstCollectTag, int iMaxWord)
    {
      this.m_cNormalPacketS.Clear();
      if (CPacketHelper.GetWordSize(lstCollectTag, (Dictionary<string, int>) null) > iMaxWord)
      {
        int num = 0;
        CPacketInfo cpacketInfo = new CPacketInfo();
        List<CTag> lstTag = new List<CTag>();
        foreach (CTag ctag1 in lstCollectTag)
        {
          if (num == iMaxWord)
          {
            num = CPacketHelper.GetWordSize(lstTag, (Dictionary<string, int>) null);
            if (num == iMaxWord)
            {
              foreach (CTag ctag2 in lstTag)
                cpacketInfo.RefTagS.KeyList.Add(ctag2.Key);
              this.m_cNormalPacketS.Add(cpacketInfo);
              cpacketInfo = new CPacketInfo();
              lstTag = new List<CTag>();
              num = 0;
            }
          }
          else
          {
            lstTag.Add(ctag1);
            ++num;
          }
        }
        if (num <= 0)
          return;
        foreach (CTag ctag in lstTag)
          cpacketInfo.RefTagS.KeyList.Add(ctag.Key);
        this.m_cNormalPacketS.Add(cpacketInfo);
      }
      else
      {
        CPacketInfo cpacketInfo = new CPacketInfo();
        for (int index = 0; index < lstCollectTag.Count; ++index)
          cpacketInfo.RefTagS.KeyList.Add(lstCollectTag[index].Key);
        this.m_cNormalPacketS.Add(cpacketInfo);
      }
    }

    public void CreateStandardPacketInfoS(List<CTag> lstHeaderTag, List<CTag> lstTag, List<CTimeLogS> lstLogS, int iMaxWordSize)
    {
      if (this.m_cStandardPacketS == null)
        this.m_cStandardPacketS = new CPacketInfoS();
      else
        this.m_cStandardPacketS.Clear();
      CTagS ctagS = new CTagS();
      for (int index = 0; index < lstTag.Count; ++index)
        ctagS.Add(lstTag[index].Key, lstTag[index]);
      CTimeLogS ctimeLogS1 = new CTimeLogS();
      for (int index = 0; index < lstLogS.Count; ++index)
        ctimeLogS1.AddRange((IEnumerable<CTimeLog>) lstLogS[index]);
      ctimeLogS1.Sort((IComparer<CTimeLog>) new CTimeLogComparer());
      List<CTag> lstTag1 = new List<CTag>();
      CTimeLogS ctimeLogS2 = new CTimeLogS();
      int num1 = 0;
      int iMinInterval = 50;
      lstTag1.AddRange((IEnumerable<CTag>) lstHeaderTag);
      int num2 = lstHeaderTag.Count;
      for (int index1 = 0; index1 < ctimeLogS1.Count; ++index1)
      {
        CTimeLog ctimeLog = ctimeLogS1[index1];
        if (ctagS.ContainsKey(ctimeLog.Key))
        {
          ctimeLogS2.Add(ctimeLog);
          CTag ctag = ctagS[ctimeLog.Key];
          if (!lstTag1.Contains(ctag))
          {
            ++num2;
            lstTag1.Add(ctag);
            if (num1 + num2 >= iMaxWordSize)
            {
              num1 = CPacketHelper.GetWordSize(lstTag1, (Dictionary<string, int>) null);
              num2 = 0;
              if (num1 >= iMaxWordSize)
              {
                if (index1 < ctimeLogS1.Count - 1)
                {
                  CTimeLog cLog2 = ctimeLogS1[index1 + 1];
                  if (!this.IsTimeIntervalGreaterThen(ctimeLog, cLog2, iMinInterval))
                  {
                    int index2 = index1 - 1;
                    while (true)
                    {
                      CTimeLog cLog1 = ctimeLogS1[index2];
                      if (!this.IsTimeIntervalGreaterThen(cLog1, ctimeLog, iMinInterval))
                      {
                        this.RemoveLogAndTag(lstTag1, ctimeLogS2, ctimeLog);
                        ctimeLog = cLog1;
                        --index2;
                      }
                      else
                        break;
                    }
                    this.RemoveLogAndTag(lstTag1, ctimeLogS2, ctimeLog);
                    index1 = index2;
                  }
                }
                CPacketSwitchCondition switchCondition = this.CreateSwitchCondition(ctimeLogS2);
                this.m_cStandardPacketS.Add(this.CreatePacketInfo(new List<CStep>(), lstTag1, switchCondition));
                ctimeLogS2.Clear();
                lstTag1.Clear();
                lstTag1 = new List<CTag>();
                ctimeLogS2 = new CTimeLogS();
                lstTag1.AddRange((IEnumerable<CTag>) lstHeaderTag);
                num1 = 0;
                num2 = lstHeaderTag.Count;
              }
            }
          }
        }
      }
      if (lstTag1 == null || lstTag1.Count <= 0)
        return;
      CPacketSwitchCondition switchCondition1 = this.CreateSwitchCondition(ctimeLogS2);
      this.m_cStandardPacketS.Add(this.CreatePacketInfo(new List<CStep>(), lstTag1, switchCondition1));
    }

    public void CreateFragModePacketInfoS(List<CTag> lstHeaderTag, List<CTag> lstStandardTag)
    {
      if (this.m_cFragmentPacketS == null)
        this.m_cFragmentPacketS = new CPacketInfoS();
      else
        this.m_cFragmentPacketS.Clear();
      List<CStep> cstepList1 = new List<CStep>();
      int iMaxWordSize = 94;
      for (int index1 = 0; index1 < lstStandardTag.Count; ++index1)
      {
        CTag cBaseTag = lstStandardTag[index1];
        if (cBaseTag.IsStandardCollectable)
        {
          CPacketInfo packetInfoFullDepth = this.CreatePacketInfoFullDepth(lstHeaderTag, cBaseTag, EMCollectModeType.Fragment, iMaxWordSize);
          this.m_cFragmentPacketS.Add(packetInfoFullDepth);
          for (int index2 = 0; index2 < packetInfoFullDepth.RefStepS.Count; ++index2)
          {
            CStep cstep = packetInfoFullDepth.RefStepS[index2];
            if (!cstepList1.Contains(cstep))
              cstepList1.Add(cstep);
          }
        }
      }
      List<CStep> lstStep = new List<CStep>();
      for (int index = 0; index < this.m_cStepS.Count; ++index)
      {
        CStep cstep = this.m_cStepS.ElementAt<KeyValuePair<string, CStep>>(index).Value;
        if (!cstepList1.Contains(cstep))
          lstStep.Add(cstep);
      }
      cstepList1.Clear();
      List<CStep> cstepList2 = new List<CStep>();
      List<CTag> ctagList = new List<CTag>();
      List<CTag> lstAddTag = new List<CTag>();
      bool flag = false;
      while (lstStep.Count > 0)
      {
        flag = this.AddStepListToMaxWordSize(lstStep, cstepList2, ctagList, EMCollectModeType.Fragment, 94, out lstAddTag);
        this.m_cFragmentPacketS.Add(this.CreatePacketInfo(cstepList2, ctagList, (CPacketSwitchCondition) null));
        for (int index = 0; index < cstepList2.Count; ++index)
          lstStep.Remove(cstepList2[index]);
        cstepList2.Clear();
        ctagList.Clear();
        lstAddTag.Clear();
      }
    }

    public void CreateFragModePacketInfoS(List<CTag> lstHeaderTag, EMCollectModeType emModeType)
    {
      if (this.m_cFragmentPacketS == null)
        this.m_cFragmentPacketS = new CPacketInfoS();
      else
        this.m_cFragmentPacketS.Clear();
      Dictionary<string, List<CDDEASymbol>> dicStep = new Dictionary<string, List<CDDEASymbol>>();
      for (int index = 0; index < this.m_cStepS.Count; ++index)
      {
        CStep cstep = this.m_cStepS.ElementAt<KeyValuePair<string, CStep>>(index).Value;
        List<CDDEASymbol> cddeaSymbolList = this.ChangeTagSToDDEASymbolList(this.SumTagSToTagList(emModeType != EMCollectModeType.Fragment ? this.GetStandardModeTagS(cstep.CoilS, this.m_cTagS) : this.GetFragModeCoilTagS(cstep.CoilS, this.m_cTagS), emModeType != EMCollectModeType.Fragment ? this.GetStandardModeContactTagS(cstep.ContactS, this.m_cTagS) : this.GetFragModeContactTagS(cstep.ContactS, this.m_cTagS)));
        dicStep.Add(cstep.Key, cddeaSymbolList);
      }
      List<CDDEASymbolS> cddeaSymbolSList = this.DivideSymbolFragPacket(this.ChangeTagListToDDEASymbolList(lstHeaderTag), this.ChangeTagListToDDEASymbolList(new List<CTag>()
      {
        this.m_cRecipe
      }), dicStep);
      for (int index = 0; index < cddeaSymbolSList.Count; ++index)
      {
        CPacketInfo cpacketInfo = new CPacketInfo();
        foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in (Dictionary<string, CDDEASymbol>) cddeaSymbolSList[index])
        {
          if (keyValuePair.Value.IndexType != EMIndexTypeMS.CreateIndex && keyValuePair.Value.IndexAddressNumber != 0 && keyValuePair.Value.AddressCount != 0)
            cpacketInfo.RefTagS.KeyList.Add(keyValuePair.Key);
        }
        cpacketInfo.RefStepS.KeyList = cddeaSymbolSList[index].StepList;
        this.m_cFragmentPacketS.Add(cpacketInfo);
      }
    }

    private bool IsEndCoilTag(CTag cTag)
    {
      if (cTag == null)
        return false;
      bool flag = true;
      for (int index = 0; index < cTag.StepRoleS.Count; ++index)
      {
        if (cTag.StepRoleS[index].RoleType != EMTagRoleType.Coil && cTag.StepRoleS[index].RoleType != EMTagRoleType.Both)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    protected List<CDDEASymbol> ChangeTagListToDDEASymbolList(List<CTag> lstCycleTag)
    {
      List<CDDEASymbol> cddeaSymbolList = new List<CDDEASymbol>();
      foreach (CTag ctag in lstCycleTag)
      {
        CDDEASymbol cddeaSymbol = new CDDEASymbol(ctag.Key, false);
        cddeaSymbol.CreateMelsecDDEASymbol(ctag.Address);
        cddeaSymbol.AddressCount = ctag.Size;
        cddeaSymbolList.Add(cddeaSymbol);
      }
      return cddeaSymbolList;
    }

    protected CTagS GetFragModeCoilTagS(CCoilS cCoilS, CTagS cAllTagS)
    {
      CTagS ctagS = new CTagS();
      List<string> stringList = new List<string>();
      foreach (CCoil ccoil in (List<CCoil>) cCoilS)
      {
        foreach (string key in ccoil.RefTagS.KeyList)
        {
          if (cAllTagS.ContainsKey(key))
          {
            if (!ctagS.ContainsKey(key) && cAllTagS[key].IsFragmentMode)
              ctagS.Add(key, cAllTagS[key]);
            else
              stringList.Add(key);
          }
        }
      }
      return ctagS;
    }

    protected CTagS GetStandardModeTagS(CCoilS cCoilS, CTagS cAllTagS)
    {
      CTagS ctagS = new CTagS();
      List<string> stringList = new List<string>();
      foreach (CCoil ccoil in (List<CCoil>) cCoilS)
      {
        foreach (string key in ccoil.RefTagS.KeyList)
        {
          if (cAllTagS.ContainsKey(key))
          {
            if (!ctagS.ContainsKey(key) && cAllTagS[key].IsStandardMode)
              ctagS.Add(key, cAllTagS[key]);
            else
              stringList.Add(key);
          }
        }
      }
      return ctagS;
    }

    protected CTagS GetFragModeContactTagS(CContactS cContactS, CTagS cAllTagS)
    {
      CTagS ctagS = new CTagS();
      List<string> stringList = new List<string>();
      foreach (CContact ccontact in (List<CContact>) cContactS)
      {
        foreach (string key in ccontact.RefTagS.KeyList)
        {
          if (cAllTagS.ContainsKey(key))
          {
            if (!ctagS.ContainsKey(key) && cAllTagS[key].IsFragmentMode)
              ctagS.Add(key, cAllTagS[key]);
            else
              stringList.Add(key);
          }
        }
      }
      return ctagS;
    }

    protected CTagS GetStandardModeContactTagS(CContactS cContactS, CTagS cAllTagS)
    {
      CTagS ctagS = new CTagS();
      List<string> stringList = new List<string>();
      foreach (CContact ccontact in (List<CContact>) cContactS)
      {
        foreach (string key in ccontact.RefTagS.KeyList)
        {
          if (cAllTagS.ContainsKey(key))
          {
            if (!ctagS.ContainsKey(key) && cAllTagS[key].IsStandardMode)
              ctagS.Add(key, cAllTagS[key]);
            else
              stringList.Add(key);
          }
        }
      }
      return ctagS;
    }

    protected List<CTag> SumTagSToTagList(CTagS cTagS1, CTagS cTagS2)
    {
      List<CTag> ctagList = new List<CTag>();
      foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>) cTagS1)
      {
        if (!ctagList.Contains(keyValuePair.Value))
          ctagList.Add(keyValuePair.Value);
      }
      foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>) cTagS2)
      {
        if (!ctagList.Contains(keyValuePair.Value))
          ctagList.Add(keyValuePair.Value);
      }
      return ctagList;
    }

    protected List<CDDEASymbolS> DivideSymbolFragPacket(List<CDDEASymbol> lstCycleSymbol, List<CDDEASymbol> lstRecipeSymbol, Dictionary<string, List<CDDEASymbol>> dicStep)
    {
      List<CDDEASymbolS> cddeaSymbolSList = new List<CDDEASymbolS>();
      CDDEASymbolS cSymbolS = new CDDEASymbolS();
      List<string> stringList = new List<string>();
      int num1 = 2;
      int num2 = 94;
      cSymbolS.AddSymbolList(lstCycleSymbol);
      cSymbolS.AddSymbolList(lstRecipeSymbol);
      int num3 = 0;
      foreach (KeyValuePair<string, List<CDDEASymbol>> keyValuePair in dicStep)
      {
        List<CDDEASymbol> lstAddSymbol = cSymbolS.AddSymbolList(keyValuePair.Value);
        num1 += lstAddSymbol.Count;
        stringList.Add(keyValuePair.Key);
        if (num2 <= num1)
        {
          num1 = this.GetWordSize(cSymbolS);
          if (num2 == num1)
          {
            cSymbolS.StepList = stringList;
            cddeaSymbolSList.Add(cSymbolS);
            cSymbolS = new CDDEASymbolS();
            stringList = new List<string>();
            cSymbolS.AddSymbolList(lstCycleSymbol);
            cSymbolS.AddSymbolList(lstRecipeSymbol);
            num1 = lstCycleSymbol.Count + lstRecipeSymbol.Count;
          }
          else if (num2 < num1)
          {
            foreach (CDDEASymbol cddeaSymbol in lstAddSymbol)
              cSymbolS.Remove(cddeaSymbol.Key);
            stringList.Remove(keyValuePair.Key);
            cSymbolS.StepList = stringList;
            cddeaSymbolSList.Add(cSymbolS);
            cSymbolS = new CDDEASymbolS();
            cSymbolS.AddSymbolList(lstCycleSymbol);
            cSymbolS.AddSymbolList(lstRecipeSymbol);
            num1 = lstCycleSymbol.Count + lstRecipeSymbol.Count;
            stringList = new List<string>();
            stringList.Add(keyValuePair.Key);
            cSymbolS.AddSymbolList(lstAddSymbol);
            num1 += cSymbolS.Count;
          }
        }
        if (num3 == dicStep.Count - 1 && cSymbolS.StepList.Count != stringList.Count)
          cSymbolS.StepList = stringList;
        ++num3;
      }
      if (num1 > lstCycleSymbol.Count + lstRecipeSymbol.Count)
        cddeaSymbolSList.Add(cSymbolS);
      return cddeaSymbolSList;
    }

    protected List<CDDEASymbol> ChangeTagSToDDEASymbolList(List<CTag> lstTag)
    {
      List<CDDEASymbol> cddeaSymbolList = new List<CDDEASymbol>();
      foreach (CTag ctag in lstTag)
      {
        CDDEASymbol cddeaSymbol1 = new CDDEASymbol(ctag.Key, false);
        cddeaSymbol1.CreateMelsecDDEASymbol(ctag.Address);
        cddeaSymbol1.AddressCount = ctag.Size;
        if (!cddeaSymbolList.Contains(cddeaSymbol1))
          cddeaSymbolList.Add(cddeaSymbol1);
        if (cddeaSymbol1.DataType == EMDataType.Word && ctag.Size > 1)
        {
          for (int index = 1; index < ctag.Size; ++index)
          {
            int num = cddeaSymbol1.AddressMajor + index;
            string str1 = cddeaSymbol1.AddressHeader + num.ToString();
            if (cddeaSymbol1.CheckAddressHexa(str1))
            {
              string str2 = string.Format("{0:x}", (object) num);
              str1 = cddeaSymbol1.AddressHeader + str2;
            }
            CDDEASymbol cddeaSymbol2 = new CDDEASymbol(str1, true);
            cddeaSymbol2.CreateMelsecDDEASymbol(str1);
            cddeaSymbol2.BaseAddress = str1;
            cddeaSymbol2.AddressCount = 0;
            if (!cddeaSymbolList.Contains(cddeaSymbol2))
              cddeaSymbolList.Add(cddeaSymbol2);
            cddeaSymbol1.DWordSecondAddress = str1;
          }
        }
      }
      return cddeaSymbolList;
    }

    protected CDDEASymbolS ChangeTagSToDDEASymbolS(List<CTag> lstTag)
    {
      CDDEASymbolS cddeaSymbolS = new CDDEASymbolS();
      foreach (CTag ctag in lstTag)
      {
        CDDEASymbol cSymbol = new CDDEASymbol(ctag.Key, false);
        cSymbol.CreateMelsecDDEASymbol(ctag.Address);
        cSymbol.AddressCount = ctag.Size;
        if (!cddeaSymbolS.ContainsKey(cSymbol.Key))
          cddeaSymbolS.AddSymbol(cSymbol);
        if (cSymbol.DataType == EMDataType.Word && ctag.Size > 1)
          cddeaSymbolS.CreateWordLength(cSymbol);
      }
      return cddeaSymbolS;
    }

    protected void AddKeyList(List<string> lstKeyList, List<CDDEASymbol> lstAddSymbol)
    {
      for (int index = 0; index < lstAddSymbol.Count; ++index)
        lstKeyList.Add(lstAddSymbol[index].Key);
    }

    protected int GetWordCountFromWordDot(List<CDDEASymbol> lstSymbol)
    {
      int num = 0;
      List<int> intList = new List<int>();
      List<CDDEASymbol> cddeaSymbolList = (List<CDDEASymbol>) null;
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
            List<CDDEASymbol> all = lstSymbol.FindAll((Predicate<CDDEASymbol>) (b => b.AddressMajor == sym.AddressMajor));
            all.Sort((IComparer<CDDEASymbol>) new CSymbolMinorComparer());
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
          ++num1;
        }
      }
      return num1;
    }

    protected List<CDDEASymbol> ChangeFromSymbolSToListSymbol(CDDEASymbolS cSymbolS)
    {
      List<CDDEASymbol> cddeaSymbolList = new List<CDDEASymbol>();
      foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in (Dictionary<string, CDDEASymbol>) cSymbolS)
        cddeaSymbolList.Add(keyValuePair.Value);
      return cddeaSymbolList;
    }

    protected int GetWordSize(CDDEASymbolS cSymbolS)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      this.m_iOverCount = 0;
      if (cSymbolS == null)
        return -1;
      List<CDDEASymbol> cddeaSymbolList1 = this.ChangeFromSymbolSToListSymbol(cSymbolS);
      List<CDDEASymbol> all1 = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>) (a => a.IndexAddressNumber != -1));
      List<string> stringList1 = new List<string>();
      foreach (CDDEASymbol cddeaSymbol1 in all1)
      {
        string sIndexAddress = cddeaSymbol1.IndexHeader + cddeaSymbol1.IndexAddressNumber.ToString();
        if (!stringList1.Contains(sIndexAddress))
        {
          CDDEASymbol cddeaSymbol2 = cddeaSymbolList1.Find((Predicate<CDDEASymbol>) (a => a.Address == sIndexAddress));
          if (cddeaSymbol2 == null)
          {
            ++num4;
            stringList1.Add(sIndexAddress);
          }
          else
            stringList1.Add(cddeaSymbol2.Address);
        }
      }
      List<string> stringList2 = new List<string>();
      CTagS ctagS = new CTagS();
      List<CDDEASymbol> cddeaSymbolList2 = new List<CDDEASymbol>();
      string str = "";
      using (List<CDDEASymbol>.Enumerator enumerator = cddeaSymbolList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          CDDEASymbol sym = enumerator.Current;
          str += string.Format("{0}/{1}\r\n", (object) sym.Address, (object) sym.AddressCount);
          if (sym.DataType == EMDataType.Bool)
          {
            if (!stringList2.Contains("B_" + sym.AddressHeader))
            {
              stringList2.Add("B_" + sym.AddressHeader);
              cddeaSymbolList2 = new List<CDDEASymbol>();
              List<CDDEASymbol> all2 = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>) (b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool));
              all2.Sort((IComparer<CDDEASymbol>) new CSymbolComparer());
              if (sym.AddressMinor != -1)
                num1 += this.GetWordCountFromWordDot(all2);
              else
                num1 += this.GetWordCountFromBit(all2);
            }
          }
          else if (sym.DataType == EMDataType.Word)
          {
            if (!stringList2.Contains("W_" + sym.AddressHeader))
            {
              stringList2.Add("W_" + sym.AddressHeader);
              cddeaSymbolList2 = new List<CDDEASymbol>();
              List<CDDEASymbol> all2 = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>) (b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word));
              all2.Sort((IComparer<CDDEASymbol>) new CSymbolComparer());
              List<CDDEASymbol> all3 = all2.FindAll((Predicate<CDDEASymbol>) (b => b.AddressCount > 1 && b.DataType == EMDataType.Word && b.AddressHeader == sym.AddressHeader));
              if (all3.Count > 0)
              {
                List<CDDEASymbol> all4 = all2.FindAll((Predicate<CDDEASymbol>) (b => b.AddressCount == 0 && b.DataType == EMDataType.Word && b.AddressHeader == sym.AddressHeader));
                int num5 = 0;
                int num6 = all3.Count + all4.Count;
                int num7 = 0;
                for (int index = 0; index < all3.Count; ++index)
                {
                  num5 += all3[index].AddressCount;
                  num7 = all3[index].AddressCount;
                }
                if (num5 > num6)
                {
                  this.m_iOverCount += num7 - (num5 - num6);
                  num2 += this.m_iOverCount;
                }
              }
              num2 += all2.Count;
            }
          }
          else if (sym.DataType == EMDataType.DWord && !stringList2.Contains("DW_" + sym.AddressHeader))
          {
            stringList2.Add("DW_" + sym.AddressHeader);
            cddeaSymbolList2 = new List<CDDEASymbol>();
            List<CDDEASymbol> all2 = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>) (b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord));
            all2.Sort((IComparer<CDDEASymbol>) new CSymbolComparer());
            num3 += all2.Count;
          }
        }
      }
      return num1 + num2 + num3 + num4;
    }

    protected int GetWordSize(List<CDDEASymbol> clstSymbol)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (clstSymbol == null)
        return -1;
      List<CDDEASymbol> cddeaSymbolList1 = clstSymbol;
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
              List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>) (b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool));
              all.Sort((IComparer<CDDEASymbol>) new CSymbolComparer());
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
              List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>) (b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word));
              all.Sort((IComparer<CDDEASymbol>) new CSymbolComparer());
              num2 += all.Count;
            }
          }
          else if (sym.DataType == EMDataType.DWord && !stringList.Contains("DW_" + sym.AddressHeader))
          {
            stringList.Add("DW_" + sym.AddressHeader);
            cddeaSymbolList2 = new List<CDDEASymbol>();
            List<CDDEASymbol> all = cddeaSymbolList1.FindAll((Predicate<CDDEASymbol>) (b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord));
            all.Sort((IComparer<CDDEASymbol>) new CSymbolComparer());
            num3 += all.Count;
          }
        }
      }
      return num1 + num2 + num3;
    }

    private int GetVirtualWordSize(List<CTag> lstTag)
    {
      int num = 0;
      for (int index = 0; index < lstTag.Count; ++index)
        num += lstTag[index].Size;
      return num;
    }

    protected List<string> GetCycleAddressList(CConditionS cStartCond, CConditionS cEndCond)
    {
      List<string> stringList = new List<string>();
      foreach (CCondition ccondition in (List<CCondition>) cStartCond)
        stringList.Add(ccondition.Address);
      foreach (CCondition ccondition in (List<CCondition>) cEndCond)
        stringList.Add(ccondition.Address);
      return stringList;
    }

    private CPacketInfo CreatePacketInfo(List<CStep> lstStep, List<CTag> lstTag, CPacketSwitchCondition cSwitchCondition)
    {
      CPacketInfo cpacketInfo = new CPacketInfo();
      for (int index = 0; index < lstStep.Count; ++index)
        cpacketInfo.RefStepS.Add(lstStep[index].Key, lstStep[index]);
      for (int index = 0; index < lstTag.Count; ++index)
        cpacketInfo.RefTagS.Add(lstTag[index].Key, lstTag[index]);
      cpacketInfo.SwitchCondition = cSwitchCondition;
      return cpacketInfo;
    }

    private CPacketSwitchCondition CreateSwitchCondition(CTimeLogS cPacketLogS)
    {
      CPacketSwitchCondition cpacketSwitchCondition = new CPacketSwitchCondition();
      CTimeLog ctimeLog = cPacketLogS.Last<CTimeLog>();
      CTimeLogS timeLogS = cPacketLogS.GetTimeLogS(ctimeLog.Key, ctimeLog.Value);
      cpacketSwitchCondition.TagKey = ctimeLog.Key;
      cpacketSwitchCondition.TagValue = ctimeLog.Value;
      cpacketSwitchCondition.TagCount = timeLogS.Count;
      timeLogS.Clear();
      return cpacketSwitchCondition;
    }

    private void RemoveLogAndTag(List<CTag> lstTag, CTimeLogS cLogS, CTimeLog cLog)
    {
      cLogS.Remove(cLog);
      CTimeLogS timeLogS = cLogS.GetTimeLogS(cLog.Key);
      if (timeLogS == null || timeLogS.Count == 0)
      {
        for (int index = 0; index < lstTag.Count; ++index)
        {
          if (lstTag[index].Key == cLog.Key)
          {
            lstTag.RemoveAt(index);
            --index;
          }
        }
      }
      else
        timeLogS.Clear();
    }

    private bool IsTimeIntervalGreaterThen(CTimeLog cLog1, CTimeLog cLog2, int iMinInterval)
    {
      bool flag = false;
      if (cLog2.Time.Subtract(cLog1.Time).TotalMilliseconds >= (double) iMinInterval)
        flag = true;
      return flag;
    }
  }
}
