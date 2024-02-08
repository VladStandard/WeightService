namespace Ws.Shared.Utils;

public static class DataUtil
{
    public static bool ByteEquals(byte[] a1, byte[] a2)
    {
        if (a1.Length != a2.Length)
            return false;

        for (int i = 0; i < a1.Length; i++)
            if (a1[i] != a2[i])
                return false;

        return true;
    }

    public static byte[] ByteClone(byte[]? value)
    {
        if (value is not null)
        {
            byte[] result = new byte[value.Length];
            value.CopyTo(result, 0);
            return result;
        }
        return Array.Empty<byte>();
    }
}