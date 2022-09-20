using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;

namespace UDMProfilerV3
{
    public partial class UCGroupPanel : System.Windows.Forms.Panel
    {

        #region Member Variables


        #endregion


        #region Initialize/Dispose

        public UCGroupPanel()
        {
            InitializeComponent();

            InitView();
        }

        public UCGroupPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            InitView();
        }

        #endregion


        #region Public Properties

        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        #endregion


        #region Public Methods


        #endregion


        #region Private Methods

        private void InitView()
        {
            this.Controls.Add(lblTitle);
            lblTitle.Left = 0;
            lblTitle.Top = 0;
        }

        private void DrawLayout(Graphics g)
        {
            Color cColor = Color.LightGray;

            Pen pen = new Pen(cColor);

            g.DrawLine(pen, lblTitle.Width + 5, lblTitle.Height / 2, this.Width, lblTitle.Height / 2);
            //g.DrawLine(pen, 0, this.Height - 1, this.Width, this.Height - 1);
        }

        #endregion


        #region Event Methods

        #region Event Source

        #endregion

        #region Override

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawLayout(e.Graphics);
        }

        #endregion

        #region Event Sink


        #endregion

        #endregion
    }
}
