using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using UDM.Common;
using UDM.Project;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmMonitorStatus : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private CProfilerProject m_cProject = null;
        private CViewMonitorStatusInfo m_cViewInfo = null;
        private EMCollectModeType m_emCollectMode = EMCollectModeType.Normal;
        private Color m_cMonitorStopColor = Color.DarkGray;
        private Color m_cMonitorRunColor = Color.GreenYellow;
        private Color m_cServerDisonnectedColor = Color.DarkGray;
        private Color m_cServerConnectedColor = Color.GreenYellow;
        private Color m_cCycleOffColor = Color.DarkGray;
        private Color m_cCycleOnColor = Color.GreenYellow;

        private delegate void UpdateStatusTileCallBack(string sText, Color cColor);
        private delegate void UpdatePropertyCallBack(CViewMonitorStatusInfo cViewInfo);

        #endregion


        #region Initialize/Dispose

        public FrmMonitorStatus()
        {
            InitializeComponent();

            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties

        public CProfilerProject Project
        {
            get { return m_cProject; }
            set { m_cProject = value; }
        }

        #endregion


        #region Public Methods
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            if (this.tleMonitorStatus.Elements.Count == 0 || this.tleServerStatus.Elements.Count == 0 || this.tleCycleStatus.Elements.Count == 0)
                return;

            this.rowTotalCycle.Caption = ResLanguage.FrmMonitorStatus_AllCycle;
            this.rowTotalPacket.Caption = ResLanguage.FrmMonitorStatus_AllPacket;
            this.catMonitor.Properties.Caption = ResLanguage.FrmMonitorStatus_collectinfo;
            this.Text = ResLanguage.FrmMonitorStatus_collectinfo;
            this.exTileView.Text = ResLanguage.FrmMonitorStatus_CollectMachineName;
            this.rowCollectMode.Properties.Caption = ResLanguage.FrmMonitorStatus_collectMode;
            this.rowTimeFrom.Properties.Caption = ResLanguage.FrmMonitorStatus_collectStart;
            this.grpStatusInfo.Text = ResLanguage.FrmMonitorStatus_collectStatuseinfo;
            this.grpMontorInfo.Text = ResLanguage.FrmMonitorStatus_collectTargetinfo;
            this.rowStandardRecipe.Properties.Caption = ResLanguage.FrmMonitorStatus_Defaultrecipe;
            this.rowTimeTo.Properties.Caption = ResLanguage.FrmMonitorStatus_Expectedtocomplete;
            this.rowCurrentCycle.Caption = ResLanguage.FrmMonitorStatus_NowCycle;
            this.rowCurrentRecipe.Properties.Caption = ResLanguage.FrmMonitorStatus_NowRecipe;
            this.catState.Properties.Caption = ResLanguage.FrmMonitorStatus_Operationinfo;
            this.rowCurrentPacket.Caption = ResLanguage.FrmMonitorStatus_Packetinfo;
            this.catRecipe.Properties.Caption = ResLanguage.FrmMonitorStatus_Recipeinfo;
            this.tleMonitorStatus.Elements[0].Text = ResLanguage.FrmMonitorStatus_DateType;
            this.tleServerStatus.Elements[0].Text = ResLanguage.FrmMonitorStatus_Collector;
            this.tleCycleStatus.Elements[0].Text = ResLanguage.FrmMonitorStatus_Cycle;
        }

        public void UpdateMonitorStatus(bool bStatus)
        {
            if (bStatus)
            {
                UpdateMonitorStatus("Run", m_cMonitorRunColor);
            }
            else
            {
                UpdateMonitorStatus("Stop", m_cMonitorStopColor);
            }
        }

        public void UpdateServerStatus(bool bStatus)
        {
            if (bStatus)
            {
                UpdateServerStatus("Connected", m_cServerConnectedColor);
            }
            else
            {
                UpdateServerStatus("Disconnected", m_cServerDisonnectedColor);
            }
        }

        public void UpdateCycleStatus(bool bStatus)
        {
            if (bStatus)
            {
                UpdateCycleStatus("ON", m_cCycleOnColor);
            }
            else
            {
                UpdateCycleStatus("OFF", m_cCycleOffColor);
            }
        }

        public void UpdateCollectMode(EMCollectModeType emModeType)
        {
            if (m_cViewInfo == null)
                return;

            string sModeType = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid1;
            if (emModeType == EMCollectModeType.Normal)
                sModeType = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid1;
            else if (emModeType == EMCollectModeType.Standard)
                sModeType = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid2;
            else if (emModeType == EMCollectModeType.Fragment)
                sModeType = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid3;
            else if (emModeType == EMCollectModeType.LOB)
                sModeType = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid4;
            else if (emModeType == EMCollectModeType.FilterNormal)
                sModeType = ResLanguage.FrmMonitorStatus_Msg_UpdateCollectModeGuid5;

            m_emCollectMode = emModeType;
            m_cViewInfo.CollectMode = sModeType;

            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdateTimeFrom(string sTime)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.TimeFrom = sTime;
            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdateTimeTo(string sTime)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.TimeTo = sTime;
            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdateCycleCount(string sCount)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.CurrentCycle = sCount;
            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdatePacketCount(string sCount)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.CurrentPacket = sCount;
            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdateTotalPacketCount(string sCount)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.TotalPacket = sCount;
            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdateTotalCycleCount(string sCount)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.TotalCycle = sCount;
            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdateCurrentRecipe(string sRecipe)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.CurrentRecipe = sRecipe;
            UpdatePropertyView(m_cViewInfo);
        }

        public void UpdateStandardRecipe(string sRecipe)
        {
            if (m_cViewInfo == null)
                return;

            m_cViewInfo.StandardRecipe = sRecipe;
            UpdatePropertyView(m_cViewInfo);
        }

        public void RefreshView()
        {
            InitView();
        }

        public void RefreshView(CProfilerProject cProject, EMCollectModeType emModeType)
        {
            m_cProject = cProject;
            m_emCollectMode = emModeType;
            m_cViewInfo = new CViewMonitorStatusInfo(m_cProject, m_emCollectMode);

            UpdatePropertyView(m_cViewInfo);
        }

        public void ToggleTitleView()
        {

        }

        public void Clear()
        {
            if (IsValid() == false)
                return;

            InitView();
        }

        #endregion


        #region Private Methods

        private bool IsValid()
        {
            if (m_cProject == null)
            {
                //CMessageHelper.ShowPopup("생성된 프로젝트가 없습니다.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void InitView()
        {
            if (IsValid() == false)
                return;

            m_cViewInfo = new CViewMonitorStatusInfo(m_cProject, m_emCollectMode);

            exTileView.Text = ResLanguage.FrmMonitorStatus_Msg_InitViewGuid1+ " : " + m_cProject.Name;

            UpdateMonitorStatus("Stop", m_cMonitorStopColor);
            UpdateServerStatus("Disconnected", m_cServerDisonnectedColor);
            UpdateCycleStatus("OFF", m_cCycleOffColor);

            UpdatePropertyView(m_cViewInfo);
        }


        private void UpdateMonitorStatus(string sText, Color cColor)
        {
            if (exTileView.InvokeRequired)
            {
                UpdateStatusTileCallBack cbUpdate = new UpdateStatusTileCallBack(UpdateMonitorStatus);
                exTileView.Invoke(cbUpdate, new object[] { sText, cColor });
            }
            else
            {
                exTileView.BeginUpdate();
                {
                    tleMonitorStatus.AppearanceItem.Hovered.BackColor = cColor;
                    tleMonitorStatus.AppearanceItem.Hovered.BorderColor = cColor;
                    tleMonitorStatus.AppearanceItem.Normal.BackColor = cColor;
                    tleMonitorStatus.AppearanceItem.Normal.BorderColor = cColor;
                    tleMonitorStatus.AppearanceItem.Selected.BackColor = cColor;
                    tleMonitorStatus.AppearanceItem.Selected.BorderColor = cColor;

                    tleMonitorStatus.Elements[1].Text = sText;
                }
                exTileView.EndUpdate();
            }
        }

        private void UpdatePropertyView(CViewMonitorStatusInfo cViewInfo)
        {
            if (exPropertyView.InvokeRequired)
            {
                UpdatePropertyCallBack cbUpdate = new UpdatePropertyCallBack(UpdatePropertyView);
                exPropertyView.Invoke(cbUpdate, new object[] { cViewInfo });
            }
            else
            {
                exPropertyView.SelectedObject = cViewInfo;
                exPropertyView.Refresh();
            }
        }

        private void UpdateServerStatus(string sText, Color cColor)
        {
            if (exTileView.InvokeRequired)
            {
                UpdateStatusTileCallBack cbUpdate = new UpdateStatusTileCallBack(UpdateServerStatus);
                exTileView.Invoke(cbUpdate, new object[] { sText, cColor });
            }
            else
            {
                exTileView.BeginUpdate();
                {
                    tleServerStatus.AppearanceItem.Hovered.BackColor = cColor;
                    tleServerStatus.AppearanceItem.Hovered.BorderColor = cColor;
                    tleServerStatus.AppearanceItem.Normal.BackColor = cColor;
                    tleServerStatus.AppearanceItem.Normal.BorderColor = cColor;
                    tleServerStatus.AppearanceItem.Selected.BackColor = cColor;
                    tleServerStatus.AppearanceItem.Selected.BorderColor = cColor;

                    tleServerStatus.Elements[1].Text = sText;
                }
                exTileView.EndUpdate();
            }
        }

        private void UpdateCycleStatus(string sText, Color cColor)
        {
            if (exTileView.InvokeRequired)
            {
                UpdateStatusTileCallBack cbUpdate = new UpdateStatusTileCallBack(UpdateCycleStatus);
                exTileView.Invoke(cbUpdate, new object[] { sText, cColor });
            }
            else
            {
                exTileView.BeginUpdate();
                {
                    tleCycleStatus.AppearanceItem.Hovered.BackColor = cColor;
                    tleCycleStatus.AppearanceItem.Hovered.BorderColor = cColor;
                    tleCycleStatus.AppearanceItem.Normal.BackColor = cColor;
                    tleCycleStatus.AppearanceItem.Normal.BorderColor = cColor;
                    tleCycleStatus.AppearanceItem.Selected.BackColor = cColor;
                    tleCycleStatus.AppearanceItem.Selected.BorderColor = cColor;

                    tleCycleStatus.Elements[1].Text = sText;
                }
                exTileView.EndUpdate();
            }
        }

        #endregion


        #region Event Methods

        #region Event Source

        private void RegisterManualEvent()
        {
        }

        #endregion

        #region Event Sink

        private void FrmMonitorStatus_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            InitView();

            Clear();
        }

        #endregion

        #endregion
    }
}