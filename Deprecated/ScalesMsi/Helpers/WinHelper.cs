// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Win32;
using ScalesMsi.Models;
using ScalesMsi.Utils;
using System;
using System.Collections.Generic;
using System.Management;
using System.Security.Principal;
using System.Threading;

// ReSharper disable HollowTypeName

namespace ScalesMsi.Helpers
{
    /// <summary>
    /// Помощник Windows.
    /// </summary>
    internal class WinHelper
    {
        #region Design pattern "Lazy Singleton"

        private static WinHelper _instance;
        public static WinHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public WinHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            // Default methods
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Проверить роль текущего пользователя.
        /// </summary>
        /// <returns></returns>
        public bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            //return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Информация о Windows.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetInfo()
        {
            var result = new Dictionary<string, string>();
            var query = new SelectQuery(@"Select Caption,OSArchitecture,SerialNumber from Win32_OperatingSystem");

            using (var searcher = new ManagementObjectSearcher(query))
            {
                try
                {
                    foreach (var process in searcher.Get())
                    {
                        ((ManagementObject)process).Get();
                        result.Add("SerialNumber", process["SerialNumber"].ToString());
                        result.Add("SystemVersion", process["Caption"].ToString());
                        result.Add("Architecture", process["OSArchitecture"].ToString());

                    }

                    result.Add("CoreVersion", Environment.OSVersion.ToString());
                    result.Add("ComputerName", Environment.MachineName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return result;
        }

        /// <summary>
        /// Найти программу средствами WMI.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<ResultWmiSoftware> SearchingSoftwareFromWmi(string search)
        {
            var result = new List<ResultWmiSoftware>();
            try
            {
                // gwmi -Class Win32_Product | select identifyingnumber, name, vendor, version, caption | where {$_.name -like "*Visual C++ Library*" }
                var query = new SelectQuery(
                    @"SELECT IDENTIFYINGNUMBER, NAME, VENDOR, VERSION, LANGUAGE FROM WIN32_PRODUCT")
                {
                    Condition = $"Name LIKE '*{search}*'",
                };
                var searcher = new ManagementObjectSearcher(query);
                var items = searcher.Get();
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        var guid = string.Empty;
                        var name = string.Empty;
                        var vendor = string.Empty;
                        var version = string.Empty;
                        var language = string.Empty;
                        foreach (var prop in item.Properties)
                        {
                            if (prop.Name.Equals("IDENTIFYINGNUMBER", StringComparison.InvariantCultureIgnoreCase))
                                language = prop.Value.ToString();
                            else if (prop.Name.Equals("NAME", StringComparison.InvariantCultureIgnoreCase))
                                name = prop.Value.ToString();
                            else if (prop.Name.Equals("VENDOR", StringComparison.InvariantCultureIgnoreCase))
                                vendor = prop.Value.ToString();
                            else if (prop.Name.Equals("VERSION", StringComparison.InvariantCultureIgnoreCase))
                                version = prop.Value.ToString();
                            else if (prop.Name.Equals("LANGUAGE", StringComparison.InvariantCultureIgnoreCase))
                                guid = prop.Value.ToString();
                        }
                        result.Add(new ResultWmiSoftware(name, vendor, version, guid, language));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Найти программу средствами реестра.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public List<ResultWmiSoftware> SearchingSoftwareFromRegistry(string search, EnumStringTemplate template)
        {
            var result = new List<ResultWmiSoftware>();
            try
            {
                var reg64 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                var reg32 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
                var keyPrograms = Registry.LocalMachine.OpenSubKey(Environment.Is64BitOperatingSystem ? reg64 : reg32);
                if (keyPrograms != null)
                {
                    foreach (var guid in keyPrograms.GetSubKeyNames())
                    {
                        var key = keyPrograms.OpenSubKey(guid);
                        if (key?.GetValue("DisplayName") != null)
                        {
                            var isFind = false;
                            var name = key.GetValue("DisplayName") as string;
                            if (!string.IsNullOrEmpty(name))
                            {
                                switch (template)
                                {
                                    case EnumStringTemplate.Equals:
                                        if (name.Equals(search, StringComparison.InvariantCultureIgnoreCase))
                                            isFind = true;
                                        break;
                                    case EnumStringTemplate.Contains:
                                        if (name.ToUpper().Contains(search.ToUpper()))
                                            isFind = true;
                                        break;
                                    case EnumStringTemplate.StartsWith:
                                        if (name.ToUpper().StartsWith(search.ToUpper()))
                                            isFind = true;
                                        break;
                                    case EnumStringTemplate.EndsWith:
                                        if (name.ToUpper().EndsWith(search.ToUpper()))
                                            isFind = true;
                                        break;
                                }
                            }
                            if (isFind)
                            {
                                var vendor = key.GetValue("Publisher") as string;
                                var version = key.GetValue("DisplayVersion") as string;
                                var language = key.GetValue("Language") as string;
                                result.Add(new ResultWmiSoftware(name, vendor, version, guid, language));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Найти программу.
        /// </summary>
        /// <param name="winProvider"></param>
        /// <param name="search"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public List<ResultWmiSoftware> SearchingSoftware(EnumWinProvider winProvider, string search, EnumStringTemplate template)
        {
            switch (winProvider)
            {
                case EnumWinProvider.Registry:
                    return SearchingSoftwareFromRegistry(search, template);
                case EnumWinProvider.Alias:
                    break;
                case EnumWinProvider.Environment:
                    break;
                case EnumWinProvider.FileSystem:
                    break;
                case EnumWinProvider.Function:
                    break;
                case EnumWinProvider.Variable:
                    break;
                case EnumWinProvider.Wmi:
                    return SearchingSoftwareFromWmi(search);
            }
            return new List<ResultWmiSoftware>();
        }

        #endregion
    }
}
