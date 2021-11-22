// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Management;
using System.Threading;

namespace DataShareCore.Wmi
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

        public object Locker { get; private set; } = new();

        #endregion

        #region Public and private methods

        public Win32OperatingSystemMemoryEntity GetWin32OperatingSystemMemory()
        {
            lock (Locker)
            {
                // PowerShell: gwmi Win32_OperatingSystem | select FreeVirtualMemory, FreePhysicalMemory, TotalVirtualMemorySize, TotalVisibleMemorySize
                // PowerShell: gwmi -query "select FreeVirtualMemory, FreePhysicalMemory, TotalVirtualMemorySize, TotalVisibleMemorySize from Win32_OperatingSystem"
                ObjectQuery wql = new("select FreeVirtualMemory, FreePhysicalMemory, TotalVirtualMemorySize, TotalVisibleMemorySize from Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new(wql);
                ManagementObjectCollection results = searcher.Get();
                ulong freeVirtual = 0;
                ulong freePhysical = 0;
                ulong totalVirtual = 0;
                ulong totalPhysical = 0;
                if (results.Count > 0)
                    foreach (ManagementObject result in results)
                    {
                        freeVirtual = Convert.ToUInt64(result["FreeVirtualMemory"]) * 1024;
                        freePhysical = Convert.ToUInt64(result["FreePhysicalMemory"]) * 1024;
                        totalVirtual = Convert.ToUInt64(result["TotalVirtualMemorySize"]) * 1024;
                        totalPhysical = Convert.ToUInt64(result["TotalVisibleMemorySize"]) * 1024;
                    }
                return new Win32OperatingSystemMemoryEntity(freeVirtual, freePhysical, totalVirtual, totalPhysical);
            }
        }

        public Win32PrinterEntity GetWin32Printer(string name)
        {
            lock (Locker)
            {
                if (string.IsNullOrEmpty(name))
                    return new Win32PrinterEntity(name, string.Empty, string.Empty, string.Empty, string.Empty, Win32PrinterStatusEnum.Error);
                // PowerShell: gwmi Win32_Printer | select DriverName, PortName, Status, PrinterState, PrinterStatus
                // PowerShell: gwmi -query "select DriverName, PortName, Status, PrinterState, PrinterStatus from Win32_Printer where Name='SCALES-PRN-DEV'"
                ObjectQuery wql = new($"select DriverName, PortName, Status, PrinterState, PrinterStatus from Win32_Printer where Name = '{name}'");
                ManagementObjectSearcher searcher = new(wql);
                ManagementObjectCollection results = searcher.Get();
                string driverName = string.Empty;
                string portName = string.Empty;
                string status = string.Empty;
                string printerState = string.Empty;
                short printerStatus = -1;
                if (results.Count > 0)
                    foreach (ManagementObject result in results)
                    {
                        driverName = Convert.ToString(result["DriverName"]);
                        portName = Convert.ToString(result["PortName"]);
                        status = Convert.ToString(result["Status"]);
                        printerState = Convert.ToString(result["PrinterState"]);
                        printerStatus = Convert.ToInt16(result["PrinterStatus"]);
                    }
                return new Win32PrinterEntity(name, driverName, portName, status, printerState, (Win32PrinterStatusEnum)printerStatus);
            }
        }

        #endregion
    }
}
