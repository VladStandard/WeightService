// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataShareCore.Wmi
{
    public class Win32OperatingSystemMemoryEntity
    {
        #region Public and private fields and properties

        public ulong FreeVirtual { get; set; }
        public ulong FreePhysical { get; set; }
        public ulong TotalVirtual { get; set; }
        public ulong TotalPhysical { get; set; }

        #endregion

        #region Constructor and destructor

        public Win32OperatingSystemMemoryEntity()
        {
            FreeVirtual = 0;
            FreePhysical = 0;
            TotalVirtual = 0;
            TotalPhysical = 0;
        }

        public Win32OperatingSystemMemoryEntity(ulong freeVirtual, ulong freePhysical, ulong totalVirtual, ulong totalPhysical)
        {
            FreeVirtual = freeVirtual;
            FreePhysical = freePhysical;
            TotalVirtual = totalVirtual;
            TotalPhysical = totalPhysical;
        }

        #endregion
    }
}
