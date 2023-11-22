namespace Ws.MassaCore.Helpers;

public class MassaRequestHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static MassaRequestHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static MassaRequestHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private BytesHelper Bytes { get; } = BytesHelper.Instance;
    private MassaCrcHelper MassaCrc { get; } = MassaCrcHelper.Instance;

    public readonly byte[] Header = { 0xF8, 0x55, 0xCE };

    #endregion

    #region Public and private methods

    public byte[] MakeRequestCrcAdd(byte[] body)
    {
        byte[] len = BitConverter.GetBytes((ushort)body.Length);
        byte[] crc = MassaCrc.CrcGet(body);
        return Bytes.MergeBytes(new() { Header, len, body, crc });
    }

    public byte[] MakeRequestCrcAdd(byte body) => MakeRequestCrcAdd(new byte[] { body });

    public byte[] MakeRequestCrcRecalc(byte[] request)
    {
        if (request.Length < 8)
            throw new ArgumentException($"Length of {nameof(request)} must be more than 8 digits!");
        if (request[0] != Header[0] || request[1] != Header[1] || request[2] != Header[2])
            throw new ArgumentException($"{nameof(Header)} must be '{Bytes.GetBytesAsHex(Header)}'!");
        byte[] len = new byte[2];
        len[0] = request[3];
        len[1] = request[4];
        ushort lenAsUshort = BitConverter.ToUInt16(len, 0);
        byte[] body = request.Skip(5).Take(lenAsUshort).ToArray();
        return Bytes.MergeBytes(new() { Header, len, MassaCrc.CrcGetWithBody(body) });
    }

    #endregion

    #region Public and private methods - API
    public byte[] CMD_GET_MASSA => MakeRequestCrcAdd(0x23);
    public byte[] CMD_GET_SCALE_PAR => MakeRequestCrcAdd(0x75);
    public byte[] CMD_SET_ZERO => MakeRequestCrcAdd(0x72);
    public byte[] CMD_SET_TARE => MakeRequestCrcAdd(new byte[] { 0xA3, 0x00, 0x00, 0x00, 0x00 });
    
    #endregion
}