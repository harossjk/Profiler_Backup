// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CMainControl
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;
using UDM.DDEA;
using UDM.DDEACommon;
using UDM.General.Serialize;
using UDM.Project;

namespace UDMProfilerV3
{
    [DataContract]
    [Serializable]
    public class CMainControl
    {
        protected string m_sProjectName = "";
        protected EMCollectMode m_emCollectMode = EMCollectMode.Wait;
        protected bool m_bSetFragModeComp = false;
        protected bool m_bSetNormalModeComp = false;
        protected bool m_bPlcConfigTest = true;
        protected List<string> m_lstDDEAFailAddress = null;
        protected string m_sLogSavePath = Application.StartupPath;
        protected string m_sReservedVariable = "";
        protected string m_sUpmSavePath = "";
        protected CProfilerProject m_cProfilerProject = null;
        protected CRefreshParameterS m_cRefreshParam = new CRefreshParameterS();
        protected CDDEAProject m_cDDEAProject = null;
        protected int m_iLogDeleteDay = 1;
        protected int m_iLogSaveTime = 60;

        public CMainControl()
        {
            if (this.m_cProfilerProject == null)
                this.m_cProfilerProject = new CProfilerProject();
            else
                this.m_cProfilerProject.Clear();
            if (this.m_cDDEAProject == null)
                this.m_cDDEAProject = new CDDEAProject("Create");
            else
                this.m_cDDEAProject.Clear();
        }

        [DataMember]
        public string ProjectName
        {
            get
            {
                return this.m_sProjectName;
            }
            set
            {
                this.m_sProjectName = value;
            }
        }

        [DataMember]
        public CProfilerProject ProfilerProject
        {
            get
            {
                return this.m_cProfilerProject;
            }
            set
            {
                this.m_cProfilerProject = value;
            }
        }

        [DataMember]
        public CDDEAProject DDEAProject
        {
            get
            {
                return this.m_cDDEAProject;
            }
            set
            {
                this.m_cDDEAProject = value;
            }
        }

        [DataMember]
        public EMCollectMode CollectMode
        {
            get
            {
                return this.m_emCollectMode;
            }
            set
            {
                this.m_emCollectMode = value;
            }
        }

        [DataMember]
        public bool IsSetCompFrag
        {
            get
            {
                return this.m_bSetFragModeComp;
            }
            set
            {
                this.m_bSetFragModeComp = value;
            }
        }

        [DataMember]
        public bool IsSetCompNormal
        {
            get
            {
                return this.m_bSetNormalModeComp;
            }
            set
            {
                this.m_bSetNormalModeComp = value;
            }
        }

        [DataMember]
        public CRefreshParameterS RefreshParameterS
        {
            get
            {
                return this.m_cRefreshParam;
            }
            set
            {
                this.m_cRefreshParam = value;
            }
        }

        [DataMember]
        public bool PLCConfigTest
        {
            get
            {
                return this.m_bPlcConfigTest;
            }
            set
            {
                this.m_bPlcConfigTest = value;
            }
        }

        [DataMember]
        public List<string> DDEAFailAddress
        {
            get
            {
                return this.m_lstDDEAFailAddress;
            }
            set
            {
                this.m_lstDDEAFailAddress = value;
            }
        }

        [DataMember]
        public string LogSavePath
        {
            get
            {
                return this.m_sLogSavePath;
            }
            set
            {
                this.m_sLogSavePath = value;
            }
        }

        [DataMember(Name = "SavedLogPath")]
        public string NotUseYet
        {
            get
            {
                return this.m_sReservedVariable;
            }
            set
            {
                this.m_sReservedVariable = value;
            }
        }

        [DataMember]
        public string UpmSaveFilePath
        {
            get
            {
                return this.m_sUpmSavePath;
            }
            set
            {
                this.m_sUpmSavePath = value;
            }
        }

        [DataMember]
        public int LogSaveTime
        {
            get
            {
                return this.m_iLogSaveTime;
            }
            set
            {
                this.m_iLogSaveTime = value;
            }
        }

        [DataMember]
        public int LogDeleteDay
        {
            get
            {
                return this.m_iLogDeleteDay;
            }
            set
            {
                this.m_iLogDeleteDay = value;
            }
        }

        public bool CreateMainControl()
        {
            if (this.m_cProfilerProject == null)
                this.m_cProfilerProject = new CProfilerProject();
            else
                this.m_cProfilerProject.Clear();
            if (this.m_cDDEAProject == null)
                this.m_cDDEAProject = new CDDEAProject("Create");
            else
                this.m_cDDEAProject.Clear();
            return true;
        }

