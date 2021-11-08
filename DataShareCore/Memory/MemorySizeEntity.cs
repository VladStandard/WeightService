// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Management;

namespace WeightCore.Memory
{
    public class SizeEntity
    {
        public ulong Bytes { get; set; } = 0;

        public ulong KiloBytes => Bytes / 1024;

        public ulong MegaBytes => Bytes / 1048576;
    }

    public class MemorySizeEntity
    {
        public DateTime DtChanged { get; set; }
        public SizeEntity VirtualFree;
        public SizeEntity VirtualTotal;
        public SizeEntity VirtualCurrent;
        public SizeEntity PhysicalFree;
        public SizeEntity PhysicalTotal;
        public SizeEntity PhysicalCurrent;

        public MemorySizeEntity()
        {
            VirtualFree = new SizeEntity();
            PhysicalFree = new SizeEntity();
            VirtualTotal = new SizeEntity();
            PhysicalTotal = new SizeEntity();
            PhysicalCurrent = new SizeEntity();
            VirtualCurrent = new SizeEntity();
            
            Update();
        }

        public void Update()
        {
            DtChanged = DateTime.Now;
            
            PhysicalCurrent.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
            VirtualCurrent.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;

            // PowerShell: Get-WmiObject -class Win32_OperatingSystem | select TotalVisibleMemorySize
            //ObjectQuery wql = new("SELECT * FROM Win32_OperatingSystem");
            ObjectQuery wql = new("SELECT FreeVirtualMemory, FreePhysicalMemory, TotalVirtualMemorySize, TotalVisibleMemorySize FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new(wql);
            ManagementObjectCollection results = searcher.Get();
            ulong freeVirtual = 0;
            ulong freePhysical = 0;
            ulong totalVirtual = 0;
            ulong totalPhysical = 0;
            foreach (ManagementObject result in results)
            {
                freeVirtual = System.Convert.ToUInt64(result["FreeVirtualMemory"]) * 1024;
                freePhysical = System.Convert.ToUInt64(result["FreePhysicalMemory"]) * 1024;
                totalVirtual = System.Convert.ToUInt64(result["TotalVirtualMemorySize"]) * 1024;
                totalPhysical = System.Convert.ToUInt64(result["TotalVisibleMemorySize"]) * 1024;
            }

            VirtualFree = new SizeEntity { Bytes = freeVirtual };
            PhysicalFree = new SizeEntity { Bytes = freePhysical };
            VirtualTotal = new SizeEntity { Bytes = totalVirtual };
            PhysicalTotal = new SizeEntity { Bytes = totalPhysical };
        }
    }
}
