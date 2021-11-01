//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
//// // https://github.com/DanyloKD/EasySerial/blob/f09b2b5a3cf4d9c4ef951fa802f8b1a97443f39a/src/EasySerial/Crc16.cs

//namespace WeightCore.MassaK
//{
//    public class Crc16JashliaoVcEntity : Crc16BaseEntity
//    {
//        public Crc16JashliaoVcEntity()
//        {
            
//        }

//        public long GetChecksum(byte[] bytes)
//		{
//			int i, j; // Byte counter, bit counter
//			long crc16; // calculation
//			crc16 = 0000; // PRESET value
//			for (i = 0; i < bytes.Length; i++) // check each Byte in the array
//			{
//				crc16 ^= bytes[i];
//				for (j = 0; j < 8; j++) // test each bit in the Byte
//				{
//					if (crc16 % 2 == 1)//(crc16 & 0x0001 )
//					{
//						crc16 >>= 1;
//						crc16 ^= 0x8408; // POLYNOMIAL x^16 + x^12 + x^5 + 1
//					}
//					else
//					{
//						crc16 >>= 1;
//					}
//				}
//			}
//			return (crc16); // returns calculated crc (16 bits)
//		}

//		public byte[] GetChecksumBytes(byte[] bytes) => GetChecksumBytes(GetChecksum(bytes));
//    }
//}
