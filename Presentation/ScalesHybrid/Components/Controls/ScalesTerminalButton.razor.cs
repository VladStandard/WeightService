using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesHybrid.Components.Controls;

public sealed partial class ScalesTerminalButton: ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private ExternalDevicesService ExternalDevices { get; set; }
    
    private bool IsScalesTerminalWasOpened { get; set; }
    private bool IsScalesAvailable { get; set; }
    private int SecToOpen { get; set; } = 0;

    protected override void OnInitialized()
    {
        ScalesSubscribe();
    }

    private string GetCooldownString() => 
        $"{Localizer["ButtonCooldown"]} {SecToOpen} {Localizer["TimeMeasureSecond"]}";

    private async Task HandleButtonOpenScalesTerminal()
    {
        if (IsScalesTerminalWasOpened) return;
        
        IsScalesTerminalWasOpened = true;
        SecToOpen = 10;
        StateHasChanged();
        ExternalDevices.Scales.Calibrate();
        while (SecToOpen > 0)
        {
            await Task.Delay(1000);
            SecToOpen--;
            StateHasChanged();
        }
        IsScalesTerminalWasOpened = false;
        StateHasChanged();
    }
    
    private void UpdateScalesStatus(object recipient, GetScaleStatusEvent message)
    {
        IsScalesAvailable = message.Status is ScalesStatus.IsConnect;
        InvokeAsync(StateHasChanged);
    }
    
    private void ScalesSubscribe() =>
        WeakReferenceMessenger.Default.Register<GetScaleStatusEvent>(this, UpdateScalesStatus);

    private void ScalesUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<GetScaleStatusEvent>(this);
    
    public void Dispose() => ScalesUnsubscribe();
}
