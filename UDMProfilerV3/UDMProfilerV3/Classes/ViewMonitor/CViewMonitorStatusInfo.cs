using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UDM.Common;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public class CViewMonitorStatusInfo : IDisposable
    {

        #region Member Variables

        private string m_sCollectMode = "-";
        private string m_sTimeFrom = "-";
        private string m_sTimeTo = "-";
        private string m_sCurrentPacket = "-";
        private string m_sTotalPacket = "-";
        private string m_sCurrentCycle = "-";
        private string m_sTotalCycle = "-";
        private string m_sStandardRecipe = "-";
        private string m_sCurrentRecipe = "-";

        #endregion


        #region Initilaize/Dispose

        public CViewMonitorStatusInfo(CProfilerProject cProject, EMCollectModeType emMode)
        {
            CreateInstance(cProject, emMode); ;
        }

        public void Dispose()
        {

        }

        #endregion


        #region Public Properties

        public string CollectMode
        {
            get { return m_sCollectMode; }
            set { m_sCollectMode = value; }
        }

        public string TimeFrom
        {
            get { return m_sTimeFrom; }
            set { m_sTimeFrom = value; }
        }

        public string TimeTo
        {
            get { return m_sTimeTo; }
            set { m_sTimeTo = value; }
        }

        public string CurrentPacket
        {
            get { return m_sCurrentPacket; }
            set { m_sCurrentPacket = value; }
        }

        public string TotalPacket
        {
            get { return m_sTotalPacket; }
            set { m_sTotalPacket = value; }
        }

        public string CurrentCycle
        {
            get { return m_sCurrentCycle; }
            set { m_sCurrentCycle = value; }
        }

        public string TotalCycle
        {
            get { return m_sTotalCycle; }
            set { m_sTotalCycle = value; }
        }

        public string StandardRecipe
        {
            get { return m_sStandardRecipe; }
            set { m_sStandardRecipe = value; }
        }

        public string CurrentRecipe
        {
            get { return m_sCurrentRecipe; }
            set { m_sCurrentRecipe = value; }
        }

        #endregion


        #region Public Methods


        #endregion


        #region Private Methods

        private void CreateInstance(CProfilerProject cProject, EMCollectModeType emMode)
        {
            
            

            if (cProject == null)
                return;

            m_sCollectMode = emMode.ToString();
            if (emMode == EMCollectModeType.Normal)
                m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid1;
            else if (emMode == EMCollectModeType.Standard)
                m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid2;
            else if (emMode == EMCollectModeType.Fragment)
                m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid3;
            else if (emMode == EMCollectModeType.LOB)
                m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid4;
            else if (emMode == EMCollectModeType.FilterNormal)
                m_sCollectMode = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid5;

            m_sTimeFrom = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            m_sTimeTo = "-";

            if (emMode == EMCollectModeType.Normal)
            {
                m_sTotalPacket = cProject.NormalPacketS.Count.ToString();
            }
            else if (emMode == EMCollectModeType.Standard)
            {
                m_sTotalPacket = cProject.FragmentPacketS.Count.ToString();
                m_sTotalCycle = cProject.CycleCount.ToString();
            }
            else if (emMode == EMCollectModeType.Fragment)
            {
                m_sTotalPacket = cProject.FragmentPacketS.Count.ToString();
                m_sTotalCycle = cProject.CycleCount.ToString();
                m_sStandardRecipe = cProject.StandardRecipe;
            }
            else if (emMode == EMCollectModeType.FilterNormal)
            {
                m_sTotalPacket = cProject.NormalPacketS.Count.ToString();
                if(cProject.GetType().IsAssignableFrom(typeof(CProfilerProject_V3)))
                    m_sTotalCycle = ((CProfilerProject_V3)cProject).FilterNormalCycleCount.ToString();
            }
        }

        #endregion
    }
}
