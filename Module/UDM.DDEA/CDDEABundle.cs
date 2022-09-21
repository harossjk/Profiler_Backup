using System;
using System.Collections.Generic;
using UDM.Common;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    [Serializable]
    public class CDDEABundle
    {
        #region Member Variables

        protected CDDEASymbolS m_cWordSymbolS = new CDDEASymbolS();
        protected CDDEASymbolS m_cMDCSymbolS = new CDDEASymbolS();
        protected CDDEASymbolS m_cBitSymbolS = new CDDEASymbolS();
        protected CDDEASymbolS m_cFilterSymbolS = new CDDEASymbolS();
        protected CDDEASymbolS m_cIndexSymbolS = new CDDEASymbolS();
        protected CDDEASymbolS m_cIncludeIndexSymbolS = new CDDEASymbolS();
        protected List<int> m_lstStepIndex = new List<int>();

        protected int m_iTotalWordCount = 0;
        protected bool m_bCycleInBitSymbolS = false;

        [NonSerialized]
        protected Dictionary<string, int> m_dicAddressLimit = null;

        #endregion


        #region Initialize

        public CDDEABundle()
        {

        }

        public CDDEABundle(Dictionary<string, int> dicAddressLimit)
        {
            m_dicAddressLimit = dicAddressLimit;
        }

        #endregion


        #region Public Properties


        public CDDEASymbolS WordSymbolS
        {
            get { return m_cWordSymbolS; }
            set { m_cWordSymbolS = value; }
        }

        public CDDEASymbolS BitSymbolS
        {
            get { return m_cBitSymbolS; }
            set { m_cBitSymbolS = value; }
        }

        public CDDEASymbolS FilterSymbolS
        {
            get { return m_cFilterSymbolS; }
            set { m_cFilterSymbolS = value; }
        }

        public CDDEASymbolS IndexSymbolS
        {
            get { return m_cIndexSymbolS; }
            set { m_cIndexSymbolS = value; }
        }

        public CDDEASymbolS IncludeIndexSymbolS
        {
            get { return m_cIncludeIndexSymbolS; }
            set { m_cIncludeIndexSymbolS = value; }
        }

        public CDDEASymbolS MDCSymbolS
        {
            get { return m_cMDCSymbolS; }
            set { m_cMDCSymbolS = value; }
        }

        /// <summary>
        /// 전체 Symbol의 Word수
        /// </summary>
        public int TotalWordCount
        {
            get { return m_iTotalWordCount; }
        }

        public List<int> StepIndexList
        {
            get { return m_lstStepIndex; }
            set { m_lstStepIndex = value; }
        }

        #endregion


        #region Public Method

        public CDDEASymbol ChangeFromSymbolToCDDEASymbol(CTag cTag)
        {
            CDDEASymbol cDDEASymbol = new CDDEASymbol(cTag.Key, true);
            cDDEASymbol.CreateMelsecDDEASymbol(cTag.Key);

            return cDDEASymbol;
        }

        /// <summary>
        /// Fragment Mode
        /// </summary>
        /// <param name="lstTagS"></param>
        /// <returns></returns>
        public List<CDDEABundle> CreateBundleList(List<CDDEASymbolS> lstSymbolS, CDDEAConfigMS cConfig)
        {
            List<CDDEABundle> lstBunble = new List<CDDEABundle>();
            for (int i = 0; i < lstSymbolS.Count; i++)
            {
                List<CDDEASymbol> lstSymbol = ChangeFromSymbolSToListSymbol(lstSymbolS[i]);
                //dicParameterVal = cReader.ReadParameterSymbolSize();
                //if (dicParameterVal == null)
                //    return null;
                CDDEABundle cBundle = ExtractBundleData(lstSymbol);

                CheckWordCount(cBundle);
                lstBunble.Add(cBundle);
                System.Threading.Thread.Sleep(1);
            }

            return lstBunble;
        }

        public List<CDDEABundle> CreateBundleList(CPacketInfoS cPacketInfoS, CDDEASymbolS cCycleSymbolS, CTagS cAllTagS)
        {
            List<CDDEABundle> lstBunble = new List<CDDEABundle>();

            foreach (CPacketInfo packet in cPacketInfoS)
            {
                List<CDDEASymbol> lstCollectSymbol = GetCollectDDEASymbolList(packet.RefTagS, cAllTagS);
                foreach (var who in cCycleSymbolS)
                {
                    if (lstCollectSymbol.Contains(who.Value) == false)
                        lstCollectSymbol.Add(who.Value);
                }
                lstBunble.Add(ExtractBundleData(lstCollectSymbol));
            }

            return lstBunble;
        }

        public CDDEABundle CreateRecipeBundle(CDDEASymbolS cRecipeSymbolS, CDDEASymbolS cGlassIDSymbolS, CDDEASymbolS cCycleSymbolS, CTagS cAllTagS)
        {
            CDDEABundle cBundle = new CDDEABundle();
            CDDEASymbolS cSymbolS = new CDDEASymbolS();
            cSymbolS.AddSymbolS(cCycleSymbolS);
            cSymbolS.AddSymbolS(cRecipeSymbolS);
            cSymbolS.AddSymbolS(cGlassIDSymbolS);

            List<CDDEASymbol> lstSymbol = ChangeFromSymbolSToListSymbol(cSymbolS);
            cBundle = ExtractBundleData(lstSymbol);

            return cBundle;
        }

        /*
        public void CreateCycleSymbol(CSymbol cSymbol, CDDEASymbolS cBitSymbolS)
        {
            CDDEASymbol cDDEASymbol = null;
            if (cSymbol != null)
            {
                m_cCycleSymbolS.Clear();

                if (cBitSymbolS.ContainsKey(cSymbol.Address))
                {
                    cDDEASymbol = cBitSymbolS[cSymbol.Address];
                    m_bCycleInBitSymbolS = true;
                }
                else
                {
                    cDDEASymbol = new CDDEASymbol(cSymbol.Key, true);
                    cDDEASymbol.CreateMelsecDDEASymbol(cSymbol.Key);
                    m_bCycleInBitSymbolS = false;
                }
                m_cCycleSymbolS.Add(cSymbol.Address, cDDEASymbol);
            }
        }

        public void CreateRecipeSymbolS(CSymbolS cSymbolS, CDDEASymbolS cWordSymbolS)
        {
            if (cSymbolS != null)
            {
                m_cRecipeSymbolS.Clear();
                foreach (var who in cSymbolS)
                {
                    CSymbol cSymbol = who.Value;
                    CDDEASymbol cDDEASymbol = null;
                    if (cWordSymbolS.ContainsKey(cSymbol.Address))
                    {
                        cDDEASymbol = cWordSymbolS[cSymbol.Address];
                    }
                    else
                    {
                        cDDEASymbol = new CDDEASymbol(cSymbol.Address, false);
                        cDDEASymbol.CreateMelsecDDEASymbol(cSymbol.Address);
                    }
                    m_cRecipeSymbolS.Add(cSymbol.Address, cDDEASymbol);
                }
            }
        }
        */

        public int GetWordSize(CDDEASymbolS cSymbolS)
        {
            int iBitToWord = 0;
            int iWordCount = 0;
            int iDWordCount = 0;
            List<CDDEASymbol> lstSymbol;

            if (cSymbolS != null)
                lstSymbol = ChangeFromSymbolSToListSymbol(cSymbolS);
            else
                return -1;

            List<string> lstHeader = new List<string>();
            CTagS cFilterTagS = new CTagS();
            List<CDDEASymbol> lstSub = new List<CDDEASymbol>();

            foreach (CDDEASymbol sym in lstSymbol)
            {
                if (sym.DataType == EMDataType.Bool)
                {
                    if (!lstHeader.Contains("B_" + sym.AddressHeader))
                    {
                        int iLimitCount = 100000;
                        if (m_dicAddressLimit != null && m_dicAddressLimit.ContainsKey(sym.AddressHeader))
                            iLimitCount = m_dicAddressLimit[sym.AddressHeader];

                        lstHeader.Add("B_" + sym.AddressHeader);

                        lstSub = new List<CDDEASymbol>();
                        lstSub = lstSymbol.FindAll(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool);
                        lstSub.Sort(new CSymbolComparer());
                        if (sym.AddressMinor != -1)
                            iBitToWord += GetWordCountFromWordDot(lstSub);
                        else
                            iBitToWord += GetWordCountFromBit(lstSub, iLimitCount);
                    }
                }
                else if (sym.DataType == EMDataType.Word)
                {
                    if (!lstHeader.Contains("W_" + sym.AddressHeader))
                    {
                        lstHeader.Add("W_" + sym.AddressHeader);

                        lstSub = new List<CDDEASymbol>();
                        lstSub = lstSymbol.FindAll(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word);
                        lstSub.Sort(new CSymbolComparer());
                        iWordCount += lstSub.Count;
                    }
                }
                else if (sym.DataType == EMDataType.DWord)
                {
                    if (!lstHeader.Contains("DW_" + sym.AddressHeader))
                    {
                        lstHeader.Add("DW_" + sym.AddressHeader);

                        lstSub = new List<CDDEASymbol>();
                        lstSub = lstSymbol.FindAll(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord);
                        lstSub.Sort(new CSymbolComparer());
                        iDWordCount += lstSub.Count;
                    }
                }
            }

            int iCnt = iBitToWord + iWordCount + iDWordCount;

            return iCnt;

        }


        #region


        public List<CDDEASymbol> ChangeFromSymbolSToListSymbol(CDDEASymbolS cSymbolS)
        {
            List<CDDEASymbol> lstSymbol = new List<CDDEASymbol>();

            foreach (var who in cSymbolS)
            {
                lstSymbol.Add(who.Value);
            }

            return lstSymbol;
        }

        #endregion


        public List<CDDEASymbolS> GetSymbolList94WordSort(List<CDDEASymbol> lstSource)
        {
            int iSize = 0;
            List<CDDEASymbolS> clstSymbolS = new List<CDDEASymbolS>();
            List<int> lstWordCount = new List<int>();

            List<CDDEASymbolS> lstResult = new List<CDDEASymbolS>();
            List<string> lstHeader = new List<string>();
            List<CDDEASymbol> lstSub = null;
            CDDEASymbolS cWordSymbolS = new CDDEASymbolS();

            lstSub = new List<CDDEASymbol>();
            lstSub = lstSource.FindAll(b => b.AddressHeader == "Z");
            lstSub.Sort(new CSymbolComparer());

            List<CDDEASymbol> lstIncludeIndex = new List<CDDEASymbol>();
            lstIncludeIndex = lstSource.FindAll(b => b.IndexAddressNumber != -1);
            lstIncludeIndex.Sort(new CSymbolComparer());

            List<string> lstIndexAddr = new List<string>();
            List<CDDEASymbol> lstAddIndex = new List<CDDEASymbol>();
            //추가될 인덱스를 찾음.
            foreach (CDDEASymbol sym in lstIncludeIndex)
            {
                string sIndex = sym.IndexHeader + sym.IndexAddressNumber.ToString();
                if (!lstIndexAddr.Contains(sIndex))
                {
                    lstIndexAddr.Add(sIndex);
                    bool bFind = false;
                    foreach (CDDEASymbol sym2 in lstSub)
                    {
                        if (sym2.Address == sIndex)
                        {
                            bFind = true;
                            lstAddIndex.Add(sym2);
                        }
                    }
                    if (bFind == false)
                        lstAddIndex.Add(AddIndexSymbol(sym));
                }
            }

            foreach (CDDEASymbol sym in lstSource)
            {
                CDDEASymbolS cDDEASymbolS = null;
                if (sym.IndexAddressNumber != -1)
                    continue;

                if (sym.DataType == EMDataType.Bool)
                {
                    if (!lstHeader.Contains("B_" + sym.AddressHeader))
                    {
                        int iLimitCount = 100000;
                        if (m_dicAddressLimit != null && m_dicAddressLimit.ContainsKey(sym.AddressHeader))
                            iLimitCount = m_dicAddressLimit[sym.AddressHeader];

                        lstHeader.Add("B_" + sym.AddressHeader);

                        lstSub = new List<CDDEASymbol>();
                        lstSub = lstSource.FindAll(b => b.AddressHeader == sym.BaseAddress && b.DataType == EMDataType.Bool);
                        lstSub.Sort(new CSymbolComparer());
                        if (sym.AddressMinor != -1)
                            iSize = GetWordCountFromWordDot(lstSub);
                        else
                            iSize = GetWordCountFromBit(lstSub, iLimitCount);

                        cDDEASymbolS = new CDDEASymbolS();
                        cDDEASymbolS.AddSymbolList(lstSub);
                        clstSymbolS.Add(cDDEASymbolS);
                        lstWordCount.Add(iSize);
                    }
                }
                else
                {
                    if (!cWordSymbolS.ContainsKey(sym.Key) && !lstAddIndex.Contains(sym))
                    {
                        cWordSymbolS.AddSymbol(sym);
                    }
                }
            }
            CDDEASymbolS cIndexTagS = new CDDEASymbolS();
            cIndexTagS.AddSymbolList(lstIncludeIndex);
            cIndexTagS.AddSymbolList(lstAddIndex);
            lstResult = GetTagS(cIndexTagS, cWordSymbolS, clstSymbolS, lstWordCount);

            return lstResult;
        }

        #endregion


        #region Private Method


        /// <summary>
        /// Bit -> 32Bit묶음 최적화 및 Bit, Word, DWord 비교 할 요소 분리
        /// </summary>
        /// <param name="clstDDESymbol"></param>
        /// <returns></returns>
        public CDDEABundle ExtractBundleData(List<CDDEASymbol> clstDDESymbol)
        {
            //중복된걸 확인

            List<string> lstHeader = new List<string>();
            Dictionary<string, CTag> DicIndexAddSym = new Dictionary<string, CTag>();
            List<CDDEASymbol> lstSub = null;
            CDDEABundle dicBundleResult = new CDDEABundle();

            //FragMode == true일 경우 Index를 포함한 주소에서 인덱스를 추출하여 Index SymbolS에 삽입(인덱스를 포함한 주소, 인덱스)
            //false일경우 이미 Tag List에 포함 되었으므로 그냥 분류만 한다.
            List<CDDEASymbol> lstIndexSymbol = InsertIncludeIndexSymbol(clstDDESymbol);
            dicBundleResult.IncludeIndexSymbolS.AddSymbolList(lstIndexSymbol);

            lstIndexSymbol = InsertIndexSymbol(clstDDESymbol);
            dicBundleResult.IndexSymbolS.AddSymbolList(lstIndexSymbol);

            foreach (CDDEASymbol sym in clstDDESymbol)
            {
                if (sym.IndexAddressNumber != -1)
                    continue;
                if (sym.IndexType == EMIndexTypeMS.IncludeAddress)
                    continue;

                if (sym.DataType == EMDataType.Bool)
                {
                    if (!lstHeader.Contains("B_" + sym.AddressHeader))
                    {
                        int iLimitCount = 100000;
                        if (m_dicAddressLimit != null && m_dicAddressLimit.ContainsKey(sym.AddressHeader))
                        {
                            iLimitCount = m_dicAddressLimit[sym.AddressHeader];
                            if (iLimitCount < 0)
                            {
                                //리미트 찾는 구문 추가
                                int a = 0;
                            }
                        }

                        lstHeader.Add("B_" + sym.AddressHeader);
                        //같은 헤더를 갖는 접점들을 추출
                        lstSub = new List<CDDEASymbol>();
                        lstSub = clstDDESymbol.FindAll(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Bool);
                        lstSub.Sort(new CSymbolComparer());
                        List<CDDEASymbol> lstAddSymbol = null;
                        //Word에 dot 붙었을 경우와 일반 Bit접점을 구분해서 Mask 입력 처리해야함.
                        if (sym.AddressMinor != -1)
                            lstAddSymbol = InsertWordDotSymbol(lstSub);
                        else
                            lstAddSymbol = InsertBitSymbol(lstSub, iLimitCount);
                        dicBundleResult.BitSymbolS.AddSymbolList(lstAddSymbol);
                    }
                }
                else if (sym.DataType == EMDataType.Word)
                {
                    if (!lstHeader.Contains("W_" + sym.AddressHeader))
                    {
                        lstHeader.Add("W_" + sym.AddressHeader);
                        lstSub = new List<CDDEASymbol>();
                        lstSub = clstDDESymbol.FindAll(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.Word && b.IndexAddressNumber == -1 && b.IndexType != EMIndexTypeMS.IncludeAddress);
                        lstSub.Sort(new CSymbolComparer());
                        //List<CDDEASymbol> lstAddSymbol = InsertWordSymbol(lstSub);
                        dicBundleResult.WordSymbolS.AddSymbolList(lstSub);
                    }

                }
                else if (sym.DataType == EMDataType.DWord)
                {
                    if (!lstHeader.Contains("DW_" + sym.AddressHeader))
                    {
                        lstHeader.Add("DW_" + sym.AddressHeader);
                        lstSub = new List<CDDEASymbol>();
                        lstSub = clstDDESymbol.FindAll(b => b.AddressHeader == sym.AddressHeader && b.DataType == EMDataType.DWord);
                        lstSub.Sort(new CSymbolComparer());
                        List<CDDEASymbol> lstAddSymbol = InsertDWordSymbol(lstSub);
                        dicBundleResult.WordSymbolS.AddSymbolList(lstAddSymbol);
                    }
                }
            }

            return dicBundleResult;
        }

        #region Private Method


        private List<CDDEASymbolS> GetTagS(CDDEASymbolS cIndexSymbolS, CDDEASymbolS cWordSymbolS, List<CDDEASymbolS> lstBitSymbolS, List<int> lstWordCount)
        {
            int iMax = 94;
            int iSize = 0;

            List<CDDEASymbolS> lstResult = new List<CDDEASymbolS>();
            List<CDDEASymbolS> lstOverBitSymbolS = new List<CDDEASymbolS>();
            CDDEASymbolS c94TagS = new CDDEASymbolS();
            c94TagS.AddSymbolS(cIndexSymbolS);
            iSize = c94TagS.Count;
            int iLastCount = 0;

            //Bit를 94Word넣을 수 있는 한도까지 넣는다(94는 초과 해서는 안됨)
            for (int i = 0; i < lstBitSymbolS.Count; i++)
            {
                if (iSize + lstWordCount[i] <= iMax)
                {
                    c94TagS.AddSymbolS(lstBitSymbolS[i]);
                    iSize = GetWordSize(c94TagS);
                    iLastCount = i;
                }
                else
                    lstOverBitSymbolS.Add(lstBitSymbolS[i]);
            }
            if (iSize == iMax)
            {
                lstResult.Add(c94TagS);
                c94TagS = new CDDEASymbolS();
                iSize = 0;
            }
            //초기에 들어가지 못한 Bit TagS List를 삽입한다.
            if (lstOverBitSymbolS.Count > 0)
            {
                foreach (CDDEASymbolS tags in lstOverBitSymbolS)
                {
                    foreach (var who in tags)
                    {
                        c94TagS.AddSymbol(who.Value);
                        iSize++;
                        if (iSize == iMax)
                        {
                            iSize = GetWordSize(c94TagS);
                            if (iSize == iMax)
                            {
                                lstResult.Add(c94TagS);
                                c94TagS = new CDDEASymbolS();
                                iSize = 0;
                            }
                            else if (iSize > iMax)
                            {
                                c94TagS.Remove(who.Key);
                                lstResult.Add(c94TagS);
                                c94TagS = new CDDEASymbolS();
                                c94TagS.AddSymbol(who.Value);
                                iSize = 1;
                            }
                        }
                    }
                }
            }

            iSize = GetWordSize(c94TagS);
            //다 안찼으므로 Word를 하나씩 집어 넣는다.
            foreach (var who in cWordSymbolS)
            {
                if (iSize < iMax)
                {
                    c94TagS.AddSymbol(who.Value);
                    iSize++;
                }
                else if (iSize == iMax)
                {
                    iSize = 1;
                    lstResult.Add(c94TagS);
                    c94TagS = new CDDEASymbolS();
                    c94TagS.AddSymbol(who.Value);
                }
            }
            if (iSize > 0)
                lstResult.Add(c94TagS);

            return lstResult;
        }

        /// <summary>
        /// Word심볼에Dot가 있는 것에 한해서 32bit단위로 최적 묶기
        /// </summary>
        /// <param name="lstSource"></param>
        /// <returns></returns>
        private List<CDDEASymbol> InsertWordDotSymbol(List<CDDEASymbol> lstSource)
        {
            int iLeaderAddress = 0;
            List<int> lstSym = new List<int>();
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();
            List<CDDEASymbol> lstSub = null;
            foreach (CDDEASymbol sym in lstSource)
            {
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
        private List<CDDEASymbol> InsertBitSymbol(List<CDDEASymbol> lstSource, int iLimitCount)
        {
            int iLeaderAddress = 0;
            int iLastSymbolMajor = -1;
            int iK8Limit = iLimitCount - 32;
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();

            foreach (CDDEASymbol sym in lstSource)
            {
                if (iLastSymbolMajor >= sym.AddressMajor)
                    continue;

                bool bLimitOver = false;
                string sLastK8Address = "";
                int iLoofCount = 0;

                if (iK8Limit <= sym.AddressMajor)
                {
                    iLeaderAddress = iK8Limit;
                    bLimitOver = true;
                    if (CheckAddressHexa(sym.AddressHeader))
                    {
                        string sHexa = string.Format("{0:x}", sym.AddressMajor);
                        sLastK8Address = sym.AddressHeader + sHexa;
                    }
                }
                else
                    iLeaderAddress = sym.AddressMajor;

                foreach (CDDEASymbol sub in lstSource)
                {
                    if (((iLeaderAddress + 31) >= sub.AddressMajor) && (iLeaderAddress <= sub.AddressMajor))
                    {
                        int isum = sub.AddressMajor - iLeaderAddress;
                        sub.Mask = (UInt32)(0x01 << isum);
                        if (bLimitOver)
                            sub.BaseAddress = sLastK8Address;
                        else
                            sub.BaseAddress = sym.Address;
                        lstResult.Add(sub);
                        iLastSymbolMajor = sub.AddressMajor;
                    }
                    if (iLoofCount > 31)
                    {
                        break;
                    }
                    if (iLeaderAddress <= sub.AddressMajor)
                        iLoofCount++;
                }
            }
            return lstResult;
        }

        private bool CheckAddressHexa(string sHeader)
        {
            try
            {
                bool bHexa = false;

                string[] sHexaValue = { "X", "Y", "B", "W" };
                string[] sHexaTwoValue = { "SW", "SB", "DX", "DY" };
                for (int i = 0; i < sHexaValue.Length; i++)
                {
                    if (sHeader == sHexaValue[i])
                    {
                        bHexa = true;
                        break;
                    }
                }
                if (!bHexa)
                {
                    for (int i = 0; i < sHexaTwoValue.Length; i++)
                    {
                        if (sHeader == sHexaTwoValue[i])
                        {
                            bHexa = true;
                            break;
                        }
                    }
                }
                return bHexa;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + " [ " + System.Reflection.MethodBase.GetCurrentMethod().Name + " ] -> ", ex.InnerException);
            }
        }

        private List<CDDEASymbol> InsertIncludeIndexSymbol(List<CDDEASymbol> lstSymbol)
        {
            List<CDDEASymbol> lstSub = lstSymbol.FindAll(b => b.IndexAddressNumber != -1);
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();
            foreach (CDDEASymbol sym in lstSub)
            {
                sym.BaseAddress = sym.Address;
                lstResult.Add(sym);
            }
            return lstResult;
        }
        private List<CDDEASymbol> InsertIndexSymbol(List<CDDEASymbol> lstSymbol)
        {
            List<CDDEASymbol> lstSub = lstSymbol.FindAll(b => b.IndexAddressNumber != -1);
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();
            foreach (CDDEASymbol sym in lstSub)
            {
                //새로 생성
                CDDEASymbol cAddSymbol = AddIndexSymbol(sym);
                lstResult.Add(cAddSymbol);
            }
            return lstResult;
        }

        private CDDEASymbol AddIndexSymbol(CDDEASymbol cSymbol)
        {
            //새로 생성
            string sAddSymbolName = cSymbol.IndexHeader + cSymbol.IndexAddressNumber.ToString();
            string sKeyName = "[Created]" + sAddSymbolName;
            CDDEASymbol cAddSymbol = new CDDEASymbol(sKeyName, true);
            cAddSymbol.CreateMelsecDDEASymbol(sAddSymbolName);
            cAddSymbol.IndexType = EMIndexTypeMS.CreateIndex;

            return cAddSymbol;
        }

        /// <summary>
        /// DWord값일 경우 Dictionary에 삽입
        /// </summary>
        /// <param name="lstSource"></param>
        /// <returns></returns>
        private List<CDDEASymbol> InsertDWordSymbol(List<CDDEASymbol> lstSource)
        {
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();
            foreach (CDDEASymbol sym in lstSource)
            {
                sym.Mask = MaskValueExtraction(sym.BitCollectNumber);
                sym.BaseAddress = sym.Address;
                lstResult.Add(sym);
            }
            return lstResult;
        }

        private CDDEASymbolS InsertIndexSymbol(Dictionary<string, CDDEASymbol> dicSym)
        {
            CDDEASymbolS lstIndexSymS = new CDDEASymbolS();

            foreach (var who in dicSym)
            {
                lstIndexSymS.Add(who.Value.Address, who.Value);
            }
            return lstIndexSymS;
        }

        /// <summary>
        /// Word값일 경우 Dictionary에 삽입
        /// </summary>
        /// <param name="lstSource"></param>
        /// <returns></returns>
        private List<CDDEASymbol> InsertWordSymbol(List<CDDEASymbol> lstSource)
        {

            return lstSource;
        }


        /// <summary>
        /// DWord Mask 값 찾기 K8 == 0xFFFFFFFF(32Bit) K1 == 0xF
        /// </summary>
        /// <param name="sSource">ex)K8</param>
        /// <returns></returns>
        private UInt32 MaskValueExtraction(int sSource)
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

        private int GetWordCountFromWordDot(List<CDDEASymbol> lstSymbol)
        {
            int iCount = 0;
            int iLeaderAddress = 0;
            List<int> lstSym = new List<int>();
            List<CDDEASymbol> lstSub = null;

            foreach (CDDEASymbol sym in lstSymbol)
            {
                iLeaderAddress = sym.AddressMajor;
                if (!lstSym.Contains(iLeaderAddress))
                {
                    lstSym.Add(iLeaderAddress);
                    lstSub = new List<CDDEASymbol>();
                    lstSub = lstSymbol.FindAll(b => b.AddressMajor == sym.AddressMajor);
                    lstSub.Sort(new CSymbolMinorComparer());
                    if (lstSub.Count > 0)
                        iCount++;
                }
            }

            return iCount;
        }

        private int GetWordCountFromBit(List<CDDEASymbol> lstSymbol, int iLimitCount)
        {
            int iCount = 0;
            int iLeaderAddress = 0;
            int iLastSymbolMajor = -1;
            int iK8Limit = iLimitCount - 32;

            foreach (CDDEASymbol sym in lstSymbol)
            {
                if (iLastSymbolMajor >= sym.AddressMajor)
                    continue;

                int iLoofCount = 0;
                if (iK8Limit <= sym.AddressMajor)
                {
                    iLeaderAddress = iK8Limit;

                }
                else
                    iLeaderAddress = sym.AddressMajor;

                foreach (CDDEASymbol sub in lstSymbol)
                {
                    if (((iLeaderAddress + 31) >= sub.AddressMajor) && (iLeaderAddress <= sub.AddressMajor))
                    {
                        iLastSymbolMajor = sub.AddressMajor;
                    }
                    if (iLoofCount > 31)
                    {
                        break;
                    }
                    if (iLeaderAddress <= sub.AddressMajor)
                        iLoofCount++;
                }
                if (iLoofCount > 0)
                    iCount++;
            }
            return iCount;
        }

        private void CheckWordCount(CDDEABundle bundle)
        {
            int iBitWordCount = GetBitWordCount(bundle.BitSymbolS);
            int iWordCount = bundle.WordSymbolS.Count;
            int iIndexCount1 = bundle.IncludeIndexSymbolS.Count;
            int iIndexCount2 = GetIndexCount(bundle.WordSymbolS, bundle.IndexSymbolS);

            m_iTotalWordCount = iBitWordCount + iWordCount + iIndexCount1 + iIndexCount2;

        }

        private int GetBitWordCount(CDDEASymbolS cSymbolS)
        {
            int iCount = 0;
            List<string> lstHeadAddress = new List<string>();

            foreach (var who in cSymbolS)
            {
                CDDEASymbol cSymbol = who.Value;
                if (!lstHeadAddress.Contains(cSymbol.BaseAddress))
                {
                    lstHeadAddress.Add(cSymbol.BaseAddress);
                    iCount++;
                }
            }
            return iCount;
        }

        /// <summary>
        /// Word에 포함 되어 있는 Z Index접점의 갯수
        /// </summary>
        /// <param name="cWordSymbolS"></param>
        /// <param name="cIndexSymbolS"></param>
        /// <returns></returns>
        private int GetIndexCount(CDDEASymbolS cWordSymbolS, CDDEASymbolS cIndexSymbolS)
        {
            int iCount = 0;

            foreach (var who in cIndexSymbolS)
            {
                if (!cWordSymbolS.ContainsKey(who.Key))
                {
                    iCount++;
                }
            }

            return iCount;
        }


        private List<CDDEASymbol> GetCollectDDEASymbolList(CRefTagS cCollectTagS, CTagS cAllTagS)
        {
            List<CDDEASymbol> cSymbolS = new List<CDDEASymbol>();
            foreach (string sKey in cCollectTagS.KeyList)
            {
                if (cAllTagS.ContainsKey(sKey))
                {
                    cSymbolS.Add(new CDDEASymbol(cAllTagS[sKey]));
                }
            }
            return cSymbolS;
        }

        #endregion

        #endregion
    }
}
