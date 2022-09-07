// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Utils;

/// <summary>
/// Utility of enumeration values.
/// </summary>
public static class EnumValuesUtils
{
    /// <summary>
    /// Список значений bool.
    /// </summary>
    /// <returns></returns>
    public static List<bool> GetBool()
    {
        return new() { false, true };
    }

    /// <summary>
    /// Список значений string.
    /// </summary>
    /// <returns></returns>
    public static List<string?> GetStringNullable()
    {
        return new() { null, "", string.Empty };
    }

    /// <summary>
    /// Список значений string.
    /// </summary>
    /// <returns></returns>
    public static List<string> GetString()
    {
        return new() { "", string.Empty };
    }

    /// <summary>
    /// Список значений ushort.
    /// </summary>
    /// <returns></returns>
    public static List<ushort> GetUshort()
    {
        return new() { ushort.MinValue, 1, ushort.MaxValue / 2, ushort.MaxValue };
    }

    /// <summary>
    /// Список значений short.
    /// </summary>
    /// <returns></returns>
    public static List<short> GetShort()
    {
        return new() { short.MinValue, 1, short.MaxValue / 2, short.MaxValue };
    }

    /// <summary>
    /// Список значений uint.
    /// </summary>
    /// <returns></returns>
    public static List<uint> GetUint()
    {
        return new() { uint.MinValue, 1, uint.MaxValue / 2, uint.MaxValue };
    }

    /// <summary>
    /// Список значений int.
    /// </summary>
    /// <returns></returns>
    public static List<int> GetInt()
    {
        return new() { int.MinValue, 1, int.MaxValue / 2, int.MaxValue };
    }

    /// <summary>
    /// Значение строки.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string AsString(this string str)
    {
        return str == null ? "<null>" : str == "" ? "<empty>" : str;
    }
}
