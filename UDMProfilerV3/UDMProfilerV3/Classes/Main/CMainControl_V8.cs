using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEA;
using UDM.General.Serialize;
using UDM.Project;

namespace UDMProfilerV3
{
    /* 
     * yjk, 20.02.03 - Parameter 수집 정보 추가로 인해 Version Upgrade
     * (Parameter 수집은 부분 수집과 같은 로직이기 때문에 DDEA 쪽으로 수집 정보가 넘어갈 때
     *  Parameter 수집에서 설정한 접점 정보들을 부분 수집으로 수집하는 것처럼 변환하여 수집하도록 함)
     */
    [DataContract]
    [Serializable]
    public class CMainControl_V8 : CMainControl_V7, ICloneable
    {

        #region Variables

        private List<CParameterModeViewTag> m_lstParameterModeViewTag = null;
        private bool m_bSetCompParameter = false;

        //jjk, 20.02.11 - opc,modbus 파일 추가
        protected CIotConfigMS m_cOpcConfigMS = null;

        #endregion


        #region Initialize

        public CMainControl_V8()
        {
            m_iVersion = 8;

            m_cProfilerProject = new CProfilerProject_V7();
            m_cDDEAProject = new CDDEAProject_V6("Create");

            m_lstParameterModeViewTag = new List<CParameterModeViewTag>();
            
            //jjk, 20.02.11 - opc,modbus 파일 추가
            m_cOpcConfigMS = new CIotConfigMS();
        }

