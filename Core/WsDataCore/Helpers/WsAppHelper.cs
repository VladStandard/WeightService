// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Security.Principal;
using WsDataCore.Enums;

namespace WsDataCore.Helpers;

/// <summary>
/// Application helper.
/// </summary>
public sealed class WsAppHelper //: BaseViewModel
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static WsAppHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static WsAppHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private methods

	public bool IsAdministrator()
	{
		using WindowsIdentity identity = WindowsIdentity.GetCurrent();
		WindowsPrincipal principal = new(identity);
		return principal.IsInRole(WindowsBuiltInRole.Administrator);
	}

	public string GetDescription(Assembly assembly)
	{
		string result = string.Empty;
		object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
		if (attributes.Length > 0)
		{
			AssemblyDescriptionAttribute descriptionAttribute = (AssemblyDescriptionAttribute)attributes[0];
			result = descriptionAttribute.Description;
		}
		return result;
	}

	public string GetCurrentVersion(WsEnumAppVerCountDigits countDigits, 
		List<WsEnumAppVerStringFormat>? stringFormats = null, Version? version = null)
	{
		if (version is null)
			version = Assembly.GetExecutingAssembly().GetName().Version;
		string version1 = string.Empty;
		string version2 = string.Empty;
		string version3 = string.Empty;
		string version4 = string.Empty;
		if (stringFormats is null || stringFormats.Count == 0)
			stringFormats = new() { WsEnumAppVerStringFormat.Use1, WsEnumAppVerStringFormat.Use2, WsEnumAppVerStringFormat.Use2 };

		WsEnumAppVerStringFormat formatMajor = stringFormats[0];
		WsEnumAppVerStringFormat formatMinor = WsEnumAppVerStringFormat.AsString;
		WsEnumAppVerStringFormat formatBuild = WsEnumAppVerStringFormat.AsString;
		WsEnumAppVerStringFormat formatRevision = WsEnumAppVerStringFormat.AsString;
		if (stringFormats.Count > 1)
			formatMinor = stringFormats[1];
		if (stringFormats.Count > 2)
			formatBuild = stringFormats[2];
		if (stringFormats.Count > 3)
			formatRevision = stringFormats[3];

		string major = GetCurrentVersionFormat(version.Major, formatMajor);
		string minor = GetCurrentVersionFormat(version.Minor, formatMinor);
		string build = GetCurrentVersionFormat(version.Build, formatBuild);
		string revision = GetCurrentVersionFormat(version.Revision, formatRevision);
		version4 = $"{major}.{minor}.{build}.{revision}";
		version3 = $"{major}.{minor}.{build}";
		version2 = $"{major}.{minor}";
		version1 = $"{major}";

		return countDigits == WsEnumAppVerCountDigits.Use1
			? version1 : countDigits == WsEnumAppVerCountDigits.Use2
				? version2 : countDigits == WsEnumAppVerCountDigits.Use3
					? version3 : version4;
	}

	public string GetCurrentVersionSubString(string input)
	{
		string result = string.Empty;
		int idx = input.LastIndexOf('.');
		if (idx >= 0)
			result = input.Substring(0, idx);
		return result;
	}

	/// <summary>
	/// Get version string.
	/// </summary>
	/// <param name="input"></param>
	/// <param name="format"></param>
	/// <returns></returns>
	private string GetCurrentVersionFormat(int input, WsEnumAppVerStringFormat format)
	{
		return format switch
		{
			WsEnumAppVerStringFormat.Use1 => $"{input:D1}",
			WsEnumAppVerStringFormat.Use2 => $"{input:D2}",
			WsEnumAppVerStringFormat.Use3 => $"{input:D3}",
			WsEnumAppVerStringFormat.Use4 => $"{input:D4}",
			_ => $"{input:D}"
        };
	}

	//public void SetNewSize(Form form, FormStartPosition startPosition = FormStartPosition.CenterScreen)
	//{
	//	if (Application.OpenForms.Count > 0)
	//	{
	//		form.Width = Application.OpenForms[0].Width;
	//		form.Height = Application.OpenForms[0].Height;
	//		form.Left = Application.OpenForms[0].Left;
	//		form.Top = Application.OpenForms[0].Top;
	//	}
	//	form.StartPosition = startPosition;
	//}

	#endregion
}