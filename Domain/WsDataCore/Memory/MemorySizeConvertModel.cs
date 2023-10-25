namespace WsDataCore.Memory;

public sealed class MemorySizeConvertModel
{
	#region Public and private fields, properties, constructor

	public ulong Bytes { get; set; }

	public ulong MegaBytes => Bytes / 1048576;

	public MemorySizeConvertModel()
	{
		//
	}

	public MemorySizeConvertModel(ulong bytes) : this()
	{
		Bytes = bytes;
	}

	#endregion
}