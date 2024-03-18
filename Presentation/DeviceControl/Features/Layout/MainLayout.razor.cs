// ReSharper disable ClassNeverInstantiated.Global
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeviceControl.Features.Layout;

public partial class MainLayout : LayoutComponentBase
{
    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = null!;
    private ClaimsPrincipal? User { get; set; }

    protected override async Task OnInitializedAsync() => User = (await AuthState).User;
}