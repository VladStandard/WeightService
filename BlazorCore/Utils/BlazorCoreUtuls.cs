// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;

namespace DataCore.Utils
{
    public static class BlazorCoreUtuls
    {
        #region Public and private methods

        public static string GetLibVersion()
        {
            string result = string.Empty;
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (fieVersionInfo == null || string.IsNullOrEmpty(fieVersionInfo.FileVersion))
                return result;

            result = fieVersionInfo.FileVersion;
            if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
                result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
            return result;
        }

        #endregion
    }
}
