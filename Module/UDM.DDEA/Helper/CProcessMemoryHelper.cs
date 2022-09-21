using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace UDM.DDEA
{
    public static class CProcessMemoryHelper
    {
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);
        public static void FlushMemory()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }


        //[DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        //static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
        //public static void FlushMemory()
        //{
        //    Process currentProcess = Process.GetCurrentProcess();

        //    GC.Collect(GC.MaxGeneration);
        //    GC.WaitForPendingFinalizers();

        //    SetProcessWorkingSetSize(currentProcess.Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
        //}
    }
}