using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDMProfilerV3.Language;

namespace UDMProfilerV3
{
    public static class CRegistryHelper
    {

        public static void ReadLangauge()
        {
            //jjk, 19.11.19 - Registy 언어 타입 저장
            string sLanguage = ReadRegisty();
            if (sLanguage == "")
            {
                WriteRegistry("Korean");
                sLanguage = ReadRegisty();
            }

            if (sLanguage == "English")
            {
                CultureInfo engLanguage = new CultureInfo("en-US", true);
                Thread.CurrentThread.CurrentCulture = engLanguage;
                Thread.CurrentThread.CurrentUICulture = engLanguage;

                ResLanguage.Culture = engLanguage;
            }
            else if (sLanguage == "Korean")
            {
                CultureInfo korLanguage = new CultureInfo("ko-KR", true);
                Thread.CurrentThread.CurrentCulture = korLanguage;
                Thread.CurrentThread.CurrentUICulture = korLanguage;

                ResLanguage.Culture = korLanguage;
            }
        }


        //jjk, 19.11.20 - 레지스트 읽어오기
        public static string ReadRegisty()
        {
            RegistryKey resLocalKey;
            if (Environment.Is64BitOperatingSystem)
                resLocalKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\UDMProfilerV3", false);
            else
                resLocalKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\UDMProfilerV3", false);

            try
            {
                if (resLocalKey == null)
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        resLocalKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\UDMProfilerV3");
                    }
                    else
                    {
                        resLocalKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\UDMProfilerV3");
                    }
                }

                string sLanguage = Convert.ToString(resLocalKey.GetValue("Language"));

                return sLanguage;
            }
            catch
            {
                return "";
            }
            finally
            {
                resLocalKey.Close();
            }
        }

        //jjk, 19.11.20 - 레지스트 쓰기
        public static void WriteRegistry(string sLanguage)
        {
            RegistryKey resLocalKey;
            if (Environment.Is64BitOperatingSystem)
                resLocalKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\UDMProfilerV3", true);
            else
                resLocalKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true);

            resLocalKey.SetValue("Language", sLanguage);
        }

    }
}
