using Microsoft.JSInterop;

namespace DeviceControl.Services;

public class JsService
{
    private readonly IJSRuntime _jsRuntime;
    
    public JsService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task CopyTextToClipboard(string text)
    {
        await _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }

    public async Task<bool> RedirectBackIfNotHomePage()
    {
        return await _jsRuntime.InvokeAsync<bool>("goBackIfNotHomePage");
    }
}