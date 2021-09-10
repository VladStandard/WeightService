// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник Windows.
    /// </summary>
    public sealed class HardwareHelper
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<HardwareHelper> _instance = new(() => new HardwareHelper());
        public static HardwareHelper Instance => _instance.Value;
        private HardwareHelper() { }

        #endregion

        #region Public methods

        /// <summary>
        /// Methods return System manufacture, model and Bios version.
        /// </summary>
        /// <param name="infoSelect"></param>
        /// <returns></returns>
        public Dictionary<string, string> HardwareInfoSelect(string infoSelect)
        {
            Dictionary<string, string> systemInfo = new();
            SelectQuery query = new(@"Select * from " + infoSelect);

            //initialize the searcher with the query it is supposed to execute
            using (ManagementObjectSearcher searcher = new(query))
            {
                try
                {
                    // execute the query
                    foreach (ManagementBaseObject proc in searcher.Get())
                    {
                        // print system info
                        ((ManagementObject) proc).Get();
                        systemInfo.Add("Manufacturer", proc["Manufacturer"].ToString());
                        systemInfo.Add("Model", proc["Model"].ToString().Trim());
                        systemInfo.Add("ComputerName", proc["Caption"].ToString().Trim());
                        systemInfo.Add("Domain", proc["Domain"].ToString().Trim());
                        systemInfo.Add("UserName", proc["PrimaryOwnerName"].ToString().Trim());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            ManagementObjectSearcher searcher1 = new("SELECT * FROM Win32_BIOS");
            ManagementObjectCollection collection = searcher1.Get();

            foreach (ManagementBaseObject obj in collection)
            {
                ((ManagementObject)obj).Get();
                if (((string[])obj["BIOSVersion"]).Length > 1)
                    systemInfo.Add("BIOSVersion", ((string[])obj["BIOSVersion"])[0] + " - " + ((string[])obj["BIOSVersion"])[1]);
                else
                    systemInfo.Add("BIOSVersion", ((string[])obj["BIOSVersion"])[0]);
            }
            return systemInfo;
        }

        /// <summary>
        /// Method return instaled program in PC.
        /// </summary>
        /// <returns></returns>
        public List<string> ProgramPrint()
        {
            Stopwatch pr = new();
            Stopwatch pr2 = new();
            List<string> installed_program = new();

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                if (key != null)
                    foreach (string subKeyName in key.GetSubKeyNames())
                    {
                        using RegistryKey subKey = key.OpenSubKey(subKeyName);
                        try
                        {
                            if (subKey != null) installed_program.Add(subKey.GetValue("DisplayName").ToString());
                        }
                        catch (Exception)
                        {
                            //
                        }
                    }
            }
            pr.Stop();
            Console.WriteLine(@"Time elapsed using: {0}", pr.Elapsed);
            Console.WriteLine(@"Time elapsed foreach: {0}", pr2.Elapsed);
            return installed_program;
        }

        #endregion
    }
}
