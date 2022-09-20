// Decompiled with JetBrains decompiler
// Type: UDM.Project.CMcscProjectManager
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using System;
using UDM.General.Serialize;

namespace UDM.Project
{
    public class CMcscProjectManager
    {
        //yjk, 18.10.08 - CMCSCProject_V3로 업그레이드 될 때 수정되야 됨
        protected CMcscProject_V2 m_cProject = new CMcscProject_V2();

        public void Dispose()
        {
            this.Clear();
        }

        public CMcscProject Project
        {
            get
            {
                return this.m_cProject;
            }
            set
            {
                this.m_cProject = (CMcscProject_V2)value;
            }
        }

        public bool Open(string sPath)
        {
            bool flag = false;
            this.Clear();

            CPackSerializer<CMcscProject_V2> cpackSerializer1 = new CPackSerializer<CMcscProject_V2>();
            int iVersion = cpackSerializer1.ReadVersion(sPath);
            CPackSerializer<CMcscProject_V2> cpackSerializer2;
            if (iVersion == 1)
            {
                CMcscProject_V2 cmcscProjectV2 = cpackSerializer1.Read(sPath, out iVersion);
                cpackSerializer1.Dispose();
                cpackSerializer2 = (CPackSerializer<CMcscProject_V2>)null;
                if (cmcscProjectV2 != null)
                {
                    this.m_cProject = cmcscProjectV2;
                    this.m_cProject.Compose();
                    flag = true;
                }
            }
            else
            {
                cpackSerializer1.Dispose();
                cpackSerializer2 = (CPackSerializer<CMcscProject_V2>)null;
                CMcscProject cOldVersion = new CPackSerializer<CMcscProject>().Read(sPath);
                if (cOldVersion != null)
                {
                    cOldVersion.Compose();
                    this.m_cProject = new CMcscProject_V2(cOldVersion);
                    flag = true;
                }
            }
            return flag;
        }

        public bool Save(string sPath)
        {
            CPackSerializer<CMcscProject_V2> cpackSerializer = new CPackSerializer<CMcscProject_V2>();
            bool flag = cpackSerializer.Write(sPath, this.m_cProject, 1);
            cpackSerializer.Dispose();
            return flag;
        }

        public void Clear()
        {
            if (this.m_cProject != null)
                this.m_cProject.Clear();
            GC.Collect();
        }

        public bool ConvertFromProfilerProject(string sPath)
        {
            CProfilerProjectManager cprofilerProjectManager = new CProfilerProjectManager();
            bool flag = cprofilerProjectManager.Open(sPath);
            if (flag)
            {
                CProfilerProject_V3 cprofilerProjectV3 = new CProfilerProject_V3();
                CProfilerProject_V3 project = (CProfilerProject_V3)cprofilerProjectManager.Project;
                this.m_cProject = new CMcscProject_V2();
                this.m_cProject.Name = project.Name;
                this.m_cProject.TagS = project.TagS;
                this.m_cProject.StepS = project.StepS;
                this.m_cProject.CycleStart = project.CycleStart;
                this.m_cProject.CycleEnd = project.CycleEnd;
                this.m_cProject.CycleTrigger = project.CycleTrigger;
                this.m_cProject.MaxCycleTime = project.MaxCycleTime;
                this.m_cProject.MinCycleTime = project.MinCycleTime;
                this.m_cProject.CycleCount = project.CycleCount;
                this.m_cProject.RecipeTag = project.RecipeTag;
                this.m_cProject.GlassIDTag = project.GlassIDTag;
                this.m_cProject.ProcessTag = project.ProcessTag;
                this.m_cProject.TackTimeTag = project.TackTimeTag;
                this.m_cProject.RefreshTag = project.RefreshTag;
                this.m_cProject.MDCTagInfoS = project.MDCTagInfoS;
                this.m_cProject.MDCColumnList = project.MDCColumnList;
                this.m_cProject.MDCItemList = project.MDCItemList;
                this.m_cProject.MotionOption = project.MotionOption;
                this.m_cProject.FilterOption = project.FilterOption;
                this.m_cProject.NormalPacketS = project.NormalPacketS;
                this.m_cProject.FragmentPacketS = project.FragmentPacketS;
                this.m_cProject.StandardPacketS = project.StandardPacketS;
                this.m_cProject.StandardRecipe = project.StandardRecipe;
                this.m_cProject.StandardCycleCount = project.StandardCycleCount;
                this.m_cProject.IsRecipeFixedCollected = project.IsRecipeFixedCollected;
                this.m_cProject.LogicChartDispItemS = project.LogicChartDispItemS;
                this.m_cProject.MdcChartDispItemS = project.MdcChartDispItemS;
                this.m_cProject.MdcChartItemDetailS = project.MdcChartItemDetailS;
                this.m_cProject.StepAddressFilterList = project.FilterOption.StepAddressFilterList;
                this.m_cProject.StepDescriptionFilterList = project.FilterOption.StepDescriptionFilterList;
                this.m_cProject.FilterNormalCycleTime = project.FilterNormalCycleTime;
                this.m_cProject.FilterNormalCycleCount = project.FilterNormalCycleCount;
                this.m_cProject.FilterNormalMinimumLogCount = project.FilterNormalMinimumLogCount;
                this.m_cProject.FilterNormalCycleTagKey = project.FilterNormalCycleTagKey;
                this.m_cProject.LogFileName = project.LogFileName;
                this.m_cProject.FilterNormalCycleStartKey = project.FilterNormalCycleStartKey;
                this.m_cProject.FilterNormalCycleTriggerKey = project.FilterNormalCycleTriggerKey;
                this.m_cProject.FilterNormalCycleStartValue = project.FilterNormalCycleStartValue;
                this.m_cProject.FilterNormalCycleTriggerValue = project.FilterNormalCycleTriggerValue;
                this.m_cProject.FilterNormalCycleTriggerOption = project.FilterNormalCycleTriggerOption;
            }
            return flag;
        }

