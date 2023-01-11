// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace BlazorCore.Razors;

public class UserSettingsModel
{
	#region Public and private fields, properties, constructor

	public bool AccessRightsIsAdmin => (byte)AccessRights >= (byte)AccessRightsEnum.Admin;
	public bool AccessRightsIsNone => (byte)AccessRights == (byte)AccessRightsEnum.None;
	public bool AccessRightsIsRead => (byte)AccessRights >= (byte)AccessRightsEnum.Read;
	public bool AccessRightsIsWrite => (byte)AccessRights >= (byte)AccessRightsEnum.Write;
	public AccessRightsEnum AccessRights { get; }
	public string? UserName { get; }
	public string UserDescription => $"{LocaleCore.Strings.AuthorizingUserName}: {UserName}";

	/// <summary>
	/// Constructor.
	/// </summary>
	public UserSettingsModel()
	{
		AccessRights = AccessRightsEnum.None;
		UserName = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="accessRights"></param>
	/// <param name="userName"></param>
	/// <param name="id"></param>
	/// <param name="ipAddress"></param>
	public UserSettingsModel(string? userName, AccessRightsEnum accessRights)
	{
		UserName = userName;
		AccessRights = accessRights;
	}

	#endregion

	#region Public and private methods

	public override string ToString() => $"{nameof(UserName)}: {UserName}. " + $"{nameof(AccessRights)}: {AccessRights}. ";

	private string GetColorAccessRights(AccessRightsEnum accessRights, uint rowCounter) =>
		accessRights switch
		{
			AccessRightsEnum.Read => rowCounter % 2 == 0 ? "rz-datatable-even-read" : "rz-datatable-odd-read",
			AccessRightsEnum.Write => rowCounter % 2 == 0 ? "rz-datatable-even-write" : "rz-datatable-odd-write",
			AccessRightsEnum.Admin => rowCounter % 2 == 0 ? "rz-datatable-even-admin" : "rz-datatable-odd-admin",
			_ => rowCounter % 2 == 0 ? "rz-datatable-even-none" : "rz-datatable-odd-none",
		};

	public string GetColorAccessRights(AccessRightsEnum accessRights) =>
		accessRights switch
		{
			AccessRightsEnum.Read => "rz-datatable-read",
			AccessRightsEnum.Write => "rz-datatable-write",
			AccessRightsEnum.Admin => "rz-datatable-admin",
			_ => ".rz-datatable-none",
		};

	#endregion
}
