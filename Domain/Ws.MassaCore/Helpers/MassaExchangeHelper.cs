namespace Ws.MassaCore.Helpers;

public class MassaExchangeHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static MassaExchangeHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static MassaExchangeHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties

	private MassaRequestHelper MassaRequest { get; } = MassaRequestHelper.Instance;
	public byte[] Request { get; set; }
    private int ScaleFactor { get; set; } = 1_000;
    private int WeightTare { get; set; }
	public MassaCmdType CmdType { get; set; }
	public ResponseParseModel ResponseParse { get; set; }

	public MassaExchangeHelper()
	{
        Request = Array.Empty<byte>();
        CmdType = MassaCmdType.Nack;
		ResponseParse = new(CmdType, Array.Empty<byte>());
	}

	public void Init(MassaCmdType cmdType)
	{
        Request = Array.Empty<byte>();
        CmdType = cmdType;
		ResponseParse = new(CmdType, Array.Empty<byte>());
	}
    
	#endregion

	#region Public and private methods

	public byte[] CmdSetTare()
	{
		byte[] request = MassaRequest.CMD_SET_TARE;
		request[6] = (byte)(WeightTare & 0xFF);
		request[7] = (byte)((byte)(WeightTare >> 0x08) & 0xFF);
		request[8] = (byte)((byte)(WeightTare >> 0x16) & 0xFF);
		request[9] = (byte)((byte)(WeightTare >> 0x32) & 0xFF);
		CmdSetaTareScaleFactor(request);
		MassaRequest.MakeRequestCrcRecalc(request);
		return request;
	}

	private void CmdSetaTareScaleFactor(byte[] data)
	{
		data[10] = ScaleFactor switch
		{
			10000 => 0x00,
			1000 => 0x01,
			100 => 0x02,
			10 => 0x03,
			1 => 0x04,
			_ => 0x01,
		};
	}

	#endregion
}