using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeviceControl.Services;

public class JsService
{
    [Inject] private IJSRuntime JsRuntime { get; init; }
    private IJSObjectReference Module { get; set; } = null!;

    public JsService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
        InitializeModuleAsync();
    }
    
    private async void InitializeModuleAsync()
    {
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/appUtils.js");
    }
}
