using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDM.Common;

namespace UDMProfilerV3
{
    /*
     * yjk, 20.02.03 - 파라미터 Data Model Class
     */
    public class CParameterModeViewTag : ICloneable
    {

        #region Variables

        private string m_sMachine = string.Empty;
        private string m_sUnit = string.Empty;
        private string m_sComment = string.Empty;
        private string m_sAddress = string.Empty;

        private bool m_bIsChecked = true;

        private CTag m_cTag = null;

        #endregion


        #region Initialize

        public CParameterModeViewTag()
        {

        }

        public CParameterModeViewTag(CTag tag)
        {
            m_cTag = tag;
        }

        #endregion


        #region Properties

        public string Machine
        {
            get { return m_sMachine; }
            set { m_sMachine = value; }
        }

        public string Unit
        {
            get { return m_sUnit; }
            set { m_sUnit = value; }
        }

        public string Comment
        {
            get { return m_sComment; }
            set { m_sComment = value; }
        }

        public string Address
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }

        public bool IsChecked
        {
            get { return m_bIsChecked; }
            set { m_bIsChecked = value; }
        }

        public CTag Tag
        {
            get { return m_cTag; }
            set { m_cTag = value; }
        }

        #endregion


        #region Public Method

        public object Clone()
        {
            CParameterModeViewTag clone = new CParameterModeViewTag();
            clone.Address = this.Address;
            clone.Comment = this.Comment;
            clone.Machine = this.Machine;
            clone.Unit = this.Unit;
            clone.IsChecked = this.IsChecked;
            clone.Tag = (CTag)this.Tag.Clone();
            return clone;
        }

        #endregion

    }
}
