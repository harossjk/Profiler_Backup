// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CAssemblyHelper
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace UDMProfilerV3
{
  public static class CAssemblyHelper
  {
    private static string m_sProfilerProcesName = "UDMProfilerV3";
    private static string m_sDDEAProcessName = "UDMDDEA";
    private static string m_sDDEAAppPath = Application.StartupPath + "\\UDMDDEA.exe";

    public static string Title
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
        if (customAttributes.Length > 0)
        {
          AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute) customAttributes[0];
          if (assemblyTitleAttribute.Title != "")
            return assemblyTitleAttribute.Title;
        }
        return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public static string Version
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public static string DDEAAppPath
    {
      get
      {
        return CAssemblyHelper.m_sDDEAAppPath;
      }
    }

    public static string ProfilerProcessName
    {
      get
      {
        return CAssemblyHelper.m_sProfilerProcesName;
      }
    }

    public static string DDEAProcessName
    {
      get
      {
        return CAssemblyHelper.m_sDDEAProcessName;
      }
    }
  }
}
