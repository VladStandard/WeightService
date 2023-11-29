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
using ScalesHybrid.Utils;
using Ws.Printers.Events;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesHybrid.Components;

public sealed partial class KneadingDisplayWeight: ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private DialogService DialogService { get; set; }
    [Inject] private ExternalDevicesService ExternalDevices { get; set; }

    private bool IsStable { get; set; } = false;
    private bool IsDisconnected { get; set; } = false;

    private Timer _timer;
    
    public KneadingDisplayWeight()
    {
        MassaSubscribe();
    }
    
    protected override void OnInitialized()
    {
        LineContext.OnStateChanged += StateHasChanged;
        _timer = new(_ => ExternalDevices.Scales.SendGetWeight(), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));
    }

    private string GetFormattedPluNestingName() => NameFormatting.GetPluNestingFormattedName(LineContext.PluNesting);
    
    private decimal GetNetWeight => (decimal)LineContext.KneadingModel.NetWeightG / 1000 - GetTareWeight;
    
    private decimal GetTareWeight => LineContext.PluNesting.WeightTare;

    private string Sign => GetNetWeight >= 0 ? string.Empty : "-";
    
    private string IntegerPart => ((int)Math.Truncate(Math.Abs(GetNetWeight))).ToString("D4");
    
    private string DecimalPart => Math.Abs(GetNetWeight % 1).ToString(".000")[1..];
    
    private void IncreaseDate() => LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(1);
    
    private void DecreaseDate() => LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(-1);
    
    private void SetNewKneading(int newKneading) => LineContext.KneadingModel.KneadingCount = newKneading;

    private void UpdateScalesInfo(object sender, GetScaleMassaEvent payload)
    {
        IsStable = payload.IsStable;
        LineContext.KneadingModel.NetWeightG = payload.Weight;
        InvokeAsync(StateHasChanged);
    }

    private void MassaSubscribe()
    {
        WeakReferenceMessenger.Default.Register<GetScaleStatusEvent>(this, UpdateScalesStatus);
        WeakReferenceMessenger.Default.Register<GetScaleMassaEvent>(this, UpdateScalesInfo);
    }

    private void UpdateScalesStatus(object recipient, GetScaleStatusEvent message)
    {
        IsDisconnected = message.Status == ScalesStatus.IsDisabled;
        InvokeAsync(StateHasChanged);
    }

    private void MassaUnsubscribe()
    {
        WeakReferenceMessenger.Default.Unregister<GetScaleStatusEvent>(this);
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
    }
    
    private async Task ShowInlineDialog() => 
        await DialogService.OpenAsync<DialogCalculator>(string.Empty,
            new() { { "CallbackFunction", new Action<int>(SetNewKneading) } },
            new() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto" });

    public void Dispose()
    {
        LineContext.OnStateChanged -= StateHasChanged;
        _timer.Dispose();
        MassaUnsubscribe();
    }
}