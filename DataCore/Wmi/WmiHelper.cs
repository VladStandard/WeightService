// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Management;
using System.Threading;
using WeightCore.Wmi;

namespace DataCore.Wmi
{
    public class WmiHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static WmiHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static WmiHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields, properties, constructor

        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public WmiWin32MemoryEntity GetWin32OperatingSystemMemory()
        {
            lock (_locker)
            {
                // PowerShell: gwmi Win32_OperatingSystem | select FreeVirtualMemory, FreePhysicalMemory, TotalVirtualMemorySize, TotalVisibleMemorySize
                // PowerShell: gwmi -query "select FreeVirtualMemory, FreePhysicalMemory, TotalVirtualMemorySize, TotalVisibleMemorySize from Win32_OperatingSystem"
                ObjectQuery wql = new("select FreeVirtualMemory, FreePhysicalMemory, TotalVirtualMemorySize, TotalVisibleMemorySize from Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new(wql);
                ManagementObjectCollection items = searcher.Get();
                ulong freeVirtual = 0;
                ulong freePhysical = 0;
                ulong totalVirtual = 0;
                ulong totalPhysical = 0;
                if (items.Count > 0)
                    foreach (ManagementObject item in items)
                    {
                        freeVirtual = Convert.ToUInt64(item["FreeVirtualMemory"]) * 1024;
                        freePhysical = Convert.ToUInt64(item["FreePhysicalMemory"]) * 1024;
                        totalVirtual = Convert.ToUInt64(item["TotalVirtualMemorySize"]) * 1024;
                        totalPhysical = Convert.ToUInt64(item["TotalVisibleMemorySize"]) * 1024;
                    }
                return new WmiWin32MemoryEntity(freeVirtual, freePhysical, totalVirtual, totalPhysical);
            }
        }

        public Dictionary<string, string> GetWin32OperatingSystemInfo()
        {
            lock (_locker)
            {
                // PowerShell: gwmi Win32_OperatingSystem | select Caption, OSArchitecture, SerialNumber
                // PowerShell: gwmi -query "select Caption, OSArchitecture, SerialNumber from Win32_OperatingSystem"
                ObjectQuery wql = new("select Caption, OSArchitecture, SerialNumber from Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new(wql);
                ManagementObjectCollection items = searcher.Get();
                Dictionary<string, string> result = new();
                if (items.Count > 0)
                    foreach (ManagementObject item in items)
                    {
                        result.Add("SerialNumber", item["SerialNumber"].ToString());
                        result.Add("SystemVersion", item["Caption"].ToString());
                        result.Add("Architecture", item["OSArchitecture"].ToString());
                    }
                result.Add("CoreVersion", Environment.OSVersion.ToString());
                result.Add("ComputerName", Environment.MachineName);
                return result;
            }
        }

        public WmiSoftwareEntity GetSoftware(string search)
        {
            lock (_locker)
            {
                // PowerShell: gwmi -Class Win32_Product | identifyingnumber, name, vendor, version, language, caption | where {$_.name -like "*Visual C++ Library*" }
                // PowerShell: gwmi -query "select identifyingnumber, name, vendor, version, language, caption from Win32_Product where name like '%Visual C++ Library%'"
                SelectQuery wql = new("select identifyingnumber, name, vendor, version, language, caption from win32_product")
                { Condition = $"Name LIKE '*{search}*'" };
                ManagementObjectSearcher searcher = new(wql);
                ManagementObjectCollection items = searcher.Get();
                string guid = string.Empty;
                string name = string.Empty;
                string vendor = string.Empty;
                string version = string.Empty;
                string language = string.Empty;
                if (items.Count > 0)
                {
                    ManagementBaseObject? item = items.GetEnumerator().Current;
                    if (item != null)
                    {
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
                    }
                }
                return new WmiSoftwareEntity(name, vendor, version, guid, language);
            }
        }

        public string GetStatusDescription(ShareEnums.Lang lang, string status)
        {
            return lang == ShareEnums.Lang.Russian
                ? status switch
                {
                    "OK" => "ОК",
                    "Error" => "Ошибка",
                    "Degraded" => "Оффлайн",
                    "Unknown" => "Неизвестно",
                    "Pred Fail" => "Неудача",
                    "Starting" => "Запуск",
                    "Stopping" => "Остановка",
                    "Service" => "Сервис",
                    "Stressed" => "Стресс",
                    "Doesn't restore" => "Не восстанавливается",
                    "No contact" => "Нет контакта",
                    "Lost Comm" => "Потерянная связь",
                    _ => string.Empty,
                }
                : status switch
                {
                    "OK" => "OK",
                    "Error" => "Error",
                    "Degraded" => "Degraded",
                    "Unknown" => "Unknown",
                    "Pred Fail" => "Pred Fail",
                    "Starting" => "Starting",
                    "Stopping" => "Stopping",
                    "Service" => "Service",
                    "Stressed" => "Stressed",
                    "Doesn't restore" => "Doesn't restore",
                    "No contact" => "No contact",
                    "Lost Comm" => "Lost Comm",
                    _ => string.Empty,
                };
        }

        #endregion
    }
}
