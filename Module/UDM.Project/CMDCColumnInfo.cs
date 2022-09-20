using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;

namespace UDM.Project
{
    [Serializable]
    public class CMDCColumnInfo
    {

        #region Member Variables

        private string m_sCode = "";
        private string m_sRefCode = "";
        private string m_sDescription = "";
        private string m_sMeasureUnit = "mm/s";
        private string m_sAxis = "";
        private int m_iDecimalPoint = 0;
        private int m_iColorR = 0;
        private int m_iColorG = 0;
        private int m_iColorB = 0;
        private bool m_bUsed = true;

        #endregion


        #region Initialize/Dispose

        public CMDCColumnInfo()
        {

        }

        #endregion


        #region Pubilc Properties

        public string Code
        {
            get { return m_sCode; }
            set { m_sCode = value; }
        }

        public string RefCode
        {
            get { return m_sRefCode; }
            set { m_sRefCode = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }

        public string MeasureUnit
        {
            get { return m_sMeasureUnit; }
            set { m_sMeasureUnit = value; }
        }

        public string Axis
        {
            get { return m_sAxis; }
            set { m_sAxis = value; }
        }

        public int DecimalPoint
        {
            get { return m_iDecimalPoint; }
            set { m_iDecimalPoint = value; }
        }

        public int ColorR
        {
            get { return m_iColorR; }
            set { m_iColorR = value; }
        }

        public int ColorG
        {
            get { return m_iColorG; }
            set { m_iColorG = value; }
        }

        public int ColorB
        {
            get { return m_iColorB; }
            set { m_iColorB = value; }
        }

        public bool Used
        {
            get { return m_bUsed; }
            set { m_bUsed = value; }
        }

        #endregion


        #region Public Mehtods

        public CMDCColumnInfo Clone()
        {
            CMDCColumnInfo cColumn = new CMDCColumnInfo();
            cColumn.Description = m_sDescription;
            cColumn.Code = m_sCode;
            cColumn.RefCode = m_sRefCode;
            cColumn.MeasureUnit = m_sMeasureUnit;
            cColumn.Axis = m_sAxis;
            cColumn.DecimalPoint = m_iDecimalPoint;
            cColumn.ColorR = m_iColorR;
            cColumn.ColorG = m_iColorG;
            cColumn.ColorB = m_iColorB;
            cColumn.Used = m_bUsed;

            return cColumn;
        }

        #endregion


        #region Private Methods


        #endregion

    }
}
