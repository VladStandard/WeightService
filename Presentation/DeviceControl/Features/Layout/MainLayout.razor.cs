// ReSharper disable ClassNeverInstantiated.Global
using System.Security.Claims;
using DeviceControl.Services;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Layout;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private UserService UserService { get; set; } = null!;
    private ClaimsPrincipal? User { get; set; }

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
    }
    
    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";
}
