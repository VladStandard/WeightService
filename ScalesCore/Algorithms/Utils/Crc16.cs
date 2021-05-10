// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace ScalesCore.Algorithms.Utils
{
	/// <summary>
	/// CRC-16-CCITT.
	/// </summary>
	public static class Crc16
	{
		private static readonly ushort[] _table = new ushort[256];

		static Crc16()
		{
			for (ushort i = 0; i < 256; ++i)
			{
				ushort value = 0;
				ushort temp = (ushort)(i << 8);
				for (byte j = 0; j < 8; ++j)
				{
					if (((value ^ temp) & 0x8000) != 0)
						value = (ushort)((value << 1) ^ 0x1021);
					else
						value <<= 1;
					temp <<= 1;
				}
				_table[i] = value;
			}
		}

		/// <summary>
		/// Посчитать CRC-код.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static ushort Calculate(byte[] data, int offset = 0, int length = 0)
		{
			if (length == 0)
				length = data.Length;
			ushort Result = 0;
			for (int i = 0; i < length; i++)
			{
				byte b = data[offset + i];
				int index = (b ^ ((Result >> 8) & 0xFF));
				Result = (ushort)((Result << 8) ^ _table[index]);
			}
			return Result;
		}

		/// <summary>
		/// Посчитать CRC-код.
		/// </summary>
		/// <param name="crc"></param>
		/// <param name="data"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		[Obsolete(@"Используй метод Calculate")]
		public static ushort ComputeCRC16CCITT(short crc, byte[] data, int len)
		{
			short i;
			ushort crc_value = 0;
			for (int len0 = 0; len0 < data.Length; len0++)
			{
				for (i = 0x80; i != 0; i >>= 1)
				{
					if ((crc_value & 0x8000) != 0)
					{
						crc_value = (ushort)((crc_value << 1) ^ 0x1021);
					}
					else
					{
						crc_value = (ushort)(crc_value << 1);
					}
					if ((data[len0] & i) != 0)
					{
						crc_value ^= 0x1021;
					}
				}
			}
			return crc_value;
		}
	}
}
