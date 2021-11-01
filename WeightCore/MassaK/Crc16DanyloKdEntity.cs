//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
//// // https://github.com/DanyloKD/EasySerial/blob/f09b2b5a3cf4d9c4ef951fa802f8b1a97443f39a/src/EasySerial/Crc16.cs

//using System;

//namespace WeightCore.MassaK
//{
//    public class Crc16DanyloKdEntity : Crc16BaseEntity
//    {
//        public Crc16DanyloKdEntity()
//        {
//            GenerateTable();
//        }

//        private void GenerateTable()
//        {
//            const ushort MsbMask = (ushort)1 << (sizeof(ushort) * 8 - 1);

//            Table = new ushort[0xFF + 1];
//            for (var inputByte = 0x01; inputByte != 0xFF; inputByte++)
//            {
//                ushort crc = (ushort)(inputByte << ((sizeof(ushort) - 1) * 8));
//                for (int j = 8; j > 0; j--)
//                {
//                    if ((crc & MsbMask) != 0)
//                    {
//                        crc <<= 1;
//                        crc ^= Polynomial;
//                    }
//                    else
//                    {
//                        crc <<= 1;
//                    }
//                }

//                Table[inputByte] = crc;
//            }
//        }

//        public ushort GetChecksum(ReadOnlySpan<byte> bytes, ushort polynomial = 0x1021, ushort init = 0xFFFF)
//        {
//            if (polynomial != Polynomial)
//            {
//                Polynomial = polynomial;
//                GenerateTable();
//            }
            
//            ushort crc = init;
//            foreach (var b in bytes)
//            {
//                byte index = (byte)(b ^ (crc >> 8));
//                crc = (ushort)((crc << 8) ^ (ushort)(Table[index]));
//            }

//            return crc;
//        }

//        public bool Verify(ReadOnlySpan<byte> bytes) => GetChecksum(bytes) == 0x00;

//        public byte[] GetChecksumBytes(byte[] bytes) => GetChecksumBytes(GetChecksum(bytes));
//    }
//}
