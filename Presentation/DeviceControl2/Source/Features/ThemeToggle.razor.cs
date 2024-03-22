using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeviceControl2.Source.Features;

public sealed partial class ThemeToggle : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? Module { get; set; }
    private bool IsOpen { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./libs/theme-utils.js");
    }

    private async Task SetTheme(string theme) =>
        await Module!.InvokeVoidAsync("switchTheme", theme);


    public async ValueTask DisposeAsync()
    {
        try
        {
            if (Module == null) return;
            await Module.DisposeAsync();
        }
        catch (Exception ex) when (ex is JSDisconnectedException or ArgumentNullException)
        {
            // pass error
        }
    }
}