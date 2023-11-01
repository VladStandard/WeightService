namespace WsDataCore.Utils;

public static class WsDataUtils
{
    #region Public and private methods

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

    public static string GetBytesLength(byte[]? value, bool isShowLabel)
    {
        if (value == null)
            return isShowLabel
                    ? $"{WsLocaleCore.Strings.DataSizeVolume}: 0 {WsLocaleCore.Strings.DataSizeBytes}"
                    : $"0 {WsLocaleCore.Strings.DataSizeBytes}";
        if (Encoding.Default.GetString(value).Length > 1024 * 1024)
            return isShowLabel
                ? $"{WsLocaleCore.Strings.DataSizeVolume}: {(float)Encoding.Default.GetString(value).Length / 1024 / 1024:### ###.###} {WsLocaleCore.Strings.DataSizeMBytes}"
                : $"{(float)Encoding.Default.GetString(value).Length / 1024 / 1024:### ###.###} {WsLocaleCore.Strings.DataSizeMBytes}";
        if (Encoding.Default.GetString(value).Length > 1024)
            return isShowLabel
                ? $"{WsLocaleCore.Strings.DataSizeVolume}: {(float)Encoding.Default.GetString(value).Length / 1024:### ###.###} {WsLocaleCore.Strings.DataSizeKBytes}"
                : $"{(float)Encoding.Default.GetString(value).Length / 1024:### ###.###} {WsLocaleCore.Strings.DataSizeKBytes}";
        return isShowLabel
            ? $"{WsLocaleCore.Strings.DataSizeVolume}: {Encoding.Default.GetString(value).Length:### ###} {WsLocaleCore.Strings.DataSizeBytes}"
            : $"{Encoding.Default.GetString(value).Length:### ###} {WsLocaleCore.Strings.DataSizeBytes}";
    }

    public static ArgumentException GetArgumentException(string argument) => new($"Argument {argument} must be setup!");
    
    
    #endregion
}