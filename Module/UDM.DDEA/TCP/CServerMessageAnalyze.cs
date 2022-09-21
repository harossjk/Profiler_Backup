using System;

namespace UDM.DDEA
{
    public class CServerMessageAnalyze
    {
        #region Member Veriables

        protected string m_sLengthCode = "";        //길이 4
        protected string m_sSendOper = "LGDCMST1   ";          //전송자 11
        protected string m_sReceiveOper = "";       //수신자 11
        protected string m_sProcessDate = "";       //처리 일자 8
        protected string m_sProcessTime = "";       //처리 시간ms까지 9
        protected string m_sCommandCode = "";       //전문 종류 4
        protected string m_sResponseCode = "";      //응답코드 6 000000은 문제 없음.나머지는 Error

        protected string m_sMachineName = "";

        protected string m_sUpmFileName = "";
        protected string m_sFtpUpmPath = "";

        protected string m_sConfigFileName = "";
        protected string m_sFtpConfigPath = "";

        protected string m_sParaFileName = "";
        protected string m_sFtpParaPath = "";

        protected string m_sRevMessage = "";
        protected string m_sSendMessage = "";
        protected int m_iSendByteCount = 0;

        protected string m_sFtpUploadTime = "";

        protected string m_sCollectType = "";
        protected EMSendMessageType m_emCommand = EMSendMessageType.NONE;

        protected string m_sSTATStatus = "N";
        protected string m_sLineName = "";

        #endregion


        #region Initialize

        public CServerMessageAnalyze()
        {

        }

        #endregion


        #region Properties

        public string MachineName
        {
            get { return m_sMachineName; }
            set { m_sMachineName = value; }
        }

        public string STAT_Status
        {
            get { return m_sSTATStatus; }
            set { m_sSTATStatus = value; }
        }

        /// <summary>
        /// E8TPHT08_PC
        /// </summary>
        public string LineName
        {
            get { return m_sLineName; }
            set { m_sLineName = value; }
        }

        /// <summary>
        /// 서버 -> DDEA Manager
        /// </summary>
        public string ReceiveMessage
        {
            get { return m_sRevMessage; }
            set
            {
                m_sRevMessage = value;
                AnalyzeReceiveMessage(value);
            }
        }

        /// <summary>
        /// DDEA Manager -> 서버
        /// </summary>
        public string SendMessage
        {
            get
            {
                return m_sSendMessage;
            }
        }

        /// <summary>
        /// 보낼 메세지의 Byte Count를 지정
        /// </summary>
        public int SendByteCount
        {
            get { return m_iSendByteCount; }
        }

        /// <summary>
        /// 수신 받은 메세지에서 추출한 정보
        /// </summary>
        public EMSendMessageType MessageCommand
        {
            get { return m_emCommand; }
        }

        public string UpmFileName
        {
            get { return m_sUpmFileName.Trim(); }
        }

        public string FtpUpmPath
        {
            get { return m_sFtpUpmPath.Trim(); }
        }
        public string ParaPath
        {
            get { return m_sFtpParaPath.Trim(); }
        }

        public string ParaFileName
        {
            get { return m_sParaFileName.Trim(); }
        }

        public string FtpParamPath
        {
            get { return m_sFtpParaPath.Trim(); }
        }

        public string ConfigPath
        {
            get { return m_sFtpConfigPath.Trim(); }
        }

        public string ConfigFileName
        {
            get { return m_sConfigFileName.Trim(); }
        }

        public string ResponseCode
        {
            get { return m_sResponseCode.Trim(); }
            set { m_sResponseCode = value; }
        }

        public string FtpUploadTime
        {
            get { return m_sFtpUploadTime.Trim(); }
        }

        /// <summary>
        /// 수집 여부만 사용
        /// </summary>
        public string CollectState
        {
            get { return m_sCollectType.Trim(); }
        }

        #endregion


        #region Protected Method

