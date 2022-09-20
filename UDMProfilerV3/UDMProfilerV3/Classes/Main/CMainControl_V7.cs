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
    //yjk, 19.06.19 - 동작연계표 새로운 버젼 Class를 만듬
    //jjk, 19.07.04 - CMainControl_V7 ICloneable Interface 추가 (다중로직차트에서 clone복사 필요)
    [DataContract]
    [Serializable]
    public class CMainControl_V7 : CMainControl_V6, ICloneable
    {

        #region Initialize

        public CMainControl_V7()
        {
            m_iVersion = 7;
            
            m_cProfilerProject = new CProfilerProject_V6();
            m_cDDEAProject = new CDDEAProject_V5("Create");
        }

        public CMainControl_V7(CMainControl_V6 cOldVersion)
        {
            m_iVersion = 7;
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        [DataMember]
        public CProfilerProject_V6 ProfilerProject_V6
        {
            get
            {
                return (CProfilerProject_V6)m_cProfilerProject;
            }
            set
            {
                m_cProfilerProject = value;
            }
        }

        #endregion


        #region Public Method

        public new bool Open(string sPath)
        {
            bool bOK = false;
            Clear();
            CPackSerializer<CMainControl_V7> cpackSerializer = new CPackSerializer<CMainControl_V7>();

            int iVersion = cpackSerializer.ReadVersion(sPath);
            if (iVersion == m_iVersion)
            {
                CMainControl_V7 cMainControl = cpackSerializer.Read(sPath, out iVersion);
                if (cMainControl != null)
                {
                    CloneV7(cMainControl);
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

                CMainControl_V6 cOldVersion1 = new CMainControl_V6();
                if (cOldVersion1.Open(sPath))
                {
                    CMainControl_V7 cMainControl = new CMainControl_V7(cOldVersion1);

                    if (cMainControl != null)
                        CloneV7(cMainControl);

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
            CPackSerializer<CMainControl_V7> cpackSerializer = new CPackSerializer<CMainControl_V7>();
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

        protected void CloneV7(CMainControl_V7 cMainControl)
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
            
        }

        #endregion


        #region Protected Method

        protected void CreateFrom(CMainControl_V6 cOldVersion)
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
        }

        #endregion


        #region Public Method
        //jjk,19.07.04 - Clone 함수 추가.
        public object Clone()
        {
            CMainControl_V7 clone = new CMainControl_V7();
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
