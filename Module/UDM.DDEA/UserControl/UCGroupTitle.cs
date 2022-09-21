// Decompiled with JetBrains decompiler
// Type: UDMDDEA.UCGroupTitle
// Assembly: UDMDDEA, Version=3.18.5.24, Culture=neutral, PublicKeyToken=null
// MVID: 9255FCB2-6F38-4411-AFDC-A0E5CCCB3BA6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMDDEA.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UDMDDEA
{
    public partial class UCGroupTitle : Panel
    {

        public UCGroupTitle()
        {
            this.InitializeComponent();
            this.InitView();
        }

        public UCGroupTitle(IContainer container)
        {
            container.Add((IComponent)this);
            this.InitializeComponent();
            this.InitView();
        }

        public string Title
        {
            get
            {
                return this.lblTitle.Text;
            }
            set
            {
                this.lblTitle.Text = value;
            }
        }

        private void InitView()
        {
            this.Controls.Add((Control)this.lblTitle);
            this.lblTitle.Left = 0;
            this.lblTitle.Top = 0;
        }

        private void DrawLayout(Graphics g)
        {
            Pen pen = new Pen(Color.LightGray);
            g.DrawLine(pen, this.lblTitle.Width + 5, this.lblTitle.Height / 2, this.Width, this.lblTitle.Height / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawLayout(e.Graphics);
        }
    }
}