        public CProfilerProject_V3 ConvertToProfilerProject(CMcscProject_V2 cMProject)
        {
            if (cMProject == null)
                return (CProfilerProject_V6)null;
            //CProfilerProject_V3 cprofilerProjectV3 = new CProfilerProject_V3();
            //jjk, 19.08.05 - 동작연계표 수정에 대한 MCSC+ USB 파일 오픈시 버전업을 같이 해줘야 하기 때문에 버전업을 시킴.
            CProfilerProject_V6 cprofilerProjectV3 = new CProfilerProject_V6();

            cprofilerProjectV3.CycleCount = cMProject.CycleCount;
            cprofilerProjectV3.CycleEnd = cMProject.CycleEnd;
            cprofilerProjectV3.CycleStart = cMProject.CycleStart;
            cprofilerProjectV3.CycleTrigger = cMProject.CycleTrigger;
            cprofilerProjectV3.FilterNormalCycleCount = cMProject.FilterNormalCycleCount;
            cprofilerProjectV3.FilterNormalCycleStartKey = cMProject.FilterNormalCycleStartKey;
            cprofilerProjectV3.FilterNormalCycleStartValue = cMProject.FilterNormalCycleStartValue;
            cprofilerProjectV3.FilterNormalCycleTagKey = cMProject.FilterNormalCycleTagKey;
            cprofilerProjectV3.FilterNormalCycleTime = cMProject.FilterNormalCycleTime;
            cprofilerProjectV3.FilterNormalCycleTriggerKey = cMProject.FilterNormalCycleTriggerKey;
            cprofilerProjectV3.FilterNormalCycleTriggerOption = cMProject.FilterNormalCycleTriggerOption;
            cprofilerProjectV3.FilterNormalCycleTriggerValue = cMProject.FilterNormalCycleTriggerValue;
            cprofilerProjectV3.FilterNormalMinimumLogCount = cMProject.FilterNormalMinimumLogCount;
            cprofilerProjectV3.FilterOption = cMProject.FilterOption;
            cprofilerProjectV3.FragmentPacketS = cMProject.FragmentPacketS;
            cprofilerProjectV3.GlassIDTag = cMProject.GlassIDTag;
            cprofilerProjectV3.IsRecipeFixedCollected = cMProject.IsRecipeFixedCollected;
            cprofilerProjectV3.LogFileName = cMProject.LogFileName;
            cprofilerProjectV3.LogicChartDispItemS = cMProject.LogicChartDispItemS;
            cprofilerProjectV3.MaxCycleTime = cMProject.MaxCycleTime;
            cprofilerProjectV3.MdcChartDispItemS = cMProject.MdcChartDispItemS;
            cprofilerProjectV3.MdcChartItemDetailS = cMProject.MdcChartItemDetailS;
            cprofilerProjectV3.MDCColumnList = cMProject.MDCColumnList;
            cprofilerProjectV3.MDCItemList = cMProject.MDCItemList;
            cprofilerProjectV3.MDCTagInfoS = cMProject.MDCTagInfoS;
            cprofilerProjectV3.MinCycleTime = cMProject.MinCycleTime;
            cprofilerProjectV3.MotionOption = cMProject.MotionOption;
            cprofilerProjectV3.Name = cMProject.Name;
            cprofilerProjectV3.NormalPacketS = cMProject.NormalPacketS;
            cprofilerProjectV3.ProcessTag = cMProject.ProcessTag;
            cprofilerProjectV3.RecipeTag = cMProject.RecipeTag;
            cprofilerProjectV3.RefreshTag = cMProject.RefreshTag;
            cprofilerProjectV3.StandardCycleCount = cMProject.StandardCycleCount;
            cprofilerProjectV3.StandardPacketS = cMProject.StandardPacketS;
            cprofilerProjectV3.StandardRecipe = cMProject.StandardRecipe;
            cprofilerProjectV3.StepS = cMProject.StepS;
            cprofilerProjectV3.TackTimeTag = cMProject.TackTimeTag;
            cprofilerProjectV3.TagS = cMProject.TagS;
            return cprofilerProjectV3;
        }
    }
}
