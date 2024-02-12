using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Features.Pallet.Create;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class PalletSelect : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    private void ReloadData() => PalletContext.ResetContext();

    private async Task ShowCreateFormDialog() => await ModalService.Show<CreateFormModal>();

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}