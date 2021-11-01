//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace WeightCore.MassaK
//{
//    public class Crc16Entity : Crc16BaseEntity
//    {
//        public Crc16Entity()
//        {
//            GenerateTable();
//        }
        
//        private void GenerateTable()
//        {
//            for (ushort i = 0; i < Table.Length; ++i)
//            {
//                ushort value = 0;
//                ushort temp = i;
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
//            foreach (byte t in bytes)
//            {
//                byte index = (byte)(crc ^ t);
//                crc = (ushort)((crc >> 8) ^ Table[index]);
//            }
//            return crc;
//        }

//        public byte[] GetChecksumBytes(byte[] bytes) => GetChecksumBytes(GetChecksum(bytes));
//    }
//}