        public void Clear()
        {
            if (this.m_cDDEAProject != null)
                this.m_cDDEAProject.Clear();
            if (this.m_cProfilerProject != null)
                this.m_cProfilerProject.Clear();
        }

        public bool Open(string sPath)
        {
            bool flag = true;
            this.Clear();
            CPackSerializer<CMainControl> cpackSerializer = new CPackSerializer<CMainControl>();
            CMainControl cMainControl = cpackSerializer.Read(sPath);
            cpackSerializer.Dispose();

            if (cMainControl != null)
                this.Clone(cMainControl);
            else
                flag = false;

            if (this.m_cProfilerProject != null && this.m_cProfilerProject.TagS != null && this.m_cProfilerProject.StepS != null)
                this.m_cProfilerProject.Compose();

            return flag;
        }

        public bool Save(string sPath)
        {
            CPackSerializer<CMainControl> cpackSerializer = new CPackSerializer<CMainControl>();
            bool flag = cpackSerializer.Write(sPath, this);
            cpackSerializer.Dispose();

            return flag;
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(this.UpmSaveFilePath))
                return false;

            return this.Save(this.UpmSaveFilePath);
        }

        protected void Clone(CMainControl cMainControl)
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

        //yjk, 18.10.05 - 구버전의 ProfilerProject를 속성으로 가지고 있는 경우 상위 버전으로 생성
        protected void SetProfilerProject(CProfilerProject cProject)
        {
            if (cProject == null)
            {
                m_cProfilerProject = null;
                return;
            }

            //구버전의 CProfilerProject를 현재 버전으로 호환
            if (cProject.GetType() == typeof(CProfilerProject_V7))
            {
                //jjk, 21.04.26 - Auto 기능 추가로 인한 버전업
                m_cProfilerProject = new CProfilerProject_V8((CProfilerProject_V7)cProject);
            }
            else if (cProject.GetType() == typeof(CProfilerProject_V6))
            {
                m_cProfilerProject = new CProfilerProject_V7((CProfilerProject_V6)cProject);
            }
            else if (cProject.GetType() == typeof(CProfilerProject_V5))
            {
                m_cProfilerProject = new CProfilerProject_V6((CProfilerProject_V5)cProject);
            }
            else if (cProject.GetType() == typeof(CProfilerProject_V4))
            {
                m_cProfilerProject = new CProfilerProject_V5((CProfilerProject_V4)cProject);
            }
            else if (cProject.GetType() == typeof(CProfilerProject_V3))
            {
                m_cProfilerProject = new CProfilerProject_V4((CProfilerProject_V3)cProject);
            }
            else if (cProject.GetType() == typeof(CProfilerProject_V2))
            {
                m_cProfilerProject = new CProfilerProject_V3((CProfilerProject_V2)cProject);
            }
            else if (cProject.GetType() == typeof(CProfilerProject))
            {
                m_cProfilerProject = new CProfilerProject_V2(cProject);
            }
            else
            {
                //현재 버전은 그대로 할당
                m_cProfilerProject = cProject;
            }
        }

        //yjk, 18.10.05 - 구버전의 DDEAProject를 속성으로 가지고 있는 경우 상위 버전으로 생성
        protected void SetDDEAProject(CDDEAProject cProject)
        {
            if (cProject == null)
            {
                m_cDDEAProject = null;
                return;
            }

            //구버전의 CProfilerProject를 현재 버전으로 호환
            if (cProject.GetType() == typeof(CDDEAProject_V7))
            {
                m_cDDEAProject = new CDDEAProject_V8((CDDEAProject_V7)cProject);
            }
            else if (cProject.GetType() == typeof(CDDEAProject_V6))
            {
                m_cDDEAProject = new CDDEAProject_V7((CDDEAProject_V6)cProject);
            }
            else if (cProject.GetType() == typeof(CDDEAProject_V5))
            {
                m_cDDEAProject = new CDDEAProject_V6((CDDEAProject_V5)cProject);
            }
            else if (cProject.GetType() == typeof(CDDEAProject_V4))
            {
                m_cDDEAProject = new CDDEAProject_V5((CDDEAProject_V4)cProject);
            }
            else if (cProject.GetType() == typeof(CDDEAProject_V3))
            {
                m_cDDEAProject = new CDDEAProject_V4((CDDEAProject_V3)cProject);
            }
            else if (cProject.GetType() == typeof(CDDEAProject_V2))
            {
                m_cDDEAProject = new CDDEAProject_V3((CDDEAProject_V2)cProject);
            }
            else if (cProject.GetType() == typeof(CDDEAProject))
            {
                m_cDDEAProject = new CDDEAProject_V2(new CDDEAProject());
            }
            else
            {
                //현재 버전은 그대로 할당
                m_cDDEAProject = cProject;
            }
        }
    }
}
