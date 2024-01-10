namespace Ws.Scales.Utils;

public static class ScalesCommandsUtil
{
    private static readonly byte[] Header = [0xF8, 0x55, 0xCE];
    public static ushort Crc16Generate(byte[] data)
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
    
    private static byte[] MergeBytes(List<byte[]> bytesList)
    {
        int len = bytesList.Sum(bytes => bytes.Length);
        List<byte> dataList = new(len);
        
        foreach (byte[] bytes in bytesList)
            dataList.AddRange(bytes);

        return dataList.ToArray();
    }
    
    public static byte[] Generate(byte[] body)
    {
        byte[] len = BitConverter.GetBytes((ushort)body.Length);
        byte[] crc = BitConverter.GetBytes(Crc16Generate(body));
        return MergeBytes([Header, len, body, crc]);
    }
    
    public static byte[] Generate(byte body)
    {
        return Generate([body]);
    }
}