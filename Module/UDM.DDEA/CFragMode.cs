// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CFragMode
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using UDM.Common;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    [Serializable]
    public class CFragMode
    {
        protected CDDEACycleSymbolS m_cCycleConditionS = new CDDEACycleSymbolS();
        protected CDDEASymbolS m_cCycleSymbolS = new CDDEASymbolS();
        protected CDDEASymbolList m_cBitSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cWordSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cIndexSymbolList = new CDDEASymbolList();
        protected CDDEASymbolList m_cIncludeIndexSymbolList = new CDDEASymbolList();
        protected List<int> m_lstStepIndex = new List<int>();

        public CDDEACycleSymbolS CycleConditionSymbolS
        {
            get
            {
                return this.m_cCycleConditionS;
            }
            set
            {
                this.m_cCycleConditionS = value;
            }
        }

        public CDDEASymbolS CycleSymbolS
        {
            get
            {
                return this.m_cCycleSymbolS;
            }
            set
            {
                this.m_cCycleSymbolS = value;
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

        public List<int> StepIndexList
        {
            get
            {
                return this.m_lstStepIndex;
            }
            set
            {
                this.m_lstStepIndex = value;
            }
        }

        public void SetDDEACycle(CConditionS cStartCondition, CConditionS cEndCondition, CConditionS cTriggerCondition, CDDEASymbolList lstSymbol)
        {
            List<string> lstCycleTagKey = this.GetCycleAddressList(cStartCondition, cEndCondition, cTriggerCondition);
            for (int i = 0; i < lstCycleTagKey.Count; ++i)
            {
                CDDEASymbol cddeaSymbol = lstSymbol.Find((Predicate<CDDEASymbol>)(b => b.Key == lstCycleTagKey[i]));
                if (cddeaSymbol != null && !this.m_cCycleSymbolS.ContainsKey(cddeaSymbol.Key))
                    this.m_cCycleSymbolS.Add(cddeaSymbol.Key, cddeaSymbol);
            }
            foreach (CCondition ccondition in (List<CCondition>)cStartCondition)
            {
                CDDEACycleSymbol cddeaCycleSymbol = new CDDEACycleSymbol();
                cddeaCycleSymbol.Condition = ccondition.TargetValue;
                foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in (Dictionary<string, CDDEASymbol>)this.m_cCycleSymbolS)
                {
                    if (keyValuePair.Value.Address == ccondition.Address)
                        cddeaCycleSymbol.Symbol = keyValuePair.Value;
                }
                this.m_cCycleConditionS.StartCycleList.Add(cddeaCycleSymbol);
            }
            foreach (CCondition ccondition in (List<CCondition>)cEndCondition)
            {
                CDDEACycleSymbol cddeaCycleSymbol = new CDDEACycleSymbol();
                cddeaCycleSymbol.Condition = ccondition.TargetValue;
                foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in (Dictionary<string, CDDEASymbol>)this.m_cCycleSymbolS)
                {
                    if (keyValuePair.Value.Address == ccondition.Address)
                        cddeaCycleSymbol.Symbol = keyValuePair.Value;
                }
                this.m_cCycleConditionS.EndCycleList.Add(cddeaCycleSymbol);
            }
            foreach (CCondition ccondition in (List<CCondition>)cTriggerCondition)
            {
                CDDEACycleSymbol cddeaCycleSymbol = new CDDEACycleSymbol();
                cddeaCycleSymbol.Condition = ccondition.TargetValue;
                foreach (KeyValuePair<string, CDDEASymbol> keyValuePair in (Dictionary<string, CDDEASymbol>)this.m_cCycleSymbolS)
                {
                    if (keyValuePair.Value.Address == ccondition.Address)
                        cddeaCycleSymbol.Symbol = keyValuePair.Value;
                }
                this.m_cCycleConditionS.TriggerCycleList.Add(cddeaCycleSymbol);
            }
        }

        protected List<string> GetCycleAddressList(CConditionS cStartCond, CConditionS cEndCond, CConditionS cTriggerCond)
        {
            List<string> stringList = new List<string>();
            foreach (CCondition ccondition in (List<CCondition>)cStartCond)
            {
                if (!stringList.Contains(ccondition.Key))
                    stringList.Add(ccondition.Key);
            }
            foreach (CCondition ccondition in (List<CCondition>)cEndCond)
            {
                if (!stringList.Contains(ccondition.Key))
                    stringList.Add(ccondition.Key);
            }
            foreach (CCondition ccondition in (List<CCondition>)cTriggerCond)
            {
                if (!stringList.Contains(ccondition.Key))
                    stringList.Add(ccondition.Key);
            }
            return stringList;
        }

        protected List<CTag> GetCycleTagList(List<string> lstAddress, CTagS cAllTagS)
        {
            List<CTag> ctagList = new List<CTag>();
            for (int index = 0; index < lstAddress.Count; ++index)
            {
                foreach (KeyValuePair<string, CTag> keyValuePair in (Dictionary<string, CTag>)cAllTagS)
                {
                    if (keyValuePair.Value.Address == lstAddress[index])
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
    }
}
