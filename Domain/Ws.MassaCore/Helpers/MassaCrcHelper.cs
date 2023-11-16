namespace Ws.MassaCore.Helpers;

public class MassaCrcHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static MassaCrcHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static MassaCrcHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	private BytesHelper Bytes { get; } = BytesHelper.Instance;

	#region Public and private methods

	public ushort CrcComputeChecksumAsUshort(byte[] data)
	{
		int bits, k, a, temp;
		int crc = 0;
		for (k = 0; k < data.Length; k++)
		{
			a = 0; temp = crc >> 8 << 8;
			for (bits = 0; bits < 8; bits++)
			{
				if (((temp ^ a) & 0x8000) != 0)
					a = a << 1 ^ 0x1021;
				else a <<= 1;
				temp <<= 1;
			}
			crc = a ^ crc << 8 ^ data[k] & 0xFF;
		}

		byte[] crcReverse = new byte[2];
		crcReverse[0] = (byte)(ushort)crc;
		crcReverse[1] = (byte)((ushort)crc >> 8);

		return BitConverter.ToUInt16(crcReverse, 0);
	}

	private byte[] CrcComputeChecksumAsBytes(byte[] data) => BitConverter.GetBytes(CrcComputeChecksumAsUshort(data));

	public byte[] CrcRecalc(byte[] body)
	{
		byte[] crcBytes = CrcComputeChecksumAsBytes(body);
		body[body.Length - 2] = crcBytes[0];
		body[body.Length - 1] = crcBytes[1];
		return body;
	}

	public byte[] CrcGet(byte[] body) => CrcComputeChecksumAsBytes(body);

	public byte[] CrcGetWithBody(byte[] body) => Bytes.MergeBytes(new() { body, CrcComputeChecksumAsBytes(body) });

	#endregion
}