// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Wmi;
using System;
using System.Diagnostics;

namespace WeightCore.Memory
{

    public class MemorySizeEntity
    {
        #region Public and private fields and properties

        public DateTime DtChanged { get; private set; }
        public MemorySizeConvertEntity VirtualCurrent { get; private set; }
        public MemorySizeConvertEntity PhysicalCurrent { get; private set; }
        public MemorySizeConvertEntity VirtualFree { get; private set; }
        public MemorySizeConvertEntity PhysicalFree { get; private set; }
        public MemorySizeConvertEntity VirtualTotal { get; private set; }
        public MemorySizeConvertEntity PhysicalTotal { get; private set; }
        private WmiHelper _wmi = WmiHelper.Instance;

        #endregion

        #region Constructor and destructor

        public MemorySizeEntity()
        {
            PhysicalCurrent = new MemorySizeConvertEntity();
            VirtualCurrent = new MemorySizeConvertEntity();
            VirtualFree = new MemorySizeConvertEntity();
            VirtualTotal = new MemorySizeConvertEntity();
            PhysicalFree = new MemorySizeConvertEntity();
            PhysicalTotal = new MemorySizeConvertEntity();
            
            Update();
        }

        #endregion

        #region Public and private methods

        public void Update()
        {
            DtChanged = DateTime.Now;

            PhysicalCurrent.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
            VirtualCurrent.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;

            Win32OperatingSystemMemoryEntity getWmi = _wmi.GetWin32OperatingSystemMemory();
            VirtualFree = new MemorySizeConvertEntity { Bytes = getWmi.FreeVirtual };
            PhysicalFree = new MemorySizeConvertEntity { Bytes = getWmi.FreePhysical };
            VirtualTotal = new MemorySizeConvertEntity { Bytes = getWmi.TotalVirtual };
            PhysicalTotal = new MemorySizeConvertEntity { Bytes = getWmi.TotalPhysical };
        }

        #endregion
    }
}
