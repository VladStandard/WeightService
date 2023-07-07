// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Security.Claims;
using DeviceControl.Services;
using WsStorageCore.Helpers;

namespace DeviceControl.Components.Common;

public partial class RazorComponentBase : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    #region Inject

    [Inject] protected DialogService DialogService { get; set; }
    [Inject] protected NotificationService NotificationService { get; set; }
    [Inject] protected UserService UserService { get; set; }

    #endregion

    #region Constants
    
    protected static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    #endregion
    
    protected ClaimsPrincipal? User { get; set; }

    #endregion

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        await base.OnInitializedAsync();
    }
}