        public CMainControl_V8(CMainControl_V7 cOldVersion)
        {
            m_iVersion = 8;
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        [DataMember]
        public CProfilerProject_V7 ProfilerProject_V7
        {
            get { return (CProfilerProject_V7)m_cProfilerProject; }
            set { m_cProfilerProject = value; }
        }

        [DataMember]
        public CDDEAProject_V6 DDEAProject_V6
        {
            get { return (CDDEAProject_V6)m_cDDEAProject; }
            set { m_cDDEAProject = value; }
        }

        [DataMember]
        public List<CParameterModeViewTag> ParameterModeViewTagS
        {
            get { return m_lstParameterModeViewTag; }
            set { m_lstParameterModeViewTag = value; }
        }

        //jjk, 20.02.11 - opc,modbus 파일 추가
        [DataMember]
        public CIotConfigMS OpcConfigMS
        {
            get { return m_cOpcConfigMS; }
            set { m_cOpcConfigMS = value; }
        }

        [DataMember]
        public bool IsSetCompParameter
        {
            get { return m_bSetCompParameter; }
            set { m_bSetCompParameter = value; }
        }

        #endregion


        #region Public Method

        public new bool Open(string sPath)
        {
            bool bOK = false;

            Clear();

            CPackSerializer<CMainControl_V8> cpackSerializer = new CPackSerializer<CMainControl_V8>();

            int iVersion = cpackSerializer.ReadVersion(sPath);
            if (iVersion == m_iVersion)
            {
                CMainControl_V8 cMainControl = cpackSerializer.Read(sPath, out iVersion);
                if (cMainControl != null)
                {
                    CloneV8(cMainControl);
                    bOK = true;
                }
                else
                {
                    CMessageHelper.ShowPopup("죄송합니다. 프로젝트를 불러오는데 실패하였습니다.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }
            else
            {
                cpackSerializer.Dispose();

                CMainControl_V7 cOldVersion1 = new CMainControl_V7();
                if (cOldVersion1.Open(sPath))
                {
                    CMainControl_V8 cMainControl = new CMainControl_V8(cOldVersion1);

                    if (cMainControl != null)
                        CloneV8(cMainControl);

                    bOK = true;
                }
            }

            if (m_cProfilerProject != null && m_cProfilerProject.TagS != null && m_cProfilerProject.StepS != null)
                m_cProfilerProject.Compose();

            for (int i = 0; i < m_cProfilerProject.TagS.Count; ++i)
            {
                CTag ctag = m_cProfilerProject.TagS.ElementAt<KeyValuePair<string, CTag>>(i).Value;
                if (ctag.DataType == EMDataType.DWord && ctag.Size != 2)
                    ctag.Size = 2;
            }

            return bOK;
        }

        public new bool Save(string sPath)
        {
            CPackSerializer<CMainControl_V8> cpackSerializer = new CPackSerializer<CMainControl_V8>();
            bool flag = cpackSerializer.Write(sPath, this, m_iVersion);
            cpackSerializer.Dispose();
            return flag;
        }

        public new bool Save()
        {
            if (string.IsNullOrEmpty(UpmSaveFilePath))
                return false;

            return Save(UpmSaveFilePath);
        }

        protected void CloneV8(CMainControl_V8 cMainControl)
        {
            if (cMainControl.ProfilerProject != null)
                m_cProfilerProject = cMainControl.ProfilerProject;

            if (cMainControl.RefreshParameterS != null)
                m_cRefreshParam = cMainControl.RefreshParameterS;

            if (cMainControl.DDEAProject != null)
            {
                m_cDDEAProject = cMainControl.DDEAProject;
                m_cProfilerProject.PLCAddressLimit = new CReadFunction(m_cDDEAProject.Config).ReadParameterSymbolSize();
            }

            ProjectName = cMainControl.ProjectName;
            CollectMode = cMainControl.CollectMode;
            IsSetCompFrag = cMainControl.IsSetCompFrag;
            IsSetCompNormal = cMainControl.IsSetCompNormal;
            PLCConfigTest = cMainControl.PLCConfigTest;
            DDEAFailAddress = cMainControl.DDEAFailAddress;
            LogSavePath = cMainControl.LogSavePath;
            NotUseYet = cMainControl.NotUseYet;
            UpmSaveFilePath = cMainControl.UpmSaveFilePath;
            LogSaveTime = cMainControl.LogSaveTime;
            LogDeleteDay = cMainControl.LogDeleteDay;

            //yjk, 18.10.08
            IsSetCompFilterNormal = cMainControl.IsSetCompFilterNormal;

            //yjk, 20.02.03
            ParameterModeViewTagS = cMainControl.ParameterModeViewTagS;
            IsSetCompParameter = cMainControl.IsSetCompParameter;

            //jjk, 20.02.11 - opc,modbus 파일 추가
            OpcConfigMS = cMainControl.OpcConfigMS;
        }

        #endregion


        #region Protected Method

        protected void CreateFrom(CMainControl_V7 cOldVersion)
        {
            m_sProjectName = cOldVersion.ProjectName;
            m_emCollectMode = cOldVersion.CollectMode;
            m_bSetFragModeComp = cOldVersion.IsSetCompFrag;
            m_bSetNormalModeComp = cOldVersion.IsSetCompNormal;
            m_cRefreshParam = cOldVersion.RefreshParameterS;
            m_bPlcConfigTest = cOldVersion.PLCConfigTest;
            m_lstDDEAFailAddress = cOldVersion.DDEAFailAddress;
            m_sLogSavePath = cOldVersion.LogSavePath;
            m_sReservedVariable = cOldVersion.NotUseYet;
            m_sUpmSavePath = cOldVersion.UpmSaveFilePath;
            m_iLogSaveTime = cOldVersion.LogSaveTime;
            m_iLogDeleteDay = cOldVersion.LogDeleteDay;

            SetProfilerProject(cOldVersion.ProfilerProject);
            SetDDEAProject(cOldVersion.DDEAProject);

            //yjk, 20.02.03
            m_lstParameterModeViewTag = new List<CParameterModeViewTag>();
            //jjk, 20.02.11 - opc,modbus 파일 추가
            m_cOpcConfigMS = new CIotConfigMS();
        }

        #endregion


        #region Public Method

        //jjk,19.07.04 - Clone 함수 추가.
        public object Clone()
        {
            CMainControl_V8 clone = new CMainControl_V8();
            clone.RenamingName = this.RenamingName;
            clone.ProfilerProject = this.ProfilerProject;
            clone.DDEAProject = this.DDEAProject;
            clone.ProfilerProject.PLCAddressLimit = new CReadFunction(this.DDEAProject.Config).ReadParameterSymbolSize();
            clone.RefreshParameterS = this.RefreshParameterS;
            clone.ProjectName = this.ProjectName;
            clone.CollectMode = this.CollectMode;
            clone.IsSetCompFrag = this.IsSetCompFrag;
            clone.IsSetCompNormal = this.IsSetCompNormal;
            clone.PLCConfigTest = this.PLCConfigTest;
            clone.DDEAFailAddress = this.DDEAFailAddress;
            clone.LogSavePath = this.LogSavePath;
            clone.NotUseYet = this.NotUseYet;
            clone.UpmSaveFilePath = this.UpmSaveFilePath;
            clone.LogSaveTime = this.LogSaveTime;
            clone.LogDeleteDay = this.LogDeleteDay;
            clone.IsSetCompFilterNormal = this.IsSetCompFilterNormal;

            return clone;
        }

        #endregion

    }
}
