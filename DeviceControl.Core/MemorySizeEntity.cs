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
        public SizeEntity Limit;
        public SizeEntity Physical;
        public SizeEntity Virtual;

        public MemorySizeEntity(ulong getLimitBytes, ulong getPhysicalBytes = 0, ulong getVirtualBytes = 0)
        {
            Limit = new SizeEntity { Bytes = getLimitBytes };
            Physical = new SizeEntity { Bytes = getPhysicalBytes };
            Virtual = new SizeEntity { Bytes = getVirtualBytes };
        }
    }
}
