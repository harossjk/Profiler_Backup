using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDM.DDEACommon;

namespace UDM.DDEA
{
    public class CXmlLog
    {
        #region Member Veriables

        protected EMCollectMode m_emCollectMode = EMCollectMode.Normal;
        protected string m_sMachineName = "";
        protected DateTime m_dtStartTime = DateTime.MinValue;
        protected DateTime m_dtEndTime = DateTime.MinValue;

        protected CMDC m_cMDCMember = new CMDC();
        protected CPlus m_cPlusMember = new CPlus();
        protected CParameter m_cParameterMember = new CParameter();
        protected CMCSC m_cMcscMember = new CMCSC();

        #endregion
        

        #region Initialize

        public CXmlLog()
        {

        }

        #endregion


        #region Properties

        public EMCollectMode CollectType
        {
            get { return m_emCollectMode; }
            set { m_emCollectMode = value; }
        }

        public string MachineName
        {
            get { return m_sMachineName; }
            set { m_sMachineName = value; }
        }

        public DateTime StartTime
        {
            get { return m_dtStartTime; }
            set { m_dtStartTime = value; }
        }

        public DateTime EndTime
        {
            get { return m_dtEndTime; }
            set { m_dtEndTime = value; }
        }

        public CMDC MDCMember
        {
            get { return m_cMDCMember; }
            set { m_cMDCMember = value; }
        }

        public CPlus PlusMember
        {
            get { return m_cPlusMember; }
            set { m_cPlusMember = value; }
        }

        public CParameter ParameterMember
        {
            get { return m_cParameterMember; }
            set { m_cParameterMember = value; }
        }

        public CMCSC MCSCMember
        {
            get { return m_cMcscMember; }
            set { m_cMcscMember = value; }
        }

        #endregion
    }

    public class CMDC
    {
        protected string m_sMdcCode = "";
        protected int m_iMdcValue = 0;
        protected string m_sMdcDName = "";

        public string MCode
        {
            get
            {
                return this.m_sMdcCode;
            }
            set
            {
                this.m_sMdcCode = value;
            }
        }

        public int MVal
        {
            get
            {
                return this.m_iMdcValue;
            }
            set
            {
                this.m_iMdcValue = value;
            }
        }

        public string DName
        {
            get
            {
                return this.m_sMdcDName;
            }
            set
            {
                this.m_sMdcDName = value;
            }
        }
    }

    public class CParameter
    {
        protected string m_sParaName = "";
        protected string m_sParaGroup = "";
        protected string m_sParaSub1 = "";
        protected string m_sParaSub2 = "";
        protected string m_sParaSub3 = "";
        protected string m_sParaRecipe = "";
        protected string m_sParaValue = "";

        public string PName
        {
            get
            {
                return this.m_sParaName;
            }
            set
            {
                this.m_sParaName = value;
            }
        }

        public string PGroup
        {
            get
            {
                return this.m_sParaGroup;
            }
            set
            {
                this.m_sParaGroup = value;
            }
        }

        public string PSub1
        {
            get
            {
                return this.m_sParaSub1;
            }
            set
            {
                this.m_sParaSub1 = value;
            }
        }

        public string PSub2
        {
            get
            {
                return this.m_sParaSub2;
            }
            set
            {
                this.m_sParaSub2 = value;
            }
        }

        public string PSub3
        {
            get
            {
                return this.m_sParaSub3;
            }
            set
            {
                this.m_sParaSub3 = value;
            }
        }

        public string PRecipe
        {
            get
            {
                return this.m_sParaRecipe;
            }
            set
            {
                this.m_sParaRecipe = value;
            }
        }

        public string PValue
        {
            get
            {
                return this.m_sParaValue;
            }
            set
            {
                this.m_sParaValue = value;
            }
        }
    }

    public class CPlus
    {
        protected string m_PlusKey = "";
        protected int m_iPlusValue = 0;
        protected int m_iPlusBlock = 0;
        protected int m_iPlusCycle = 0;
        protected string m_sPlusNote = "";
        protected string m_sPlusRecipe = "";
        protected string m_sPlusModel = "";
        protected string m_sPlusGlass = "";
        protected string m_sPlusLot = "";

        public string Key
        {
            get
            {
                return this.m_PlusKey;
            }
            set
            {
                this.m_PlusKey = value;
            }
        }

        public int Value
        {
            get
            {
                return this.m_iPlusValue;
            }
            set
            {
                this.m_iPlusValue = value;
            }
        }

        public int BlockIndex
        {
            get
            {
                return this.m_iPlusBlock;
            }
            set
            {
                this.m_iPlusBlock = value;
            }
        }

        public int CycleIndex
        {
            get
            {
                return this.m_iPlusCycle;
            }
            set
            {
                this.m_iPlusCycle = value;
            }
        }

        public string Note
        {
            get
            {
                return this.m_sPlusNote;
            }
            set
            {
                this.m_sPlusNote = value;
            }
        }

        public string Recipe
        {
            get
            {
                return this.m_sPlusRecipe;
            }
            set
            {
                this.m_sPlusRecipe = value;
            }
        }

        public string Model
        {
            get
            {
                return this.m_sPlusModel;
            }
            set
            {
                this.m_sPlusModel = value;
            }
        }

        public string Glass
        {
            get
            {
                return this.m_sPlusGlass;
            }
            set
            {
                this.m_sPlusGlass = value;
            }
        }

        public string Lot
        {
            get
            {
                return this.m_sPlusLot;
            }
            set
            {
                this.m_sPlusLot = value;
            }
        }
    }

    public class CMCSC
    {
        protected string m_sMcscUnit = "";
        protected string m_sMcscGlass = "";
        protected string m_sMcscRecipe = "";
        protected string m_sMcscLot = "";
        protected string m_sMcscDName = "";

        public string Unit
        {
            get
            {
                return this.m_sMcscUnit;
            }
            set
            {
                this.m_sMcscUnit = value;
            }
        }

        public string DName
        {
            get
            {
                return this.m_sMcscDName;
            }
            set
            {
                this.m_sMcscDName = value;
            }
        }

        public string Glass
        {
            get
            {
                return this.m_sMcscGlass;
            }
            set
            {
                this.m_sMcscGlass = value;
            }
        }

        public string Recipe
        {
            get
            {
                return this.m_sMcscRecipe;
            }
            set
            {
                this.m_sMcscRecipe = value;
            }
        }

        public string Lot
        {
            get
            {
                return this.m_sMcscLot;
            }
            set
            {
                this.m_sMcscLot = value;
            }
        }
    }
}
