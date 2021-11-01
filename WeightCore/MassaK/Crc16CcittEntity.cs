//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
//// http://www.sanity-free.com/133/crc_16_ccitt_in_csharp.html

//namespace WeightCore.MassaK
//{
//    public enum InitialCrcValue { Zeros, NonZero1 = 0xffff, NonZero2 = 0x1D0F }

//    public class Crc16CcittEntity : Crc16BaseEntity
//    {
//        private ushort _initialValue = 0;

//        public Crc16CcittEntity()
//        {
//            GenerateTable(InitialCrcValue.Zeros);
//        }
        
//        private void GenerateTable(InitialCrcValue init)
//        {
//            _initialValue = (ushort)init;
//            ushort temp, a;
//            for (int i = 0; i < Table.Length; ++i)
//            {
//                temp = 0;
//                a = (ushort)(i << 8);
//                for (int j = 0; j < 8; ++j)
//                {
//                    if (((temp ^ a) & 0x8000) != 0)
//                    {
//                        temp = (ushort)((temp << 1) ^ Polynomial);
//                    }
//                    else
//                    {
//                        temp <<= 1;
//                    }
//                    a <<= 1;
//                }
//                Table[i] = temp;
//            }
//        }

//        public ushort GetChecksum(byte[] bytes, ushort polynomial = 0x1021, InitialCrcValue init = InitialCrcValue.Zeros)
//        {
//            if (polynomial != Polynomial)
//            {
//                Polynomial = polynomial;
//                GenerateTable(init);
//            }

//            ushort crc = _initialValue;
//            for (int i = 0; i < bytes.Length; ++i)
//            {
//                crc = (ushort)((crc << 8) ^ Table[((crc >> 8) ^ (0xff & bytes[i]))]);
//            }
//            return crc;
//        }

//        public byte[] GetChecksumBytes(byte[] bytes, ushort polynomial = 0x1021, InitialCrcValue init = InitialCrcValue.Zeros) => 
//            GetChecksumBytes(GetChecksum(bytes, polynomial, init));
//    }
//}
