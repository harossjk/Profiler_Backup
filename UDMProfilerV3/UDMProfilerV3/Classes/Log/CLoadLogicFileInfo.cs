using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UDMProfilerV3
{
    //yjk, 18.08.21 - 다중로직에서 로드한 파일 정보
    public class CLoadLogFileInfo
    {

        #region Member Variables

        private string m_sUpmPath = string.Empty;
        private List<string> m_lstLogFilePath = new List<string>();
        private bool m_bIsProfilerProject = true;

        //yjk, 19.07.11 - Index Column 추가로 인해 추가
        private int m_iIndex = -1;

        #endregion


        #region Properties

        public string UpmFilePath
        {
            get { return m_sUpmPath; }
            set { m_sUpmPath = value; }
        }

        public List<string> LogFileSPath
        {
            get { return m_lstLogFilePath; }
            set { m_lstLogFilePath = value; }
        }

        public bool IsProfilerProject
        {
            get { return m_bIsProfilerProject; }
            set { m_bIsProfilerProject = value; }
        }


        //yjk, 19.07.11 - Index Column
        public int Index
        {
            get { return m_iIndex; }
            set { m_iIndex = value; }
        }

        #endregion

    }
}


