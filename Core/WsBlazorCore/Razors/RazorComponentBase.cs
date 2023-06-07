// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using WsBlazorCore.Settings;
using WsStorageCore.Helpers;
using WsStorageCore.Models;

namespace WsBlazorCore.Razors;

public partial class RazorComponentBase : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    #region Inject

    [Inject] protected DialogService DialogService { get; set; }
    [Inject] protected NotificationService NotificationService { get; set; }
    [Inject] protected IHttpContextAccessor HttpContextAccess { get; set; }

    #endregion

    #region Constants

    public static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public static WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    protected static BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;
    public HttpContext? HttpContext => HttpContextAccess?.HttpContext;

    #endregion

    #region Parameters

    [CascadingParameter] private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

    #endregion

    [Parameter] public WsSqlTableBase? SqlItem { get; set; }

    public ClaimsPrincipal? User { get; set; }

    public RazorComponentBase()
    {
        SqlItem = null;
    }

    #endregion

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationStateTask is not null)
        {
            AuthenticationState authState = await AuthenticationStateTask;
            User = authState?.User;
        }
    }
}