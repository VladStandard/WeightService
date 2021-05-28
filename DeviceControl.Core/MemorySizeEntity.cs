namespace DeviceControl.Core
{
    public class SizeEntity
    {
        public ulong Bytes { get; set; }

        public ulong KiloBytes => Bytes / 1024;

        public ulong MegaBytes => Bytes / 1048576;
    }

    public class MemorySizeEntity
    {
        public SizeEntity Physical;
        public SizeEntity Virtual;

        public MemorySizeEntity(ulong getPhysicalBytes = 0, ulong getVirtualBytes = 0)
        {
            Physical = new SizeEntity { Bytes = getPhysicalBytes };
            Virtual = new SizeEntity { Bytes = getVirtualBytes };
        }
    }
}
