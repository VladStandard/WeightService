// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;

namespace DataCore.Utils;

public class DataUtils
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

    public static byte[] ByteClone(byte[] bytes)
    {
        byte[] result = new byte[bytes.Length];
        bytes.CopyTo(result, 0);
        return result;
    }

    public static string GetBytesLength(byte[]? bytes)
    {
        if (bytes == null)
            return $"{LocaleCore.Strings.DataSizeVolume}: 0 {LocaleCore.Strings.DataSizeBytes}";
        if (Encoding.Default.GetString(bytes).Length > 1024 * 1024)
            return $"{LocaleCore.Strings.DataSizeVolume}: {(float)Encoding.Default.GetString(bytes).Length / 1024 / 1024:### ###.###} {LocaleCore.Strings.DataSizeMBytes}";
        if (Encoding.Default.GetString(bytes).Length > 1024)
            return $"{LocaleCore.Strings.DataSizeVolume}: {(float)Encoding.Default.GetString(bytes).Length / 1024:### ###.###} {LocaleCore.Strings.DataSizeKBytes}";
        return $"{LocaleCore.Strings.DataSizeVolume}: {Encoding.Default.GetString(bytes).Length:### ###} {LocaleCore.Strings.DataSizeBytes}";
    }

    public static object? GetDefaultValue(Type t)
    {
        if (t.IsValueType)
            return Activator.CreateInstance(t);
        return null;
    }

    public static string GetStringLength(string str)
    {
        if (string.IsNullOrEmpty(str))
            return $"{LocaleCore.Strings.DataSizeLength}: 0 {LocaleCore.Strings.DataSizeChars}";
        return $"{LocaleCore.Strings.DataSizeLength}: {str.Length:### ###} {LocaleCore.Strings.DataSizeChars}";
    }

    public static byte[] GetBytes(Stream stream, bool useBase64)
    {
        MemoryStream memoryStream = new();
        //await stream.CopyToAsync(memoryStream);
        stream.CopyTo(memoryStream);

        if (useBase64)
        {
            string base64String = Convert.ToBase64String(memoryStream.ToArray(), Base64FormattingOptions.None);
            return Encoding.Default.GetBytes(base64String);
        }
        return memoryStream.ToArray();
    }

    public static Image GetImage(byte[] bytes, bool useBase64)
    {
        MemoryStream ms = new(bytes, 0, bytes.Length);
        ms.Write(useBase64 ? Convert.FromBase64String(bytes.ToString()) : bytes, 0, bytes.Length);
        return Image.FromStream(ms, true);
    }
}
