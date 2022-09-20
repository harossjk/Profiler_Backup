using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using UDM.DDEA;
using UDM.Project;
using UDM.General.Serialize;
using System.Windows.Forms;
using UDM.Common;

namespace UDMProfilerV3
{
    //yjk, 18.10.02
    [DataContract]
    [Serializable]
    public class CMainControl_V6 : CMainControl_V5
    {
        protected bool m_bSetCompFilterNormal = false;

        public CMainControl_V6()
        {
            m_iVersion = 6;

            if (m_cProfilerProject != null)
                m_cProfilerProject.Clear();

            m_cProfilerProject = new CProfilerProject_V5();

            if (m_cDDEAProject != null)
                m_cDDEAProject.Clear();

            m_cDDEAProject = new CDDEAProject_V5("Create");
        }

        public CMainControl_V6(CMainControl_V5 cOldVersion)
        {
            m_iVersion = 6;
            CreateFrom(cOldVersion);
        }

        [DataMember]
        public CDDEAProject_V5 DDEAProject_V5
        {
            get
            {
                return (CDDEAProject_V5)m_cDDEAProject;
            }
            set
            {
                m_cDDEAProject = value;
            }
        }

        [DataMember]
        public CProfilerProject_V5 ProfilerProject_V5
        {
            get
            {
                return (CProfilerProject_V5)m_cProfilerProject;
            }
            set
            {
                m_cProfilerProject = value;
            }
        }
        
        [DataMember]
        public bool IsSetCompFilterNormal
        {
            get
            {
                return m_bSetCompFilterNormal;
            }
            set
            {
                m_bSetCompFilterNormal = value;
            }
        }

        public new bool Open(string sPath)
        {
            bool bOK = false;
            Clear();
            CPackSerializer<CMainControl_V6> cpackSerializer = new CPackSerializer<CMainControl_V6>();

            int iVersion = cpackSerializer.ReadVersion(sPath);
            if (iVersion == m_iVersion)
            {
                CMainControl_V6 cMainControl = cpackSerializer.Read(sPath, out iVersion);
                if (cMainControl != null)
                {
                    CloneV6(cMainControl);
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

                CMainControl_V5 cOldVersion1 = new CMainControl_V5();
                if (cOldVersion1.Open(sPath))
                {
                    CMainControl_V6 cMainControl = new CMainControl_V6(cOldVersion1);

                    if (cMainControl != null)
                        CloneV6(cMainControl);

                    bOK = true;
                }
                //else
                //{
                //    CMainControl_V4 cOldVersion2 = new CMainControl_V4();
                //    flag = cOldVersion2.Open(sPath);

                //    if (flag)
                //    {
                //        CMainControl_V4 cMainControl = new CMainControl_V4(cOldVersion2);

                //        if (cMainControl != null)
                //            CloneV5(cMainControl);

                //        flag = true;
                //    }
                //    else
                //    { 

                //    }
                //}
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
            CPackSerializer<CMainControl_V6> cpackSerializer = new CPackSerializer<CMainControl_V6>();
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

        protected void CloneV6(CMainControl_V6 cMainControl)
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

        protected void CreateFrom(CMainControl_V5 cOldVersion)
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
    }
}
