// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Settings;
using DataCore.Models;
using DataCore.Sql.TableScaleModels.Access;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using System.ComponentModel;
using DataCore.Sql.Core.Helpers;
using DataCore.Sql.Core.Models;

namespace BlazorCore.Razors;

public partial class RazorComponentBase : LayoutComponentBase
{
	#region Public and private fields, properties, constructor

    #region Inject
    
    [Inject] public DialogService? DialogService { get; set; }
    [Inject] public IJSRuntime? JsRuntime { get; set; }
    [Inject] public NavigationManager? NavigationManager { get; set; }
    [Inject] public NotificationService? NotificationService { get; set; }
    [Inject] public TooltipService? TooltipService { get; set; }
    [Inject] public IHttpContextAccessor? HttpContextAccess { get; set; }
    [Inject] public ContextMenuService? ContextMenuService { get; set; }

    #endregion

    #region Cascade Parameters
    
    [CascadingParameter(Name = "user")]
    public UserSettingsModel? UserSettings { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; }

    #endregion

    #region Constants

    public DataAccessHelper DataAccess => DataAccessHelper.Instance;
    public HttpContext? HttpContext => HttpContextAccess?.HttpContext;
    protected BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;
    public DataContextModel DataContext { get; } = new();
    
    #endregion

    #region Parameters

    [Parameter] public RazorFieldConfigModel RazorFieldConfig { get; set; }
    [Parameter] public Guid? IdentityUid { get; set; }
    [Parameter] public long? IdentityId { get; set; }
    [Parameter] public string IdentityUidStr { get => IdentityUid?.ToString() ?? Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
    [Parameter] public string Title { get; set; }
    [Parameter] public SqlCrudConfigModel SqlCrudConfigItem { get; set; }
    [Parameter] public SqlCrudConfigModel SqlCrudConfigList { get; set; }

    #endregion

    [Parameter] public SqlTableBase? SqlItem { get; set; }
    public SqlTableBase? SqlItemFilter { get; set; }
	public List<SqlTableBase>? SqlSection { get; set; }
    public List<SqlTableBase>? SqlLinkedItems { get; set; }
    
	public RazorComponentBase()
	{
        UserSettings = null;
        Title = string.Empty;

		SqlItem = null;
        SqlItemFilter = null;
		SqlSection = null;
        SqlLinkedItems = null;

		RazorFieldConfig = new();
		SqlCrudConfigItem = SqlCrudConfigUtils.GetCrudConfigItem(true);
        SqlCrudConfigList = SqlCrudConfigUtils.GetCrudConfigComboBox();
	}
    
	protected async void SetUserSettings()
	{
		var auth = await AuthenticationStateTask;
		if (auth?.User.Identity is null) return;

		string? userName = auth.User.Identity.Name;
		if (string.IsNullOrEmpty(userName)) return;

		AccessModel? access = DataAccess.GetItemAccessNullable(userName);
		if (access is null) return;
		access.LoginDt = DateTime.Now;
		DataAccess.UpdateForce(access);

		UserSettings = new(userName, (AccessRightsEnum)access.Rights);
    }
    
    #endregion
}
