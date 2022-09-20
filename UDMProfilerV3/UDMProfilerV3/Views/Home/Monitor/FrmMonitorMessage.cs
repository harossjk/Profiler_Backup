using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmMonitorMessage : DevExpress.XtraEditors.XtraForm, IView
    {

        #region Member Variables

        private delegate void UpdateMessageCallBack(string sSender, string sMessage);

        #endregion


        #region Initialize/Dispose

        public FrmMonitorMessage()
        {
            InitializeComponent();
            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Properties


        #endregion


        #region Public Methods

        public void UpdateMessage(string sSender, string sMessage)
        {
            AddMessage(sSender, sMessage);
        }
        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.Text = ResLanguage.FrmMonitorMessage_CollectMessage;
            ucMessageTable.SetTextLanguage();
        }

        public void RefreshView()
        {

        }

        public void ToggleTitleView()
        {
            
        }

        public void Clear()
        {
            ucMessageTable.Clear();
        }
        

        #endregion


        #region Private Methods

        private void AddMessage(string sSender, string sMessage)
        {
            if (ucMessageTable.InvokeRequired)
            {
                UpdateMessageCallBack cbUpdate = new UpdateMessageCallBack(AddMessage);
                ucMessageTable.Invoke(cbUpdate, new object[] { sSender, sMessage });
            }
            else
            {
                ucMessageTable.AddMessage(DateTime.Now, sSender, sMessage);
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

        private void FrmMonitorMessage_Load(object sender, EventArgs e)
        {
            RegisterManualEvent();

            Clear();
        }

        #endregion        

        #endregion        
    }
}