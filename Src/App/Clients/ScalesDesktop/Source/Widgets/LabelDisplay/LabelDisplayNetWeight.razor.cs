using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Scales.Messages;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayNetWeight : ComponentBase, IRecipient<ScaleMassaMsg>, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private LineContext LineContext { get; set; } = default!;

    # endregion

    private bool IsStable { get; set; }

    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register(this);
        LabelContext.OnStateChanged += StateHasChanged;
        LineContext.StartWeightPolling();
    }

    private decimal GetNetWeight => (decimal)LabelContext.KneadingModel.NetWeightG / 1000 - GetTareWeight;

    private decimal GetTareWeight => LabelContext.Plu.GetWeightWithNesting;

    private string Sign => GetNetWeight >= 0 ? string.Empty : "-";

    private string IntegerPart => $"{Math.Truncate(Math.Abs(GetNetWeight)):0000}";

    private string DecimalPart => Math.Abs(GetNetWeight % 1).ToString(".000")[1..];

    public void Receive(ScaleMassaMsg message)
    {
        IsStable = message.IsStable;
        LabelContext.KneadingModel.NetWeightG = message.Weight;
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        LabelContext.OnStateChanged -= StateHasChanged;
    }
}