// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.Program
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraSplashScreen;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using UDM.Common;
using UDM.DDEA;
using UDM.DDEA.Language;
using UDM.TimeChart.Language;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SkinManager.EnableFormSkins();
            BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            if (!Program.CheckVersionFromRegistry() && CMessageHelper.ShowPopup("현재 .NET Framework v4.5 설치되어 있지 않습니다. 프로그램이 정상적으로 동작하지 않을 수 있습니다. 그래도 계속 진행하시겠습니까?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                /*
                 *  jjk, 22.08.22 
                 *  Profiler 배포전 항상 확인하고 진행
                 *  USB Key Lock Mode 
                 *  회사 사이트에 따라 분기 Enum Type추가  
                 *  LG Energy Solution 이외에는 지멘스 기능 false 
                 */
                Utils.m_bUSBKeyLock = false;
                Utils.m_emCompanySite = EMCompanySite.LG_ENERGY_SOLUTION;

                //jjk, 20.07.31 - Lnaguage 추가
                ResLanguage.Culture = CRegistryHelper.ReadLangauge();
                ResDDEA.Culture = CRegistryHelper.ReadLangauge();
                ResTimeChart.Culture = CRegistryHelper.ReadLangauge();
                UDM.LogicViewer.ResLogicView.Culture = CRegistryHelper.ReadLangauge();

                //jjk, 20.06.18 - USB Key Lock Mode true 로 배포 하면 usb key락이 필요 false 일때는 기존 하드웨어 라이센스로 적용 및 데모 적용 
                if (Utils.m_bUSBKeyLock)
                {
                    CLogicHelper.USBKeyLock = Utils.m_bUSBKeyLock;
                    SplashScreenManager.ShowForm(typeof(FrmSplashScreen), true, false);
                    System.Threading.Thread.Sleep(1000);
                    Application.Run((Form)new FrmMain());
                }
                else
                {
                    CAppLicense cappLicense = new CAppLicense();
                    cappLicense.UEventDemoTodayTimeOut += new EventHandler(Program.cAppLicense_UEventDemoTodayTimeOut);

                    bool flag = cappLicense.Check();

                    if (flag)
                    {
                        SplashScreenManager.ShowForm(typeof(FrmSplashScreen), true, false);
                        System.Threading.Thread.Sleep(1000);
                        Application.Run((Form)new FrmMain());
                    }

                    if (!flag)
                        return;

                    cappLicense.Dispose();
                }

            }
            catch (Exception ex)
            {
                CMessageHelper.ShowPopup("Error : " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ex.Data.Clear();
            }
        }

        private static void cAppLicense_UEventDemoTodayTimeOut(object sender, EventArgs e)
        {
            ((CAppLicense)sender).Dispose();
            Application.ExitThread();
            Application.Exit();
        }

        private static bool CheckVersionFromRegistry()
        {
            using (RegistryKey subRegKey1 = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\"))
            {
                /*
                * .NetFramwork 4.5 이하 까지는 아래의 코드로 확인 가능하나 4.5 버전은 다름
                *
                foreach (string subKeyName in registryKey1.GetSubKeyNames())
                {
                    if (subKeyName.StartsWith("v3.5"))
                    {
                        RegistryKey registryKey2 = registryKey1.OpenSubKey(subKeyName);
                        string str = (string)registryKey2.GetValue("Version", (object)"");
                        if (registryKey2.GetValue("SP", (object)"").ToString() == "1" && registryKey2.GetValue("Install", (object)"").ToString() == "1")
                            return true;
                    }
                }
                */

                //yjk, 18.12.21 - .netFramwork 4.5 버전 확인
                foreach (string subKeyName in subRegKey1.GetSubKeyNames())
                {
                    if (subKeyName.StartsWith("v4"))
                    {
                        using (RegistryKey subRegKey2 = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full"))
                        {
                            object rValue = subRegKey2.GetValue("Release");
                            if (rValue != null)
                            {
                                int iVal = int.Parse(rValue.ToString());
                                if (iVal >= 378389)
                                    return true;
                            }
                        }
                    }
                }

                return false;
            }
        }
    }
}
