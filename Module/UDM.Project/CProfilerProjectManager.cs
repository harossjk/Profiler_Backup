// Decompiled with JetBrains decompiler
// Type: UDM.Project.CProfilerProjectManager
// Assembly: UDM.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3ED0201D-D318-4E1A-86FD-F515809D7DD2
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDM.Project.DLL

using System;
using UDM.General.Serialize;

namespace UDM.Project
{
    public class CProfilerProjectManager
    {
        protected CProfilerProject m_cProject = new CProfilerProject_V8();

        public void Dispose()
        {
            this.Clear();
        }

        public CProfilerProject Project
        {
            get
            {
                return this.m_cProject;
            }
            set
            {
                this.m_cProject = value;
            }
        }

        public bool Open(string sPath)
        {
            bool flag = true;
            this.Clear();

            CPackSerializer<CProfilerProject_V8> cpackSerializer = new CPackSerializer<CProfilerProject_V8>();
            CProfilerProject_V8 cprofilerProjectV8 = cpackSerializer.Read(sPath);
            cpackSerializer.Dispose();

            if (cprofilerProjectV8 != null)
            {
                m_cProject = (CProfilerProject)cprofilerProjectV8;
                m_cProject.Compose();
            }
            else
                flag = false;

            return flag;
        }

        public bool Save(string sPath)
        {
            CPackSerializer<CProfilerProject_V8> cpackSerializer = new CPackSerializer<CProfilerProject_V8>();
            bool flag = cpackSerializer.Write(sPath, (CProfilerProject_V8)m_cProject);
            cpackSerializer.Dispose();
            return flag;
        }

        public void Clear()
        {
            if (m_cProject != null)
                m_cProject.Clear();

            GC.Collect();
        }

        public bool ConvertFromMcscProject(string sPath)
        {
            CMcscProjectManager cmcscProjectManager = new CMcscProjectManager();
            bool flag = cmcscProjectManager.Open(sPath);

            if (flag)
            {
                ////yjk, 18.10.08 - CMcscProject_V3로 작업을 해야 할 때
                ////MCSC System에는 아직 LS 통신에 관한 부분이 없음 추후에 생긴다면 할당해 주어야함
                //((CProfilerProject_V4)m_cProject).LSConfig
                //((CProfilerProject_V4)m_cProject).PLCMaker
                //((CProfilerProject_V5)m_cProject).FilterNormalPacketS = ((CMcscProject_V3)project).FilterNormalPacketS;

                CMcscProject_V2 project = (CMcscProject_V2)cmcscProjectManager.Project;

                m_cProject = new CProfilerProject_V8();
                m_cProject.Name = project.Name;
                m_cProject.TagS = project.TagS;
                m_cProject.StepS = project.StepS;
                m_cProject.CycleStart = project.CycleStart;
                m_cProject.CycleEnd = project.CycleEnd;
                m_cProject.CycleTrigger = project.CycleTrigger;
                m_cProject.MaxCycleTime = project.MaxCycleTime;
                m_cProject.MinCycleTime = project.MinCycleTime;
                m_cProject.CycleCount = project.CycleCount;
                m_cProject.RecipeTag = project.RecipeTag;
                m_cProject.GlassIDTag = project.GlassIDTag;
                m_cProject.ProcessTag = project.ProcessTag;
                m_cProject.TackTimeTag = project.TackTimeTag;
                m_cProject.RefreshTag = project.RefreshTag;
                m_cProject.MDCTagInfoS = project.MDCTagInfoS;
                m_cProject.MDCColumnList = project.MDCColumnList;
                m_cProject.MDCItemList = project.MDCItemList;
                m_cProject.MotionOption = project.MotionOption;
                m_cProject.FilterOption = project.FilterOption;
                m_cProject.NormalPacketS = project.NormalPacketS;
                m_cProject.FragmentPacketS = project.FragmentPacketS;
                m_cProject.StandardPacketS = project.StandardPacketS;
                m_cProject.StandardRecipe = project.StandardRecipe;
                m_cProject.StandardCycleCount = project.StandardCycleCount;
                m_cProject.IsRecipeFixedCollected = project.IsRecipeFixedCollected;
                m_cProject.LogicChartDispItemS = project.LogicChartDispItemS;
                m_cProject.MdcChartDispItemS = project.MdcChartDispItemS;
                m_cProject.MdcChartItemDetailS = project.MdcChartItemDetailS;
                m_cProject.FilterOption.StepAddressFilterList = project.StepAddressFilterList;
                m_cProject.FilterOption.StepDescriptionFilterList = project.StepDescriptionFilterList;

                ((CProfilerProject_V8)m_cProject).FilterNormalCycleTime = project.FilterNormalCycleTime;
                ((CProfilerProject_V8)m_cProject).FilterNormalCycleCount = project.FilterNormalCycleCount;
                ((CProfilerProject_V8)m_cProject).FilterNormalMinimumLogCount = project.FilterNormalMinimumLogCount;
                ((CProfilerProject_V8)m_cProject).FilterNormalCycleTagKey = project.FilterNormalCycleTagKey;
                ((CProfilerProject_V8)m_cProject).LogFileName = project.LogFileName;
                ((CProfilerProject_V8)m_cProject).FilterNormalCycleStartKey = project.FilterNormalCycleStartKey;
                ((CProfilerProject_V8)m_cProject).FilterNormalCycleTriggerKey = project.FilterNormalCycleTriggerKey;
                ((CProfilerProject_V8)m_cProject).FilterNormalCycleStartValue = project.FilterNormalCycleStartValue;
                ((CProfilerProject_V8)m_cProject).FilterNormalCycleTriggerValue = project.FilterNormalCycleTriggerValue;
                ((CProfilerProject_V8)m_cProject).FilterNormalCycleTriggerOption = project.FilterNormalCycleTriggerOption;
            }

            return flag;
        }
    }
}
