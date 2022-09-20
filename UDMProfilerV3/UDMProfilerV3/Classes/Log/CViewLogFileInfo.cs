// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CViewLogFileInfo
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.IO;

namespace UDMProfilerV3
{
    public class CViewLogFileInfo : IDisposable
    {
        private string m_sName = "";
        private string m_sFormat = "";
        private string m_sPath = "";
        private int m_iSize = 0;

        public CViewLogFileInfo(FileInfo fInfo)
        {
            this.CreateInstance(fInfo);
        }

        public void Dispose()
        {
        }

        public string Name
        {
            get
            {
                return this.m_sName;
            }
            set
            {
                this.m_sName = value;
            }
        }

        public string Format
        {
            get
            {
                return this.m_sFormat;
            }
            set
            {
                this.m_sFormat = value;
            }
        }

        public string Path
        {
            get
            {
                return this.m_sPath;
            }
            set
            {
                this.m_sPath = value;
            }
        }

        public int Size
        {
            get
            {
                return this.m_iSize;
            }
            set
            {
                this.m_iSize = value;
            }
        }

        private void CreateInstance(FileInfo fInfo)
        {
            this.m_sName = fInfo.Name;
            this.m_sFormat = fInfo.Extension;
            this.m_sPath = fInfo.FullName;
            this.m_iSize = (int)(fInfo.Length / 1024L);
        }
    }
}
