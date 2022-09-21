// Decompiled with JetBrains decompiler
// Type: UDM.DDEA.CDDEAGroup
// Assembly: UDM.DDEA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21E65A2E-3C3B-4A61-8757-F69B2434ED5D
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.DDEA.DLL

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEACommon;
using UDM.General.Threading;
using UDM.Log;
using System.Linq;
using UDM.DDEA.Language;

namespace UDM.DDEA
{
    public class CDDEAGroup : CThreadWithQueBase<CDDEAPacketData>
    {
        protected CDDEAProject_V5 m_cProject;
        protected CDDEATask m_cTask = null;
        protected CDDEAConfigMS_V3 m_cConfig = null;
        protected List<CDDEAPacketData> m_cPacketDataS = new List<CDDEAPacketData>();
        protected List<CDDEAPacketData> m_cFragMasterPacketDataS = new List<CDDEAPacketData>();

        //필터 수집 후 필터링 된 PacketData
        protected List<CDDEAPacketData> m_cFilterPacketDataS = new List<CDDEAPacketData>();

        protected bool m_bMainMessageOut = false;
        protected bool m_bRefreshFlag = false;
        protected int m_iCycleNumber = 0;
        protected int m_iCycleBaseCount = 0;
        protected int m_iPacketNumber = 0;
        protected bool m_bFragCompFlag = false;
        protected bool m_bFragRecipeErrorFlag = false;

        protected EMCollectMode m_emCollectMode = EMCollectMode.Normal;

        protected bool m_bCycleStartLogFlag = false;
        protected Stopwatch m_swCycle = new Stopwatch();

        protected bool m_bCycleStart = false;
        protected bool m_bCycleTrigger = true;
        protected bool m_bCycleEnd = false;
        protected bool m_bCycleStartComplete = false;
        protected bool m_bCycleTimeOut = false;

        protected int m_iFragMasterCycle = 0;
        protected bool m_bFragMasterRecycle = false;
        protected bool m_bFragMasterCycleComp = false;
        protected int m_iFragMasterRecipe = -1;
        protected int m_iLastestRecipe = -1;
        protected object m_oLock = new object();

        protected bool m_bRecipeRestart = false;
        protected Dictionary<string, int> m_dicAddressLimit = null;

        protected bool m_bFilterNormalCompFlag = false;
        protected bool m_bFilterNormalCycleStarted = false;
        protected bool m_bFilterNormalCycleTriggered = false;
        protected bool m_bFilterNormalCycleValid = false;
        protected string m_sFilterNormalCycleTagKey = "";
        protected string m_sFilterNormalCycleStartKey = "";
        protected string m_sFilterNormalCycleTriggerKey = "";
        protected int m_iFilterNormalCycleMaxTime = 120000;
        protected int m_iFilterNormalCycleStartValue = 1;
        protected int m_iFilterNormalCycleTriggerValue = 1;
        protected bool m_bFilterNormalCycleTriggerOption = true;
        protected DateTime m_dtFilterNormalCycleStart = DateTime.MinValue;


        public event UEventHandlerCycleChanged UEventCycleChanged;
        public event UEventHandlerMainMessage UEventGroupMessage;
        public event UEventHandlerDDEGroupDataChanged UEventGroupTrackerData;
        public event UEventHandlerFragMasterSwitching UEventFragMasterSwitch;

        //jjk, 19.05.20 - 시간 생성시 다른 Thread 접근 제어
        protected Mutex m_Mutex = new Mutex();

        //yjk, 19.03.05 - 각 영역의 한계점
        private Dictionary<string, string> m_dictLimitAddressNumber = new Dictionary<string, string>();


        public CDDEAGroup(CDDEAProject_V5 cProject)
        {
            m_cProject = cProject;
            m_cConfig = cProject.Config_V3;

            if (cProject.CollectMode != EMCollectMode.Normal)
                m_bMainMessageOut = true;

            if (cProject == null)
                return;

            m_emCollectMode = cProject.CollectMode;
        }

        public List<CDDEAPacketData> PacketData
        {
            get
            {
                return m_cPacketDataS;
            }
        }

        public List<CDDEAPacketData> FragMasterPacketData
        {
            get
            {
                return m_cFragMasterPacketDataS;
            }
        }

        public List<CDDEAPacketData> FilterPacketData
        {
            get
            {
                return m_cFilterPacketDataS;
            }
        }

        public bool IsQueueClear
        {
            get
            {
                return m_cQue.Count == 0;
            }
        }

        public int QueueCount
        {
            get
            {
                return m_cQue.Count;
            }
        }

        public void ReadedDataEvent(CDDEAPacketData cData)
        {
            lock (m_oLock)
                EnQue(cData);
        }

        public void SetFragmentComplete(bool bCompFlag, bool bErrorFlag)
        {
            m_bFragCompFlag = bCompFlag;
            m_bFragRecipeErrorFlag = bErrorFlag;
        }

        public void SetFilterNormalComplete(bool bCompFlag)
        {
            m_bFilterNormalCompFlag = bCompFlag;
        }

        private string CreateReadAddress(CDDEASymbolS cSyms, int iPossibleSize)
        {
            string sResult = "";
            /*int iTagAddressSize = cSyms[0].AddressMajor;
            string sKHeader = ChangeReadBlockName(cSyms.MaskData);

            if (sKHeader != "")
            {
                sResult = sKHeader + cSyms[0].Address + "\n";
            }
            else
            {
                if (m_bMainProcess)
                {
                    string sMsg = string.Format("파라메터 : {0} = {1} Address : {2} 조합이 실패하였습니다.",
                        cSyms[0].AddressHeader, iPossibleSize, cSyms[0].Address);
                    SetEventMessage(sMsg);
                    SetEventMessage("ALL STOP");
                    m_bRun = false;
                }
            }*/
            return sResult;
        }

        private string ChangeReadBlockName(uint lMask)
        {
            string sResult = "";

            if (lMask <= 0xF)
                sResult = "K1";
            else if (lMask <= 0xFF)
                sResult = "K2";
            else if (lMask <= 0xFFF)
                sResult = "K3";
            else if (lMask <= 0xFFFF)
                sResult = "K4";
            else if (lMask <= 0xFFFFF)
                sResult = "K5";
            else if (lMask <= 0xFFFFFF)
                sResult = "K6";
            else if (lMask <= 0xFFFFFFF)
                sResult = "K7";
            else
                sResult = "";

            return sResult;
        }

        private void GetCycleSymbol(CDDEASymbolList cBitSymbolList, CDDEACycleSymbolS cCycleCondition)
        {
            CDDEASymbolList cCycleSymbolList = new CDDEASymbolList();
            foreach (CDDEACycleSymbol sym in cCycleCondition.StartCycleList)
            {
                List<CDDEASymbol> lstBitSym = cBitSymbolList.FindAll(b => b.Address == sym.Symbol.Address);
                if (lstBitSym.Count == 0)
                    cBitSymbolList.AddSymbol(sym.Symbol);
            }

            foreach (CDDEACycleSymbol sym in cCycleCondition.EndCycleList)
            {
                List<CDDEASymbol> lstBitSym = cBitSymbolList.FindAll(b => b.Address == sym.Symbol.Address);
                if (lstBitSym.Count == 0)
                    cBitSymbolList.AddSymbol(sym.Symbol);
            }
        }

        private CDDEASymbolS GetCycleSymbol(CDDEACycleSymbolS cCycleCondition)
        {
            CDDEASymbolS cCycleSymbolS = new CDDEASymbolS();

            foreach (CDDEACycleSymbol sym in cCycleCondition.StartCycleList)
            {
                if (cCycleSymbolS.ContainsKey(sym.Symbol.Key) == false)
                    cCycleSymbolS.AddSymbol(sym.Symbol);
            }
            foreach (CDDEACycleSymbol sym in cCycleCondition.EndCycleList)
            {
                if (cCycleSymbolS.ContainsKey(sym.Symbol.Key) == false)
                    cCycleSymbolS.AddSymbol(sym.Symbol);
            }

            return cCycleSymbolS;
        }

        /// <summary>
        /// LOB에서 사용
        /// Cycle 이 속한 HeadAddress만 제외하고 추출
        /// </summary>
        /// <param name="cBitSymbolS"></param>
        /// <param name="cCycleSymbolS"></param>
        /// <returns></returns>
        private void GetReadAddressBitSymbolS(CDDEASymbolList cBitSymbolList, CDDEACycleSymbolS cCycleCondition, int iPacketNo, bool bFragMaster)
        {
            if (bFragMaster == false)
            {
                if (m_cPacketDataS.Count < iPacketNo)
                    return;
            }
            else
            {
                if (m_cFragMasterPacketDataS.Count < iPacketNo)
                    return;
            }

            CDDEAPacketData cPacket = null;
            if (bFragMaster == false)
                cPacket = m_cPacketDataS[iPacketNo];
            else
                cPacket = m_cFragMasterPacketDataS[iPacketNo];

            string sPacketAllAddress = "";
            List<string> lstHeadAddress = new List<string>();

            //GetCycleSymbol(cBitSymbolList, cCycleCondition);

            foreach (CDDEASymbol sym in cBitSymbolList)
            {
                string sHeadAddress = sym.BaseAddress;

                if (cPacket.ReadDataList.ContainsKey(sHeadAddress))
                    continue;
                if (sym.AddressMinor == -1)
                {
                    if (!lstHeadAddress.Contains("K8" + sHeadAddress))
                    {
                        lstHeadAddress.Add("K8" + sHeadAddress);
                        sPacketAllAddress += "K8" + sHeadAddress + "\n";

                        CDDEAReadAddressData cData = new CDDEAReadAddressData();
                        cData.Address = sHeadAddress;
                        cData.AddressLength = sym.AddressCount;
                        cPacket.ReadDataList.Add(sHeadAddress, cData);
                    }
                }
                else
                {
                    if (!lstHeadAddress.Contains(sHeadAddress))
                    {
                        lstHeadAddress.Add(sHeadAddress);
                        sPacketAllAddress += sHeadAddress + "\n";

                        CDDEAReadAddressData cData = new CDDEAReadAddressData();
                        cData.Address = sHeadAddress;
                        cData.AddressLength = sym.AddressCount;
                        cPacket.ReadDataList.Add(sHeadAddress, cData);
                    }
                }
            }
            cPacket.PacketAddress += sPacketAllAddress;
            cPacket.PacketCount += lstHeadAddress.Count;

            return;
        }

