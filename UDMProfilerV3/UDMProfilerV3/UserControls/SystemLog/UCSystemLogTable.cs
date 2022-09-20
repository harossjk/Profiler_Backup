using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public partial class UCSystemLogTable : UserControl
    {
        #region Member Variables

        private DataTable m_dbTable = new DataTable();
        private int m_iMaxRow = 50;

        private bool m_bLoadedAlready = false;

        private delegate void AddMessageCallBack(DateTime dtTime, string sSender, string sMessage);

        #endregion


        #region Initialize/Dispose

        public UCSystemLogTable()
        {
            InitializeComponent();

            CreateTableScheme();

            exGridMain.DataSource = m_dbTable;

            //jjk, 19.11.14 -  Language 함수 추가
            SetTextLanguage();
        }

        #endregion


        #region Public Methods

        //jjk, 19.11.14 -  Language 함수 추가
        public void SetTextLanguage()
        {
            this.colTime.Caption = ResLanguage.UCSystemLogTable_Time;
            this.colSender.Caption = ResLanguage.UCSystemLogTable_Send;
            this.colMessage.Caption = ResLanguage.UCSystemLogTable_Message;
        }

        public void AddMessage(DateTime dtTime, string sSender, string sMessage)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    AddMessageCallBack cbAddMessage = new AddMessageCallBack(AddMessage);
                    this.Invoke(cbAddMessage, new object[] { dtTime, sSender, sMessage });
                }
                else
                {
                    if (m_dbTable != null)
                    {
                        string sTime = dtTime.ToString("yy/MM/dd HH:mm:ss.fff");

                        DataRow dbRow = m_dbTable.NewRow();
                        dbRow[colTime.FieldName] = (string)sTime;
                        dbRow[colSender.FieldName] = (string)sSender;
                        dbRow[colMessage.FieldName] = (string)sMessage;

                        //m_dbTable.Rows.Add(dbRow);
                        m_dbTable.Rows.InsertAt(dbRow, 0);

                        if (m_dbTable.Rows.Count > m_iMaxRow)
                            m_dbTable.Rows.RemoveAt(m_dbTable.Rows.Count - 1);

                        //kch@udmtek, 17.02.21
                        if (exGridView.FocusedRowHandle >= 0 && exGridView.FocusedRowHandle < 4)
                            exGridView.FocusedRowHandle = 0;

                        exGridMain.Refresh();

                        //kch@udmtek, 17.02.21
                        if (m_dbTable.Rows.Count == 1)
                            exGridView.FocusedRowHandle = 0;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : {0} [{1}]", ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name); ex.Data.Clear();
            }
        }

        public void Clear()
        {
            if (m_dbTable != null)
                m_dbTable.Clear();

            exGridMain.Refresh();
        }

        #endregion


        #region Private Methods

        private void CreateTableScheme()
        {
            if (m_dbTable == null)
                m_dbTable = new DataTable();

            m_dbTable.Rows.Clear();
            m_dbTable.Columns.Clear();

            string sField;
            for (int i = 0; i < exGridView.Columns.Count; i++)
            {
                sField = exGridView.Columns[i].FieldName;
                m_dbTable.Columns.Add(sField);
            }
        }

        #endregion


        #region Event Methods

        private void UCMessageLog_Load(object sender, EventArgs e)
        {
            if (m_bLoadedAlready)
                return;

            m_bLoadedAlready = true;
        }

        private void mnuClearAll_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #endregion
    }
}
