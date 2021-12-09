// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Models;
using DataShareCore.Wmi;
using System;
using System.Diagnostics;

namespace DataShareCore.Memory
{

    public class MemorySizeEntity : DisposableBase, IDisposableBase
    {
        #region Public and private fields and properties

        public DateTime? DtChanged { get; private set; }
        public MemorySizeConvertEntity? VirtualCurrent { get; private set; }
        public MemorySizeConvertEntity? PhysicalCurrent { get; private set; }
        public MemorySizeConvertEntity? VirtualFree { get; private set; }
        public MemorySizeConvertEntity? PhysicalFree { get; private set; }
        public MemorySizeConvertEntity? VirtualTotal { get; private set; }
        public MemorySizeConvertEntity? PhysicalTotal { get; private set; }
        private WmiHelper? Wmi { get; set; } = WmiHelper.Instance;

        #endregion

        #region Constructor and destructor

        public MemorySizeEntity() : base()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);

            PhysicalCurrent = new MemorySizeConvertEntity();
            VirtualCurrent = new MemorySizeConvertEntity();
            VirtualFree = new MemorySizeConvertEntity();
            VirtualTotal = new MemorySizeConvertEntity();
            PhysicalFree = new MemorySizeConvertEntity();
            PhysicalTotal = new MemorySizeConvertEntity();
        }

        #endregion

        #region Public and private methods

        public new void Open()
        {
            base.Open();
            CheckIsDisposed();

            DtChanged = DateTime.Now;

            if (PhysicalCurrent != null)
                PhysicalCurrent.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
            if (VirtualCurrent != null)
                VirtualCurrent.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;

            if (Wmi != null)
            {
                WmiWin32MemoryEntity getWmi = Wmi.GetWin32OperatingSystemMemory();
                VirtualFree = new MemorySizeConvertEntity { Bytes = getWmi.FreeVirtual };
                PhysicalFree = new MemorySizeConvertEntity { Bytes = getWmi.FreePhysical };
                VirtualTotal = new MemorySizeConvertEntity { Bytes = getWmi.TotalVirtual };
                PhysicalTotal = new MemorySizeConvertEntity { Bytes = getWmi.TotalPhysical };
            }
        }

        public void CloseMethod()
        {
            //
        }

        public void ReleaseManaged()
        {
            DtChanged = null;
            VirtualCurrent = null;
            PhysicalCurrent = null;
            VirtualFree = null;
            PhysicalFree = null;
            VirtualTotal = null;
            PhysicalTotal = null;
            Wmi = null;
        }

        public void ReleaseUnmanaged()
        {
            //
        }

        #endregion
    }
}
