// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WeightCore.MassaK
{
    public static class Crc16
    {
        #region Private fields and properties

        private const ushort Polynomial = 0xA001;
        private static readonly ushort[] Table = new ushort[256];

        #endregion

        #region Constructor

        static Crc16()
        {
            for (ushort i = 0; i < Table.Length; ++i)
            {
                ushort value = 0;
                ushort temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ Polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                Table[i] = value;
            }
        }

        #endregion

        #region Public methods

        public static ushort ComputeChecksum(byte[] bytes)
        {
            ushort crc = 0;
            foreach (byte t in bytes)
            {
                byte index = (byte)(crc ^ t);
                crc = (ushort)((crc >> 8) ^ Table[index]);
            }
            return crc;
        }

        #endregion
    }
}
