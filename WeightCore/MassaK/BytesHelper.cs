// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WeightCore.MassaK
{
    public class BytesHelper
    {
        #region Design pattern "Lazy Singleton"

        private static BytesHelper _instance;
        public static BytesHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private methods

        public string GetBytesAsHex(byte[] bytes, char delimeter = ' ') =>
                    string.Join(delimeter != ' ' ? $"{delimeter} " : " ", bytes.Select(b => b.ToString("X2")));

        public byte[] MergeBytes(List<byte[]> bytesList)
        {
            int len = 0;
            foreach (byte[] bytes in bytesList)
            {
                len += bytes.Length;
            }
            byte[] data = new byte[len];

            int i = 0;
            foreach (byte[] bytes in bytesList)
            {
                foreach (byte item in bytes)
                {
                    data[i] = item;
                    i++;
                }
            }
            return data;
        }

        public ushort CrcComputeChecksumAsUshort(byte[] data)
        {
            int bits, k, a, temp;
            int crc = 0;
            for (k = 0; k < data.Length; k++)
            {
                a = 0; temp = (crc >> 8) << 8;
                for (bits = 0; bits < 8; bits++)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        a = (a << 1) ^ 0x1021;
                    else a <<= 1;
                    temp <<= 1;
                }
                crc = a ^ (crc << 8) ^ (data[k] & 0xFF);
            }

            byte[] crcReverse = new byte[2];
            crcReverse[0] = (byte)(ushort)crc;
            crcReverse[1] = (byte)((ushort)crc >> 8);

            return BitConverter.ToUInt16(crcReverse, 0);
        }

        public byte[] CrcComputeChecksumAsBytes(byte[] data) => BitConverter.GetBytes(CrcComputeChecksumAsUshort(data));

        public byte[] CrcRecalc(byte[] body)
        {
            byte[] crcBytes = CrcComputeChecksumAsBytes(body);
            body[body.Length - 2] = crcBytes[0];
            body[body.Length - 1] = crcBytes[1];
            return body;
        }

        public byte[] CrcGet(byte[] body) => CrcComputeChecksumAsBytes(body);

        public byte[] CrcGetWithBody(byte[] body) => MergeBytes(new List<byte[]>() { body, CrcComputeChecksumAsBytes(body) });

        #endregion
    }
}
