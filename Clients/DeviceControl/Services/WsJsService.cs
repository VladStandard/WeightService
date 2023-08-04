// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Services;

public class WsJsService
{
    private readonly IJSRuntime _jsRuntime;
    
    public WsJsService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task CopyTextToClipboard(string text)
    {
        await _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }

    public async Task RedirectBack()
    {
       await _jsRuntime.InvokeVoidAsync("goBack");
    }
}