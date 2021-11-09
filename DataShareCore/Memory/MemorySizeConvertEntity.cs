// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WeightCore.Memory
{
    public class MemorySizeConvertEntity
    {
        public ulong Bytes { get; set; } = 0;

        public ulong KiloBytes => Bytes / 1024;

        public ulong MegaBytes => Bytes / 1048576;
    }
}
