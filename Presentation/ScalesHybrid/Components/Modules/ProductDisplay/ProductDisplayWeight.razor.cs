using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Scales.Events;

namespace ScalesHybrid.Components.Modules.ProductDisplay;

public sealed partial class ProductDisplayWeight: ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    
    private bool IsStable { get; set; }

    protected override void OnInitialized()
    {
        LineContext.OnStateChanged += StateHasChanged;
        MassaSubscribe();
    }

    private decimal GetNetWeight => (decimal)LineContext.KneadingModel.NetWeightG / 1000 - GetTareWeight;
    
    private decimal GetTareWeight => LineContext.PluNesting.WeightTare;

    private string Sign => GetNetWeight >= 0 ? string.Empty : "-";
    
    private string IntegerPart => ((int)Math.Truncate(Math.Abs(GetNetWeight))).ToString("D4");
    
    private string DecimalPart => Math.Abs(GetNetWeight % 1).ToString(".000")[1..];
    
    private void UpdateScalesInfo(object sender, GetScaleMassaEvent payload)
    {
        IsStable = payload.IsStable;
        LineContext.KneadingModel.NetWeightG = payload.Weight;
        InvokeAsync(StateHasChanged);
    }

    private void MassaSubscribe() =>
        WeakReferenceMessenger.Default.Register<GetScaleMassaEvent>(this, UpdateScalesInfo);
    
    private void MassaUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
    
    public void Dispose()
    {
        LineContext.OnStateChanged -= StateHasChanged;
        MassaUnsubscribe();
    }
}