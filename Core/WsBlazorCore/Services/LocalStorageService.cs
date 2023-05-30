using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace WsBlazorCore.Services;

public class LocalStorageService
{
    private readonly IConfiguration _config;
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IConfiguration config, IJSRuntime jsRuntime)
    {
        _config = config;
        _jsRuntime = jsRuntime;
    }

    public async Task<string> GetItem(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
    }

    public async Task SetItem(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    public async Task RemoveItem(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}