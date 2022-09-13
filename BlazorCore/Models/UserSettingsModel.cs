// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Core;
using DataCore.Sql.Models;

namespace BlazorCore.Models;

public class UserSettingsModel
{
	#region Public and private fields, properties, constructor

	public bool AccessRightsIsAdmin => (byte)AccessRights >= (byte)AccessRightsEnum.Admin;
	public bool AccessRightsIsNone => (byte)AccessRights == (byte)AccessRightsEnum.None;
	public bool AccessRightsIsRead => (byte)AccessRights >= (byte)AccessRightsEnum.Read;
	public bool AccessRightsIsWrite => (byte)AccessRights >= (byte)AccessRightsEnum.Write;
	public AccessRightsEnum AccessRights { get; private set; }
	public string UserName { get; private set; }
	public string Id { get; set; }
	public string IpAddress { get; set; }
	public string IdDescription => $"{LocaleCore.Strings.AuthorizingId}: {Id}";
	public string IpAddressDescription => $"{LocaleCore.Strings.AuthorizingApAddress}: {IpAddress}";
	public string UserDescription => $"{LocaleCore.Strings.AuthorizingUserName}: {UserName}";

	/// <summary>
	/// Constructor.
	/// </summary>
	public UserSettingsModel()
	{
		AccessRights = AccessRightsEnum.None;
		UserName = string.Empty;
		Id = string.Empty;
		IpAddress = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="accessRights"></param>
	/// <param name="userName"></param>
	/// <param name="id"></param>
	/// <param name="ipAddress"></param>
	public UserSettingsModel(AccessRightsEnum accessRights, string userName, string id, string ipAddress)
	{
		AccessRights = accessRights;
		UserName = userName;
		Id = id;
		IpAddress = ipAddress;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="httpContext"></param>
	public UserSettingsModel(HttpContext httpContext) : this()
	{
		IpAddress = httpContext.Connection.RemoteIpAddress is null ? string.Empty : httpContext.Connection.RemoteIpAddress.ToString();
		Id = httpContext.Connection.Id;
	}

	#endregion

	#region Public and private methods

	public override string ToString() => $"{nameof(UserName)}: {UserName}. " + $"{nameof(AccessRights)}: {AccessRights}. ";

	private string GetColorAccessRights(AccessRightsEnum accessRights, uint rowCounter)
	{
		if (accessRights == AccessRightsEnum.None)
			accessRights = AccessRights;
		return accessRights switch
		{
			AccessRightsEnum.Read => rowCounter % 2 == 0 ? "rz-datatable-even-read" : "rz-datatable-odd-read",
			AccessRightsEnum.Write => rowCounter % 2 == 0 ? "rz-datatable-even-write" : "rz-datatable-odd-write",
			AccessRightsEnum.Admin => rowCounter % 2 == 0 ? "rz-datatable-even-admin" : "rz-datatable-odd-admin",
			_ => rowCounter % 2 == 0 ? "rz-datatable-even-none" : "rz-datatable-odd-none",
		};
	}

	public string GetColorAccessRights(byte accessRights, uint rowCounter) => GetColorAccessRights((AccessRightsEnum)accessRights, rowCounter);

	public string GetColorAccessRights(AccessRightsEnum accessRights)
	{
		if (accessRights == AccessRightsEnum.None)
			accessRights = AccessRights;
		return accessRights switch
		{
			AccessRightsEnum.Read => "rz-datatable-read",
			AccessRightsEnum.Write => "rz-datatable-write",
			AccessRightsEnum.Admin => "rz-datatable-admin",
			_ => ".rz-datatable-none",
		};
	}

	public string GetColorAccessRights(byte accessRights) => GetColorAccessRights((AccessRightsEnum)accessRights);

	public void SetupUserName(string? value, RazorPageBase? parentRazorPage)
	{
		UserName = value ?? string.Empty;

		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new() { new(SqlFieldEnum.User, SqlFieldComparerEnum.Equal, UserName) },
			null, 0, false, false);
		AccessModel access = DataAccessHelper.Instance.GetItemNotNull<AccessModel>(sqlCrudConfig);
		AccessRights = (AccessRightsEnum)access.Rights;

		if (parentRazorPage is not null)
		{
			//parentRazorPage.UserSettings = this;
			parentRazorPage.UserSettings.SetupUserName(UserName, parentRazorPage.ParentRazor);
		}
	}

	#endregion
}
