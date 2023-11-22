namespace Ws.MassaCore.Helpers;

public class BytesHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static BytesHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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

	#endregion
}