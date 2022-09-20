using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDMProfilerV3
{
    public partial class UCChannelSetting :  DevExpress.XtraEditors.XtraUserControl
    {
        public UCChannelSetting()
        {
            InitializeComponent();



            this.cmbPassword.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPassword.Properties.Items.Add(false);
            this.cmbPassword.Properties.Items.Add(true);
            this.cmbPassword.SelectedIndex = 0;


        }


        public void Save()
        {
      
        }
    }
}
