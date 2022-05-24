// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using System;
using System.Collections.Generic;
using System.Management;
using System.Threading;

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

        #region Public and private fields and properties

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

        public WmiWin32PrinterEntity GetWin32Printer(string name)
        {
            lock (_locker)
            {
                if (string.IsNullOrEmpty(name))
                    return new WmiWin32PrinterEntity(name, string.Empty, string.Empty, string.Empty, string.Empty, Win32PrinterStatusEnum.Error);
                // PowerShell: gwmi Win32_Printer | select DriverName, PortName, Status, PrinterState, PrinterStatus
                // PowerShell: gwmi -query "select DriverName, PortName, Status, PrinterState, PrinterStatus from Win32_Printer where Name='SCALES-PRN-DEV'"
                ObjectQuery wql = new($"select DriverName, PortName, Status, PrinterState, PrinterStatus from Win32_Printer where Name = '{name}'");
                ManagementObjectSearcher searcher = new(wql);
                ManagementObjectCollection items = searcher.Get();
                string driverName = string.Empty;
                string portName = string.Empty;
                string status = string.Empty;
                string printerState = string.Empty;
                short printerStatus = -1;
                if (items.Count > 0)
                {
                    foreach (ManagementObject item in items)
                    {
                        driverName = Convert.ToString(item["DriverName"]);
                        portName = Convert.ToString(item["PortName"]);
                        status = Convert.ToString(item["Status"]);
                        printerState = Convert.ToString(item["PrinterState"]);
                        printerStatus = Convert.ToInt16(item["PrinterStatus"]);
                    }
                }
                return new WmiWin32PrinterEntity(name, driverName, portName, status, printerState, (Win32PrinterStatusEnum)printerStatus);
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

        public string GetPrinterStatusDescription(ShareEnums.Lang lang, Win32PrinterStatusEnum printerStatus)
        {
            return lang switch
            {
                ShareEnums.Lang.Russian => printerStatus switch
                {
                    Win32PrinterStatusEnum.Idle => "Бездействие",
                    Win32PrinterStatusEnum.Paused => "Пауза",
                    Win32PrinterStatusEnum.Error => "Ошибка",
                    //Win32PrinterStatusEnum.PendingDeletion => "Ожидание печати", // Ожидание удаления
                    Win32PrinterStatusEnum.PendingDeletion => LocaleCore.Print.StatusIsReadyToPrint,
                    Win32PrinterStatusEnum.PaperJam => "Застревание бумаги",
                    Win32PrinterStatusEnum.PaperOut => "Выдача бумаги",
                    Win32PrinterStatusEnum.ManualFeed => "Ручная подача",
                    Win32PrinterStatusEnum.PaperProblem => "Проблема с бумагой",
                    Win32PrinterStatusEnum.Offline => "Не в сети",
                    Win32PrinterStatusEnum.IoActive => "Ввод-вывод активен",
                    Win32PrinterStatusEnum.Busy => "Занято",
                    Win32PrinterStatusEnum.Printing => "Печать",
                    Win32PrinterStatusEnum.OutputBinFull => "Выходной лоток полон",
                    Win32PrinterStatusEnum.NotAvailable => "Недоступно",
                    Win32PrinterStatusEnum.Waiting => "Ожидание",
                    Win32PrinterStatusEnum.Processing => "Обработка",
                    Win32PrinterStatusEnum.Initialization => "Инициализация",
                    Win32PrinterStatusEnum.WarmingUp => "Прогрев",
                    Win32PrinterStatusEnum.TonerLow => "Мало тонера",
                    Win32PrinterStatusEnum.NoToner => "Нет тонера",
                    Win32PrinterStatusEnum.PagePunt => "Страница беспечатана",
                    Win32PrinterStatusEnum.UserInterventionRequired => "Требуется вмешательство пользователя",
                    Win32PrinterStatusEnum.OutOfMemory => "Недостаточно памяти",
                    Win32PrinterStatusEnum.DoorOpen => "Открыта дверца",
                    Win32PrinterStatusEnum.ServerUnknown => "Сервер неизвестен",
                    Win32PrinterStatusEnum.PowerSave => "Энергосбережение",
                    _ => "Ошибка чтения статуса!",
                },
                _ => printerStatus switch
                {
                    Win32PrinterStatusEnum.Idle => "Idle",
                    Win32PrinterStatusEnum.Paused => "Paused",
                    Win32PrinterStatusEnum.Error => "Error",
                    Win32PrinterStatusEnum.PendingDeletion => "Waiting for printing", // "Pending deletion"
                    Win32PrinterStatusEnum.PaperJam => "Paper jam",
                    Win32PrinterStatusEnum.PaperOut => "Paper out",
                    Win32PrinterStatusEnum.ManualFeed => "Manual feed",
                    Win32PrinterStatusEnum.PaperProblem => "Paper problem",
                    Win32PrinterStatusEnum.Offline => "Offline",
                    Win32PrinterStatusEnum.IoActive => "Io active",
                    Win32PrinterStatusEnum.Busy => "Busy",
                    Win32PrinterStatusEnum.Printing => "Printing",
                    Win32PrinterStatusEnum.OutputBinFull => "Output bin full",
                    Win32PrinterStatusEnum.NotAvailable => "Not available",
                    Win32PrinterStatusEnum.Waiting => "Waiting",
                    Win32PrinterStatusEnum.Processing => "Processing",
                    Win32PrinterStatusEnum.Initialization => "Initialization",
                    Win32PrinterStatusEnum.WarmingUp => "Warming up",
                    Win32PrinterStatusEnum.TonerLow => "Toner low",
                    Win32PrinterStatusEnum.NoToner => "No toner",
                    Win32PrinterStatusEnum.PagePunt => "Page punt",
                    Win32PrinterStatusEnum.UserInterventionRequired => "User intervention required",
                    Win32PrinterStatusEnum.OutOfMemory => "Out of memory",
                    Win32PrinterStatusEnum.DoorOpen => "Door open",
                    Win32PrinterStatusEnum.ServerUnknown => "Server unknown",
                    Win32PrinterStatusEnum.PowerSave => "Power save",
                    _ => "Status reading error!",
                },
            };
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
