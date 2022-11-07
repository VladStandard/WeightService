// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Settings;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using DataCore.Sql.Models;

namespace BlazorCore.Razors;

public partial class RazorComponentBase : LayoutComponentBase
{
	#region Public and private fields, properties, constructor

	[Inject] public DialogService? DialogService { get; set; }
	[Inject] public IJSRuntime? JsRuntime { get; set; }
	[Inject] public NavigationManager? NavigationManager { get; set; }
	[Inject] public NotificationService? NotificationService { get; set; }
	[Inject] public TooltipService? TooltipService { get; set; }
	[Inject] public IHttpContextAccessor? HttpContextAccess { get; set; }
	[Parameter] public bool IsShowReload { get; set; }
	[Parameter]
	public string IsShowReloadStr
	{
		get => IsShowReload ? "true" : "false";
		set => IsShowReload = value.Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);
	}
	[Parameter] public RazorFieldConfigModel RazorFieldConfig { get; set; }
	[Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }
	[Parameter] public Guid? IdentityUid { get; set; }
	[Parameter] public string IdentityUidStr { get => IdentityUid?.ToString() ?? Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
	[Parameter] public long? IdentityId { get; set; }
	[Parameter] public RazorComponentBase? ParentRazor { get; set; }
	[Parameter] public UserSettingsModel? UserSettings { get; set; }
	[Parameter] public HttpContext? HttpContext { get; set; }
	[Parameter] public string Title { get; set; }
	protected BlazorAppSettingsHelper BlazorAppSettings { get; } = BlazorAppSettingsHelper.Instance;
	private string Id => HttpContext is null ? string.Empty : HttpContext.Connection.Id;
	private string IpAddress => HttpContext?.Connection.RemoteIpAddress is null ? string.Empty : HttpContext.Connection.RemoteIpAddress.ToString();
	protected string IdDescription => $"{LocaleCore.Strings.AuthorizingId}: {Id}";
	protected string IpAddressDescription => $"{LocaleCore.Strings.AuthorizingApAddress}: {IpAddress}";
	public SqlTableBase? SqlItem { get; set; }
	public SqlTableBase? SqlItemFilter { get; set; }
	public List<SqlTableBase>? SqlItems { get; set; }
	public AuthorizeView? AuthorizeViewBase { get; set; }
	public AuthenticationState? AuthenticationStateBase { get; set; }
	public DataContextModel DataContext { get; } = new();
	public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	[Parameter] public SqlCrudConfigModel SqlCrudConfigItem { get; set; }
	[Parameter] public SqlCrudConfigModel SqlCrudConfigList { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public RazorComponentBase()
	{
		NotificationService = null;
		NavigationManager = null;
		JsRuntime = null;
		DialogService = null;
		TooltipService = null;

		HttpContext = null;
		AuthorizeViewBase = null;
		AuthenticationStateBase = null;
		UserSettings = null;
		ButtonSettings = null;
		Title = string.Empty;

		SqlItem = null;
		SqlItemFilter = null;
		SqlItems = null;

		RazorFieldConfig = new();
		SqlCrudConfigItem = SqlCrudConfigUtils.GetCrudConfigItem(true);
		SqlCrudConfigList = SqlCrudConfigUtils.GetCrudConfigList(false);
	}

	private void SetPropertiesFromParent()
	{
		if (ParentRazor is null) return;

		if (ParentRazor.HttpContext is not null)
			HttpContext = ParentRazor.HttpContext;
		if (ParentRazor.AuthorizeViewBase is not null)
			AuthorizeViewBase = ParentRazor.AuthorizeViewBase;
		if (ParentRazor.AuthenticationStateBase is not null)
			AuthenticationStateBase = ParentRazor.AuthenticationStateBase;
		if (ParentRazor.UserSettings is not null)
			UserSettings = ParentRazor.UserSettings;
		if (ParentRazor.IdentityId is not null)
			IdentityId = ParentRazor.IdentityId;
		if (ParentRazor.IdentityUid is not null)
			IdentityUid = ParentRazor.IdentityUid;
		if (ParentRazor.SqlItem is not null)
			SqlItem = ParentRazor.SqlItem;
		if (ParentRazor.SqlItems is not null)
			SqlItems = ParentRazor.SqlItems;
		if (ParentRazor.ButtonSettings is not null)
			ButtonSettings = ParentRazor.ButtonSettings;
		SqlCrudConfigItem = ParentRazor.SqlCrudConfigItem;
		SqlCrudConfigList = ParentRazor.SqlCrudConfigList;
	}

	private void SetPropertiesToParent()
	{
		if (ParentRazor is null) return;

		if (HttpContext is not null)
			ParentRazor.HttpContext = HttpContext;
		if (AuthorizeViewBase is not null)
			ParentRazor.AuthorizeViewBase = AuthorizeViewBase;
		if (AuthenticationStateBase is not null)
			ParentRazor.AuthenticationStateBase = AuthenticationStateBase;
		if (UserSettings is not null)
			ParentRazor.UserSettings = UserSettings;
		if (IdentityId is not null)
			ParentRazor.IdentityId = IdentityId;
		if (IdentityUid is not null)
			ParentRazor.IdentityUid = IdentityUid;
		if (SqlItem is not null)
			ParentRazor.SqlItem = SqlItem;
		if (SqlItems is not null)
			ParentRazor.SqlItems = SqlItems;
		if (ButtonSettings is not null)
			ParentRazor.ButtonSettings = ButtonSettings;
		ParentRazor.SqlCrudConfigItem = SqlCrudConfigItem;
		ParentRazor.SqlCrudConfigList = SqlCrudConfigList;
	}

	private void SetUserSettings(AuthenticationState? authenticationState)
	{
		AuthenticationStateBase = authenticationState;
		if (AuthenticationStateBase?.User.Identity is null) return;

		string? userName = AuthenticationStateBase.User.Identity.Name;
		if (string.IsNullOrEmpty(userName)) return;

		AccessModel? access = DataAccessHelper.Instance.GetItemAccess(userName);
		if (access is null) return;
        access.LoginDt = DateTime.Now;
        DataAccessHelper.Instance.Update(access);

        UserSettings = new(userName, (AccessRightsEnum)access.Rights);

		if (ParentRazor is not null)
		{
			bool isOnChange = ParentRazor.UserSettings is null;
			ParentRazor.UserSettings = UserSettings;
			if (isOnChange)
				ParentRazor.OnChange();
		}
	}

	protected string SetAuthorizing()
	{
		SetUserSettings(null);
		return LocaleCore.Strings.AuthorizingProcess;
	}

	protected string SetAuthorized(AuthenticationState authenticationState)
	{
		SetUserSettings(authenticationState);
		return LocaleCore.Strings.AuthorizingSuccess;
	}

	protected string SetNotAuthorized()
	{
		SetUserSettings(null);
		return LocaleCore.System.SystemIdentityNotAuthorized;
	}

	#endregion
}