        /// <summary>
        /// NormalMode에서 사용
        /// </summary>
        /// <param name="cBitSymbolS"></param>
        /// <param name="cCycleSymbolS"></param>
        /// <returns></returns>
        private void GetReadAddressBitSymbolS(CDDEASymbolList cBitSymbolList, int iPacketNo)
        {
            if (m_cPacketDataS.Count < iPacketNo)
                return;

            //yjk, 19.03.05 - Header의 최대 영역값 저장
            foreach (CDDEASymbol sym in cBitSymbolList)
            {
                if (sym.AddressMinor == -1)
                {
                    if (!m_dictLimitAddressNumber.ContainsKey(sym.AddressHeader))
                    {
                        m_dictLimitAddressNumber.Add(sym.AddressHeader, sym.AddressHeadRemainder);
                    }
                    else
                    {
                        string sComp = m_dictLimitAddressNumber[sym.AddressHeader];

                        //크기 비교하여 큰 값으로 할당
                        if (CheckAddressHexa(sym.AddressHeader))
                        {
                            int iCur = int.Parse(sComp, System.Globalization.NumberStyles.HexNumber);
                            int iTar = int.Parse(sym.AddressHeadRemainder, System.Globalization.NumberStyles.HexNumber);

                            if (iTar > iCur)
                                m_dictLimitAddressNumber[sym.AddressHeader] = sym.AddressHeadRemainder;
                        }
                        else
                        {
                            int iCur = int.Parse(sComp);
                            int iTar = int.Parse(sym.AddressHeadRemainder);

                            if (iTar > iCur)
                                m_dictLimitAddressNumber[sym.AddressHeader] = sym.AddressHeadRemainder;
                        }
                    }
                }
            }

            CDDEAPacketData cPacket = m_cPacketDataS[iPacketNo];
            string sPacketAllAddress = "";
            List<string> lstHeadAddress = new List<string>();

            //yjk, 19.03.05 - 수집 영역 내에 포함되어 있는지 여부
            bool bInclude = false;

            foreach (CDDEASymbol sym in cBitSymbolList)
            {
                string sHeadAddress = sym.BaseAddress;

                if (sym.AddressMinor == -1)
                {
                    //yjk, 19.03.05 - [32Bit(K8) + BaseAddress Bit]와 수집 접점의 Header Limit 값에 넘지 않는지 계산하여 Limit 아래로 sHeadAddress 설정
                    string sBaseAddressNum = sym.BaseAddress.Remove(0, sym.AddressHeader.Length);

                    if (CheckAddressHexa(sym.AddressHeader))
                    {
                        //K8 = 32Bit
                        int iBaseNum = int.Parse(sBaseAddressNum, System.Globalization.NumberStyles.HexNumber);
                        int iBaseMaxArea = iBaseNum + 31;
                        int iLimit = int.Parse(m_dictLimitAddressNumber[sym.AddressHeader], System.Globalization.NumberStyles.HexNumber);
                        int iMySelfNum = int.Parse(sym.AddressHeadRemainder, System.Globalization.NumberStyles.HexNumber);

                        if (iBaseMaxArea > iLimit)
                        {
                            int iNewBaseNum = iLimit - 31;

                            //yjk, 19.05.07 - 31을 뺀 값이 음수인 경우 BaseAddress는 0
                            if (iNewBaseNum < 0)
                                iNewBaseNum = 0;

                            //Limit 값 보다 큰 경우 그 차이만큼 빼서 BaseAddress로 지정해줌
                            sHeadAddress = sym.AddressHeader + iNewBaseNum.ToString("X4");   //To Hex
                            sym.BaseAddress = sHeadAddress;

                            //Mask(읽을 bit 지점) 계산
                            int iMask = iMySelfNum - iNewBaseNum;
                            sym.Mask = (UInt32)(0x01 << iMask);
                        }
                        else
                        {
                            //수집 영역 내에 포함이 되어 있는지 확인하여 없다면 영역 Address를 새로 만듬
                            List<string> lstSameAddressHeader = lstHeadAddress.Where(x => x.StartsWith("K8" + sym.AddressHeader)).ToList();
                            if (lstSameAddressHeader != null && lstSameAddressHeader.Count > 0)
                            {
                                foreach (string address in lstSameAddressHeader)
                                {
                                    string sMajor = address.Remove(0, sym.AddressHeader.Length + 2);
                                    int iStart = int.Parse(sMajor, System.Globalization.NumberStyles.HexNumber);
                                    int iEnd = int.Parse(sMajor, System.Globalization.NumberStyles.HexNumber) + 31;
                                    int iComp = int.Parse(sBaseAddressNum, System.Globalization.NumberStyles.HexNumber);

                                    if (iStart <= iComp && iEnd >= iComp)
                                    {
                                        bInclude = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //K8 = 32Bit
                        int iBaseNum = int.Parse(sBaseAddressNum);
                        int iBaseMaxArea = iBaseNum + 31;
                        int iLimit = int.Parse(m_dictLimitAddressNumber[sym.AddressHeader]);
                        int iMySelfNum = int.Parse(sym.AddressHeadRemainder);

                        //Limit 값 보다 큰 경우 그 차이만큼 빼서 BaseAddress로 지정해줌
                        if (iBaseMaxArea > iLimit)
                        {
                            int iNewBaseNum = iLimit - 31;

                            //yjk, 19.05.07 - 31을 뺀 값이 음수인 경우 BaseAddress는 0
                            if (iNewBaseNum < 0)
                                iNewBaseNum = 0;

                            sHeadAddress = sym.AddressHeader + iNewBaseNum.ToString();
                            sym.BaseAddress = sHeadAddress;

                            //Mask(읽을 bit 지점) 계산
                            int iMask = iMySelfNum - iNewBaseNum;
                            sym.Mask = (UInt32)(0x01 << iMask);
                        }
                        else
                        {
                            //수집 영역 내에 포함이 되어 있는지 확인하여 없다면 영역 Address를 새로 만듬
                            List<string> lstSameAddressHeader = lstHeadAddress.Where(x => x.StartsWith("K8" + sym.AddressHeader)).ToList();
                            if (lstSameAddressHeader != null && lstSameAddressHeader.Count > 0)
                            {
                                foreach (string address in lstSameAddressHeader)
                                {
                                    string sMajor = address.Remove(0, sym.AddressHeader.Length + 2);
                                    int iStart = int.Parse(sMajor);
                                    int iEnd = int.Parse(sMajor) + 31;
                                    int iComp = int.Parse(sBaseAddressNum);

                                    if (iStart <= iComp && iEnd >= iComp)
                                    {
                                        bInclude = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (!bInclude && !lstHeadAddress.Contains("K8" + sHeadAddress))
                    {
                        lstHeadAddress.Add("K8" + sHeadAddress);
                        sPacketAllAddress += "K8" + sHeadAddress + "\n";

                        CDDEAReadAddressData cData = new CDDEAReadAddressData();
                        cData.Address = sHeadAddress;
                        cData.AddressLength = sym.AddressCount;
                        cPacket.ReadDataList.Add(sHeadAddress, cData);
                    }

                    bInclude = false;
                }
                else
                {
                    if (!lstHeadAddress.Contains(sHeadAddress))
                    {
                        lstHeadAddress.Add(sHeadAddress);
                        sPacketAllAddress += sHeadAddress + "\n";

                        CDDEAReadAddressData cData = new CDDEAReadAddressData();
                        cData.Address = sHeadAddress;
                        cData.AddressLength = sym.AddressCount;
                        cPacket.ReadDataList.Add(sHeadAddress, cData);
                    }
                }
            }

            cPacket.PacketAddress += sPacketAllAddress;
            cPacket.PacketCount += lstHeadAddress.Count;

            return;
        }

        /// <summary>
        /// LOB Mode에서 사용
        /// </summary>
        /// <param name="cBitSymbolS"></param>
        /// <param name="cCycleSymbolS"></param>
        /// <returns></returns>
        private void GetReadAddressBitSymbolS(CDDEASymbolList cBitSymbolList, CDDEASymbol cRefreshSymbol, int iPacketNo)
        {
            if (m_cPacketDataS.Count < iPacketNo)
                return;
            
            CDDEAPacketData cPacket = m_cPacketDataS[iPacketNo];
            string sPacketAllAddress = "";
            List<string> lstHeadAddress = new List<string>();

            foreach (CDDEASymbol sym in cBitSymbolList)
            {
                string sHeadAddress = sym.BaseAddress;

                if (sym.AddressMinor == -1)
                {
                    if (!lstHeadAddress.Contains("K8" + sHeadAddress))
                    {
                        lstHeadAddress.Add("K8" + sHeadAddress);
                        sPacketAllAddress += "K8" + sHeadAddress + "\n";

                        CDDEAReadAddressData cData = new CDDEAReadAddressData();
                        cData.Address = sHeadAddress;
                        cData.AddressLength = sym.AddressCount;
                        cPacket.ReadDataList.Add(sHeadAddress, cData);
                    }
                }
                else
                {
                    if (!lstHeadAddress.Contains(sHeadAddress))
                    {
                        lstHeadAddress.Add(sHeadAddress);
                        sPacketAllAddress += sHeadAddress + "\n";

                        CDDEAReadAddressData cData = new CDDEAReadAddressData();
                        cData.Address = sHeadAddress;
                        cData.AddressLength = sym.AddressCount;
                        cPacket.ReadDataList.Add(sHeadAddress, cData);
                    }
                }
            }

            if (cRefreshSymbol != null)
            {
                if (cPacket.ReadDataList.ContainsKey(cRefreshSymbol.BaseAddress) == false)
                {
                    string sHeadAddress = cRefreshSymbol.BaseAddress;
                    if (cRefreshSymbol.AddressMinor == -1)
                    {
                        if (!lstHeadAddress.Contains("K8" + sHeadAddress))
                        {
                            lstHeadAddress.Add("K8" + sHeadAddress);
                            sPacketAllAddress += "K8" + sHeadAddress + "\n";

                            CDDEAReadAddressData cData = new CDDEAReadAddressData();
                            cData.Address = sHeadAddress;
                            cData.AddressLength = cRefreshSymbol.AddressCount;
                            cPacket.ReadDataList.Add(sHeadAddress, cData);
                        }
                    }
                    else
                    {
                        if (!lstHeadAddress.Contains(sHeadAddress))
                        {
                            lstHeadAddress.Add(sHeadAddress);
                            sPacketAllAddress += sHeadAddress + "\n";

                            CDDEAReadAddressData cData = new CDDEAReadAddressData();
                            cData.Address = sHeadAddress;
                            cData.AddressLength = cRefreshSymbol.AddressCount;
                            cPacket.ReadDataList.Add(sHeadAddress, cData);
                        }
                    }
                }
            }
            cPacket.PacketAddress += sPacketAllAddress;
            cPacket.PacketCount += lstHeadAddress.Count;

            return;
        }

        //yjk, 19.06.22 - Hexa Check 
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

        /// <summary>
        /// Index, Recipe제외한 나머지
        /// </summary>
        /// <param name="cWordSymbolS"></param>
        /// <param name="cRecipeSymbolS"></param>
        /// <param name="cIndexSymbolS"></param>
        /// <returns></returns>
        private void GetReadAddressWordSymbolS(CDDEASymbolList cWordSymbolList, CDDEASymbolList cRecipeSymbolList, CDDEASymbolList cIndexSymbolList, int iPacketNo)
        {
            if (m_cPacketDataS.Count < iPacketNo)
                return;

            List<string> lstHeadAddress = new List<string>();
            List<string> lstRecipeAddress = new List<string>();
            List<string> lstIndexAddress = new List<string>();
            CDDEAPacketData cPacket = m_cPacketDataS[iPacketNo];
            string sPacketAllAddress = "";

            foreach (CDDEASymbol sym in cRecipeSymbolList)
                lstRecipeAddress.Add(sym.BaseAddress);

            foreach (CDDEASymbol sym in cIndexSymbolList)
                lstIndexAddress.Add(sym.BaseAddress);

            foreach (CDDEASymbol sym in cWordSymbolList)
            {
                string sHeadAddress = sym.Address;
                if (lstRecipeAddress.Contains(sHeadAddress))
                    continue;

                if (lstIndexAddress.Contains(sHeadAddress))
                    continue;

                if (cPacket.ReadDataList.ContainsKey(sHeadAddress))
                    continue;

                if (sym.AddressHeader == "T") // ijsong 08.24 Timer ReadType 
                {
                    if (m_cConfig.TimerReadType != EMTimerReadType.TN)
                    {
                        sHeadAddress = sHeadAddress.Replace(sym.AddressHeader, m_cConfig.TimerReadType.ToString());
                    }
                }

                lstHeadAddress.Add(sHeadAddress);
                sPacketAllAddress += sHeadAddress + "\n";

                CDDEAReadAddressData cData = new CDDEAReadAddressData();
                cData.Address = sHeadAddress;
                cData.AddressLength = sym.AddressCount;
                cPacket.ReadDataList.Add(sHeadAddress, cData);
            }

            cPacket.PacketAddress += sPacketAllAddress;
            cPacket.PacketCount += lstHeadAddress.Count;

            m_cPacketDataS[iPacketNo] = cPacket;
        }

        /// <summary>
        /// NormalMode에서 사용
        /// Index 제외한 나머지
        /// </summary>
        /// <param name="cWordSymbolS"></param>
        /// <param name="cRecipeSymbolS"></param>
        /// <param name="cIndexSymbolS"></param>
        /// <returns></returns>
        private void GetReadAddressWordSymbolS(CDDEASymbolList cWordSymbolList, CDDEASymbolList cIndexSymbolList, int iPacketNo)
        {
            if (m_cPacketDataS.Count < iPacketNo) return;

            List<string> lstHeadAddress = new List<string>();
            List<string> lstIndexAddress = new List<string>();
            CDDEAPacketData cPacket = m_cPacketDataS[iPacketNo];
            string sPacketAllAddress = "";

            foreach (CDDEASymbol sym in cIndexSymbolList)
            {
                lstIndexAddress.Add(sym.BaseAddress);
            }

            foreach (CDDEASymbol sym in cWordSymbolList)
            {
                string sHeadAddress = sym.Address;

                if (lstIndexAddress.Contains(sHeadAddress))
                    continue;

                if (cPacket.ReadDataList.ContainsKey(sHeadAddress))
                    continue;

                if (sym.AddressHeader == "T") // ijsong 08.24 Timer ReadType 
                {
                    if (m_cConfig.TimerReadType != EMTimerReadType.TN)
                    {
                        sHeadAddress = sHeadAddress.Replace(sym.AddressHeader, m_cConfig.TimerReadType.ToString());
                    }
                }

                lstHeadAddress.Add(sHeadAddress);
                sPacketAllAddress += sHeadAddress + "\n";

                CDDEAReadAddressData cData = new CDDEAReadAddressData();
                cData.Address = sHeadAddress;
                cData.AddressLength = sym.AddressCount;
                cPacket.ReadDataList.Add(sHeadAddress, cData);
            }

            cPacket.PacketAddress += sPacketAllAddress;
            cPacket.PacketCount += lstHeadAddress.Count;

            m_cPacketDataS[iPacketNo] = cPacket;
        }

        private void GetReadAddressSymbolS(CDDEASymbolList cSymbolList, bool bBit, int iPacketNo, bool bFragMaster)
        {
            if (bFragMaster == false)
            {
                if (m_cPacketDataS.Count < iPacketNo) return;
            }
            else
            {
                if (m_cFragMasterPacketDataS.Count < iPacketNo) return;
            }

            List<string> lstHeadAddress = new List<string>();
            CDDEAPacketData cPacket = null;
            if (bFragMaster == false)
                cPacket = m_cPacketDataS[iPacketNo];
            else
                cPacket = m_cFragMasterPacketDataS[iPacketNo];
            string sPacketAllAddress = "";

            foreach (CDDEASymbol sym in cSymbolList)
            {
                string sHeadAddress = "";
                if (bBit)
                    sHeadAddress = "K8" + sym.BaseAddress;
                else
                    sHeadAddress = sym.BaseAddress;

                if (cPacket.ReadDataList.ContainsKey(sHeadAddress))
                    continue;

                lstHeadAddress.Add(sHeadAddress);
                sPacketAllAddress += sHeadAddress + "\n";

                CDDEAReadAddressData cData = new CDDEAReadAddressData();
                cData.Address = sHeadAddress;
                cData.AddressLength = sym.AddressCount;
                cPacket.ReadDataList.Add(sHeadAddress, cData);

            }
            cPacket.PacketAddress += sPacketAllAddress;
            cPacket.PacketCount += lstHeadAddress.Count;

            return;
        }

        private void GetReadAddressMDCSymbolS(CLOBMode cLOBMode, int iPacketNo)
        {
            if (m_cPacketDataS.Count < iPacketNo) return;

            List<string> lstHeadAddress = new List<string>();
            CDDEAPacketData cPacket = m_cPacketDataS[iPacketNo];
            string sPacketAllAddress = "";

            foreach (CDDEAMdcSymbol mdc in cLOBMode.MDCSymbolList)
            {
                string sHeadAddress = "";
                sHeadAddress = mdc.BaseAddress;

                if (cPacket.ReadDataList.ContainsKey(sHeadAddress))
                    continue;

                if (lstHeadAddress.Contains(sHeadAddress))
                    continue;

                lstHeadAddress.Add(sHeadAddress);
                sPacketAllAddress += sHeadAddress + "\n";

                CDDEAReadAddressData cData = new CDDEAReadAddressData();
                cData.Address = sHeadAddress;
                cData.AddressLength = mdc.AddressCount;
                cPacket.ReadDataList.Add(sHeadAddress, cData);
            }
            cPacket.PacketAddress += sPacketAllAddress;
            cPacket.PacketCount += lstHeadAddress.Count;

            m_cPacketDataS[iPacketNo] = cPacket;

            return;
        }

        private void GetReadAddressSymbol(CDDEASymbol cSymbol, bool bBit, int iPacketNo)
        {
            if (m_cPacketDataS.Count < iPacketNo || cSymbol == null)
                return;
            List<string> stringList = new List<string>();
            CDDEAPacketData cddeaPacketData = m_cPacketDataS[iPacketNo];
            string str1 = "";
            string key = !bBit ? cSymbol.BaseAddress : "K8" + cSymbol.BaseAddress;
            if (cddeaPacketData.ReadDataList.ContainsKey(key))
                return;
            stringList.Add(key);
            string str2 = str1 + key + "\n";
            cddeaPacketData.ReadDataList.Add(key, new CDDEAReadAddressData()
            {
                Address = key,
                AddressLength = cSymbol.AddressCount
            });
            cddeaPacketData.PacketAddress += str2;
            cddeaPacketData.PacketCount += stringList.Count;
            m_cPacketDataS[iPacketNo] = cddeaPacketData;
        }



        private void GetReadFilterAddressSymbolS(CDDEASymbolS cSymbolS)
        {
            int iMaxCount = 94;

            if (m_cFilterPacketDataS.Count != 0)
                m_cFilterPacketDataS.Clear();

            List<string> lstHeadAddress = new List<string>();
            CDDEAPacketData cPacket = new CDDEAPacketData();

            string sFilterAllAddress = "";
            int iCount = 0;

            foreach (var who in cSymbolS)
            {
                string sHeadAddress = "";
                if (who.Value.DataType == EMDataType.Bool)
                    sHeadAddress = "K8" + who.Value.BaseAddress;
                else
                    sHeadAddress = who.Value.BaseAddress;

                if (iMaxCount == iCount)
                {
                    cPacket.PacketAddress = sFilterAllAddress;
                    cPacket.PacketCount = iCount;
                    sFilterAllAddress = "";
                    iCount = 0;

                    m_cFilterPacketDataS.Add(cPacket);
                    cPacket = new CDDEAPacketData();
                }
                else
                {
                    if (lstHeadAddress.Contains(who.Value.BaseAddress))
                        continue;
                    lstHeadAddress.Add(who.Value.BaseAddress);

                    sFilterAllAddress += sHeadAddress + "\n";
                    iCount++;

                    CDDEAReadAddressData cData = new CDDEAReadAddressData();
                    cData.Address = sHeadAddress;
                    cData.AddressLength = who.Value.AddressCount;
                    cPacket.ReadDataList.Add(sHeadAddress, cData);
                }

            }
            if (iCount > 0)
            {
                cPacket.PacketAddress = sFilterAllAddress;
                cPacket.PacketCount = iCount;

                m_cFilterPacketDataS.Add(cPacket);
            }
            return;
        }

        private void GetParameter()
        {
            CReadFunction cReadFun = new CReadFunction((CDDEAConfigMS_V3)m_cConfig);
            m_dicAddressLimit = cReadFun.ReadParameterSymbolSize();
        }

        private void ExtractAddressFromBundle(CDDEASymbolList cRecipeSymbolList, CFragMode cFragMode, int iBlockNum)
        {
            CDDEAPacketData cPacket = new CDDEAPacketData();
            m_cPacketDataS.Add(cPacket);

            GetReadAddressSymbolS(cRecipeSymbolList, false, iBlockNum, false);
            GetReadAddressSymbolS(cFragMode.IncludeIndexSymbolList, false, iBlockNum, false);
            GetReadAddressSymbolS(cFragMode.IndexSymbolList, false, iBlockNum, false);
            GetReadAddressWordSymbolS(cFragMode.WordSymbolList, cRecipeSymbolList, cFragMode.IndexSymbolList, iBlockNum);
            GetReadAddressBitSymbolS(cFragMode.BitSymbolList, cFragMode.CycleConditionSymbolS, iBlockNum, false);

            //GetReadFilterAddressSymbolS(cBundle.FilterSymbolS);
        }

        private void ExtractAddressFromBundle(CDDEASymbolList cRecipeSymbolList, CFragMasterMode cFragMaster, int iBlockNum)
        {
            CDDEAPacketData cPacket = new CDDEAPacketData();
            m_cFragMasterPacketDataS.Add(cPacket);

            GetReadAddressSymbolS(cRecipeSymbolList, false, iBlockNum, true);
            GetReadAddressBitSymbolS(cFragMaster.BitSymbolList, cFragMaster.CycleConditionSymbolS, iBlockNum, true);
        }

        /// <summary>
        /// Normal Mode
        /// </summary>
        /// <param name="cNormalMode"></param>
        /// <param name="iBlockNum"></param>
        private void ExtractAddressFromBundle(CNormalMode cNormalMode, int iBlockNum)
        {
            CDDEAPacketData cPacket = new CDDEAPacketData();
            m_cPacketDataS.Add(cPacket);

            GetReadAddressBitSymbolS(cNormalMode.BitSymbolList, iBlockNum);
            GetReadAddressWordSymbolS(cNormalMode.WordSymbolList, cNormalMode.IndexSymbolList, iBlockNum);
            GetReadAddressSymbolS(cNormalMode.IncludeIndexSymbolList, false, iBlockNum, false);
            GetReadAddressSymbolS(cNormalMode.IndexSymbolList, false, iBlockNum, false);

        }

        private void ExtractAddressFromBundle(CLOBMode cLOBMode, int iBlockNum)
        {
            m_cPacketDataS.Add(new CDDEAPacketData());
            GetReadAddressBitSymbolS(cLOBMode.BitSymbolList, cLOBMode.RefreshSymbol, iBlockNum);
            GetReadAddressWordSymbolS(cLOBMode.WordSymbolList, new CDDEASymbolList(), iBlockNum);
            GetReadAddressSymbolS(cLOBMode.GlassIDSymbolList, false, iBlockNum, false);
            GetReadAddressSymbolS(cLOBMode.ProcessIDSymbolList, false, iBlockNum, false);
            GetReadAddressSymbol(cLOBMode.ProcessSymbol, false, iBlockNum);
            GetReadAddressSymbol(cLOBMode.TactTimeSymbol, false, iBlockNum);
            GetReadAddressSymbolS(m_cProject.RecipeSymbolList, false, iBlockNum, false);
            GetReadAddressMDCSymbolS(cLOBMode, iBlockNum);
        }

        private bool BrforeRunCollectMode()
        {
            try
            {
                int iWordSize = 0;
                if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil)
                {
                    SetEventMessage("", ResDDEA.CDDEAGroup_EMCollctModeFrag_Msg1 + m_cProject.FragBundleList.Count.ToString());

                    for (int i = 0; i < m_cProject.FragBundleList.Count; ++i)
                    {
                        ExtractAddressFromBundle(m_cProject.RecipeSymbolList, m_cProject.FragBundleList[i], i);
                        iWordSize += m_cPacketDataS[i].PacketCount;
                    }

                    SetEventMessage("", ResDDEA.CDDEAGroup_Info + string.Format(ResDDEA.CDDEAGroup_EMCollctModeFrag_Format1, iWordSize));

                    if (m_emCollectMode == EMCollectMode.Frag && m_cProject.FragMasterBundleList.Count > 0)
                    {
                        iWordSize = 0;
                        for (int i = 0; i < m_cProject.FragMasterBundleList.Count; ++i)
                        {
                            ExtractAddressFromBundle(m_cProject.RecipeSymbolList, m_cProject.FragMasterBundleList[i], i);
                            iWordSize += m_cFragMasterPacketDataS[i].PacketCount;
                        }

                        SetEventMessage("", ResDDEA.CDDEAGroup_Info + string.Format(ResDDEA.CDDEAGroup_EMCollctModeFrag_Format2, iWordSize));
                    }
                }
                //yjk, 20.02.12 - 파라미터 수집 조건 추가(파라미터 수집은 부분 수집과 동일한 방식이기 때문에 NormalBundleList를 같이 사용함)
                else if (m_emCollectMode == EMCollectMode.Normal || m_emCollectMode == EMCollectMode.ParameterNormal)
                {
                    string sMode = ResDDEA.CDDEAGroup_EMCollectModeNormal_Msg;

                    if (m_emCollectMode == EMCollectMode.ParameterNormal)
                        sMode = ResDDEA.CDDEAGroup_EMCollectmodeParameterNormal_Msg;

                    SetEventMessage("", ResDDEA.CDDEAGroup_Info + sMode + ResDDEA.CDDEAGroup_NormalBundleListCount_Msg + m_cProject.NormalBundleList.Count.ToString());

                    for (int i = 0; i < m_cProject.NormalBundleList.Count; ++i)
                    {
                        ExtractAddressFromBundle(m_cProject.NormalBundleList[i], i);
                        iWordSize += m_cPacketDataS[i].PacketCount;

                        string sAddressList = "";
                        for (int j = 0; j < m_cProject.NormalBundleList[i].BitSymbolList.Count; ++j)
                            sAddressList = sAddressList + m_cProject.NormalBundleList[i].BitSymbolList[j].Address + ", ";

                        if (!string.IsNullOrEmpty(sAddressList))
                            SetEventMessage("SubDataView", ResDDEA.CDDEAGroup_PackerInfo_Msg1+ i.ToString() + " : " + sAddressList);

                        sAddressList = "";
                        for (int j = 0; j < m_cProject.NormalBundleList[i].WordSymbolList.Count; ++j)
                            sAddressList = sAddressList + m_cProject.NormalBundleList[i].WordSymbolList[j].Address + ", ";

                        if (!string.IsNullOrEmpty(sAddressList))
                            SetEventMessage("SubDataView", ResDDEA.CDDEAGroup_PackerInfo_Msg1 + i.ToString() + " : " + sAddressList);
                    }

                    for (int i = 0; i < 20; ++i)
                    {
                        Application.DoEvents();
                        Thread.Sleep(100);
                    }

                    SetEventMessage("", ResDDEA.CDDEAGroup_Info + string.Format(ResDDEA.CDDEAGroup_DoEvents_Format1, iWordSize, sMode));

                    if (m_cFilterPacketDataS.Count > 0)
                    {
                        iWordSize = 0;

                        for (int i = 0; i < m_cFilterPacketDataS.Count; ++i)
                            iWordSize += m_cFilterPacketDataS[i].PacketCount;

                        SetEventMessage("", ResDDEA.CDDEAGroup_Info + string.Format(ResDDEA.CDDEAGroup_DoEvents_Format2, iWordSize));
                    }
                }
                //yjk, 18.10.05 - 필터 수집 전용 패킷을 만듦
                else if (m_emCollectMode == EMCollectMode.FilterNormal)
                {
                    SetEventMessage("", ResDDEA.CDDEAGroup_FilterNormalBundleListCount_Msg + m_cProject.FilterNormalBundleList.Count.ToString());

                    for (int i = 0; i < m_cProject.FilterNormalBundleList.Count; ++i)
                    {
                        ExtractAddressFromBundle(m_cProject.FilterNormalBundleList[i], i);
                        iWordSize += m_cPacketDataS[i].PacketCount;

                        string sAddressList = "";
                        for (int j = 0; j < m_cProject.FilterNormalBundleList[i].BitSymbolList.Count; ++j)
                            sAddressList = sAddressList + m_cProject.FilterNormalBundleList[i].BitSymbolList[j].Address + ", ";

                        if (!string.IsNullOrEmpty(sAddressList))
                            SetEventMessage("SubDataView", ResDDEA.CDDEAGroup_PackerInfo_Msg1 + i.ToString() + " : " + sAddressList);

                        sAddressList = "";
                        for (int j = 0; j < m_cProject.FilterNormalBundleList[i].WordSymbolList.Count; ++j)
                            sAddressList = sAddressList + m_cProject.FilterNormalBundleList[i].WordSymbolList[j].Address + ", ";

                        if (!string.IsNullOrEmpty(sAddressList))
                            SetEventMessage("SubDataView", ResDDEA.CDDEAGroup_PackerInfo_Msg1 + i.ToString() + " : " + sAddressList);
                    }

                    for (int i = 0; i < 20; ++i)
                    {
                        Application.DoEvents();
                        Thread.Sleep(100);
                    }

                    SetEventMessage("", ResDDEA.CDDEAGroup_Info + string.Format(ResDDEA.CDDEAGroup_DoEvents_Format3, iWordSize, m_emCollectMode.ToString()));

                    if (m_cFilterPacketDataS.Count > 0)
                    {
                        iWordSize = 0;

                        for (int i = 0; i < m_cFilterPacketDataS.Count; ++i)
                            iWordSize += m_cFilterPacketDataS[i].PacketCount;

                        SetEventMessage("", ResDDEA.CDDEAGroup_Info + string.Format(ResDDEA.CDDEAGroup_DoEvents_Format4, iWordSize));
                    }
                }
                else if (m_emCollectMode == EMCollectMode.LOB)
                {
                    SetEventMessage("", ResDDEA.CDDEAGroup_LOBBundleListCount_Msg + m_cProject.LOBBundleList.Count.ToString());

                    for (int i = 0; i < m_cProject.LOBBundleList.Count; ++i)
                    {
                        ExtractAddressFromBundle(m_cProject.LOBBundleList[i], i);
                        iWordSize += m_cPacketDataS[i].PacketCount;
                    }

                    SetEventMessage("", ResDDEA.CDDEAGroup_Info + string.Format(ResDDEA.CDDEAGroup_EMCollectModeLOB_Format1, iWordSize));
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("BrforeRun", ResDDEA.CDDEAGroup_BrforeRunCollectMode_Exception_Msg + ex.Message + ")");
            }
            return true;
        }

        #region 일반함수

        public CTagS ChangeFromListTagToTagS(List<CTag> lstTag)
        {
            CTagS cTagS = new CTagS();

            foreach (CTag tag in lstTag)
            {
                cTagS.Add(tag.Key, tag);
            }

            return cTagS;
        }

        public List<CTag> ChangeFromTagSToTagList(CTagS cTagS)
        {
            List<CTag> lstTag = new List<CTag>();

            foreach (var who in cTagS)
            {
                lstTag.Add(who.Value);
            }

            return lstTag;
        }

        public List<string> ChangeFromTagSToAddressList(CTagS cTagS)
        {
            List<string> lstResult = new List<string>();

            foreach (var who in cTagS)
            {
                CTag cTag = who.Value;
                lstResult.Add(cTag.Address);
            }

            return lstResult;
        }

        protected void SetEventMessage(string sSender, string sMessage)
        {
            if (UEventGroupMessage != null)
            {
                if (sSender == "")
                    UEventGroupMessage(this, "DDEAGroup", sMessage);
                else
                    UEventGroupMessage(this, sSender, sMessage);
            }
        }

        protected void SendTrackerData(CTimeLogS cLogS)
        {
            if (m_cProject.ConnectApp == EMConnectAppType.Tracker)
            {
                if (UEventGroupTrackerData != null)
                {
                    UEventGroupTrackerData(this, cLogS);
                }
            }
        }

        protected bool CheckValue(int[] aiNow, int[] aiOld)
        {
            bool bFind = false;
            for (int i = 0; i < aiNow.Length; i++)
            {
                if (aiNow[i] != aiOld[i])
                    bFind = true;

            }
            if (bFind) return false;
            return true;
        }

        #endregion


        #region TimeLog생성

        protected CTimeLog GetTimeLog(string sKey, DateTime dtTime, int iValue)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = dtTime;

            return cLog;
        }

        protected CTimeLog GetTimeLog(CDDEASymbol cSymbol, CDDEAReadAddressData cData, DateTime dtTime)
        {
            int iarrVal = cData.Value;
            CTimeLog cLog = new CTimeLog();
            cLog.Key = cSymbol.Key;
            cLog.Value = iarrVal;
            cLog.Time = dtTime;

            return cLog;
        }

        //kch@udmtek, 17.02.01, int iPacketIndex, int iCycleIndex 추가
        protected CTimeLog GetNormalTimeLog(string sKey, DateTime dtTime, int iValue, int iPacketIndex, int iCycleIndex)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = dtTime;
            cLog.RisingTime = dtTime;
            cLog.PacketIndex = iPacketIndex;
            cLog.CycleIndex = iCycleIndex;

            return cLog;
        }

        //kch@udmtek, 17.02.01, int iPacketIndex, int iCycleIndex 추가
        protected CTimeLog GetNormalWordTimeLog(CDDEASymbol cSymbol, DateTime dtTime, int iValue, int iPacketIndex, int iCycleIndex)
        {
            //int iResultVal = -1;
            //iResultVal = (int)cSymbol.Mask & iValue;
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = cSymbol.Key;
            cLog.Value = iValue;
            cLog.Time = dtTime;
            cLog.RisingTime = dtTime;
            cLog.PacketIndex = iPacketIndex;
            cLog.CycleIndex = iCycleIndex;

            return cLog;
        }

        protected CTimeLog GetFragTimeLog(string sKey, int iValue, CDDEAPacketData cData)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.PacketIndex = m_iPacketNumber;
            cLog.CycleIndex = m_iCycleNumber;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.Note = cData.NoteReadString;

            return cLog;
        }

        protected CTimeLog GetFragTimeLog(string sKey, int iValue, CDDEAPacketData cData, string sNote)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.PacketIndex = 0;
            cLog.CycleIndex = m_iFragMasterCycle;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.Note = sNote;

            return cLog;
        }
        protected CTimeLog GetFragTimeInitLog(string sKey, int iValue, CDDEAPacketData cData, string sNote)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.PacketIndex = cData.GroupNumber;
            cLog.CycleIndex = m_iCycleNumber;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.Note = sNote;

            return cLog;
        }

        protected CTimeLog SetFragCycleLog(string sKey, int iValue, CDDEAPacketData cData, string sNote)
        {
            CTimeLogS cLogS = new CTimeLogS();

            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.PacketIndex = m_iPacketNumber;
            cLog.CycleIndex = m_iCycleNumber;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.Note = sNote;

            return cLog;
        }

        protected CTimeLog SetFragMasterCycleLog(string sKey, int iValue, CDDEAPacketData cData, string sNote)
        {
            CTimeLogS cLogS = new CTimeLogS();

            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.PacketIndex = 0;
            cLog.CycleIndex = m_iFragMasterCycle;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.Note = sNote;

            return cLog;
        }

        protected CTimeLog GetLOBTimeLog(string sKey, int iValue, CDDEAPacketData cData)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = sKey;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.PacketIndex = -1;
            cLog.CycleIndex = -1;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.GlassID = cData.GlassIDReadString;
            cLog.Process = cData.ProcessReadValue.ToString();
            cLog.ProcessID = cData.ProcessIDReadString;
            cLog.Model = cData.ModelReadString;
            cLog.Note = cData.NoteReadString;

            return cLog;
        }

        protected void SetLOBTactTimeLog(CDDEASymbol cSymbol, CDDEAPacketData cData)
        {
            CTimeLogS cLogS = new CTimeLogS();

            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = cSymbol.Key;
            cLog.Value = cData.TactTimeReadValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.GlassID = cData.GlassIDReadString;
            cLog.Process = cData.ProcessReadValue.ToString();
            cLog.ProcessID = cData.ProcessIDReadString;
            cLog.Model = cData.ModelReadString;
            cLog.Note = "TACT";

            cLogS.Add(cLog);

            m_cTask.EventDataChanged(cLogS);
        }

        protected CTimeLog SetIndexLog(CDDEASymbol cSourceSymbol, CDDEASymbol cIndexSymbol, int iSourceValue, int iIndexValue, CDDEAPacketData cData)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = cSourceSymbol.Key;
            cLog.Value = iSourceValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.GlassID = cData.GlassIDReadString;
            cLog.Process = cData.ProcessReadValue.ToString();
            cLog.ProcessID = cData.ProcessIDReadString;
            cLog.Model = cData.ModelReadString;

            // 부분수집인 경우
            // cLog.PacketIndex = m_iPacketNumber;
            // cLog.CycleIndex = m_iCycleNumber;
            //yjk, 20.02.12 - 파라미터 수집 조건 추가
            if (m_emCollectMode == EMCollectMode.Normal || m_emCollectMode == EMCollectMode.ParameterNormal)
            {
                cLog.PacketIndex = -1;
                cLog.CycleIndex = -1;
            }
            else
            {
                cLog.PacketIndex = m_iPacketNumber;
                cLog.CycleIndex = m_iCycleNumber;
            }

            string sNote = string.Format("{0}={1}", cIndexSymbol.Address, iIndexValue);
            cLog.Note = sNote;

            return cLog;
        }

        protected CTimeLog GetMDCTimeLog(string sDName, string sPCode, int iValue, CDDEAPacketData cData)
        {
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.MDC;
            cLog.Key = sDName;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.MCode = sPCode;

            return cLog;
        }

        protected CTimeLog GetLOBMCSCTimeLog(CDDEASymbol cSymbol, CDDEAPacketData cData)
        {
            CTimeLog cLog = new CTimeLog();
            string sAddress = cSymbol.BaseAddress;
            cLog.UDFType = EMUdfLogType.MCSC;
            cLog.Key = cSymbol.Key;
            cLog.Time = cData.Time;
            cLog.RisingTime = cSymbol.MCSCBitRisingTime;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.GlassID = cData.GlassIDReadString;
            cLog.ProcessID = cData.ProcessIDReadString;

            return cLog;
        }

        protected CTimeLog GetFragTimeLogWord(CDDEASymbol cSymbol, int iValue, CDDEAPacketData cData, string sNote)
        {
            //int iResultVal = -1;
            //iResultVal = (int)cSymbol.Mask & iValue;
            CTimeLog cLog = new CTimeLog();

            cLog.UDFType = EMUdfLogType.PLUS;
            cLog.Key = cSymbol.Key;
            cLog.Value = iValue;
            cLog.Time = cData.Time;
            cLog.RisingTime = cData.Time;
            cLog.PacketIndex = m_iPacketNumber;
            cLog.CycleIndex = m_iCycleNumber;
            cLog.Recipe = cData.RecipeReadValue.ToString();
            cLog.Note = sNote;

            return cLog;
        }

        #endregion



        protected void WriteNormalBitFirstLogS(List<CDDEASymbol> lstBitSymbol, int iValue, CDDEAPacketData cData, CTimeLogS cAddTimeLogS, bool bCountLog)
        {
            foreach (CDDEASymbol sym in lstBitSymbol)
            {
                CTimeLog cLog = new CTimeLog();
                int iMaskValue = iValue & (int)sym.Mask;
                if (iMaskValue != 0)
                    cLog = GetNormalTimeLog(sym.Key, cData.Time, 1, cData.GroupNumber, cData.CycleNumber);
                else
                    cLog = GetNormalTimeLog(sym.Key, cData.Time, 0, cData.GroupNumber, cData.CycleNumber);

                //kch@udmtek, 17.02.01
                if (bCountLog)
                    sym.LogCount += 1;

                cAddTimeLogS.Add(cLog);
            }
        }

        protected void WriteNormalWordFirstLogS(List<CDDEASymbol> lstWordSymbol, int iValue, CDDEAPacketData cData, CTimeLogS cAddTimeLogS, bool bCountLog)
        {
            foreach (CDDEASymbol sym in lstWordSymbol)
            {
                CTimeLog cLog = new CTimeLog();

                if (sym.AddressCount > 1)
                {
                    //kch@udmtek, 17.02.01
                    if (bCountLog)
                        sym.LogCount += 1;

                    //Dword 처리
                    cData.GetDwordValue(sym.Address, sym.DWordSecondAddress);
                    cLog = GetNormalTimeLog(sym.Key, cData.Time, cData.ReadDataList[sym.Address].DWordValue, cData.GroupNumber, cData.CycleNumber);
                    cAddTimeLogS.Add(cLog);
                }
                else
                {
                    //kch@udmtek, 17.02.01
                    if (bCountLog)
                        sym.LogCount += 1;

                    short i16Bit = (short)iValue;
                    iValue = i16Bit;
                    cLog = GetNormalWordTimeLog(sym, cData.Time, iValue, cData.GroupNumber, cData.CycleNumber);
                    cAddTimeLogS.Add(cLog);
                }
            }
        }

        protected void WriteNormalBitLogS(List<CDDEASymbol> lstBitSymbol, int iValue, int iLastValue, CDDEAPacketData cData, CTimeLogS cAddTimeLogS, bool bCountLog)
        {
            //jjk, 19.05.20 - 시간 생성시 다른 Thread 접근 제어
            m_Mutex.WaitOne();
            foreach (CDDEASymbol sym in lstBitSymbol)
            {
                CTimeLog cLog = new CTimeLog();

                int iMaskValue = iValue & (int)sym.Mask;
                int iLastMaskValue = iLastValue & (int)sym.Mask;

                if (iMaskValue == iLastMaskValue)
                    continue;

                if (iMaskValue != 0)
                    cLog = GetNormalTimeLog(sym.Key, cData.Time, 1, cData.GroupNumber, cData.CycleNumber);
                else
                    cLog = GetNormalTimeLog(sym.Key, cData.Time, 0, cData.GroupNumber, cData.CycleNumber);

                //kch@udmtek, 17.02.01
                if (bCountLog)
                    sym.LogCount += 1;

                cAddTimeLogS.Add(cLog);
            }
            m_Mutex.ReleaseMutex();
        }

        protected void WriteNormalWordLogS(List<CDDEASymbol> lstWordSymbol, int iValue, CDDEAPacketData cData, CDDEAPacketData cLastData, CTimeLogS cAddTimeLogS, bool bCountLog)
        {
            foreach (CDDEASymbol sym in lstWordSymbol)
            {
                CTimeLog cLog = new CTimeLog();

                if (sym.AddressCount > 1)
                {
                    //Dword 처리
                    cData.GetDwordValue(sym.Address, sym.DWordSecondAddress);
                    int iLastDword = cLastData.ReadDataList[sym.Address].DWordValue;
                    int iNowDword = cData.ReadDataList[sym.Address].DWordValue;

                    if (iNowDword == iLastDword)
                        continue;

                    //kch@udmtek, 17.02.01
                    if (bCountLog)
                        sym.LogCount += 1;

                    cLog = GetNormalTimeLog(sym.Key, cData.Time, cData.ReadDataList[sym.Address].DWordValue, cData.GroupNumber, cData.CycleNumber);
                    cAddTimeLogS.Add(cLog);
                }
                else
                {
                    if (sym.AddressCount == 0)
                        continue;

                    int iLastValue = cLastData.ReadDataList[sym.Address].Value;
                    if (iValue == iLastValue)
                        continue;

                    short i16Bit = (short)iValue;
                    iValue = i16Bit;

                    //kch@udmtek, 17.02.01
                    if (bCountLog)
                        sym.LogCount += 1;

                    cLog = GetNormalWordTimeLog(sym, cData.Time, iValue, cData.GroupNumber, cData.CycleNumber);
                    cAddTimeLogS.Add(cLog);
                }
            }
        }

        protected void WriteIndexLogS(List<CDDEASymbol> lstIncludeIndexSymbol, List<CDDEASymbol> lstIndexSymbol, int iValue, CDDEAPacketData cData, CTimeLogS cAddTimeLogS)
        {
            foreach (CDDEASymbol sym in lstIncludeIndexSymbol)
            {
                CTimeLog cLog = new CTimeLog();
                CDDEASymbol cFindSymbol = lstIndexSymbol.Find(b => b.AddressMajor == sym.IndexAddressNumber);
                if (cFindSymbol == null)
                    SetEventMessage("", ResDDEA.CDDEAGroup_Info + sym.Address + ResDDEA.CDDEAGroup_IndexSymbolNone + sym.IndexAddressNumber);
                else
                {
                    cLog = SetIndexLog(sym, cFindSymbol, iValue, cData.ReadDataList[cFindSymbol.Address].Value, cData);
                    cAddTimeLogS.Add(cLog);
                }
            }
        }

        protected void WriteFragBitFirstLogS(List<CDDEASymbol> lstBitSymbol, int iValue, CDDEAPacketData cData, CFragMode cFrag, CTimeLogS cAddTimeLogS)
        {
            foreach (CDDEASymbol sym in lstBitSymbol)
            {
                CTimeLog cLog = new CTimeLog();
                int iMaskValue = iValue & (int)sym.Mask;

                int iBitVal = 0;

                if (iMaskValue != 0)

                    iBitVal = 1;
                else iBitVal = 0;

                if (cFrag.CycleConditionSymbolS.StartCycleList[0].Symbol.Address == sym.Address)
                    continue;

                if (cFrag.CycleConditionSymbolS.EndCycleList[0].Symbol.Address == sym.Address)
                    continue;
                cLog = GetFragTimeInitLog(sym.Key, iBitVal, cData, "Init");
                cAddTimeLogS.Add(cLog);

                //if (sym.Address == "M578")
                //{
                //    string sMsg = string.Format("F : {0}/ {1}/ {2}/ {3}/ {4}/ {5}", sym.Key, iBitVal, cData.Time.ToString("HHmmss.fff"), cData.GroupNumber, cData.CycleNumber, m_iDataCount);
                //    SetEventMessage(sMsg);

                //}
            }
        }

        protected void WriteFragWordFirstLogS(List<CDDEASymbol> lstWordSymbol, int iValue, CDDEAPacketData cData, CTimeLogS cAddTimeLogS)
        {
            foreach (CDDEASymbol sym in lstWordSymbol)
            {
                CTimeLog cLog = new CTimeLog();

                if (sym.AddressCount > 1)
                {
                    //Dword 처리
                    cData.GetDwordValue(sym.Address, sym.DWordSecondAddress);
                    cLog = GetFragTimeLog(sym.Key, cData.ReadDataList[sym.Address].DWordValue, cData);
                    cAddTimeLogS.Add(cLog);
                }
                else
                {
                    short i16Bit = (short)iValue;
                    iValue = i16Bit;
                    cLog = GetFragTimeLogWord(sym, iValue, cData, "Init");
                    cAddTimeLogS.Add(cLog);
                }
            }
        }

        protected void WriteFragBitLogS(List<CDDEASymbol> lstBitSymbol, int iValue, int iLastValue, CDDEAPacketData cData, CFragMode cFrag, CTimeLogS cAddTimeLogS, bool bRecipeError)
        {
            string sKeyCheck = "";
            try
            {
                foreach (CDDEASymbol sym in lstBitSymbol)
                {
                    CTimeLog cLog = new CTimeLog();
                    int iMaskValue = iValue & (int)sym.Mask;
                    int iLastMaskValue = iLastValue & (int)sym.Mask;

                    if (iMaskValue == iLastMaskValue)
                        continue;

                    int iBitVal = 0;
                    if (iMaskValue != 0)
                        iBitVal = 1;
                    else
                        iBitVal = 0;

                    if (bRecipeError == false)
                    {
                        if (cFrag.CycleConditionSymbolS.StartCycleList[0].Symbol.Key == sym.Key)
                            continue;

                        if (cFrag.CycleConditionSymbolS.EndCycleList[0].Symbol.Key == sym.Key)
                            continue;

                        if (cFrag.CycleConditionSymbolS.TriggerCycleList.Count > 0)
                        {
                            if (cFrag.CycleConditionSymbolS.TriggerCycleList[0].Symbol.Key == sym.Key)
                                continue;
                        }
                    }

                    sKeyCheck = sym.Key;
                    cLog = GetFragTimeLog(sym.Key, iBitVal, cData);
                    cAddTimeLogS.Add(cLog);
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_WriteFragBitLogS_Exception_Msg + sKeyCheck + ex.Message + ")");
            }
        }

        protected void WriteFragMasterBitLogS(List<CDDEASymbol> lstBitSymbol, int iValue, int iLastValue, CDDEAPacketData cData, CFragMasterMode cFrag, CTimeLogS cAddTimeLogS, bool bRecipeError)
        {
            string sKeyCheck = "";
            try
            {
                foreach (CDDEASymbol sym in lstBitSymbol)
                {
                    CTimeLog cLog = new CTimeLog();
                    int iMaskValue = iValue & (int)sym.Mask;
                    int iLastMaskValue = iLastValue & (int)sym.Mask;

                    if (iMaskValue == iLastMaskValue)
                        continue;

                    int iBitVal = 0;
                    if (iMaskValue != 0)
                        iBitVal = 1;
                    else
                        iBitVal = 0;

                    if (bRecipeError == false)
                    {
                        if (cFrag.CycleConditionSymbolS.StartCycleList[0].Symbol.Key == sym.Key)
                            continue;

                        if (cFrag.CycleConditionSymbolS.EndCycleList[0].Symbol.Key == sym.Key)
                            continue;

                        if (cFrag.CycleConditionSymbolS.TriggerCycleList.Count > 0)
                        {
                            if (cFrag.CycleConditionSymbolS.TriggerCycleList[0].Symbol.Key == sym.Key)
                                continue;
                        }
                    }

                    sKeyCheck = sym.Key;
                    cLog = GetFragTimeLog(sym.Key, iBitVal, cData, "Standard");
                    cAddTimeLogS.Add(cLog);
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_WriteFragBitLogS_Exception_Msg + sKeyCheck + ex.Message + ")");
            }
        }

        protected void WriteFragWordLogS(List<CDDEASymbol> lstWordSymbol, int iValue, CDDEAPacketData cData, CDDEAPacketData cLastData, CTimeLogS cAddTimeLogS)
        {
            string sKeyCheck = "";
            try
            {
                foreach (CDDEASymbol sym in lstWordSymbol)
                {
                    CTimeLog cLog = new CTimeLog();

                    if (sym.AddressCount > 1)
                    {
                        //Dword 처리
                        cData.GetDwordValue(sym.Address, sym.DWordSecondAddress);
                        int iLastDword = cLastData.ReadDataList[sym.Address].DWordValue;
                        int iNowDword = cData.ReadDataList[sym.Address].DWordValue;

                        if (iNowDword == iLastDword)
                            continue;

                        cLog = GetFragTimeLog(sym.Key, cData.ReadDataList[sym.Address].DWordValue, cData);
                        cAddTimeLogS.Add(cLog);
                    }
                    else
                    {
                        if (sym.AddressCount == 0)
                            continue;
                        int iLastValue = cLastData.ReadDataList[sym.Address].Value;
                        if (iValue == iLastValue)
                            continue;
                        short i16Bit = (short)iValue;
                        iValue = i16Bit;
                        cLog = GetFragTimeLogWord(sym, iValue, cData, "");
                        cAddTimeLogS.Add(cLog);
                    }
                    sKeyCheck = sym.Key;
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_WriteFragBitLogS_Exception_Msg + sKeyCheck + ex.Message + ")");
            }
        }

        protected void WriteLOBFirstBitLogS(List<CDDEASymbol> lstBitSymbol, int iValue, CDDEAPacketData cData, CTimeLogS cAddTimeLogS)
        {
            foreach (CDDEASymbol sym in lstBitSymbol)
            {
                CTimeLog cLog = new CTimeLog();
                string sAddress = sym.Address;
                sym.MCSCBitRisingTime = DateTime.MinValue;

                int iMaskValue = iValue & (int)sym.Mask;
                if (iMaskValue != 0)
                    cLog = GetLOBTimeLog(sym.Key, 1, cData);
                else
                    cLog = GetLOBTimeLog(sym.Key, 0, cData);

                cAddTimeLogS.Add(cLog);
            }
        }

        protected void WriteLOBFirstMdcLogS(List<CDDEAMdcSymbol> lstMdcSymbol, int iValue, CDDEAPacketData cData, CTimeLogS cAddTimeLogS)
        {
            foreach (CDDEAMdcSymbol sym in lstMdcSymbol)
            {
                CTimeLog cLog = new CTimeLog();
                if (sym.AddressCount == 0)
                    continue;
                if (sym.AddressCount > 1)
                {
                    cData.GetDwordValue(sym.Address, sym.DWordSecondAddress);
                    int iDWordValue = cData.ReadDataList[sym.Address].DWordValue;
                    for (int i = 0; i < sym.PCodeList.Count; i++)
                    {
                        string sPCode = sym.PCodeList[i];
                        string sDName = sym.DNameList[i];
                        cLog = GetMDCTimeLog(sDName, sPCode, iDWordValue, cData);
                        cAddTimeLogS.Add(cLog);
                    }
                }
                else
                {
                    for (int i = 0; i < sym.PCodeList.Count; i++)
                    {
                        string sPCode = sym.PCodeList[i];
                        string sDName = sym.DNameList[i];
                        short i16Bit = (short)iValue;
                        iValue = i16Bit;
                        cLog = GetMDCTimeLog(sDName, sPCode, iValue, cData);
                        cAddTimeLogS.Add(cLog);
                    }
                }
            }
        }

        protected void WriteLOBWordLogS(List<CDDEASymbol> lstWordSymbol, int iValue, CDDEAPacketData cData, CDDEAPacketData cLastData, CTimeLogS cAddTimeLogS, bool bCountLog)
        {
            foreach (CDDEASymbol cddeaSymbol in lstWordSymbol)
            {
                CTimeLog ctimeLog = new CTimeLog();
                if (cddeaSymbol.AddressCount > 1)
                {
                    cData.GetDwordValue(cddeaSymbol.Address, cddeaSymbol.DWordSecondAddress);
                    int dwordValue = cLastData.ReadDataList[cddeaSymbol.Address].DWordValue;
                    if (cData.ReadDataList[cddeaSymbol.Address].DWordValue != dwordValue)
                    {
                        if (bCountLog)
                            ++cddeaSymbol.LogCount;
                        CTimeLog lobTimeLog = GetLOBTimeLog(cddeaSymbol.Key, cData.ReadDataList[cddeaSymbol.Address].DWordValue, cData);
                        cAddTimeLogS.Add(lobTimeLog);
                    }
                }
                else if (cddeaSymbol.AddressCount != 0)
                {
                    int num3 = cLastData.ReadDataList[cddeaSymbol.Address].Value;
                    if (iValue != num3)
                    {
                        iValue = (int)(short)iValue;
                        if (bCountLog)
                            ++cddeaSymbol.LogCount;
                        CTimeLog lobTimeLog = GetLOBTimeLog(cddeaSymbol.Key, iValue, cData);
                        cAddTimeLogS.Add(lobTimeLog);
                    }
                }
            }
        }

        protected void WriteLOBBitLogS(List<CDDEASymbol> lstBitSymbol, CDDEAPacketData cData, CDDEAPacketData cLastData, CTimeLogS cAddTimeLogS)
        {
            foreach (CDDEASymbol sym in lstBitSymbol)
            {
                CTimeLog cLog = new CTimeLog();
                string sAddress = sym.BaseAddress;

                int iMaskValue = cData.ReadDataList[sAddress].Value & (int)sym.Mask;
                int iLastMaskValue = cLastData.ReadDataList[sAddress].Value & (int)sym.Mask;

                if (iMaskValue != 0)
                {
                    if (iMaskValue != iLastMaskValue)
                    {
                        cLog = GetLOBTimeLog(sym.Key, 1, cData);
                        cAddTimeLogS.Add(cLog);
                        if (sym.MCSCBitRisingTime == DateTime.MinValue)
                            sym.MCSCBitRisingTime = cData.Time;
                    }

                }
                else
                {
                    if (iMaskValue != iLastMaskValue)
                    {
                        cLog = GetLOBTimeLog(sym.Key, 0, cData);
                        cAddTimeLogS.Add(cLog);
                        if (sym.MCSCBitRisingTime != DateTime.MinValue)
                        {
                            //로그 찍음.
                            cLog = GetLOBMCSCTimeLog(sym, cData);
                            sym.MCSCBitRisingTime = DateTime.MinValue;
                            cAddTimeLogS.Add(cLog);
                        }
                    }
                }
            }
        }

        protected void WriteLOBMdcLogS(List<CDDEAMdcSymbol> lstMdcSymbol, CDDEAPacketData cData, CDDEAPacketData cLastData, CTimeLogS cAddTimeLogS)
        {
            foreach (CDDEAMdcSymbol sym in lstMdcSymbol)
            {
                CTimeLog cLog = new CTimeLog();
                if (sym.AddressCount == 0)
                    continue;
                if (sym.AddressCount > 1)
                {
                    cData.GetDwordValue(sym.Address, sym.DWordSecondAddress);
                    cLastData.GetDwordValue(sym.Address, sym.DWordSecondAddress);

                    int iDWordValue = cData.ReadDataList[sym.Address].DWordValue;
                    int iLastDWordValue = cLastData.ReadDataList[sym.Address].DWordValue;

                    if (iDWordValue == iLastDWordValue)
                        continue;

                    for (int i = 0; i < sym.PCodeList.Count; i++)
                    {
                        string sPCode = sym.PCodeList[i];
                        string sDName = sym.DNameList[i];
                        cLog = GetMDCTimeLog(sDName, sPCode, iDWordValue, cData);
                        cAddTimeLogS.Add(cLog);
                    }
                }
                else
                {
                    int iNowValue = cData.ReadDataList[sym.Address].Value;
                    int iLastValue = cLastData.ReadDataList[sym.Address].Value;

                    if (iNowValue == iLastValue)
                        continue;
                    short i16Bit = (short)iNowValue;
                    iNowValue = i16Bit;
                    for (int i = 0; i < sym.PCodeList.Count; i++)
                    {
                        string sPCode = sym.PCodeList[i];
                        string sDName = sym.DNameList[i];
                        cLog = GetMDCTimeLog(sDName, sPCode, iNowValue, cData);
                        cAddTimeLogS.Add(cLog);
                    }
                }
            }
        }

        #region Recipe, GlassID, Process, Refresh 분석

        protected int GetValue(CDDEAPacketData cData, CDDEASymbol cSymbol)
        {
            int iValue = cData.ReadDataList[cSymbol.BaseAddress].Value;
            int iMask = iValue & (int)cSymbol.Mask;
            if (iMask != 0)
                return 1;
            return 0;
        }

        protected bool GetRecipeValue(CDDEAPacketData cData)
        {
            bool bFind = false;
            foreach (CDDEASymbol sym in m_cProject.RecipeSymbolList)
            {
                if (cData.ReadDataList.ContainsKey(sym.BaseAddress))
                {
                    cData.RecipeReadValue = cData.ReadDataList[sym.BaseAddress].Value;
                    bFind = true;
                }
                //else
                //    Console.WriteLine("Recipe 주소가 없습니다.");
                break;
            }
            return bFind;
        }

        protected void GetProcessValue(CDDEAPacketData cData, CDDEASymbol cSymbol)
        {
            if (cSymbol == null) return;
            if (cData.ReadDataList.ContainsKey(cSymbol.BaseAddress))
            {
                cData.ProcessReadValue = cData.ReadDataList[cSymbol.BaseAddress].Value;
            }
            //else
            //    Console.WriteLine("Process 주소가 없습니다.");

        }

        protected void GetGlassID(CDDEAPacketData cData, CDDEASymbolList cSymbolList)
        {
            if (cSymbolList.Count == 0) return;
            string sResult = "";
            List<string> lstReceive = new List<string>();
            foreach (CDDEASymbol sym in cSymbolList)
            {
                if (cData.ReadDataList.ContainsKey(sym.BaseAddress))
                {
                    int iValue = cData.ReadDataList[sym.BaseAddress].Value;
                    byte[] n2Char = new byte[2];
                    n2Char[0] = (byte)iValue;
                    n2Char[1] = (byte)(iValue >> 8);
                    string sAscii = Encoding.ASCII.GetString(n2Char);
                    if (sAscii == "\0\0")
                    {
                        sAscii = "";
                    }
                    lstReceive.Add(sAscii);
                    sResult += sAscii;
                }
            }
            if (lstReceive.Count > 1)
            {
                cData.ModelReadString = lstReceive[1].Trim();
                cData.GlassIDReadString = sResult.Trim();
            }
        }

        protected void GetProcessID(CDDEAPacketData cData, CDDEASymbolList cSymbolList)
        {
            if (cSymbolList.Count == 0) return;
            string sResult = "";
            List<string> lstReceive = new List<string>();
            foreach (CDDEASymbol sym in cSymbolList)
            {
                if (cData.ReadDataList.ContainsKey(sym.BaseAddress))
                {
                    int iValue = cData.ReadDataList[sym.BaseAddress].Value;
                    byte[] n2Char = new byte[2];
                    n2Char[0] = (byte)iValue;
                    n2Char[1] = (byte)(iValue >> 8);
                    string sAscii = Encoding.ASCII.GetString(n2Char);
                    if (sAscii == "\0\0")
                    {
                        sAscii = "";
                    }
                    lstReceive.Add(sAscii);
                    sResult += sAscii;
                }
            }
            if (lstReceive.Count > 1)
            {
                // cData.ModelReadString = lstReceive[1].Trim();
                cData.ProcessIDReadString = sResult.Trim();
            }
        }

        protected void CheckRefreshSymbol(CDDEAPacketData cData, CDDEASymbol cRefreshSymbol, CDDEASymbol cTactSymbol)
        {
            if (cRefreshSymbol == null) return;
            if (cTactSymbol == null) return;

            if (cData.ReadDataList.ContainsKey(cRefreshSymbol.BaseAddress))
            {
                int iValue = cData.ReadDataList[cRefreshSymbol.BaseAddress].Value & (int)cRefreshSymbol.Mask;
                //On될때 한번만 찍어야함.
                if (iValue != 0 && m_bRefreshFlag == false)
                {
                    m_bRefreshFlag = true;
                    if (cData.ReadDataList.ContainsKey(cTactSymbol.BaseAddress))
                    {
                        cData.TactTimeReadValue = cData.ReadDataList[cTactSymbol.BaseAddress].Value;
                        //TactTime Log 찍어야함.
                        SetLOBTactTimeLog(cTactSymbol, cData);
                    }
                    //else
                    //{
                    //    Console.WriteLine("TactTime 주소가 없습니다.");
                    //}
                }
                else if (iValue == 0)
                {
                    m_bRefreshFlag = false;
                }
            }
            else
                Console.WriteLine(ResDDEA.CDDEAGroup_NoneRefreshAddress);
        }

        #endregion


        private bool IsCycleSatisfied(CDDEACycleSymbol cCycleSymbol, int iValue)
        {
            return cCycleSymbol.Condition == iValue;
        }

        private bool IsCycleTimeInRange(CFragMode cFargmentMode, long nCycleTime)
        {
            return (long)cFargmentMode.CycleConditionSymbolS.CycleMinTimeMs <= nCycleTime && (long)cFargmentMode.CycleConditionSymbolS.CycleMaxTimeMs >= nCycleTime;
        }

        private bool IsCycleTimeInRange(CFragMasterMode cFragMasterMode, long nCycleTime)
        {
            return (long)cFragMasterMode.CycleConditionSymbolS.CycleMinTimeMs <= nCycleTime && (long)cFragMasterMode.CycleConditionSymbolS.CycleMaxTimeMs >= nCycleTime;
        }

        private void StartWatch(Stopwatch cWatch)
        {
            cWatch.Reset();
            cWatch.Start();
        }

        private long StopWatch(Stopwatch cWatch)
        {
            cWatch.Stop();
            return cWatch.ElapsedMilliseconds;
        }

        protected void DoFragmentCycleProcess(CFragMode cFargmentMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData, CTimeLogS cTotalTimeLogS)
        {
            try
            {
                CDDEACycleSymbol cCycleStart = cFargmentMode.CycleConditionSymbolS.StartCycleList[0];
                CDDEACycleSymbol cCycleEnd = cFargmentMode.CycleConditionSymbolS.EndCycleList[0];
                CDDEACycleSymbol cCycleTrigger = null;

                if (cFargmentMode.CycleConditionSymbolS.TriggerCycleList.Count > 0)
                    cCycleTrigger = cFargmentMode.CycleConditionSymbolS.TriggerCycleList[0];

                int iCycleStartCurrentValue = GetValue(cCurrentPacketData, cCycleStart.Symbol);
                int iCycleStartLastValue = GetValue(cLastPacketData, cCycleStart.Symbol);
                int iCycleEndCurrentValue = GetValue(cCurrentPacketData, cCycleEnd.Symbol);
                int iCycleEndLastValue = GetValue(cLastPacketData, cCycleEnd.Symbol);
                int iCycleTriggerCurrentValue = -1;
                int iCycleTriggerLastValue = -1;

                string sCycleStartLogNote = "";
                string sCycleEndLogNote = "";

                if (cCycleTrigger != null)
                {
                    iCycleTriggerCurrentValue = GetValue(cCurrentPacketData, cCycleTrigger.Symbol);
                    iCycleTriggerLastValue = GetValue(cLastPacketData, cCycleTrigger.Symbol);
                    m_bCycleTrigger = IsCycleSatisfied(cCycleTrigger, iCycleTriggerCurrentValue);
                }

                // 최초 수집 시작 시, Cycle이 종료될 때까지 Cycle로 인정 안함.
                if (m_bCycleStart == false && m_bCycleEnd == false)
                {
                    if (iCycleEndCurrentValue != iCycleEndLastValue)
                    {
                        if (IsCycleSatisfied(cCycleEnd, iCycleEndCurrentValue))
                        {
                            m_bCycleEnd = true;
                            m_iCycleNumber = 0;
                        }
                    }
                }
                else if (!m_bCycleStart && m_bCycleEnd && m_bCycleTrigger)
                {
                    if (iCycleStartCurrentValue != iCycleStartLastValue && IsCycleSatisfied(cCycleStart, iCycleStartCurrentValue))
                    {
                        m_bCycleStart = true;
                        m_bCycleEnd = false;
                        SetEventMessage("", "CycleState,On");
                        SetEventMessage("", ResDDEA.CDDEAGroup_bCycleStart);
                        StartWatch(m_swCycle);
                        sCycleStartLogNote = "CycleStart";
                    }
                }
                else if (m_bCycleStart && !m_bCycleEnd && iCycleEndCurrentValue != iCycleEndLastValue)
                {
                    if (IsCycleSatisfied(cCycleEnd, iCycleEndCurrentValue))
                    {
                        m_bCycleStart = false;
                        m_bCycleEnd = true;
                        SetEventMessage("", "CycleState,Off");
                        long nCycleTime = StopWatch(m_swCycle);
                        if (IsCycleTimeInRange(cFargmentMode, nCycleTime))
                        {
                            SetEventMessage("", ResDDEA.CDDEAGroup_bCycleEnd_TimeOut_false);
                            m_bCycleTimeOut = false;
                        }
                        else
                        {
                            SetEventMessage("", ResDDEA.CDDEAGroup_bCycleEnd_TimeOut_true);
                            m_bCycleTimeOut = true;
                        }
                        if (!m_bCycleTimeOut)
                        {
                            UEventCycleChanged((object)this, cCurrentPacketData.CycleNumber + 1);
                            if (m_cProject.CollectMode == EMCollectMode.Frag)
                                m_iCycleBaseCount = 0;
                            if (m_iCycleNumber < m_iCycleBaseCount)
                            {
                                ++m_iCycleNumber;
                            }
                            else
                            {
                                m_iCycleNumber = 0;
                                m_iCycleBaseCount = m_cProject.CycleCount;
                                ++m_iPacketNumber;
                            }
                        }
                        else if (m_iCycleNumber == 0)
                        {
                            UEventCycleChanged((object)this, cCurrentPacketData.CycleNumber + 1);
                            ++m_iCycleNumber;
                        }
                        else
                        {
                            ++m_iCycleNumber;
                            ++m_iCycleBaseCount;
                        }
                    }
                    if (m_bCycleEnd)
                    {
                        sCycleStartLogNote = "";
                        sCycleEndLogNote = "CycleEnd";

                        if (iCycleStartCurrentValue != iCycleStartLastValue && IsCycleSatisfied(cCycleStart, iCycleStartCurrentValue))
                        {
                            m_bCycleStart = true;
                            m_bCycleEnd = false;
                            SetEventMessage("", "CycleState,On");
                            SetEventMessage("", ResDDEA.CDDEAGroup_bCycleStart);
                            StartWatch(m_swCycle);

                            if (cCycleStart.Symbol.Key != cCycleEnd.Symbol.Key)
                                sCycleStartLogNote = "CycleStart";
                            else if (cCycleStart.Condition == cCycleEnd.Condition)
                                sCycleStartLogNote = "CycleEnd/CycleStart";
                        }
                    }
                }

                if (cCycleTrigger != null && iCycleTriggerCurrentValue != iCycleTriggerLastValue)
                {
                    // 서로 키가 다르면 로그 남김
                    if (cCycleTrigger.Symbol.Key != cCycleStart.Symbol.Key && cCycleTrigger.Symbol.Key != cCycleEnd.Symbol.Key)
                    {
                        CTimeLog cLog = SetFragCycleLog(cCycleTrigger.Symbol.Key, iCycleTriggerCurrentValue, cCurrentPacketData, "");
                        cTotalTimeLogS.Add(cLog);
                    }
                }

                // Cycle 시작과 Cycle 종료 조건의 Key 가 다르므로 각각 로그 처리
                if (cCycleStart.Symbol.Key != cCycleEnd.Symbol.Key)
                {
                    if (iCycleEndCurrentValue != iCycleEndLastValue)
                    {
                        CTimeLog cLog = SetFragCycleLog(cCycleEnd.Symbol.Key, iCycleEndCurrentValue, cCurrentPacketData, sCycleEndLogNote);
                        cTotalTimeLogS.Add(cLog);
                    }

                    if (iCycleStartCurrentValue != iCycleStartLastValue)
                    {
                        CTimeLog cLog = SetFragCycleLog(cCycleStart.Symbol.Key, iCycleStartCurrentValue, cCurrentPacketData, sCycleStartLogNote);
                        cTotalTimeLogS.Add(cLog);
                    }
                }
                // Cycle 시작과 Cycle 종료 조건의 Key 가 같으므로 한 로그로 처리
                else
                {
                    if (iCycleStartCurrentValue != iCycleStartLastValue)
                    {
                        string sNote = sCycleStartLogNote;
                        if (sNote == "")
                            sNote = sCycleEndLogNote;

                        CTimeLog cLog = SetFragCycleLog(cCycleStart.Symbol.Key, iCycleStartCurrentValue, cCurrentPacketData, sNote);
                        cTotalTimeLogS.Add(cLog);
                    }
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_DoFragmentCycleProcess_Exception_Msg+ ex.Message + ")");
            }
        }

        protected void DoFragmentMasterCycleProcess(CFragMasterMode cFragMasterMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData, CTimeLogS cTotalTimeLogS)
        {
            try
            {
                CDDEACycleSymbol cCycleStart = cFragMasterMode.CycleConditionSymbolS.StartCycleList[0];
                CDDEACycleSymbol cCycleEnd = cFragMasterMode.CycleConditionSymbolS.EndCycleList[0];
                CDDEACycleSymbol cCycleTrigger = (CDDEACycleSymbol)null;

                if (cFragMasterMode.CycleConditionSymbolS.TriggerCycleList.Count > 0)
                    cCycleTrigger = cFragMasterMode.CycleConditionSymbolS.TriggerCycleList[0];

                int iCycleStartCurrentValue = GetValue(cCurrentPacketData, cCycleStart.Symbol);
                int iCycleStartLastValue = GetValue(cLastPacketData, cCycleStart.Symbol);
                int iCycleEndCurrentValue = GetValue(cCurrentPacketData, cCycleEnd.Symbol);
                int iCycleEndLastValue = GetValue(cLastPacketData, cCycleEnd.Symbol);
                int iCycleTriggerCurrentValue = -1;
                int iCycleTriggerLastValue = -1;

                string sCycleStartLogNote = "";
                string sCycleEndLogNote = "";

                if (cCycleTrigger != null)
                {
                    iCycleTriggerCurrentValue = GetValue(cCurrentPacketData, cCycleTrigger.Symbol);
                    iCycleTriggerLastValue = GetValue(cLastPacketData, cCycleTrigger.Symbol);
                    m_bCycleTrigger = IsCycleSatisfied(cCycleTrigger, iCycleTriggerCurrentValue);
                }

                if (!m_bCycleStart && !m_bCycleEnd)
                {
                    if (iCycleEndCurrentValue != iCycleEndLastValue && IsCycleSatisfied(cCycleEnd, iCycleEndCurrentValue))
                    {
                        m_bCycleEnd = true;
                        m_iFragMasterCycle = 1;
                    }
                }
                else if (!m_bCycleStart && m_bCycleEnd && m_bCycleTrigger)
                {
                    if (iCycleStartCurrentValue != iCycleStartLastValue && IsCycleSatisfied(cCycleStart, iCycleStartCurrentValue))
                    {
                        m_bCycleStart = true;
                        m_bCycleEnd = false;
                        SetEventMessage("", "CycleState,On");
                        SetEventMessage("", ResDDEA.CDDEAGroup_bCycleStart);
                        StartWatch(m_swCycle);
                        sCycleStartLogNote = "Standard CycleStart";
                    }
                }
                else if (m_bCycleStart && !m_bCycleEnd && iCycleEndCurrentValue != iCycleEndLastValue)
                {
                    if (IsCycleSatisfied(cCycleEnd, iCycleEndCurrentValue))
                    {
                        m_bCycleStart = false;
                        m_bCycleEnd = true;
                        SetEventMessage("", "CycleState,Off");
                        long nCycleTime = StopWatch(m_swCycle);
                        if (IsCycleTimeInRange(cFragMasterMode, nCycleTime))
                        {
                            SetEventMessage("", ResDDEA.CDDEAGroup_bCycleEnd_TimeOut_false);
                            m_bCycleTimeOut = false;
                        }
                        else
                        {
                            SetEventMessage("", ResDDEA.CDDEAGroup_bCycleEnd_TimeOut_true);
                            m_bCycleTimeOut = true;
                        }

                        // 정상 Cycle 이면
                        if (m_bCycleTimeOut == false)
                        {
                            if (UEventFragMasterSwitch != null)
                                UEventFragMasterSwitch(this, 9999);     // Cycle 수집 완료

                            m_iFragMasterCycle = 0;
                        }
                        else
                        {
                            if (UEventFragMasterSwitch != null)
                                UEventFragMasterSwitch(this, 8888);     // Cycle 범위 오류

                            m_iFragMasterCycle++;
                            cFragMasterMode.SwitchCurrentCount = 0;
                        }
                    }

                    if (m_bCycleEnd)
                    {
                        sCycleStartLogNote = "";
                        sCycleEndLogNote = "Standard CycleEnd";

                        // Cycle End 되자마자 Cycle Start
                        if (iCycleStartCurrentValue != iCycleStartLastValue)
                        {
                            if (IsCycleSatisfied(cCycleStart, iCycleStartCurrentValue))
                            {
                                m_bCycleStart = true;
                                m_bCycleEnd = false;

                                SetEventMessage("", "CycleState,On");
                                SetEventMessage("", ResDDEA.CDDEAGroup_bCycleStart);
                                StartWatch(m_swCycle);

                                if (cCycleStart.Symbol.Key != cCycleEnd.Symbol.Key)
                                {
                                    sCycleStartLogNote = "Standard CycleStart";
                                }
                                else if (cCycleStart.Condition == cCycleEnd.Condition)
                                {
                                    sCycleStartLogNote = "Standard CycleEnd/Standard CycleStart";
                                }
                            }
                        }
                    }
                }

                if (cCycleTrigger != null && iCycleTriggerCurrentValue != iCycleTriggerLastValue)
                {
                    // 서로 키가 다르면 로그 남김
                    if (cCycleTrigger.Symbol.Key != cCycleStart.Symbol.Key && cCycleTrigger.Symbol.Key != cCycleEnd.Symbol.Key)
                    {
                        CTimeLog cLog = SetFragMasterCycleLog(cCycleTrigger.Symbol.Key, iCycleTriggerCurrentValue, cCurrentPacketData, "");
                        cTotalTimeLogS.Add(cLog);
                    }
                }

                // Cycle 시작과 Cycle 종료 조건의 Key 가 다르므로 각각 로그 처리
                if (cCycleStart.Symbol.Key != cCycleEnd.Symbol.Key)
                {
                    if (iCycleEndCurrentValue != iCycleEndLastValue)
                    {
                        CTimeLog cLog = SetFragMasterCycleLog(cCycleEnd.Symbol.Key, iCycleEndCurrentValue, cCurrentPacketData, sCycleEndLogNote);
                        cTotalTimeLogS.Add(cLog);
                    }

                    if (iCycleStartCurrentValue != iCycleStartLastValue)
                    {
                        CTimeLog cLog = SetFragMasterCycleLog(cCycleStart.Symbol.Key, iCycleStartCurrentValue, cCurrentPacketData, sCycleStartLogNote);
                        cTotalTimeLogS.Add(cLog);
                    }
                }
                // Cycle 시작과 Cycle 종료 조건의 Key 가 같으므로 한 로그로 처리
                else
                {
                    if (iCycleStartCurrentValue != iCycleStartLastValue)
                    {
                        string sNote = sCycleStartLogNote;
                        if (sNote == "")
                            sNote = sCycleEndLogNote;

                        CTimeLog cLog = SetFragMasterCycleLog(cCycleStart.Symbol.Key, iCycleStartCurrentValue, cCurrentPacketData, sNote);
                        cTotalTimeLogS.Add(cLog);
                    }
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_DoFragmentMasterCycleProcess_Exception_Msg + ex.Message + ")");
            }
        }

        protected void DoCycleSwitch(CFragMasterMode cFargMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData)
        {
            if (cCurrentPacketData.PacketCount < 2) return;
            int iValue = GetValue(cCurrentPacketData, cFargMode.SwitchingSymbol);
            int iLastValue = GetValue(cLastPacketData, cFargMode.SwitchingSymbol);

            if (iValue != iLastValue)
            {
                if (cFargMode.SwitchingValue == iValue && m_bCycleStartComplete)
                {
                    if (cFargMode.SwitchCurrentCount == cFargMode.SwitchingCount - 1)
                    {
                        cFargMode.SwitchCurrentCount = 0;

                        if (UEventFragMasterSwitch != null)
                            UEventFragMasterSwitch(this, cCurrentPacketData.GroupNumber);
                    }
                    else
                    {
                        cFargMode.SwitchCurrentCount++;
                    }
                }
            }
        }

        protected void DoFilterNormalCycleProcess(CTimeLogS cTimeLogS)
        {
            bool bCycleStartedNow = false;
            bool bCycleEndedNow = false;
            bool bCycleTimeOut = false;

            if (!m_bFilterNormalCycleTriggered)
            { 
                if (m_sFilterNormalCycleTriggerKey != "")
                {
                    for (int index = 0; index < cTimeLogS.Count; ++index)
                    {
                        CTimeLog ctimeLog = cTimeLogS[index];
                        if (ctimeLog.Key == m_sFilterNormalCycleTriggerKey && ctimeLog.Value == m_iFilterNormalCycleTriggerValue)
                        {
                            m_bFilterNormalCycleTriggered = true;
                            break;
                        }
                    }
                }
                else
                    m_bFilterNormalCycleTriggered = true;
            }

            if (!m_bFilterNormalCycleStarted && m_bFilterNormalCycleTriggered)
            {
                if (m_sFilterNormalCycleStartKey != "")
                {
                    for (int index = 0; index < cTimeLogS.Count; ++index)
                    {
                        CTimeLog ctimeLog = cTimeLogS[index];
                        if (ctimeLog.Key == m_sFilterNormalCycleStartKey && ctimeLog.Value == m_iFilterNormalCycleStartValue)
                        {
                            m_bFilterNormalCycleStarted = true;
                            m_dtFilterNormalCycleStart = DateTime.Now;
                            bCycleStartedNow = true;

                            //Cycle 유효 키가 있으면 
                            if (m_sFilterNormalCycleTagKey != "")
                                m_bFilterNormalCycleValid = false;
                            else
                                m_bFilterNormalCycleValid = true;

                            //각 Cycle 마다 Cycle Trigger 체크 여부
                            if (m_bFilterNormalCycleTriggerOption)
                                m_bFilterNormalCycleTriggered = false;
                            else
                                m_bFilterNormalCycleTriggered = true;

                            break;
                        }
                    }
                }
                else
                {
                    m_bFilterNormalCycleStarted = true;
                    m_dtFilterNormalCycleStart = DateTime.Now;
                    bCycleStartedNow = true;

                    //Cycle 유효 키가 있으면 
                    if (m_sFilterNormalCycleTagKey != "")
                        m_bFilterNormalCycleValid = false;
                    else
                        m_bFilterNormalCycleValid = true;

                    //각 Cycle 마다 Cycle Trigger 여부 체크
                    if (m_bFilterNormalCycleTriggerOption)
                        m_bFilterNormalCycleTriggered = false; //Cycle 마다 체크
                    else
                        m_bFilterNormalCycleTriggered = true;  //한번 Cycle ON 이면 Trigger 체크 안함
                }
            }
            else if (m_bFilterNormalCycleStarted)
            {
                if (m_sFilterNormalCycleStartKey != "")
                {
                    //Cycle 종료 및 재시작 조건 체크                    
                    if (m_iFilterNormalCycleMaxTime > 0)
                    {
                        //시간 체크
                        TimeSpan tsTime = DateTime.Now.Subtract(m_dtFilterNormalCycleStart);
                        if (tsTime.TotalMilliseconds >= m_iFilterNormalCycleMaxTime)
                        {
                            //Cycle 종료
                            m_bFilterNormalCycleStarted = false;
                            bCycleEndedNow = true;
                            bCycleTimeOut = true;

                            //Cycle 유효 키가 있으면 
                            if (m_sFilterNormalCycleTagKey != "")
                                m_bFilterNormalCycleValid = false;
                            else
                                m_bFilterNormalCycleValid = true;

                            //각 Cycle 마다 Cycle Trigger 여부 체크
                            if (m_bFilterNormalCycleTriggerOption)
                                m_bFilterNormalCycleTriggered = false; //Cycle 마다 체크
                            else
                                m_bFilterNormalCycleTriggered = true;  //한번 Cycle ON 이면 Trigger 체크 안함
                        }
                    }

                    //Cycle 재시작 체크
                    if (bCycleTimeOut == false && m_bFilterNormalCycleTriggered)
                    {
                        CTimeLog cLog;
                        for (int i = 0; i < cTimeLogS.Count; i++)
                        {
                            cLog = cTimeLogS[i];
                            if (cLog.Key == m_sFilterNormalCycleStartKey && cLog.Value == m_iFilterNormalCycleStartValue)
                            {
                                m_bFilterNormalCycleStarted = true;
                                m_dtFilterNormalCycleStart = DateTime.Now;
                                bCycleEndedNow = true;
                                bCycleStartedNow = true;

                                //Cycle 유효 키가 있으면 
                                if (m_sFilterNormalCycleTagKey != "")
                                    m_bFilterNormalCycleValid = false;
                                else
                                    m_bFilterNormalCycleValid = true;

                                //각 Cycle 마다 Cycle Trigger 여부 체크
                                if (m_bFilterNormalCycleTriggerOption)
                                    m_bFilterNormalCycleTriggered = false; //Cycle 마다 체크
                                else
                                    m_bFilterNormalCycleTriggered = true;  //한번 Cycle ON 이면 Trigger 체크 안함

                                break;
                            }
                        }
                    }
                }
                else
                {
                    TimeSpan tsTime = DateTime.Now.Subtract(m_dtFilterNormalCycleStart);
                    if (tsTime.TotalMilliseconds >= m_iFilterNormalCycleMaxTime)
                    {
                        m_bFilterNormalCycleStarted = false;
                        bCycleEndedNow = true;

                        if (m_bFilterNormalCycleTriggered)
                        {
                            m_bFilterNormalCycleStarted = true;
                            m_dtFilterNormalCycleStart = DateTime.Now;
                            bCycleStartedNow = true;
                        }

                        //Cycle 유효 키가 있으면 
                        if (m_sFilterNormalCycleTagKey != "")
                            m_bFilterNormalCycleValid = false;
                        else
                            m_bFilterNormalCycleValid = true;

                        //각 Cycle 마다 Cycle Trigger 여부 체크
                        if (m_bFilterNormalCycleTriggerOption)
                            m_bFilterNormalCycleTriggered = false; //Cycle 마다 체크
                        else
                            m_bFilterNormalCycleTriggered = true;  //한번 Cycle ON 이면 Trigger 체크 안함

                    }
                }
            }

            if (m_bFilterNormalCycleStarted && !m_bFilterNormalCycleValid)
            {
                if (m_sFilterNormalCycleTagKey != "")
                {
                    for (int index = 0; index < cTimeLogS.Count; ++index)
                    {
                        CTimeLog ctimeLog = cTimeLogS[index];
                        if (ctimeLog.Key == m_sFilterNormalCycleTagKey && ctimeLog.Value != 0)
                        {
                            m_bFilterNormalCycleValid = true;
                            break;
                        }
                    }
                }
                else
                    m_bFilterNormalCycleValid = true;
            }

            if (bCycleEndedNow)
            {
                if (m_bFilterNormalCycleValid)
                {
                    m_bFilterNormalCycleValid = false;
                    int iCycleNumber = m_iCycleNumber + 1;

                    if (m_iCycleNumber < m_cProject.FilterNormalCycleCount - 1)
                    {
                        ++m_iCycleNumber;
                    }
                    else
                    {
                        m_iCycleNumber = 0;
                        ++m_iPacketNumber;
                    }

                    SetEventMessage("", "CycleState,Off");

                    if (m_sFilterNormalCycleTagKey != "")
                        SetEventMessage("", ResDDEA.CDDEAGroup_sFilterNormalCycle_End_Msg1 + m_sFilterNormalCycleTagKey + ResDDEA.CDDEAGroup_ContactOperate);
                    else
                        SetEventMessage("", ResDDEA.CDDEAGroup_sFilterNormalCycle_End_Msg2);

                    if (UEventCycleChanged != null)
                        UEventCycleChanged((object)this, iCycleNumber);
                }
                else
                {
                    SetEventMessage("", "CycleState,Off");

                    if (m_sFilterNormalCycleTagKey != "")
                        SetEventMessage("", ResDDEA.CDDEAGroup_sFilterNormalCycle_End_Msg3+ m_sFilterNormalCycleTagKey + ResDDEA.CDDEAGroup_ContactInoperate);
                    else
                        SetEventMessage("", ResDDEA.CDDEAGroup_sFilterNormalCycle_End_Msg4);
                }
            }

            if (!bCycleStartedNow)
                return;

            SetEventMessage("", "CycleState,On");
            SetEventMessage("", ResDDEA.CDDEAGroup_bCycleStart);
        }

        protected void DoNormalFirstProcess(CNormalMode cNormalMode, CDDEAPacketData cPacketData)
        {
            CTimeLogS ctimeLogS = new CTimeLogS();
            foreach (KeyValuePair<string, CDDEAReadAddressData> readData in cPacketData.ReadDataList)
            {
                string sBaseAddress = readData.Value.Address;
                List<CDDEASymbol> lstBitEqulSymbol = cNormalMode.BitSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                List<CDDEASymbol> lstWordEqulSymbol = cNormalMode.WordSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                List<CDDEASymbol> lstIndexIncludeEqulSymbol = cNormalMode.IncludeIndexSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);

                WriteNormalBitFirstLogS(lstBitEqulSymbol, readData.Value.Value, cPacketData, ctimeLogS, false);
                WriteNormalWordFirstLogS(lstWordEqulSymbol, readData.Value.Value, cPacketData, ctimeLogS, false);
                WriteIndexLogS(lstIndexIncludeEqulSymbol, (List<CDDEASymbol>)cNormalMode.IndexSymbolList, readData.Value.Value, cPacketData, ctimeLogS);
            }

            if (ctimeLogS.Count <= 0)
                return;

            SendTrackerData(ctimeLogS);
            m_cTask.EventDataChanged(ctimeLogS);
        }

        protected void DoNormalProcess(CNormalMode cNormalMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData)
        {
            //jjk, 19.05.20 - 시간 생성시 다른 Thread 접근 제어
            m_Mutex.WaitOne();
            CTimeLogS cEventTimeLogS = new CTimeLogS();

            foreach (KeyValuePair<string, CDDEAReadAddressData> readData in cCurrentPacketData.ReadDataList)
            {
                string sBaseAddress = readData.Value.Address;
                int iNowValue = readData.Value.Value;
                int iLastValue = cLastPacketData.ReadDataList[sBaseAddress].Value;

                List<CDDEASymbol> lstWordEqulSymbol = cNormalMode.WordSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                WriteNormalWordLogS(lstWordEqulSymbol, iNowValue, cCurrentPacketData, cLastPacketData, cEventTimeLogS, false);
                if (iNowValue == iLastValue)
                    continue;

                List<CDDEASymbol> lstBitEqulSymbol = cNormalMode.BitSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                List<CDDEASymbol> lstIndexIncludeEqulSymbol = cNormalMode.IncludeIndexSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                //List<CDDEASymbol> lstIndexEqulSymbol = cNormal.IndexSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);

                WriteNormalBitLogS(lstBitEqulSymbol, iNowValue, iLastValue, cCurrentPacketData, cEventTimeLogS, false);
                WriteIndexLogS(lstIndexIncludeEqulSymbol, cNormalMode.IndexSymbolList, readData.Value.Value, cCurrentPacketData, cEventTimeLogS);
            }

            if (cEventTimeLogS.Count <= 0)
                return;

            SendTrackerData(cEventTimeLogS);
            m_cTask.EventDataChanged(cEventTimeLogS);
            m_Mutex.ReleaseMutex();
        }

        protected void DoFilterNormalFirstProcess(CNormalMode cNormalMode, CDDEAPacketData cPacketData)
        {
            //jjk, 19.05.20 - 시간 생성시 다른 Thread 접근 제어
            m_Mutex.WaitOne();
            // DoNormalFirstProcess 와 거의 동일

            CTimeLogS cEventTimeLogS = new CTimeLogS();
            foreach (var who in cPacketData.ReadDataList)
            {
                string sBaseAddress = who.Value.Address;

                List<CDDEASymbol> lstBitEqulSymbol = cNormalMode.BitSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                List<CDDEASymbol> lstWordEqulSymbol = cNormalMode.WordSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                List<CDDEASymbol> lstIndexIncludeEqulSymbol = cNormalMode.IncludeIndexSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                //List<CDDEASymbol> lstIndexEqulSymbol = cNormalMode.IndexSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);

                WriteNormalBitFirstLogS(lstBitEqulSymbol, who.Value.Value, cPacketData, cEventTimeLogS, false);
                WriteNormalWordFirstLogS(lstWordEqulSymbol, who.Value.Value, cPacketData, cEventTimeLogS, false);
                WriteIndexLogS(lstIndexIncludeEqulSymbol, cNormalMode.IndexSymbolList, who.Value.Value, cPacketData, cEventTimeLogS);
            }
            if (cEventTimeLogS.Count > 0)
            {
                //kch@udmtek, 17.03.06, Cycle Trigger 한번만 체크                
                if (m_sFilterNormalCycleTriggerKey != "" && m_bFilterNormalCycleTriggerOption == false)
                {
                    CTimeLog cLog;
                    for (int i = 0; i < cEventTimeLogS.Count; i++)
                    {
                        cLog = cEventTimeLogS[i];
                        if (cLog.Key == m_sFilterNormalCycleTriggerKey && cLog.Value == m_iFilterNormalCycleTriggerValue)
                        {
                            m_bFilterNormalCycleTriggered = true;
                            break;
                        }
                    }
                }

                SendTrackerData(cEventTimeLogS);
                m_cTask.EventDataChanged(cEventTimeLogS);
            }
            m_Mutex.ReleaseMutex();
        }

        protected void DoFilterNormalProcess(CNormalMode cNormalMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData)
        {
            //jjk, 19.05.20 - 시간 생성시 다른 Thread 접근 제어
            m_Mutex.WaitOne();
            //yjk, 18.07.31 - 예외 조건
            if (cCurrentPacketData.ReadDataList == null)
                return;

            // DoNormalProcess 와 거의 동일
            CTimeLogS cEventTimeLogS = new CTimeLogS();
            foreach (var who in cCurrentPacketData.ReadDataList)
            {
                string sBaseAddress = who.Value.Address;
                int iNowValue = who.Value.Value;
                int iLastValue = cLastPacketData.ReadDataList[sBaseAddress].Value;

                List<CDDEASymbol> lstWordEqulSymbol = cNormalMode.WordSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                WriteNormalWordLogS(lstWordEqulSymbol, iNowValue, cCurrentPacketData, cLastPacketData, cEventTimeLogS, true);
                if (iNowValue == iLastValue)
                    continue;

                List<CDDEASymbol> lstBitEqulSymbol = cNormalMode.BitSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                List<CDDEASymbol> lstIndexIncludeEqulSymbol = cNormalMode.IncludeIndexSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);
                //List<CDDEASymbol> lstIndexEqulSymbol = cNormal.IndexSymbolList.FindEqulBaseAddressSymbol(sBaseAddress);

                WriteNormalBitLogS(lstBitEqulSymbol, iNowValue, iLastValue, cCurrentPacketData, cEventTimeLogS, true);
                WriteIndexLogS(lstIndexIncludeEqulSymbol, cNormalMode.IndexSymbolList, who.Value.Value, cCurrentPacketData, cEventTimeLogS);
            }

            //kch@udmtek, 17.02.28
            // 설비의 동작 여부를 FilterNormalCycleTagKey 로그 카운트로 확인
            DoFilterNormalCycleProcess(cEventTimeLogS);

            if (cEventTimeLogS.Count > 0)
            {
                SendTrackerData(cEventTimeLogS);
                m_cTask.EventDataChanged(cEventTimeLogS);
            }
            m_Mutex.ReleaseMutex();
        }

