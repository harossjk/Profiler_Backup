using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;
using UDM.TimeChart;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class FrmDeleteLineList : DevExpress.XtraEditors.XtraForm
    {
        #region Variables

        private CTrendLineViewS m_cDataSource = new CTrendLineViewS();

        public event UEventHandlerTrendLineDelete UEventDeleteTrendLine;

        #endregion


        #region Initialize

        public FrmDeleteLineList(CTrendLineViewS cData)
        {
            InitializeComponent();
            InitView(cData);
            RegistEvent();
            //jjk, 20.04.06 - Language 추가
            SetTextLanguage();
        }

        //jjk, 20.04.06 - Language 추가
        public void SetTextLanguage()
        {
            this.colChecked.Caption = ResLanguage.FrmDeleteLineList_Display;
            this.Text = ResLanguage.FrmDeleteLineList_RemoveLine;

            if (this.groupControl.CustomHeaderButtons.Count != 0)
            {
                this.groupControl.CustomHeaderButtons[0].Properties.Caption = ResLanguage.FrmDeleteLineList_Check;
                this.groupControl.CustomHeaderButtons[1].Properties.Caption = ResLanguage.FrmDeleteLineList_Uncheck;
            }
        }

        #endregion


        #region Private Method

        private void RegistEvent()
        {
            btnDelete.Click += BtnDelete_Click;
            btnCancel.Click += BtnCancel_Click;
            groupControl.CustomButtonClick += GroupControl_CustomButtonClick;

            this.KeyPreview = true;
            this.KeyUp += FrmDeleteLineList_KeyUp;

            //yjk, test - Column Header CheckBox Embedded
            //tlsViewItem.CustomDrawColumnHeader += TlsViewItem_CustomDrawColumnHeader;
            //tlsViewItem.MouseUp += TlsViewItem_MouseUp;
            //exCheck.CheckedChanged += ExCheck_CheckedChanged;
        }

        private void EmbeddedCheckBoxChecked()
        {
            if (IsAllSelected())
                DeSelectAll();
            else
                SelectAll();
        }

        private void SelectAll()
        {
            foreach (CTrendLineView item in m_cDataSource)
                item.IsChecked = true;

            tlsViewItem.RefreshDataSource();
        }

        private void DeSelectAll()
        {
            foreach (CTrendLineView item in m_cDataSource)
                item.IsChecked = false;

            tlsViewItem.RefreshDataSource();
        }

        private bool IsAllSelected()
        {
            bool bAllSelected = true;
            foreach (CTrendLineView item in m_cDataSource)
            {
                if (!item.IsChecked)
                {
                    bAllSelected = false;
                    break;
                }
            }

            return bAllSelected;
        }

        private void DrawCheckBox(GraphicsCache cache, RepositoryItemCheckEdit edit, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
            info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.CalcViewInfo();
            args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, cache, r);
            painter.Draw(args);
        }

        private void InitView(CTrendLineViewS cData)
        {
            if (cData == null)
                return;

            m_cDataSource = cData;

            //CheckBox Column Header를 클릭하여 전체 선택하려고 할 시 Sorting도 되어 버리기 때문에 AllowSort = false
            tlsViewItem.Columns[0].OptionsColumn.AllowSort = false;

            tlsViewItem.DataSource = m_cDataSource;
            tlsViewItem.RefreshDataSource();
        }

        #endregion


        #region Event

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (UEventDeleteTrendLine != null)
            {
                CTrendLineViewS cLineS = new CTrendLineViewS();
                foreach (CTrendLineView line in m_cDataSource)
                {
                    if (line.IsChecked)
                        cLineS.Add(line);
                }

                UEventDeleteTrendLine(cLineS);
            }

            this.Close();
        }

        private void GroupControl_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption.Equals(ResLanguage.FrmDeleteLineList_Check))
            {
                SelectAll();
            }
            else if (e.Button.Properties.Caption.Equals(ResLanguage.FrmDeleteLineList_Uncheck))
            {
                DeSelectAll();
            }
        }

        private void ExCheck_CheckedChanged(object sender, EventArgs e)
        {
            OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, 45, 8, 0));
        }

        private void TlsViewItem_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            Point pt = new Point(e.X, e.Y);
            TreeListHitInfo hit = tree.CalcHitInfo(pt);
            if (hit.Column != null)
            {
                ColumnInfo info = tree.ViewInfo.ColumnsInfo[hit.Column];
                Rectangle checkRect = new Rectangle(info.Bounds.Left + 3, info.Bounds.Top + 3, 12, 12);
                if (checkRect.Contains(pt))
                {
                    EmbeddedCheckBoxChecked();
                }
            }
        }

        private void TlsViewItem_CustomDrawColumnHeader(object sender, CustomDrawColumnHeaderEventArgs e)
        {
            if (e.Column != null && e.Column == tlsViewItem.Columns[0])
            {
                Rectangle checkRect = new Rectangle(e.Bounds.Left + 3, e.Bounds.Top + 3, 12, 12);
                ColumnInfo info = (ColumnInfo)e.ObjectArgs;
                if (info.CaptionRect.Left < 30)
                    info.CaptionRect = new Rectangle(new Point(info.CaptionRect.Left + 15, info.CaptionRect.Top), info.CaptionRect.Size);

                e.Painter.DrawObject(info);
                DrawCheckBox(e.Cache, exCheck, checkRect, IsAllSelected());
                e.Handled = true;
            }
        }

        private void FrmDeleteLineList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnDelete_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                BtnCancel_Click(null, null);
            }
        }


        #endregion

    }
}