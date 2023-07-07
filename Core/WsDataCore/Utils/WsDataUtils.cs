// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;

namespace WsDataCore.Utils;

/// <summary>
/// Утилиты данных.
/// </summary>
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

    public static string GetBytesLength(string value, bool isShowLabel)
    {
        List<byte> listBytes = new();
        foreach (char ch in value.ToArray())
        {
            listBytes.Add((byte)ch);
        }
        byte[] bytes = listBytes.ToArray();
        return GetBytesLength(bytes, isShowLabel);
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
            return $"{WsLocaleCore.Strings.DataSizeLength}: 0 {WsLocaleCore.Strings.DataSizeChars}";
        return $"{WsLocaleCore.Strings.DataSizeLength}: {str.Length:### ###} {WsLocaleCore.Strings.DataSizeChars}";
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

    public static WsEnumFormatType GetFormatType(string formatString) => formatString.ToUpper() switch
    {
        "TEXT" => WsEnumFormatType.Text,
        "JAVASCRIPT" => WsEnumFormatType.JavaScript,
        "JSON" => WsEnumFormatType.Json,
        "HTML" => WsEnumFormatType.Html,
        "XML" or "" or "XMLUTF8" => WsEnumFormatType.Xml,
        "XMLUTF16" => WsEnumFormatType.XmlUtf16,
        _ => throw GetArgumentException(nameof(formatString))
    };

    public static string GetContentType(WsEnumFormatType formatType) => formatType switch
    {
        WsEnumFormatType.Text => "application/text",
        WsEnumFormatType.JavaScript => "application/js",
        WsEnumFormatType.Json => "application/json",
        WsEnumFormatType.Html => "application/html",
        WsEnumFormatType.Xml or WsEnumFormatType.XmlUtf8 or WsEnumFormatType.XmlUtf16 => "application/xml",
        _ => throw GetArgumentException(nameof(formatType))
    };

    public static string GetContentType(string formatString) =>
        GetContentType(GetFormatType(formatString));

    public static ArgumentException GetArgumentException(string argument) => new($"Argument {argument} must be setup!");

    #endregion
}