        protected bool DoFragmetFirstProcess(CFragMode cFragmentMode, CDDEAPacketData cPacketData)
        {
            CTimeLogS cEventTimeLogS = new CTimeLogS();

            if (GetRecipeValue(cPacketData))
            {
                if (m_cProject.MasterRecipeValue != 0)
                {
                    if (m_cProject.MasterRecipeValue != cPacketData.RecipeReadValue)
                    {
                        string empty = string.Empty;
                        SetEventMessage("", string.Format("Target Recipe = [{0}], Current Recipe = [{1}] ... Wait.. {2}", (object)m_cProject.MasterRecipeValue, (object)cPacketData.RecipeReadValue, (object)MethodBase.GetCurrentMethod().Name.ToString()));
                        SetEventMessage("", "RecipeValue," + (object)cPacketData.RecipeReadValue + "," + (object)m_cProject.MasterRecipeValue);
                        if (m_iLastestRecipe != cPacketData.RecipeReadValue)
                        {
                            m_iLastestRecipe = cPacketData.RecipeReadValue;
                            SetEventMessage("", ResDDEA.CDDEAGroup_cPackerData_RecipeWait+ (object)cPacketData.RecipeReadValue);
                            SetEventMessage("", string.Format("RecipeValue,{0},{1},Recipe", (object)cPacketData.RecipeReadValue, (object)m_cProject.MasterRecipeValue));
                            SetEventMessage("", "**RecipeError**");
                        }
                        return false;
                    }
                    SetEventMessage("", "**RecipeOK**");
                }
                if (cFragmentMode.CycleConditionSymbolS.RecipeValue != 0 && cFragmentMode.CycleConditionSymbolS.RecipeValue != cPacketData.RecipeReadValue)
                {
                    SetEventMessage("", "RecipeValue," + (object)cPacketData.RecipeReadValue);
                    SetEventMessage("", ResDDEA.CDDEAGroup_cPackerData_CurrentRecipe + (object)cPacketData.RecipeReadValue);
                    cFragmentMode.CycleConditionSymbolS.RecipeValue = cPacketData.RecipeReadValue;
                }
            }
            else if (m_cProject.MasterRecipeValue != 0)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_cPackerData_NotfoundRecipe);
                SetEventMessage("", "**RecipeNFD**");
                return false;
            }
            return true;
        }

        protected bool DoFragmentProcess(CFragMode cFragmentMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData)
        {
            CTimeLogS ctimeLogS = new CTimeLogS();
            bool bRecipeError = false;
            if (GetRecipeValue(cCurrentPacketData))
            {
                if (m_cProject.MasterRecipeValue != 0)
                {
                    if (m_cProject.MasterRecipeValue != cCurrentPacketData.RecipeReadValue)
                    {
                        string empty = string.Empty;
                        SetEventMessage("", string.Format("Target Recipe = [{0}], Current Recipe = [{1}] ... Wait.. {2}", (object)m_cProject.MasterRecipeValue, (object)cCurrentPacketData.RecipeReadValue, (object)MethodBase.GetCurrentMethod().Name.ToString()));
                        SetEventMessage("", string.Format("RecipeValue,{0},{1},Recipe", (object)cCurrentPacketData.RecipeReadValue, (object)m_cProject.MasterRecipeValue));
                        if (m_iLastestRecipe != cCurrentPacketData.RecipeReadValue)
                        {
                            m_iLastestRecipe = cCurrentPacketData.RecipeReadValue;
                            SetEventMessage("", ResDDEA.CDDEAGroup_cCurrentpacketData_RecipeWait + (object)cCurrentPacketData.RecipeReadValue);
                            SetEventMessage("", string.Format("RecipeValue,{0},{1},Recipe", (object)cCurrentPacketData.RecipeReadValue, (object)m_cProject.MasterRecipeValue));
                            SetEventMessage("", "**RecipeError**");
                        }
                        if (m_cProject.MasterRecipeValue != cCurrentPacketData.RecipeReadValue && m_emCollectMode == EMCollectMode.Frag)
                            ;
                        return false;
                    }
                    SetEventMessage("", "**RecipeOK**");
                }
                if (cFragmentMode.CycleConditionSymbolS.RecipeValue != 0 && cFragmentMode.CycleConditionSymbolS.RecipeValue != cCurrentPacketData.RecipeReadValue)
                {
                    SetEventMessage("", "RecipeValue," + (object)cFragmentMode.CycleConditionSymbolS.RecipeValue);
                    SetEventMessage("", ResDDEA.CDDEAGroup_cCurrentpacketData_CurrentRecipe + (object)cCurrentPacketData.RecipeReadValue);
                    cFragmentMode.CycleConditionSymbolS.RecipeValue = cCurrentPacketData.RecipeReadValue;
                }
            }
            else if (m_cProject.MasterRecipeValue != 0)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_cCurrentpacketData_NotfoundRecipe);
                SetEventMessage("", "**RecipeNFD**");
            }
            if (!bRecipeError)
                DoFragmentCycleProcess(cFragmentMode, cCurrentPacketData, cLastPacketData, ctimeLogS);
            foreach (KeyValuePair<string, CDDEAReadAddressData> readData in cCurrentPacketData.ReadDataList)
            {
                string address = readData.Value.Address;
                int iValue = readData.Value.Value;
                int iLastValue = cLastPacketData.ReadDataList[address].Value;
                WriteFragWordLogS(cFragmentMode.WordSymbolList.FindEqulBaseAddressSymbol(address), iValue, cCurrentPacketData, cLastPacketData, ctimeLogS);
                if (iValue != iLastValue)
                {
                    List<CDDEASymbol> baseAddressSymbol1 = cFragmentMode.BitSymbolList.FindEqulBaseAddressSymbol(address);
                    List<CDDEASymbol> baseAddressSymbol2 = cFragmentMode.IncludeIndexSymbolList.FindEqulBaseAddressSymbol(address);
                    cFragmentMode.IndexSymbolList.FindEqulBaseAddressSymbol(address);
                    WriteFragBitLogS(baseAddressSymbol1, iValue, iLastValue, cCurrentPacketData, cFragmentMode, ctimeLogS, bRecipeError);
                    WriteIndexLogS(baseAddressSymbol2, (List<CDDEASymbol>)cFragmentMode.IndexSymbolList, readData.Value.Value, cCurrentPacketData, ctimeLogS);
                }
            }
            if (ctimeLogS.Count > 0)
            {
                SendTrackerData(ctimeLogS);
                m_cTask.EventDataChanged(ctimeLogS);
            }
            return true;
        }

        protected bool DoFragmentMasterProcess(CFragMasterMode cFragMasterMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData)
        {
            try
            {
                CTimeLogS ctimeLogS = new CTimeLogS();
                bool bRecipeError = false;
                if (GetRecipeValue(cCurrentPacketData))
                {
                    if (m_cProject.MasterRecipeValue != 0)
                    {
                        if (m_cProject.MasterRecipeValue != cCurrentPacketData.RecipeReadValue)
                        {
                            SetEventMessage("", string.Format("Target Recipe = [{0}], Current Recipe = [{1}] ... Wait.. {2}", (object)m_cProject.MasterRecipeValue, (object)cCurrentPacketData.RecipeReadValue, (object)MethodBase.GetCurrentMethod().Name.ToString()));
                            SetEventMessage("", string.Format("RecipeValue,{0},{1},Recipe", (object)cCurrentPacketData.RecipeReadValue, (object)m_cProject.MasterRecipeValue));
                            if (m_iLastestRecipe != cCurrentPacketData.RecipeReadValue)
                            {
                                m_iLastestRecipe = cCurrentPacketData.RecipeReadValue;
                                SetEventMessage("", ResDDEA.CDDEAGroup_cPackerData_RecipeWait + (object)cCurrentPacketData.RecipeReadValue);
                                SetEventMessage("", string.Format("RecipeValue,{0},{1},Recipe", (object)cCurrentPacketData.RecipeReadValue, (object)m_cProject.MasterRecipeValue));
                                SetEventMessage("", "**RecipeError**");
                            }
                            if (m_cProject.MasterRecipeValue != cCurrentPacketData.RecipeReadValue)
                                bRecipeError = true;
                            return false;
                        }
                        SetEventMessage("", "**RecipeOK**");
                    }
                    if (cFragMasterMode.CycleConditionSymbolS.RecipeValue != 0 && cFragMasterMode.CycleConditionSymbolS.RecipeValue != cCurrentPacketData.RecipeReadValue)
                    {
                        SetEventMessage("", "RecipeValue," + (object)cCurrentPacketData.RecipeReadValue);
                        SetEventMessage("", ResDDEA.CDDEAGroup_cPackerData_CurrentRecipe + (object)cCurrentPacketData.RecipeReadValue);
                        cFragMasterMode.CycleConditionSymbolS.RecipeValue = cCurrentPacketData.RecipeReadValue;
                    }
                }
                if (!bRecipeError)
                {
                    DoFragmentMasterCycleProcess(cFragMasterMode, cCurrentPacketData, cLastPacketData, ctimeLogS);
                    if (m_cProject.FragMasterBundleList.Count > 1)
                        DoCycleSwitch(cFragMasterMode, cCurrentPacketData, cLastPacketData);
                }
                foreach (KeyValuePair<string, CDDEAReadAddressData> readData in cCurrentPacketData.ReadDataList)
                {
                    string address = readData.Value.Address;
                    int iValue = readData.Value.Value;
                    int iLastValue = cLastPacketData.ReadDataList[address].Value;
                    if (iValue != iLastValue)
                        WriteFragMasterBitLogS(cFragMasterMode.BitSymbolList.FindEqulBaseAddressSymbol(address), iValue, iLastValue, cCurrentPacketData, cFragMasterMode, ctimeLogS, bRecipeError);
                }
                if (ctimeLogS.Count > 0)
                {
                    SendTrackerData(ctimeLogS);
                    m_cTask.EventDataChanged(ctimeLogS);
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_DoFragmentMasterProces_Exception_Msg + ex.Message + ")");
            }
            return true;
        }

        protected void DoLOBFirstProcess(CLOBMode cLOBMode, CDDEAPacketData cPacketData)
        {
            CTimeLogS ctimeLogS = new CTimeLogS();
            GetRecipeValue(cPacketData);
            GetProcessValue(cPacketData, cLOBMode.ProcessSymbol);
            GetGlassID(cPacketData, cLOBMode.GlassIDSymbolList);
            GetProcessID(cPacketData, cLOBMode.ProcessIDSymbolList);
            foreach (KeyValuePair<string, CDDEAReadAddressData> readData in cPacketData.ReadDataList)
            {
                string address = readData.Value.Address;
                List<CDDEASymbol> baseAddressSymbol1 = cLOBMode.BitSymbolList.FindEqulBaseAddressSymbol(address);
                List<CDDEASymbol> baseAddressSymbol2 = cLOBMode.WordSymbolList.FindEqulBaseAddressSymbol(address);
                List<CDDEAMdcSymbol> baseAddressSymbol3 = cLOBMode.MDCSymbolList.FindEqulBaseAddressSymbol(address);
                WriteLOBFirstBitLogS(baseAddressSymbol1, readData.Value.Value, cPacketData, ctimeLogS);
                WriteNormalWordFirstLogS(baseAddressSymbol2, readData.Value.Value, cPacketData, ctimeLogS, false);
                WriteLOBFirstMdcLogS(baseAddressSymbol3, readData.Value.Value, cPacketData, ctimeLogS);
            }
            if (ctimeLogS.Count <= 0)
                return;
            SendTrackerData(ctimeLogS);
            m_cTask.EventDataChanged(ctimeLogS);
        }

        protected void DoLOBProcess(CLOBMode cLOBMode, CDDEAPacketData cCurrentPacketData, CDDEAPacketData cLastPacketData)
        {
            CTimeLogS ctimeLogS = new CTimeLogS();
            GetRecipeValue(cCurrentPacketData);
            GetProcessValue(cCurrentPacketData, cLOBMode.ProcessSymbol);
            GetGlassID(cCurrentPacketData, cLOBMode.GlassIDSymbolList);
            GetProcessID(cCurrentPacketData, cLOBMode.ProcessIDSymbolList);
            CheckRefreshSymbol(cCurrentPacketData, cLOBMode.RefreshSymbol, cLOBMode.TactTimeSymbol);
            foreach (KeyValuePair<string, CDDEAReadAddressData> readData in cCurrentPacketData.ReadDataList)
            {
                string address = readData.Value.Address;
                int iValue = readData.Value.Value;
                int num = cLastPacketData.ReadDataList[address].Value;
                WriteLOBMdcLogS(cLOBMode.MDCSymbolList.FindEqulBaseAddressSymbol(address), cCurrentPacketData, cLastPacketData, ctimeLogS);
                if (iValue != num)
                {
                    WriteLOBWordLogS(cLOBMode.WordSymbolList.FindEqulBaseAddressSymbol(address), iValue, cCurrentPacketData, cLastPacketData, ctimeLogS, false);
                    WriteLOBBitLogS(cLOBMode.BitSymbolList.FindEqulBaseAddressSymbol(address), cCurrentPacketData, cLastPacketData, ctimeLogS);
                }
            }
            if (ctimeLogS.Count <= 0)
                return;
            SendTrackerData(ctimeLogS);
            m_cTask.EventDataChanged(ctimeLogS);
        }

        private void UpdatePacketData(CDDEAPacketData cLastPacketData, CDDEAPacketData cNewPacketData)
        {
            if (cLastPacketData == null)
                cLastPacketData = new CDDEAPacketData();
            cLastPacketData.ReadDataList.Clear();
            cLastPacketData.ReadDataList = new Dictionary<string, CDDEAReadAddressData>();
            foreach (KeyValuePair<string, CDDEAReadAddressData> readData in cNewPacketData.ReadDataList)
            {
                CDDEAReadAddressData cddeaReadAddressData = (CDDEAReadAddressData)readData.Value.Clone();
                cLastPacketData.ReadDataList.Add(readData.Key, cddeaReadAddressData);
            }
            cLastPacketData.GroupNumber = cNewPacketData.GroupNumber;
            cLastPacketData.CycleNumber = cNewPacketData.CycleNumber;
            cLastPacketData.Time = cNewPacketData.Time;
            cLastPacketData.PacketAddress = cNewPacketData.PacketAddress;
            cLastPacketData.PacketCount = cNewPacketData.PacketCount;
            cLastPacketData.PacketValues = cNewPacketData.PacketValues == null ? cNewPacketData.PacketValues : (int[])cNewPacketData.PacketValues.Clone();
            cLastPacketData.FilterRead = cNewPacketData.FilterRead;
            cLastPacketData.FragMasterRead = cNewPacketData.FragMasterRead;
            cLastPacketData.ModelReadString = cNewPacketData.ModelReadString;
            cLastPacketData.GlassIDReadString = cNewPacketData.GlassIDReadString;
            cLastPacketData.RecipeReadValue = cNewPacketData.RecipeReadValue;
            cLastPacketData.ProcessReadValue = cNewPacketData.ProcessReadValue;
            cLastPacketData.ProcessIDReadString = cNewPacketData.ProcessIDReadString;
            cLastPacketData.TactTimeReadValue = cNewPacketData.TactTimeReadValue;
            cLastPacketData.NoteReadString = cNewPacketData.NoteReadString;
        }

        protected override bool BeforeRun()
        {
            if (m_cProject == null)
                return false;

            ClearQue();

            m_dtFilterNormalCycleStart = DateTime.MinValue;
            m_bFilterNormalCompFlag = false;
            m_bFilterNormalCycleStarted = false;
            m_bFilterNormalCycleTriggered = false;
            m_bFilterNormalCycleValid = false;

            m_sFilterNormalCycleTagKey = m_cProject.FilterNormalCycleTagKey;
            m_sFilterNormalCycleStartKey = m_cProject.FilterNormalCycleStartKey;
            m_sFilterNormalCycleTriggerKey = m_cProject.FilterNormalCycleTriggerKey;
            m_iFilterNormalCycleMaxTime = m_cProject.FilterNormalCycleTime;
            m_iFilterNormalCycleStartValue = m_cProject.FilterNormalCycleStartValue;
            m_iFilterNormalCycleTriggerValue = m_cProject.FilterNormalCycleTriggerValue;
            m_bFilterNormalCycleTriggerOption = m_cProject.FilterNormalCycleTriggerOption;

            m_iCycleNumber = 0;
            m_iPacketNumber = 0;

            m_cPacketDataS.Clear();
            m_cFilterPacketDataS.Clear();

            GetParameter();

            m_iCycleBaseCount = m_cProject.CycleCount;

            m_bRun = BrforeRunCollectMode();

            if (!m_bRun)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_BeforeRun_Msg1);
                SetEventMessage("", "StartError,");
                return false;
            }

            if (m_cProject.ConnectApp != EMConnectAppType.Tracker)
            {
                m_cTask = new CDDEATask((CDDEAProject_V3)m_cProject);
                m_cTask.UEventMessage += new UEventHandlerMainMessage(m_cTask_UEventMessage);
                m_bRun = m_cTask.Run();

                if (!m_bRun)
                {
                    SetEventMessage("", ResDDEA.CDDEAGroup_BeforeRun_Msg2);
                    SetEventMessage("", "StartError,");
                }
            }
            return m_bRun;
        }

        protected override bool AfterRun()
        {
            return true;
        }

        protected override bool BeforeStop()
        {
            if (m_cQue.Count > 0)
                ClearQue();

            try
            {
                if (m_cTask != null)
                {
                    m_cTask.Stop();
                    m_cTask.UEventMessage -= new UEventHandlerMainMessage(m_cTask_UEventMessage);
                    m_cTask = (CDDEATask)null;
                }
                if (m_cProject != null && m_cProject.FragBundleList != null)
                {
                    for (int index = 0; index < m_cProject.FragBundleList.Count; ++index)
                        m_cProject.FragBundleList[index].CycleConditionSymbolS.ClearComplateFlag();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            m_bRun = false;

            return true;
        }

        protected override bool AfterStop()
        {
            return true;
        }

        protected override void DoThreadWork()
        {
            CDDEAReadAddressData[] cLastData = new CDDEAReadAddressData[m_cPacketDataS.Count];

            Dictionary<int, CDDEAPacketData> dicDDEAData = new Dictionary<int, CDDEAPacketData>();
            Dictionary<int, CDDEAPacketData> dicFragMasterData = new Dictionary<int, CDDEAPacketData>();

            try
            {
                while (m_bRun)
                {

                    if (m_cProject.ConnectApp != EMConnectAppType.Profiler)
                        Thread.Sleep(1);

                    if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil)
                    {
                        if (m_bFragCompFlag && !m_bFragRecipeErrorFlag)
                        {
                            SetEventMessage("", "FragComp,");
                            m_cQue.Clear();
                            m_cTask.Stop();
                            m_cTask.UEventMessage -= new UEventHandlerMainMessage(m_cTask_UEventMessage);
                            m_cTask = (CDDEATask)null;
                            break;
                        }
                    }

                    if (m_emCollectMode == EMCollectMode.FilterNormal && m_bFilterNormalCompFlag)
                    {
                        SetEventMessage("", "CycleState,Off");
                        SetEventMessage("", "FilterNormalComp,");
                        m_cQue.Clear();
                        m_cTask.Stop();
                        m_cTask.UEventMessage -= new UEventHandlerMainMessage(m_cTask_UEventMessage);
                        m_cTask = (CDDEATask)null;
                        break;
                    }

                    if (m_cQue.Count > 0)
                    {
                        CDDEAPacketData cPacketData = m_cQue.DeQue();

                        if (cPacketData != null)
                        {
                            if (!m_cConfig.TimerReadType.ToString().Equals(EMTimerReadType.TN.ToString()))
                                cPacketData.SetCollectDataParsing(m_cConfig.TimerReadType.ToString());
                            else
                                cPacketData.SetValueParsing();

                            if (m_emCollectMode == EMCollectMode.StandardCoil)
                                m_cProject.MasterRecipeValue = 0;

                            DateTime time;

                            //yjk, 20.02.12 - 파라미터 모드 조건 추가
                            if (m_emCollectMode == EMCollectMode.Normal || m_emCollectMode == EMCollectMode.ParameterNormal)
                            {
                                if (!dicDDEAData.ContainsKey(cPacketData.GroupNumber))
                                {
                                    dicDDEAData.Add(cPacketData.GroupNumber, cPacketData);
                                    DoNormalFirstProcess(m_cProject.NormalBundleList[cPacketData.GroupNumber], cPacketData);
                                }
                                else
                                {
                                    if (dicDDEAData[cPacketData.GroupNumber].Time == cPacketData.Time)
                                    {
                                        CDDEAPacketData cddeaPacketData2 = cPacketData;
                                        time = cPacketData.Time;
                                        DateTime dateTime = time.AddMilliseconds(5.0);
                                        cddeaPacketData2.Time = dateTime;
                                    }

                                    DoNormalProcess(m_cProject.NormalBundleList[cPacketData.GroupNumber], cPacketData, dicDDEAData[cPacketData.GroupNumber]);
                                    dicDDEAData[cPacketData.GroupNumber] = cPacketData;
                                }
                            }
                            else if (m_emCollectMode == EMCollectMode.FilterNormal)
                            {
                                if (!dicDDEAData.ContainsKey(cPacketData.GroupNumber))
                                {
                                    dicDDEAData.Add(cPacketData.GroupNumber, cPacketData);
                                    DoFilterNormalFirstProcess(m_cProject.FilterNormalBundleList[cPacketData.GroupNumber], cPacketData);
                                }
                                else if (cPacketData.GroupNumber != m_iPacketNumber || cPacketData.CycleNumber != m_iCycleNumber)
                                {
                                    m_dtFilterNormalCycleStart = DateTime.Now;
                                }
                                else
                                {
                                    if (dicDDEAData[cPacketData.GroupNumber].Time == cPacketData.Time)
                                    {
                                        CDDEAPacketData cddeaPacketData2 = cPacketData;
                                        time = cPacketData.Time;

                                        DateTime dateTime = time.AddMilliseconds(5.0);
                                        cddeaPacketData2.Time = dateTime;
                                    }

                                    DoFilterNormalProcess(m_cProject.FilterNormalBundleList[cPacketData.GroupNumber], cPacketData, dicDDEAData[cPacketData.GroupNumber]);
                                    dicDDEAData[cPacketData.GroupNumber] = cPacketData;
                                }
                            }
                            else if (m_emCollectMode == EMCollectMode.Frag || m_emCollectMode == EMCollectMode.StandardCoil)
                            {
                                if (cPacketData.FragMasterRead)
                                {
                                    if (!m_bFragMasterCycleComp)
                                    {
                                        if (!dicFragMasterData.ContainsKey(cPacketData.GroupNumber))
                                        {
                                            GetRecipeValue(cPacketData);
                                            if (m_cProject.MasterRecipeValue != 0)
                                            {
                                                if (cPacketData.RecipeReadValue == m_cProject.MasterRecipeValue)
                                                {
                                                    dicFragMasterData.Add(cPacketData.GroupNumber, cPacketData);
                                                    SetEventMessage("", ResDDEA.CDDEAGroup_RecipeMatch);
                                                }
                                                else if (m_iLastestRecipe != cPacketData.RecipeReadValue)
                                                {
                                                    m_iLastestRecipe = cPacketData.RecipeReadValue;

                                                    SetEventMessage("", ResDDEA.CDDEAGroup_cPacketData_RecipeWait + (object)cPacketData.RecipeReadValue);
                                                    SetEventMessage("", string.Format("RecipeValue,{0},{1},Recipe", (object)cPacketData.RecipeReadValue, (object)m_cProject.MasterRecipeValue));
                                                }
                                            }
                                            else
                                            {
                                                if (m_cProject.FragMasterBundleList[cPacketData.GroupNumber].CycleConditionSymbolS.RecipeValue != cPacketData.RecipeReadValue)
                                                {
                                                    SetEventMessage("", "RecipeValue," + (object)cPacketData.RecipeReadValue);
                                                    m_cProject.FragMasterBundleList[cPacketData.GroupNumber].CycleConditionSymbolS.RecipeValue = cPacketData.RecipeReadValue;
                                                }

                                                dicFragMasterData.Add(cPacketData.GroupNumber, cPacketData);
                                            }
                                        }
                                        else
                                        {
                                            if (dicFragMasterData[cPacketData.GroupNumber].Time == cPacketData.Time)
                                            {
                                                CDDEAPacketData cddeaPacketData2 = cPacketData;
                                                time = cPacketData.Time;
                                                DateTime dateTime = time.AddMilliseconds(5.0);
                                                cddeaPacketData2.Time = dateTime;
                                            }
                                            if (m_bFragMasterRecycle)
                                            {
                                                dicFragMasterData.Clear();
                                                m_bFragMasterRecycle = false;
                                            }
                                            if (DoFragmentMasterProcess(m_cProject.FragMasterBundleList[cPacketData.GroupNumber], cPacketData, dicFragMasterData[cPacketData.GroupNumber]))
                                            {
                                                dicFragMasterData[cPacketData.GroupNumber] = (CDDEAPacketData)cPacketData.Clone();
                                            }
                                            else
                                            {
                                                dicFragMasterData.Clear();
                                                m_bCycleStart = false;
                                                m_bCycleTrigger = false;
                                                m_bCycleStartComplete = false;
                                                SetEventMessage("", ResDDEA.CDDEAGroup_cPacketData_RecipeDifferent_Msg1);
                                            }
                                        }
                                    }
                                }
                                else if (!dicDDEAData.ContainsKey(cPacketData.GroupNumber))
                                {
                                    if (DoFragmetFirstProcess(m_cProject.FragBundleList[cPacketData.GroupNumber], cPacketData))
                                    {
                                        dicDDEAData.Add(cPacketData.GroupNumber, (CDDEAPacketData)cPacketData.Clone());
                                        if (m_bRecipeRestart)
                                        {
                                            m_bRecipeRestart = false;
                                            if (m_iCycleNumber != 0)
                                            {
                                                ++m_iCycleNumber;
                                                SetEventMessage("", ResDDEA.CDDEAGroup_cPacketData_CycleChanged);
                                            }
                                        }
                                        else
                                            SetEventMessage("", m_iPacketNumber.ToString() + ResDDEA.CDDEAGroup_cPacketData_PacketLeave);
                                    }
                                }
                                else
                                {
                                    if (dicDDEAData[cPacketData.GroupNumber].Time == cPacketData.Time)
                                    {
                                        CDDEAPacketData cddeaPacketData2 = cPacketData;
                                        time = cPacketData.Time;
                                        DateTime dateTime = time.AddMilliseconds(5.0);
                                        cddeaPacketData2.Time = dateTime;
                                    }
                                    if (DoFragmentProcess(m_cProject.FragBundleList[cPacketData.GroupNumber], cPacketData, dicDDEAData[cPacketData.GroupNumber]))
                                    {
                                        dicDDEAData[cPacketData.GroupNumber] = (CDDEAPacketData)cPacketData.Clone();
                                    }
                                    else
                                    {
                                        dicDDEAData.Remove(cPacketData.GroupNumber);
                                        m_bCycleStart = false;
                                        m_bCycleTrigger = false;
                                        m_bCycleStartComplete = false;
                                        SetEventMessage("", ResDDEA.CDDEAGroup_cPacketData_RecipeDifferent_Msg1);
                                        m_bRecipeRestart = true;
                                    }
                                }
                            }
                            else if (m_emCollectMode == EMCollectMode.LOB)
                            {
                                if (!dicDDEAData.ContainsKey(cPacketData.GroupNumber))
                                {
                                    dicDDEAData.Add(cPacketData.GroupNumber, cPacketData);
                                    DoLOBFirstProcess(m_cProject.LOBBundleList[cPacketData.GroupNumber], cPacketData);
                                }
                                else
                                {
                                    if (dicDDEAData[cPacketData.GroupNumber].Time == cPacketData.Time)
                                    {
                                        CDDEAPacketData cddeaPacketData2 = cPacketData;
                                        time = cPacketData.Time;
                                        DateTime dateTime = time.AddMilliseconds(5.0);
                                        cddeaPacketData2.Time = dateTime;
                                    }
                                    DoLOBProcess(m_cProject.LOBBundleList[cPacketData.GroupNumber], cPacketData, dicDDEAData[cPacketData.GroupNumber]);
                                    dicDDEAData[cPacketData.GroupNumber] = cPacketData;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetEventMessage("", ResDDEA.CDDEAGroup_DoThreadWork_Exception_Msg + ex.Message + ")");
                SetEventMessage("", "StartError,");
            }
        }

        private void m_cTask_UEventMessage(object sender, string sSender, string sMessage)
        {
            if (UEventGroupMessage == null)
                return;

            SetEventMessage(sSender, sMessage);
        }
    }
}