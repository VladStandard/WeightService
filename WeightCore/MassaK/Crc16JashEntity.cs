//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
//// https://github.com/jash-git/CS-CRC16-CCITT-TABLE/blob/master/CS_crc16_CCITT_TABLE/CS_crc16_CCITT_TABLE/Crc16.cs

//namespace WeightCore.MassaK
//{
//    public class Crc16JashEntity : Crc16BaseEntity
//    {
//        public Crc16JashEntity()
//        {
//            GenerateTable();
//        }

//        private void GenerateTable()
//        {
//            ushort value;
//            ushort temp;
//            for (ushort i = 0; i < Table.Length; ++i)
//            {
//                value = 0;
//                temp = i;
//                for (byte j = 0; j < 8; ++j)
//                {
//                    if (((value ^ temp) & 0x0001) != 0)
//                    {
//                        value = (ushort)((value >> 1) ^ Polynomial);
//                    }
//                    else
//                    {
//                        value >>= 1;
//                    }
//                    temp >>= 1;
//                }
//                Table[i] = value;
//            }
//        }

//        public ushort GetChecksum(byte[] bytes, ushort polynomial = 0x1021)
//        {
//            if (polynomial != Polynomial)
//            {
//                Polynomial = polynomial;
//                GenerateTable();
//            }

//            ushort crc = 0;
//            for (int i = 0; i < bytes.Length; ++i)
//            {
//                byte index = (byte)(crc ^ bytes[i]);
//                crc = (ushort)((crc >> 8) ^ Table[index]);
//            }
//            return crc;
//        }

//        public byte[] GetChecksumBytes(byte[] bytes) => GetChecksumBytes(GetChecksum(bytes));
//    }
//}
