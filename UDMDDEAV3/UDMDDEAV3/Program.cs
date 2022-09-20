using System;
using System.Windows.Forms;
using System.Reflection;
using UDM.General.AssemblyLoader;
using System.Diagnostics;
using System.Security.Principal;
using UDMDDEA.Language;
using UDM.DDEA;

namespace UDMDDEA
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] arrPath)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load Assembly
            CAssemblyLoader.AppNameSpace = "UDMDDEA";
            CAssemblyLoader.CurrentDomain = AppDomain.CurrentDomain;
            CAssemblyLoader.ExecutingAssembly = Assembly.GetExecutingAssembly();

            if(arrPath == null || arrPath.Length == 0)
                arrPath = new string[2] { "Profiler", "TCP" };

            //jjk, 20.07.28 - Language 추가
            ResDDEAMain.Culture = CRegistryHelper.ReadLangauge();
            Application.Run(new FrmMain(arrPath));
        }
    }
}
