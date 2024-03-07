namespace Ws.Scales.Utils;

internal static class CrcUtil
{
    private static readonly byte[] HeaderBytes = [0xF8, 0x55, 0xCE];
    
    public static ushort CalculateCrc16(byte[] data)
    {
        int k;
        int crc = 0;
        for (k = 0; k < data.Length; k++)
        {
            int a = 0;
            int temp = crc >> 8 << 8;
            int bits;
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

    private static byte[] MergeBytes(List<byte[]> bytesList)
    {
        int len = bytesList.Sum(bytes => bytes.Length);
        List<byte> dataList = new(len);

        foreach (byte[] bytes in bytesList)
            dataList.AddRange(bytes);

        return dataList.ToArray();
    }

    private static byte[] Generate(byte[] body)
    {
        byte[] len = BitConverter.GetBytes((ushort)body.Length);
        byte[] crc = BitConverter.GetBytes(CalculateCrc16(body));
        return MergeBytes([HeaderBytes, len, body, crc]);
    }

    public static byte[] Generate(byte body) => Generate([body]);
}