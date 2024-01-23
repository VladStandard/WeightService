using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Domain.Models.Entities.Print;

namespace ScalesHybrid.Features.Pallet.Modules;

public sealed partial class PalletViewer : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    private List<LabelEntity> SelectedLabels { get; set; } = [];

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    private void CloseCurrentPallet() => PalletContext.ChangePallet(new());

    private void OnSelectedLabelsChanged()
    {
        InvokeAsync(StateHasChanged);
    } 

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}