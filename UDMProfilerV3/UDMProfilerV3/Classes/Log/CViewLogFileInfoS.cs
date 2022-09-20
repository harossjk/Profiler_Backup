// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CViewLogFileInfoS
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace UDMProfilerV3
{
    public class CViewLogFileInfoS : List<CViewLogFileInfo>, IDisposable
    {
        public CViewLogFileInfoS(string[] saFileS)
        {
            this.CreateInstance(saFileS);
        }

        public void Dispose()
        {
            this.Clear();
        }

        private void CreateInstance(string[] saFiles)
        {
            this.Clear();
            if (saFiles == null || saFiles.Length == 0)
                return;
            for (int index = 0; index < saFiles.Length; ++index)
                this.Add(new CViewLogFileInfo(new FileInfo(saFiles[index])));
        }
    }
}
