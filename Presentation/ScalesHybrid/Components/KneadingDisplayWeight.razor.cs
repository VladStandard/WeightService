using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using NHibernate.Criterion;
using Radzen;
using Radzen.Blazor;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Models;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Printers.Events;
using Ws.Scales.Events;

namespace ScalesHybrid.Components;

public sealed partial class KneadingDisplayWeight: ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private DialogService DialogService { get; set; }
    [Inject] private ExternalDevicesService ExternalDevices { get; set; }

    private string Sign => LineContext.KneadingModel.NetWeight >= 0 ? string.Empty : "-";
    
    private string IntegerPart => ((int)Math.Truncate(Math.Abs(LineContext.KneadingModel.NetWeight))).ToString("D4");
    
    private string DecimalPart => Math.Abs(LineContext.KneadingModel.NetWeight % 1).ToString(".000")[1..];
    
    private void IncreaseDate() => LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(1);
    
    private void DecreaseDate() => LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(-1);
    
    private void SetNewKneading(int newKneading) => LineContext.KneadingModel.KneadingCount = newKneading;

    private Timer _timer;
    
    public KneadingDisplayWeight()
    {
        MassaSubscribe();
    }
    
    protected override void OnInitialized()
    {
        _timer = new(_ => ExternalDevices.Scales.SendGetWeight(), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));
    }

    private void UpdateScalesInfo(object sender, GetScaleMassaEvent payload)
    {
        if (payload.IsStable)
        {
            LineContext.KneadingModel.NetWeight = payload.Weight;
            InvokeAsync(StateHasChanged);
        }
    }

    private void MassaSubscribe()
    {
        WeakReferenceMessenger.Default.Register<GetScaleMassaEvent>(this, UpdateScalesInfo);
    }
    
    private void MassaUnsubscribe()
    {
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
    }
    
    private async Task ShowInlineDialog() => 
        await DialogService.OpenAsync<DialogCalculator>(string.Empty,
            new() { { "CallbackFunction", new Action<int>(SetNewKneading) } },
            new() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto" });
    
    public void Dispose()
    {
        _timer.Dispose();
        MassaUnsubscribe();
        DialogService?.Dispose();
    }
}