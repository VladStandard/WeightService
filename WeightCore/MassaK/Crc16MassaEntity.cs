// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace WeightCore.MassaK
{
    public class Crc16MassaEntity
    {
        #region Design pattern "Lazy Singleton"

        private static Crc16MassaEntity _instance;
        public static Crc16MassaEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ushort ComputeChecksum(byte[] buffer, bool isReverse = true)
        {
            int bits, k, a, temp;
            int crc = 0;
            for (k = 0; k < buffer.Length; k++)
            {
                a = 0; temp = (crc >> 8) << 8;
                for (bits = 0; bits < 8; bits++)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        a = (a << 1) ^ 0x1021; else a <<= 1;
                    temp <<= 1;
                }
                crc = a ^ (crc << 8) ^ (buffer[k] & 0xFF);
            }

            if (!isReverse)
            {
                return (ushort)crc;
            }
            byte[] crcReverse = new byte[2];
            crcReverse[1] = (byte)(ushort)crc;
            crcReverse[0] = (byte)((ushort)crc >> 8);
            return BitConverter.ToUInt16(crcReverse, 0);
        }
    }
}
