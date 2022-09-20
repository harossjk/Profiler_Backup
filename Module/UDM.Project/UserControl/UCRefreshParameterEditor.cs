using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UDM;
using UDM.Common;
using UDM.DDEACommon;

namespace UDM.Project.UI
{
    public partial class UCRefreshParameterEditor : DevExpress.XtraEditors.XtraUserControl
    {

        #region Member Variables

        private CRefreshParameterS m_cParameterS = null;
        private CTagS m_cTagS = null;

        private bool m_bLoadedAlready = false;

        #endregion


        #region Initialize/Dispose

        public UCRefreshParameterEditor()
        {
            InitializeComponent();
        }

        #endregion


        #region Public Properties

        public CRefreshParameterS ParameterS
        {
            get { return m_cParameterS; }
            set { m_cParameterS = value; ShowParameterS(); }
        }

        public CTagS TagS
        {
            get { return m_cTagS; }
            set { m_cTagS = value; }
        }

        #endregion


        #region Public Methods


        public void ShowParameterS()
        {
            if (m_cParameterS != null)
                txtLinkSide.Text = m_cParameterS.LinkSide;
            else
                txtLinkSide.Text = "";

            grdMain.DataSource = m_cParameterS;
            grdView.RefreshData();
        }

        #endregion


        #region Private Methods


        private bool GetHeaderHexa(string sHeader)
        {
            bool bOK = false;

            string[] arrHeader = { "X", "Y", "B", "W", "SB", "SW" };

            for (int i = 0; i < arrHeader.Length; i++)
            {
                if (arrHeader[i] == sHeader)
                {
                    bOK = true;
                }
            }

            return bOK;
        }
        private string GetLinkAddress(bool bHexa, string sHeader, int iNowPos)
        {
            string sResult = string.Empty;
            string s = string.Empty;
            if (bHexa)
                s = string.Format("{0:X}", iNowPos);
            else
                s = string.Format("{0}", iNowPos);
            sResult = sHeader + s;

            return sResult;
        }

        private int GetStringNumberToInt(string sSource, bool bHexa)
        {
            int iResult = 0;
            if (bHexa)
                iResult = Convert.ToInt32(sSource, 16);
            else
                iResult = Convert.ToInt32(sSource);

            return iResult;
        }

        private List<CDDEASymbol> FindTagSToHeader(string sHeader)
        {
            List<CDDEASymbol> lstResult = new List<CDDEASymbol>();

            foreach (var who in m_cTagS)
            {
                if (who.Value.Address.StartsWith(sHeader))
                {
                    CDDEASymbol sym = new CDDEASymbol(who.Value);
                    lstResult.Add(sym);
                }
            }

            return lstResult;
        }


        #endregion


        #region Event Methods

        private void UCRefreshParameterEditor_Load(object sender, EventArgs e)
        {
            if (m_bLoadedAlready)
                return;

            m_bLoadedAlready = true;

            ShowParameterS();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("적용 하시겠습니까?", "Refresh Parameter", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.No)
                return;
            for (int i = 0; i < m_cParameterS.Count; i++)
            {
                bool bLinkHexa = true;

                string sLinkHeader = m_cParameterS[i].LinkDevice;
                int iLinkStartNumber = GetStringNumberToInt(m_cParameterS[i].LinkStart, bLinkHexa);
                int iLinkEndNumber = GetStringNumberToInt(m_cParameterS[i].LinkEnd, bLinkHexa);

                string sPLCHeader = m_cParameterS[i].PLCDevice;
                bool bPLCHexa = GetHeaderHexa(sPLCHeader);
                int iPLCStartNumber = GetStringNumberToInt(m_cParameterS[i].PLCStart, bPLCHexa);
                int iPLCEndNumber = GetStringNumberToInt(m_cParameterS[i].PLCEnd, bPLCHexa);

                int iStepNumber = 0;

                //DDEA Symbol로 변경해야만 할 수 있음.

                List<CDDEASymbol> lstSymbol = FindTagSToHeader(sPLCHeader);
                if (lstSymbol.Count == 0)
                    continue;
                List<CDDEASymbol> lstFindSymbol = lstSymbol.FindAll(b => b.AddressMajor >= iPLCStartNumber && b.AddressMajor <= iPLCEndNumber);
                if (lstFindSymbol.Count == 0)
                    continue;
                lstFindSymbol.Sort(new CSymbolComparer());
                foreach (CDDEASymbol sym in lstFindSymbol)
                {
                    iStepNumber = 0;
                    for (int j = iPLCStartNumber; j < iPLCEndNumber; j++)
                    {
                        if (sym.AddressMajor == j)
                        {
                            if (m_cTagS.ContainsKey(sym.Key))
                            {
                                int iLinkNumber = iLinkStartNumber + iStepNumber;
                                string sLink = GetLinkAddress(bLinkHexa, sLinkHeader, iLinkNumber);
                                if (sym.AddressMinor != -1)
                                {
                                    string sDot = string.Format(".{0:X}", sym.AddressMinor);
                                    sLink += sDot;
                                }
                                m_cTagS[sym.Key].LinkAddress = sLink;
                                break;
                            }
                        }
                        iStepNumber++;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("초기화 하시겠습니까?", "Refresh Parameter", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.No)
                return;
            m_cParameterS.Clear();
            foreach (var who in m_cTagS)
            {
                CTag cTag = who.Value;
                cTag.LinkAddress = "";
            }
            ShowParameterS();
        }

        private void grdMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                string sText = Clipboard.GetText();
                if (sText == null || sText == "")
                    return;

                if (m_cParameterS == null)
                    m_cParameterS = new CRefreshParameterS();

                m_cParameterS.Clear();

                string[] saSplitter = new string[] {"\r\n"};
                string[] saToken = sText.Split(saSplitter, StringSplitOptions.None);

                if (saToken == null || saToken.Length == 1)
                    return;

                string[] saParameter;
                string sRow;
                CRefreshParameter cParam;
                for (int i = 0; i < saToken.Length; i++)
                {
                    saParameter = null;

                    sRow = saToken[i];
                    saParameter = sRow.Split('\t');
                    if (saParameter.Length != 10)
                        continue;
                    if (saParameter[0] == string.Empty)
                        continue;
                    if (saParameter[2] == string.Empty || saParameter[7] == string.Empty)
                        continue;

                    int iCheck = 0;
                    bool bNumeric = int.TryParse(saParameter[2], out iCheck);
                    if (!bNumeric)
                        continue;

                    cParam = new CRefreshParameter();
                    cParam.Category = saParameter[0];

                    cParam.LinkDevice = saParameter[1];
                    cParam.LinkPoints = saParameter[2];
                    cParam.LinkStart = saParameter[3];
                    cParam.LinkEnd = saParameter[4];

                    cParam.PLCDevice = saParameter[6];
                    cParam.PLCPoints = saParameter[7];
                    cParam.PLCStart = saParameter[8];
                    cParam.PLCEnd = saParameter[9];

                    m_cParameterS.Add(cParam);
                }
                if(m_cParameterS.Count >0)
                    ShowParameterS();
            }
        }

        #endregion
       
    }
}
