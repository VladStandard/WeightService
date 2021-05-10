// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace ScalesCore.Win.Utils
{
    /// <summary>
    /// Dll Kernel32.
    /// </summary>
    public static class Kernel32
    {
        #region DllImport methods
        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion
    }
}
