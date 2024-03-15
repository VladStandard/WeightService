using System.Security.Claims;
using DeviceControl2.Source.Shared.Auth;
using Microsoft.AspNetCore.Components;

namespace DeviceControl2.Source.App;

public sealed partial class MainLayout : LayoutComponentBase
{
    [Inject] private UserService UserService { get; set; } = null!;
    private ClaimsPrincipal? User { get; set; }

    protected override async Task OnInitializedAsync() => User = await UserService.GetUser();
}