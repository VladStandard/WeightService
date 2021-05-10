// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Management;
using System.Threading;

// ReSharper disable HollowTypeName

namespace ScalesMsi.Helpers
{
    /// <summary>
    /// Помощник WMI.
    /// </summary>
    internal class WmiHelper
    {
        #region Design pattern "Lazy Singleton"

        private static WmiHelper _instance;
        public static WmiHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public WmiHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            // Default methods
        }

        #endregion

        #region Public methods

        public string GetProcessMsi(string procName)
        {
            var result = string.Empty;
            try
            {
                var wmiQueryString = $"select processid, executablepath, commandline from win32_process where commandline like '%msiexec%'";
                using (var searcher = new ManagementObjectSearcher(wmiQueryString))
                using (var results = searcher.Get())
                {
                    foreach (ManagementObject item in results.Cast<ManagementObject>())
                    {
                        var cmd = item["commandline"].ToString();
                        if (!string.IsNullOrEmpty(cmd))
                        {
                            if (cmd.Contains(procName))
                            {
                                var posStart = cmd.IndexOf(@"/i """, StringComparison.Ordinal) + 4;
                                if (cmd.EndsWith(@""" "))
                                    result = cmd.Substring(posStart, cmd.Length - posStart - 2);
                                if (cmd.EndsWith(@""""))
                                    result = cmd.Substring(posStart, cmd.Length - posStart - 1);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //
            }
            return result;
        }

        #endregion
    }
}
