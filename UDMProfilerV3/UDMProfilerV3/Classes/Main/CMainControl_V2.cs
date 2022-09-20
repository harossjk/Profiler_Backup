// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CMainControl_V2
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using UDM.DDEA;
using UDM.General.Serialize;
using UDM.Project;

namespace UDMProfilerV3
{
    [DataContract]
    [Serializable]
    public class CMainControl_V2 : CMainControl
    {
        [NonSerialized]
        protected int m_iVersion = 0;

        public CMainControl_V2()
        {
            this.m_iVersion = 2;
            if (this.m_cProfilerProject != null)
                this.m_cProfilerProject.Clear();
            this.m_cProfilerProject = (CProfilerProject)new CProfilerProject_V2();
            if (this.m_cDDEAProject != null)
                this.m_cDDEAProject.Clear();
            this.m_cDDEAProject = new CDDEAProject("Create");
        }

        public CMainControl_V2(CMainControl cOldVersion)
        {
            this.m_iVersion = 2;
            this.CreateFrom(cOldVersion);
        }

        public new CProfilerProject ProfilerProject
        {
            get
            {
                return this.m_cProfilerProject;
            }
            set
            {
                this.SetProfilerProject(value);
            }
        }

        [DataMember]
        public CProfilerProject_V2 ProfilerProject_V2
        {
            get
            {
                return (CProfilerProject_V2)this.m_cProfilerProject;
            }
            set
            {
                this.m_cProfilerProject = (CProfilerProject)value;
            }
        }

        public new bool CreateMainControl()
        {
            if (this.m_cProfilerProject == null)
                this.m_cProfilerProject = (CProfilerProject)new CProfilerProject_V2();
            else
                this.m_cProfilerProject.Clear();
            if (this.m_cDDEAProject == null)
                this.m_cDDEAProject = new CDDEAProject("Create");
            else
                this.m_cDDEAProject.Clear();
            return true;
        }

        public new bool Open(string sPath)
        {
            bool flag = false;
            this.Clear();
            CPackSerializer<CMainControl_V2> cpackSerializer = new CPackSerializer<CMainControl_V2>();
            int iVersion = cpackSerializer.ReadVersion(sPath);
            if (iVersion == this.m_iVersion)
            {
                CMainControl_V2 cMainControl = cpackSerializer.Read(sPath, out iVersion);
                if (cMainControl != null)
                {
                    this.CloneV2(cMainControl);
                    flag = true;
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
                
                CMainControl cOldVersion = new CPackSerializer<CMainControl>().Read(sPath);
                
                if (cOldVersion != null)
                {
                    this.CreateFrom(cOldVersion);
                    flag = true;
                }
            }

            if (this.m_cProfilerProject != null && this.m_cProfilerProject.TagS != null && this.m_cProfilerProject.StepS != null)
                this.m_cProfilerProject.Compose();
          
            return flag;
        }

        public new bool Save(string sPath)
        {
            CPackSerializer<CMainControl_V2> cpackSerializer = new CPackSerializer<CMainControl_V2>();
            bool flag = cpackSerializer.Write(sPath, this, this.m_iVersion);
            cpackSerializer.Dispose();
            return flag;
        }

        public new bool Save()
        {
            if (string.IsNullOrEmpty(this.UpmSaveFilePath))
                return false;
            return this.Save(this.UpmSaveFilePath);
        }

        protected void CreateFrom(CMainControl cOldVersion)
        {
            this.m_sProjectName = cOldVersion.ProjectName;
            this.m_cDDEAProject = cOldVersion.DDEAProject;
            this.m_emCollectMode = cOldVersion.CollectMode;
            this.m_bSetFragModeComp = cOldVersion.IsSetCompFrag;
            this.m_bSetNormalModeComp = cOldVersion.IsSetCompNormal;
            this.m_cRefreshParam = cOldVersion.RefreshParameterS;
            this.m_bPlcConfigTest = cOldVersion.PLCConfigTest;
            this.m_lstDDEAFailAddress = cOldVersion.DDEAFailAddress;
            this.m_sLogSavePath = cOldVersion.LogSavePath;
            this.m_sReservedVariable = cOldVersion.NotUseYet;
            this.m_sUpmSavePath = cOldVersion.UpmSaveFilePath;
            this.m_iLogSaveTime = cOldVersion.LogSaveTime;
            this.m_iLogDeleteDay = cOldVersion.LogDeleteDay;

            SetProfilerProject(cOldVersion.ProfilerProject);
            SetDDEAProject(cOldVersion.DDEAProject);
        }

        protected void CloneV2(CMainControl_V2 cMainControl)
        {
            if (cMainControl.ProfilerProject != null)
                this.m_cProfilerProject = cMainControl.ProfilerProject;
            if (cMainControl.RefreshParameterS != null)
                this.m_cRefreshParam = cMainControl.RefreshParameterS;
            if (cMainControl.DDEAProject != null)
                this.m_cDDEAProject = cMainControl.DDEAProject;

            this.ProjectName = cMainControl.ProjectName;
            this.CollectMode = cMainControl.CollectMode;
            this.IsSetCompFrag = cMainControl.IsSetCompFrag;
            this.PLCConfigTest = cMainControl.PLCConfigTest;
            this.DDEAFailAddress = cMainControl.DDEAFailAddress;
            this.LogSavePath = cMainControl.LogSavePath;
            this.NotUseYet = cMainControl.NotUseYet;
            this.UpmSaveFilePath = cMainControl.UpmSaveFilePath;
            this.LogSaveTime = cMainControl.LogSaveTime;
            this.LogDeleteDay = cMainControl.LogDeleteDay;
        }
    }
}
