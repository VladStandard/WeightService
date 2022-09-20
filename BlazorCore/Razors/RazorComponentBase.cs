// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;

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
	[Parameter] public RazorComponentConfigModel RazorComponentConfig { get; set; }
	[Parameter] public RazorFieldConfigModel RazorFieldConfig { get; set; }
	[Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }
	[Parameter] public SqlTableActionEnum? TableAction { get; set; }
	[Parameter] public Guid? IdentityUid { get; set; }
	[Parameter] public string IdentityUidStr { get => IdentityUid?.ToString() ?? Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
	[Parameter] public long? IdentityId { get; set; }
	[Parameter] public RazorComponentBase? ParentRazor { get; set; }
	[Parameter] public UserSettingsModel? UserSettings { get; set; }
	[Parameter] public HttpContext? HttpContext { get; set; }
	[Parameter] public string Title { get; set; }
	protected AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;
	protected bool IsActionsParametersSetFinished { get; set; }
	private string Id => HttpContext is null ? string.Empty : HttpContext.Connection.Id;
	private string IpAddress => HttpContext?.Connection.RemoteIpAddress is null ? string.Empty : HttpContext.Connection.RemoteIpAddress.ToString();
	protected string IdDescription => $"{LocaleCore.Strings.AuthorizingId}: {Id}";
	protected string IpAddressDescription => $"{LocaleCore.Strings.AuthorizingApAddress}: {IpAddress}";
	[Parameter] public string AuthorizingText { get; set; }
	public SqlTableBase? SqlItem { get; set; }
	public List<SqlTableBase>? SqlItems { get; set; }

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

		UserSettings = null;
		ButtonSettings = null;
		TableAction = null;
		IsActionsParametersSetFinished = false;
		Title = string.Empty;
		AuthorizingText = string.Empty;

		SqlItem = null;
		SqlItems = null;
		
        RazorFieldConfig = new();
		RazorComponentConfig = new();
	}

	private void SetPropertiesFromParent()
	{
		if (ParentRazor is null) return;

		if (ParentRazor.UserSettings is not null)
			UserSettings = ParentRazor.UserSettings;
		if (ParentRazor.HttpContext is not null)
			HttpContext = ParentRazor.HttpContext;
		//if (ParentRazor.RazorComponentConfig is not null)
			RazorComponentConfig = ParentRazor.RazorComponentConfig;
		if (ParentRazor.IdentityId is not null)
			IdentityId = ParentRazor.IdentityId;
		if (ParentRazor.IdentityUid is not null)
			IdentityUid = ParentRazor.IdentityUid;
		if (ParentRazor.SqlItem is not null)
			SqlItem = ParentRazor.SqlItem;
		if (ParentRazor.SqlItems is not null)
			SqlItems = ParentRazor.SqlItems;
		if (ParentRazor.TableAction is not null)
			TableAction = ParentRazor.TableAction;
		if (ParentRazor.ButtonSettings is not null)
			ButtonSettings = ParentRazor.ButtonSettings;
	}

	private void SetupUserSettings(string? userName)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new() { new(nameof(AccessModel.User), SqlFieldComparerEnum.Equal, userName) },
		null, 0, false, false);
		AccessModel access = DataAccessHelper.Instance.GetItemNotNull<AccessModel>(sqlCrudConfig);

		UserSettings = new(userName, (AccessRightsEnum)access.Rights);
        if (ParentRazor is not null)
		{
			bool isOnChange = ParentRazor.UserSettings is null;
            ParentRazor.UserSettings = UserSettings;
			if (isOnChange)
                ParentRazor.OnChange();
        }
    }

	protected string GetAuthorizingText()
	{
		AuthorizingText = LocaleCore.Strings.AuthorizingProcess;
		SetupUserSettings(string.Empty);
        //OnChange();
		return AuthorizingText;
	}

	protected string GetAuthorizedText(string? name)
	{
		AuthorizingText = LocaleCore.Strings.AuthorizingSuccess;
		SetupUserSettings(name);
        //OnChange();
        return AuthorizingText;
	}

	protected string GetNotAuthorizedText()
	{
		AuthorizingText = LocaleCore.Strings.AuthorizingNot;
		SetupUserSettings(LocaleCore.System.SystemIdentityNotAuthorized);
        //OnChange();
        return AuthorizingText;
	}

	#endregion
}
