// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CMainControl_V4
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEA;
using UDM.General.Serialize;
using UDM.Project;

namespace UDMProfilerV3
{
    [DataContract]
    [Serializable]
    public class CMainControl_V4 : CMainControl_V3
    {

        #region Member Variables

        [NonSerialized]
        private string m_sRenamingName = string.Empty;

        #endregion


        #region Initialize

        public CMainControl_V4()
        {
            m_iVersion = 4;

            if (m_cProfilerProject != null)
                m_cProfilerProject.Clear();

            m_cProfilerProject = new CProfilerProject_V3();

            if (m_cDDEAProject != null)
                m_cDDEAProject.Clear();

            m_cDDEAProject = new CDDEAProject_V3("Create");
        }

        public CMainControl_V4(CMainControl_V3 cOldVersion)
        {
            m_iVersion = 4;
            CreateFrom(cOldVersion);
        }

        #endregion


        #region Properties

        public new CProfilerProject ProfilerProject
        {
            get
            {
                return m_cProfilerProject;
            }
            set
            {
                SetProfilerProject(value);
            }
        }

        public new CProfilerProject_V2 ProfilerProject_V2
        {
            get
            {
                return (CProfilerProject_V2)m_cProfilerProject;
            }
            set
            {
                SetProfilerProject((CProfilerProject)value);
            }
        }

        [DataMember]
        public CProfilerProject_V3 ProfilerProject_V3
        {
            get
            {
                return (CProfilerProject_V3)m_cProfilerProject;
            }
            set
            {
                m_cProfilerProject = (CProfilerProject)value;
            }
        }

        public new CDDEAProject DDEAProject
        {
            get
            {
                return m_cDDEAProject;
            }
            set
            {
                SetDDEAProject(value);
            }
        }

        public new CDDEAProject_V2 DDEAProject_V2
        {
            get
            {
                return (CDDEAProject_V2)m_cDDEAProject;
            }
            set
            {
                SetDDEAProject((CDDEAProject)value);
            }
        }

        [DataMember]
        public CDDEAProject_V3 DDEAProject_V3
        {
            get
            {
                return (CDDEAProject_V3)m_cDDEAProject;
            }
            set
            {
                m_cDDEAProject = (CDDEAProject)value;
            }
        }

        public string RenamingName
        {
            get
            {
                return m_sRenamingName;
            }
            set
            {
                //if (string.IsNullOrEmpty(m_sRenamingName))
                //    m_sRenamingName = ProfilerProject.Name;
                //else
                m_sRenamingName = value;
            }
        }

        #endregion


        #region Public Method

        public new bool CreateMainControl()
        {
            if (m_cProfilerProject == null)
                m_cProfilerProject = (CProfilerProject)new CProfilerProject_V3();
            else
                m_cProfilerProject.Clear();

            if (m_cDDEAProject == null)
                m_cDDEAProject = (CDDEAProject)new CDDEAProject_V2("Create");
            else
                m_cDDEAProject.Clear();

            return true;
        }

        public new bool Open(string sPath)
        {
            bool bOK = false;

            Clear();

            CPackSerializer<CMainControl_V4> cpackSerializer = new CPackSerializer<CMainControl_V4>();
            int iVersion = cpackSerializer.ReadVersion(sPath);
            if (iVersion == m_iVersion)
            {
                CMainControl_V4 cMainControl = cpackSerializer.Read(sPath, out iVersion);
                if (cMainControl != null)
                {
                    CloneV4(cMainControl);
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

                CMainControl_V3 cOldVersion = new CMainControl_V3();
                bOK = cOldVersion.Open(sPath);
                if (bOK)
                {
                    CloneV4(new CMainControl_V4(cOldVersion));
                    bOK = true;
                }
                else
                    return false;
            }

            if (m_cProfilerProject != null && m_cProfilerProject.TagS != null && m_cProfilerProject.StepS != null)
                m_cProfilerProject.Compose();

            for (int index = 0; index < m_cProfilerProject.TagS.Count; ++index)
            {
                CTag ctag = m_cProfilerProject.TagS.ElementAt<KeyValuePair<string, CTag>>(index).Value;
                if (ctag.DataType == EMDataType.DWord && ctag.Size != 2)
                    ctag.Size = 2;
            }

            return bOK;
        }

        public new bool Save(string sPath)
        {
            CPackSerializer<CMainControl_V4> cpackSerializer = new CPackSerializer<CMainControl_V4>();
            bool bOK = cpackSerializer.Write(sPath, this, m_iVersion);
            cpackSerializer.Dispose();

            return bOK;
        }

        public new bool Save()
        {
            if (string.IsNullOrEmpty(UpmSaveFilePath))
                return false;
            return Save(UpmSaveFilePath);
        }

        //yjk, 18.07.25 - PLCAddress Limit 설정 (갱신 Word 값에 영향있음)
        public void SetPLCAddressLimit()
        {
            //파라메터 정보를 취득한다.
            CReadFunction cReadFun = new CReadFunction((CDDEAConfigMS_V3)m_cDDEAProject.Config);
            m_cProfilerProject.PLCAddressLimit = cReadFun.ReadParameterSymbolSize();
        }

        #endregion


        #region Private Method

        protected void CreateFrom(CMainControl_V3 cOldVersion)
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

        protected void CloneV4(CMainControl_V4 cMainControl)
        {
            if (cMainControl.ProfilerProject != null)
                m_cProfilerProject = cMainControl.ProfilerProject;

            if (cMainControl.RefreshParameterS != null)
                m_cRefreshParam = cMainControl.RefreshParameterS;

            if (cMainControl.DDEAProject != null)
            {
                m_cDDEAProject = cMainControl.DDEAProject;

                //파라메타 정보 취득
                SetPLCAddressLimit();
            }

            ProjectName = cMainControl.ProjectName;
            CollectMode = cMainControl.CollectMode;
            IsSetCompFrag = cMainControl.IsSetCompFrag;
            PLCConfigTest = cMainControl.PLCConfigTest;
            DDEAFailAddress = cMainControl.DDEAFailAddress;
            LogSavePath = cMainControl.LogSavePath;
            NotUseYet = cMainControl.NotUseYet;
            UpmSaveFilePath = cMainControl.UpmSaveFilePath;
            LogSaveTime = cMainControl.LogSaveTime;
            LogDeleteDay = cMainControl.LogDeleteDay;
            RenamingName = cMainControl.RenamingName;
        }

        #endregion

    }
}
