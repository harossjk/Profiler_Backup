using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UDM.DDEA
{
    public class CDDEAPacketData : ICloneable
    {
        #region Member Variables

        protected Dictionary<string, CDDEAReadAddressData> m_lstData = new Dictionary<string,CDDEAReadAddressData>();
        protected string m_sPacketAddress = string.Empty;
        protected int m_iPacketCount = 0;
        protected int m_iGroupNumber = 0;
        protected int m_iCycleNumber = 1;
        protected int[] m_iaPacketValue = null;
        protected string[] m_saAddressList = null;
        protected bool m_bFilterRead = false;
        protected bool m_bFragMasterRead = false;
        protected DateTime m_dtTime = DateTime.MinValue;
        
        protected int m_iRecipeValue = -1;
        protected string m_sGlassID = "";
        protected string m_sProcessID = "";
        protected string m_sModel = "";
        protected int m_iTactTimeValue = -1;
        protected int m_iProcessValue = -1;
        protected string m_sNoteString = "";

        #endregion


        #region Initialize/Dispose

        public CDDEAPacketData()
        {

        }

        #endregion


        #region Public Properties
        
        /// <summary>
        /// 보낼 주소의 상세 정보표시
        /// Dword일 때는 시작주소부터 Addres Count가 있는 것까지 값을 갖고 있음.
        /// 키는 주소
        /// </summary>
        public Dictionary<string, CDDEAReadAddressData> ReadDataList
        {
            get { return m_lstData; }
            set { m_lstData = value; }
        }

        /// <summary>
        /// 수집시 PLC로 보낼 주소 묶음
        /// </summary>
        public string PacketAddress
        {
            get { return m_sPacketAddress; }
            set { m_sPacketAddress = value; }
        }

        /// <summary>
        /// 묶음 주소의 갯수
        /// </summary>
        public int PacketCount
        {
            get { return m_iPacketCount; }
            set { m_iPacketCount = value; }
        }

        /// <summary>
        /// 수집 후 결과를 담음
        /// </summary>
        public int[] PacketValues
        {
            get { return m_iaPacketValue; }
            set { m_iaPacketValue = value; }
        }

        /// <summary>
        /// PLC로 수집후 받은시각
        /// </summary>
        public DateTime Time
        {
            get { return m_dtTime; }
            set { m_dtTime = value; }
        }

        /// <summary>
        /// 몇번째 그룹인지 표시
        /// Fragment Mode
        /// </summary>
        public int GroupNumber
        {
            get { return m_iGroupNumber; }
            set { m_iGroupNumber = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CycleNumber
        {
            get { return m_iCycleNumber; }
            set { m_iCycleNumber = value; }
        }

        /// <summary>
        /// 수집후 Filter접점을 수집한것이라고 표시
        /// </summary>
        public bool FilterRead
        {
            get { return m_bFilterRead; }
            set { m_bFilterRead = value; }
        }

        public bool FragMasterRead
        {
            get { return m_bFragMasterRead; }
            set { m_bFragMasterRead = value; }
        }

        public int RecipeReadValue
        {
            get { return m_iRecipeValue; }
            set { m_iRecipeValue = value; }
        }

        public int ProcessReadValue
        {
            get { return m_iProcessValue; }
            set { m_iProcessValue = value; }
        }

        public int TactTimeReadValue
        {
            get { return m_iTactTimeValue; }
            set { m_iTactTimeValue = value; }
        }

        public string ModelReadString
        {
            get { return m_sModel; }
            set { m_sModel = value; }
        }

        public string ProcessIDReadString
        {
            get { return m_sProcessID; }
            set { m_sProcessID = value; }
        }

        public string GlassIDReadString
        {
            get { return m_sGlassID; }
            set { m_sGlassID = value; }
        }

        public string NoteReadString
        {
            get { return m_sNoteString; }
            set { m_sNoteString = value; }
        }


        #endregion


        #region Protected Nethod

        protected string GetAddressExtraction(string sSourceAddress)
        {
            string sResult = string.Empty;
            if (sSourceAddress.Substring(0, 1) == "K")
            {
                //K* 제거
                sResult = sSourceAddress.Substring(2, sSourceAddress.Length - 2);
            }
            // else if ((sSourceAddress.Substring(0, 2) == "TS") || (sSourceAddress.Substring(0, 2) == "CS"))
            //{
            //    subAddress = sSourceAddress.Substring(0, 1);
            //    subAddress += sSourceAddress.Substring(2, sSourceAddress.Length - 2);
            //}
            else
                sResult = sSourceAddress;

            return sResult;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// 수집 대상 접점의 수집값을 전달하기 위해 저장합니다.
        /// 아래 SetValueParsing와 구분되는 점은 타이머 읽기에 대한 처리입니다.
        /// </summary>
        /// <param name="sTimerReadMode"></param>
        /// <returns></returns>
        public bool SetCollectDataParsing(string sTimerReadMode)
        {
            if (m_iaPacketValue == null) return false;
            if (m_sPacketAddress == "") return false;

            m_saAddressList = m_sPacketAddress.Split('\n');

            if (sTimerReadMode != null && !sTimerReadMode.Equals("TN"))
            {
                for (int i = 0; i < m_saAddressList.Length; i++)
                {
                    string sTemp = m_saAddressList[i];
                    if (sTemp == "") continue;

                    if (sTemp.Contains(sTimerReadMode))
                    {
                        // Remove OldKey
                        m_lstData.Remove(sTemp);
                        m_saAddressList[i] = sTemp.Replace(sTimerReadMode, "T");

                        CDDEAReadAddressData cDDeaReadData = new CDDEAReadAddressData();
                        cDDeaReadData.Address = m_saAddressList[i];

                        m_lstData.Add(m_saAddressList[i], cDDeaReadData);
                    }
                }
            }

            for (int i = 0; i < m_iaPacketValue.Length; i++)
            {
                string sAddress = m_saAddressList[i];
                if (sAddress == "")
                    continue;

                if (m_lstData.ContainsKey(sAddress))
                {
                    m_lstData[sAddress].Value = m_iaPacketValue[i];
                }
                else
                {
                    sAddress = GetAddressExtraction(m_saAddressList[i]);
                    if (m_lstData.ContainsKey(sAddress))
                    {
                        m_lstData[sAddress].Value = m_iaPacketValue[i];
                    }
                    else
                    {
                        Console.WriteLine("없네");
                        return false;
                    }
                }
            }

            return true;

        }

        /// <summary>
        /// 수집대상 접점의 수집값을 삽입시켜줌.
        /// </summary>
        /// <returns></returns>
        public bool SetValueParsing()
        {
            if (m_iaPacketValue == null) return false;
            if (m_sPacketAddress == "") return false;

            m_saAddressList = m_sPacketAddress.Split('\n');

            for (int i = 0; i < m_iaPacketValue.Length; i++)
            {
                string sAddress = m_saAddressList[i];
                if (sAddress == "")
                    continue;

                if (m_lstData.ContainsKey(sAddress))
                {
                    m_lstData[sAddress].Value = m_iaPacketValue[i];
                }
                else
                {
                    sAddress = GetAddressExtraction(m_saAddressList[i]);
                    if (m_lstData.ContainsKey(sAddress))
                    {
                        m_lstData[sAddress].Value = m_iaPacketValue[i];
                    }
                    else
                    {
                        Console.WriteLine("없네");
                        return false;
                    }
                }
            }

            return true;
        }

        public object Clone()
        {
            CDDEAPacketData oData = new CDDEAPacketData();

            foreach (var who in m_lstData)
            {
                CDDEAReadAddressData cData = (CDDEAReadAddressData)who.Value.Clone();
                oData.ReadDataList.Add(who.Key, cData);
            }
            oData.GroupNumber = m_iGroupNumber;
            oData.CycleNumber = m_iCycleNumber;
            oData.Time = m_dtTime;
            oData.PacketAddress = m_sPacketAddress;
            oData.PacketCount = m_iPacketCount;
            if (this.PacketValues != null)
                oData.PacketValues = (int[])this.PacketValues.Clone();
            else
                oData.PacketValues = this.PacketValues;
            oData.FilterRead = m_bFilterRead;
            oData.FragMasterRead = m_bFragMasterRead;
            oData.ModelReadString = m_sModel;
            oData.GlassIDReadString = m_sGlassID;
            oData.RecipeReadValue = m_iRecipeValue;
            oData.ProcessReadValue = m_iProcessValue;
            oData.ProcessIDReadString = m_sProcessID;
            oData.TactTimeReadValue = m_iTactTimeValue;
            oData.NoteReadString = m_sNoteString;

            return oData;
        }

        public void Clear()
        {
            m_lstData.Clear();
            m_iGroupNumber = 0;
            m_iCycleNumber = 0;
            m_dtTime = DateTime.MinValue;
            m_sPacketAddress = "";
            m_iPacketCount = 0;
        }

        public void GetDwordValue(string sBaseAddress, string sSecondAddress)
        {
            int iBaseValue = -1;
            if (m_lstData.ContainsKey(sBaseAddress))
            {
                iBaseValue = m_lstData[sBaseAddress].Value & 0x0000ffff;
            }
            //else
            //    Console.WriteLine("Dwod분석 : 기본주소가 없습니다.");

            if (m_lstData.ContainsKey(sSecondAddress) && iBaseValue != -1)
            {
                int iDWordVal = (m_lstData[sSecondAddress].Value & 0x0000ffff) << 16;
                int iSum = iDWordVal + iBaseValue;
                m_lstData[sBaseAddress].DWordValue = iSum;
            }
            //else
            //    Console.WriteLine("Dwod분석 : 두번째 주소가 없습니다.");

        }

        #endregion
    }
}
