// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com



// ReSharper disable UnusedMember.Global
// Last changed 2021-01-28.

namespace WsWeightCoreTests;

/// <summary>
/// Enumeration of values.
/// </summary>
internal static class EnumValues
{
	/// <summary>
	/// List of bool values.
	/// </summary>
	/// <returns></returns>
	public static List<bool> GetBool()
	{
		return new List<bool>() { false, true };
	}

	/// <summary>
	/// List of string values.
	/// </summary>
	/// <returns></returns>
	public static List<string> GetString()
	{
		return new List<string>() { null, "", string.Empty };
	}

	/// <summary>
	/// List of ushort values.
	/// </summary>
	/// <returns></returns>
	public static List<ushort> GetUshort()
	{
		return new List<ushort>() { ushort.MinValue, 1, ushort.MaxValue / 2, ushort.MaxValue };
	}

	/// <summary>
	/// List of progress values.
	/// </summary>
	/// <returns></returns>
	public static List<ushort> GetProgress()
	{
		return new List<ushort>() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };
	}

	/// <summary>
	/// List of short values.
	/// </summary>
	/// <returns></returns>
	public static List<short> GetShort()
	{
		return new List<short>() { short.MinValue, 1, short.MaxValue / 2, short.MaxValue };
	}

	/// <summary>
	/// List of uint values.
	/// </summary>
	/// <returns></returns>
	public static List<uint> GetUint()
	{
		return new List<uint>() { uint.MinValue, 1, uint.MaxValue / 2, uint.MaxValue };
	}

	/// <summary>
	/// List of int values.
	/// </summary>
	/// <returns></returns>
	public static List<int> GetInt()
	{
		return new List<int>() { int.MinValue, 1, int.MaxValue / 2, int.MaxValue };
	}

	/// <summary>
	/// List of decimal values.
	/// </summary>
	/// <returns></returns>
	public static List<decimal> GetDecimal()
	{
		return new List<decimal>() { decimal.MinValue, 1, decimal.MaxValue / 2, int.MaxValue };
	}

	/// <summary>
	/// List of long values.
	/// </summary>
	/// <returns></returns>
	public static List<long> GetLong()
	{
		return new List<long>() { long.MinValue, 1, long.MaxValue / 2, long.MaxValue };
	}

	/// <summary>
	/// List of DateTime values.
	/// </summary>
	/// <returns></returns>
	public static List<DateTime> GetDateTime()
	{
		return new List<DateTime>() { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.Today, DateTime.UtcNow };
	}

	/// <summary>
	/// String value.
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static string AsString(this string str)
	{
		return str == null ? "<null>" : str == "" ? "<empty>" : str;
	}

	/// <summary>
	/// List of uri values.
	/// </summary>
	/// <returns></returns>
	public static List<Uri> GetUri()
	{
		return new List<Uri>() { new Uri("http://google.com/"), new Uri("http://microsoft.com/") };
	}

	/// <summary>
	/// List of timeout values in ms.
	/// </summary>
	/// <returns></returns>
	public static List<int> GetTimeoutMs()
	{
		return new List<int>() { 50, 500 };
	}

	/// <summary>
	/// List of bytes.
	/// </summary>
	/// <returns></returns>
	public static List<byte[]> GetBytes()
	{
		return new List<byte[]>() { null, new byte[0], new byte[] { byte.MinValue, 0x00, byte.MaxValue } };
	}

	/// <summary>
	/// List of byte.
	/// </summary>
	/// <returns></returns>
	public static List<byte> GetByte()
	{
		return new List<byte>() { byte.MinValue, 0x00, byte.MaxValue };
	}

	/// <summary>
	/// List of Guid.
	/// </summary>
	/// <returns></returns>
	public static List<Guid> GetGuid()
	{
		return new List<Guid>() { Guid.Empty, Guid.NewGuid(), Guid.Parse("108B0D40-1A9A-4A41-B396-E30931FEDA86") };
	}

	/// <summary>
	/// List of SQL servers.
	/// </summary>
	/// <returns></returns>
	public static List<string> GetSqlServer()
	{
		return new List<string>() { "TEST-SERVER", "TEST-SERVER\\TEST-INSTANCE" };
	}

	/// <summary>
	/// List of SQL databases.
	/// </summary>
	/// <returns></returns>
	public static List<string> GetSqlDb()
	{
		return new List<string>() { "TEST-DB" };
	}

	/// <summary>
	/// List of SQL usernames.
	/// </summary>
	/// <returns></returns>
	public static List<string> GetSqlUsername()
	{
		return new List<string>() { "User" };
	}

	/// <summary>
	/// List of SQL passwords.
	/// </summary>
	/// <returns></returns>
	public static List<string> GetSqlPassword()
	{
		return new List<string>() { "Password" };
	}
}