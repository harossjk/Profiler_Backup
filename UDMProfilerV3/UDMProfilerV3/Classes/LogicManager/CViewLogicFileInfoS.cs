// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CViewLogicFileInfoS
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System;
using System.Collections.Generic;

namespace UDMProfilerV3
{
    public class CViewLogicFileInfoS : List<CViewLogicFileInfo>, IDisposable
    {
        public void Dispose()
        {
            this.Clear();
        }

        public void SetValidAll(bool bValue)
        {
            for (int index = 0; index < this.Count; ++index)
                this[index].IsValid = bValue;
        }
    }
}
