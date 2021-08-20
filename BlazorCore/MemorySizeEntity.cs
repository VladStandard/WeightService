// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore
{
    public class SizeEntity
    {
        public ulong Bytes { get; set; }

        public ulong KiloBytes => Bytes > 0 ? Bytes / 1024 : 0;

        public ulong MegaBytes => Bytes > 0 ? Bytes / 1048576 : 0;
    }

    public class MemorySizeEntity
    {
        public SizeEntity Physical { get; private set; }
        public SizeEntity Virtual { get; private set; }

        public MemorySizeEntity(ulong getPhysicalBytes = 0, ulong getVirtualBytes = 0)
        {
            Physical = new SizeEntity { Bytes = getPhysicalBytes };
            Virtual = new SizeEntity { Bytes = getVirtualBytes };
        }
    }
}
