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
    public class CMainControl_V11 : CMainControl_V10, ICloneable
    {
        #region Member Variables


        #endregion


        #region Initialize/Dispose

        public CMainControl_V11()
        {
            m_iVersion = 11;
            m_cProfilerProject = new CProfilerProject_V8();
            m_cDDEAProject = new CDDEAProject_V8("Create");
        }

        public CMainControl_V11(CMainControl_V10 cOldVersion)
        {
            m_iVersion = 11;
            CreateFrom(cOldVersion);
        }

        #endregion

        #region Public Properties
        [DataMember]
        public CDDEAProject_V8 DDEAProject_V8
        {
            get { return (CDDEAProject_V8)m_cDDEAProject; }
            set { m_cDDEAProject = value; }
        }

        [DataMember]
        public new CProfilerProject_V8 ProfilerProject_V8
        {
            get { return (CProfilerProject_V8)m_cProfilerProject; }
            set { m_cProfilerProject = value; }
        }

        #endregion

        #region Public Methods

        public new bool Open(string sPath)
        {
            bool bOK = false;
            Clear();
            CPackSerializer<CMainControl_V11> cpackSerializer = new CPackSerializer<CMainControl_V11>();

            int iVersion = cpackSerializer.ReadVersion(sPath);
            if (iVersion == m_iVersion)
            {
                CMainControl_V11 cMainControl = cpackSerializer.Read(sPath, out iVersion);
                if (cMainControl != null)
                {
                    CloneV11(cMainControl);
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
                CMainControl_V10 cOldVersion = new CMainControl_V10();
                if (cOldVersion.Open(sPath))
                {
                    CMainControl_V11 cMainControl = new CMainControl_V11(cOldVersion);

                    if (cMainControl != null)
                        CloneV11(cMainControl);

                    bOK = true;
                }
            }

            if (m_cProfilerProject != null && m_cProfilerProject.TagS != null && m_cProfilerProject.StepS != null)
            {
                Utils.ConvertOldChName(m_cProfilerProject.GetType(), m_cProfilerProject);
                m_cProfilerProject.Compose();
            }

            for (int i = 0; i < m_cProfilerProject.TagS.Count; i++)
            {
                CTag ctag = m_cProfilerProject.TagS.ElementAt<KeyValuePair<string, CTag>>(i).Value;
                if (ctag.DataType == EMDataType.DWord && ctag.Size != 2)
                    ctag.Size = 2;
            }

            return bOK;
        }

        public new bool Save(string sPath)
        {
            CPackSerializer<CMainControl_V11> cpackSerializer = new CPackSerializer<CMainControl_V11>();
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

        #region Private Methods



        #endregion

        #region Protected Method

        protected new void CreateFrom(CMainControl_V10 cOldVersion)
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

        protected void CloneV11(CMainControl_V10 cMainControl)
        {
            if (cMainControl.ProfilerProject != null)
                m_cProfilerProject = cMainControl.ProfilerProject;

            if (cMainControl.RefreshParameterS != null)
                m_cRefreshParam = cMainControl.RefreshParameterS;

            if (cMainControl.DDEAProject_V7 != null)
                m_cDDEAProject = cMainControl.DDEAProject_V7;

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
    }
}
