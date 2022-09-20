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
    [DataContract]
    [Serializable]
    public class CMainControl_V9 : CMainControl_V8 , ICloneable
    {
        #region Variables

        #endregion

        #region Initialize

        public CMainControl_V9()
        {
            m_iVersion = 9;
            m_cProfilerProject = new CProfilerProject_V7();
            m_cDDEAProject = new CDDEAProject_V7("Create");

        }

        public CMainControl_V9(CMainControl_V8 cOldVersion)
        {
            m_iVersion = 9;
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        [DataMember]
        public CDDEAProject_V7 DDEAProject_V7
        {
            get { return (CDDEAProject_V7)m_cDDEAProject; }
            set { m_cDDEAProject = value; }
        }

        #endregion


        #region Public Method

        public new bool Open(string sPath)
        {
            bool bOK = false;
            Clear();
            CPackSerializer<CMainControl_V9> cpackSerializer = new CPackSerializer<CMainControl_V9>();

            int iVersion = cpackSerializer.ReadVersion(sPath);
            if (iVersion == m_iVersion)
            {
                CMainControl_V9 cMainControl = cpackSerializer.Read(sPath, out iVersion);
                if (cMainControl != null)
                {
                    CloneV9(cMainControl);
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

                CMainControl_V8 cOldVersion1 = new CMainControl_V8();
                if (cOldVersion1.Open(sPath))
                {
                    CMainControl_V9 cMainControl = new CMainControl_V9(cOldVersion1);

                    if (cMainControl != null)
                        CloneV9(cMainControl);

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
            CPackSerializer<CMainControl_V9> cpackSerializer = new CPackSerializer<CMainControl_V9>();
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

        #endregion


        #region Protected Method

        protected void CreateFrom(CMainControl_V8 cOldVersion)
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
            ParameterModeViewTagS = cOldVersion.ParameterModeViewTagS;
            OpcConfigMS = cOldVersion.OpcConfigMS;
        }

        protected void CloneV9(CMainControl_V8 cMainControl)
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
            IsSetCompFilterNormal = cMainControl.IsSetCompFilterNormal;
            ParameterModeViewTagS = cMainControl.ParameterModeViewTagS;
            IsSetCompParameter = cMainControl.IsSetCompParameter;
            OpcConfigMS = cMainControl.OpcConfigMS;
        }
        #endregion


        #region Public Method

        #endregion
    }
}