        protected void AnalyzeReceiveMessage(string sReceiveMessage)
        {
            try
            {
                //0074LGDCMST1   E8TPHT02_PC20151223173236515SCHC000000
                //공통
                m_sLengthCode = sReceiveMessage.Substring(0, 4);
                m_sSendOper = sReceiveMessage.Substring(4, 11);
                m_sReceiveOper = sReceiveMessage.Substring(15, 11);
                m_sProcessDate = sReceiveMessage.Substring(26, 8);
                m_sProcessTime = sReceiveMessage.Substring(34, 9);
                m_sCommandCode = sReceiveMessage.Substring(43, 4);
                m_sResponseCode = sReceiveMessage.Substring(47, 6);

                //세부(오른쪽 정렬)
                if (EMSendMessageType.PCST.ToString() == m_sCommandCode)
                {
                    m_sMachineName = sReceiveMessage.Substring(53, 10);
                    m_sUpmFileName = sReceiveMessage.Substring(63, 20);
                    m_sFtpUpmPath = sReceiveMessage.Substring(83, sReceiveMessage.Length - 83);
                    m_iSendByteCount = 129;
                    m_emCommand = EMSendMessageType.PCST;
                }
                else if (EMSendMessageType.PMPR.ToString() == m_sCommandCode)
                {
                    m_sMachineName = sReceiveMessage.Substring(53, 10);
                    m_sParaFileName = sReceiveMessage.Substring(63, 20);
                    m_sFtpParaPath = sReceiveMessage.Substring(83, sReceiveMessage.Length - 83);
                    m_iSendByteCount = 129;
                    m_emCommand = EMSendMessageType.PMPR;
                }
                else if (EMSendMessageType.PSTT.ToString() == m_sCommandCode)
                {
                    m_sMachineName = sReceiveMessage.Substring(53, 10);
                    m_sConfigFileName = sReceiveMessage.Substring(63, 20);
                    m_sFtpConfigPath = sReceiveMessage.Substring(83, sReceiveMessage.Length - 83);
                    m_iSendByteCount = 129;
                    m_emCommand = EMSendMessageType.PSTT;
                }
                else if (EMSendMessageType.STAT.ToString() == m_sCommandCode)
                {
                    //주기적 보고 가동 중인 것만(L2 -> Server)
                    m_emCommand = EMSendMessageType.STAT;
                    if (sReceiveMessage.Length > 63)
                        m_sMachineName = sReceiveMessage.Substring(53, 10);
                    m_iSendByteCount = 66;
                }
                else if (EMSendMessageType.SCHJ.ToString() == m_sCommandCode)
                {
                    //FTP Upload주기
                    m_emCommand = EMSendMessageType.SCHJ;
                    m_sFtpUploadTime = sReceiveMessage.Substring(53, 2);
                    m_iSendByteCount = 51;
                }
                else if (EMSendMessageType.SCHS.ToString() == m_sCommandCode)
                {
                    //수집 여부 변경
                    m_sMachineName = sReceiveMessage.Substring(53, 10);
                    m_sCollectType = sReceiveMessage.Substring(63, 1);
                    m_emCommand = EMSendMessageType.SCHS;
                    m_iSendByteCount = 94;
                }
                else if (EMSendMessageType.SCHC.ToString() == m_sCommandCode)
                {
                    //수집 모드 변경
                    m_emCommand = EMSendMessageType.SCHC;
                }
                else if (EMSendMessageType.SCHT.ToString() == m_sCommandCode)
                {
                    //전체수집 완료보고(L2->Server)
                    //받는 데이터는 없음.(보낼때는 머신명10 끝을 알리는 N
                    m_emCommand = EMSendMessageType.SCHT;
                }
                else
                {
                    m_emCommand = EMSendMessageType.NONE;
                    m_iSendByteCount = 0;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }


        #endregion


        #region Public Method

        public void SetSendMessage()
        {
            if (m_sResponseCode == "")
                return;
            DateTime dtNow = DateTime.Now;
            m_sProcessDate = dtNow.ToString("yyyyMMdd");
            m_sProcessTime = dtNow.ToString("HHmmssfff");
            string sMsg = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                          m_iSendByteCount.ToString("0000"), m_sReceiveOper, m_sSendOper, m_sProcessDate, m_sProcessTime, m_sCommandCode, m_sResponseCode);

            if (m_emCommand == EMSendMessageType.PCST)
                sMsg += m_sMachineName + m_sUpmFileName + m_sFtpUpmPath;
            else if (m_emCommand == EMSendMessageType.PMPR)
                sMsg += m_sMachineName + m_sParaFileName + m_sFtpParaPath;
            else if (m_emCommand == EMSendMessageType.PSTT)
                sMsg += m_sMachineName + m_sConfigFileName + m_sFtpConfigPath;
            else if (m_emCommand == EMSendMessageType.SCHJ)
                sMsg += m_sMachineName + m_sFtpUploadTime;
            else if (m_emCommand == EMSendMessageType.SCHS)
                sMsg += m_sMachineName + m_sCollectType + "00000000000000000" + "00000000000000000";
            //else if (m_emCommand == EMSendMessageType.SCHY)
            //    sMsg += m_sMachineName + "N";
            m_sSendMessage = sMsg;
        }

        public string SetStartMessage(string sScanTime, string sResponesCode)
        {
            m_iSendByteCount = 66;
            m_sCommandCode = "STAT";
            m_sResponseCode = sResponesCode;

            DateTime dtNow = DateTime.Now;
            m_sProcessDate = dtNow.ToString("yyyyMMdd");
            m_sProcessTime = dtNow.ToString("HHmmssfff");
            string sMsg = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8,6}{9}",
                          m_iSendByteCount.ToString("0000"), m_sLineName, m_sSendOper, m_sProcessDate, m_sProcessTime, m_sCommandCode, m_sResponseCode,
                          m_sMachineName, sScanTime, m_sSTATStatus);

            m_sSendMessage = sMsg;
            return sMsg;
        }

        public string SetLastStatusMessage(string sCollectMode, string sResponesCode)
        {
            m_iSendByteCount = 78;
            m_sCommandCode = "SCHC";
            m_sResponseCode = sResponesCode;

            DateTime dtNow = DateTime.Now;
            m_sProcessDate = dtNow.ToString("yyyyMMdd");
            m_sProcessTime = dtNow.ToString("HHmmssfff");
            string sMsg = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                          m_iSendByteCount.ToString("0000"), m_sLineName, m_sSendOper, m_sProcessDate, m_sProcessTime, m_sCommandCode, m_sResponseCode,
                          m_sMachineName, sCollectMode, DateTime.Now.ToString("yyyyMMddHHmmss"));

            m_sSendMessage = sMsg;
            return sMsg;
        }

        public string SetFragCompleteMessage()
        {
            m_iSendByteCount = 60;
            m_sCommandCode = "SCHT";
            m_sResponseCode = "000000";

            DateTime dtNow = DateTime.Now;
            m_sProcessDate = dtNow.ToString("yyyyMMdd");
            m_sProcessTime = dtNow.ToString("HHmmssfff");
            string sMsg = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                          m_iSendByteCount.ToString("0000"), m_sLineName, m_sSendOper, m_sProcessDate, m_sProcessTime, m_sCommandCode, m_sResponseCode,
                          m_sMachineName, "N");

            m_sSendMessage = sMsg;
            return sMsg;
        }

        #endregion

    }
}
