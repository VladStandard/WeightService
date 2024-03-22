using System.Security.Claims;
using DeviceControl2.Source.Shared.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeviceControl2.Source.App;

public sealed partial class MainLayout : LayoutComponentBase
{
    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = null!;
    private ClaimsPrincipal? User { get; set; }

    protected override async Task OnInitializedAsync() => User = (await AuthState).User;
}