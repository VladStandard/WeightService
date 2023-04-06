// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using System.Security.Claims;
using DataCore.Sql.Core.Helpers;
using DataCore.Sql.Core.Models;

namespace BlazorCore.Razors;

public partial class RazorComponentBase : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    #region Inject

    [Inject] protected DialogService? DialogService { get; set; }
    [Inject] protected IJSRuntime? JsRuntime { get; set; }
    [Inject] protected NavigationManager? NavigationManager { get; set; }
    [Inject] protected NotificationService? NotificationService { get; set; }
    [Inject] protected TooltipService? TooltipService { get; set; }
    [Inject] protected IHttpContextAccessor? HttpContextAccess { get; set; }
    [Inject] protected ContextMenuService? ContextMenuService { get; set; }
    [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    #endregion

    #region Constants

    public WsDataAccessHelper DataAccess => WsDataAccessHelper.Instance;
    public HttpContext? HttpContext => HttpContextAccess?.HttpContext;
    protected BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;
    public WsDataContextModel DataContext { get; } = new();

    #endregion

    #region Parameters

    [Parameter] public RazorFieldConfigModel RazorFieldConfig { get; set; }
    [Parameter] public Guid? IdentityUid { get; set; }
    [Parameter] public long? IdentityId { get; set; }

    [Parameter]
    public string IdentityUidStr
    {
        get => IdentityUid?.ToString() ?? Guid.Empty.ToString();
        set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty;
    }

    [Parameter] public string Title { get; set; }
    [Parameter] public SqlCrudConfigModel SqlCrudConfigItem { get; set; }

    #endregion

    [Parameter] public SqlTableBase? SqlItem { get; set; }
    public SqlTableBase? SqlItemFilter { get; set; }
    public List<SqlTableBase>? SqlLinkedItems { get; set; }
    public ClaimsPrincipal? User { get; set; }
    
	public RazorComponentBase()
	{
        Title = string.Empty;

		SqlItem = null;
        SqlItemFilter = null;
        SqlLinkedItems = null;

		RazorFieldConfig = new();
		SqlCrudConfigItem = WsSqlCrudConfigUtils.GetCrudConfigItem(true);
    }

    #endregion

    protected override async Task OnInitializedAsync()
    {
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        User = authState.User;
    }
}
