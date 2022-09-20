using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCAddressTypeColor : DevExpress.XtraEditors.XtraUserControl
    {

        #region Variables

        private DataTable m_dtColor = null;

        #endregion


        #region Initialize

        public UCAddressTypeColor()
        {
            InitializeComponent();

            //jjk, 19.11.14 - Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Method

        //jjk, 19.11.14 - Language 함수 추가
        public void SetTextLanguage()
        {
            this.btnDelete.ToolTip = ResLanguage.UCAddressTypeColor_Delete;
            this.btnAdd.ToolTip = ResLanguage.UCAddressTypeColor_Add;
            
        }

        public void InitData()
        {
            if (CParameterHelper.Parameter.AddressTypeColor != null && CParameterHelper.Parameter.AddressTypeColor.Count > 0)
            {
                //Binding Source
                m_dtColor = new DataTable();
                m_dtColor.Columns.Add("Header");
                m_dtColor.Columns.Add("Color");
                m_dtColor.Columns.Add("Description");

                foreach (string header in CParameterHelper.Parameter.AddressTypeColor.Keys)
                {
                    int argb = int.Parse(CParameterHelper.Parameter.AddressTypeColor[header][0]);
                    string desc = CParameterHelper.Parameter.AddressTypeColor[header][1];
                    m_dtColor.Rows.Add(header, argb, desc);
                }

                //Binding
                grdAddressTypeColor.DataSource = m_dtColor;
            }
        }

        public void Save()
        {
            Dictionary<string, List<string>> tmpColor = DataTableToDictionary((DataTable)grdAddressTypeColor.DataSource);

            //Address Header 값에 중복이 있는 경우 Return
            if (tmpColor == null)
                return;

            CParameterHelper.Parameter.AddressTypeColor = tmpColor;
        }

        #endregion


        #region Private Method

        //AddressTypeColor DataSource(DataTable)을 Parameter의 Dictionary 속성값으로 변환
        private Dictionary<string, List<string>> DataTableToDictionary(DataTable dtSource)
        {
            Dictionary<string, List<string>> dictColor = new Dictionary<string, List<string>>();
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                //[0] : Header
                //[1] : Color ToARGB
                //[2] : Description
                object[] items = dtSource.Rows[i].ItemArray;

                if (string.IsNullOrEmpty(items[0].ToString()))
                    continue;

                //같은 키가 있는 경우 경고 메시지 박스 후 작업 중단
                if (dictColor.ContainsKey(items[0].ToString()))
                {
                    CMessageHelper.ShowPopup("같은 주소 Header 값이 존재합니다.\n확인 후 다시 시도해주십시오.\n[중복 Header : " + items[0].ToString() + "]", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return null;
                }

                dictColor.Add(items[0].ToString(), new List<string> { items[1].ToString(), items[2].ToString() });
            }

            return dictColor;
        }

        #endregion


        #region Event

        private void btnAdd_Click(object sender, EventArgs e)
        {
            m_dtColor.Rows.Add("", Color.Transparent.ToArgb());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] selRowsIdx = grvAddressTypeColor.GetSelectedRows();

            if (selRowsIdx.Length > 0)
            {
                for (int i = 0; i < selRowsIdx.Length; i++)
                {
                    int idx = selRowsIdx[i];
                    string addressHeader = grvAddressTypeColor.GetRowCellDisplayText(idx, colAddressHeader);

                    //Parameter에 있는 리스트에서 삭제
                    CParameterHelper.Parameter.AddressTypeColor.Remove(addressHeader);
                }

                grvAddressTypeColor.DeleteSelectedRows();
            }
        }

        private void grvAddressTypeColor_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //Address Header 중복 체크
            GridView view = sender as GridView;
            DataView currentDataView = (sender as GridView).DataSource as DataView;
            if (view.FocusedColumn.FieldName == "Header")
            {
                //check duplicate code
                string currentCode = e.Value.ToString();
                for (int i = 0; i < currentDataView.Count; i++)
                {
                    if (i != view.GetDataSourceRowIndex(view.FocusedRowHandle))
                    {
                        if (currentDataView[i]["Header"].ToString() == currentCode)
                        {
                            e.ErrorText = ResLanguage.UCAddressTypeColor_OverlapHeader;
                            e.Valid = false;
                            break;
                        }
                    }
                }
            }
        }

        #endregion

    }
}
