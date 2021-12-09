// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Security.Principal;
using System.Threading;
using WeightCore.Models;

namespace WeightCore.Helpers
{
    public class WinHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static WinHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static WinHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public methods

        /// <summary>
        /// Проверить роль текущего пользователя.
        /// </summary>
        /// <returns></returns>
        public bool IsAdministrator()
        {
            using WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
            //return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Информация о Windows.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetInfo()
        {
            Dictionary<string, string> result = new();
            SelectQuery query = new(@"Select Caption,OSArchitecture,SerialNumber from Win32_OperatingSystem");

            using (ManagementObjectSearcher searcher = new(query))
            {
                try
                {
                    foreach (ManagementBaseObject process in searcher.Get())
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
        public ResultWmiSoftware SearchingSoftwareFromWmi(string search)
        {
            try
            {
                // gwmi -Class Win32_Product | select identifyingnumber, name, vendor, version, caption | where {$_.name -like "*Visual C++ Library*" }
                SelectQuery query = new(
                    @"SELECT IDENTIFYINGNUMBER, NAME, VENDOR, VERSION, LANGUAGE FROM WIN32_PRODUCT")
                {
                    Condition = $"Name LIKE '*{search}*'",
                };
                ManagementObjectSearcher searcher = new(query);
                ManagementObjectCollection items = searcher.Get();
                if (items.Count > 0)
                {
                    foreach (ManagementBaseObject item in items)
                    {
                        string guid = string.Empty;
                        string name = string.Empty;
                        string vendor = string.Empty;
                        string version = string.Empty;
                        string language = string.Empty;
                        foreach (PropertyData prop in item.Properties)
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
                        return new ResultWmiSoftware(name, vendor, version, guid, language);
                    }
                }
            }
            catch (Exception)
            {
                //
            }
            return new ResultWmiSoftware();
        }

        /// <summary>
        /// Найти программу средствами реестра.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public ResultWmiSoftware SearchingSoftwareFromRegistry(string search, ShareEnums.StringTemplate template)
        {
            try
            {
                string reg64 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                string reg32 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
                RegistryKey keyPrograms = Registry.LocalMachine.OpenSubKey(Environment.Is64BitOperatingSystem ? reg64 : reg32);
                if (keyPrograms != null)
                {
                    foreach (string guid in keyPrograms.GetSubKeyNames())
                    {
                        RegistryKey key = keyPrograms.OpenSubKey(guid);
                        if (key?.GetValue("DisplayName") != null)
                        {
                            bool isFind = false;
                            string name = key.GetValue("DisplayName") as string;
                            if (!string.IsNullOrEmpty(name))
                            {
                                switch (template)
                                {
                                    case ShareEnums.StringTemplate.Equals:
                                        if (name.Equals(search, StringComparison.InvariantCultureIgnoreCase))
                                            isFind = true;
                                        break;
                                    case ShareEnums.StringTemplate.Contains:
                                        if (name.ToUpper().Contains(search.ToUpper()))
                                            isFind = true;
                                        break;
                                    case ShareEnums.StringTemplate.StartsWith:
                                        if (name.ToUpper().StartsWith(search.ToUpper()))
                                            isFind = true;
                                        break;
                                    case ShareEnums.StringTemplate.EndsWith:
                                        if (name.ToUpper().EndsWith(search.ToUpper()))
                                            isFind = true;
                                        break;
                                }
                            }
                            if (isFind)
                            {
                                string vendor = key.GetValue("Publisher") as string;
                                string version = key.GetValue("DisplayVersion") as string;
                                string language = key.GetValue("Language") as string;
                                return new ResultWmiSoftware(name, vendor, version, guid, language);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultWmiSoftware(ex.Message, string.Empty, string.Empty, string.Empty, string.Empty);
            }
            return new ResultWmiSoftware();
        }

        /// <summary>
        /// Найти программу.
        /// </summary>
        /// <param name="winProvider"></param>
        /// <param name="search"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public ResultWmiSoftware SearchingSoftware(ShareEnums.WinProvider winProvider, string search, ShareEnums.StringTemplate template)
        {
            switch (winProvider)
            {
                case ShareEnums.WinProvider.Registry:
                    return SearchingSoftwareFromRegistry(search, template);
                case ShareEnums.WinProvider.Alias:
                    break;
                case ShareEnums.WinProvider.Environment:
                    break;
                case ShareEnums.WinProvider.FileSystem:
                    break;
                case ShareEnums.WinProvider.Function:
                    break;
                case ShareEnums.WinProvider.Variable:
                    break;
                case ShareEnums.WinProvider.Wmi:
                    return SearchingSoftwareFromWmi(search);
            }

            return new ResultWmiSoftware();
        }

        #endregion
    }
}
