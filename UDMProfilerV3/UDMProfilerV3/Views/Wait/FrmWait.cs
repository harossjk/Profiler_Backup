using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

using UDM.General.WaitForm;

namespace UDMProfilerV3
{
    public partial class FrmWait : WaitForm
    {

        #region Member Variables

        private int m_iMaxValue = 100;

        #endregion


        #region Initialize/Dispose

        public FrmWait()
        {
            InitializeComponent();
            this.pnlProgress.AutoHeight = true;
        }

        #endregion


        #region Public Properites

        public int MaxValue
        {
            get { return m_iMaxValue; }
            set { m_iMaxValue = value; }
        }

        #endregion


        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.pnlProgress.Caption = caption;
        }

        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.pnlProgress.Description = description;
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }        

        #endregion

        public enum WaitFormCommand
        {
        }
    }
}