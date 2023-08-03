// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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