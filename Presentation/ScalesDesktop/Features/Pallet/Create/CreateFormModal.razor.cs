using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Pallet.Resources;

namespace ScalesDesktop.Features.Pallet.Create;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class CreateFormModal: ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<PalletResources> PalletLocalizer { get; set; } = null!;

    private async Task CloseModal() => await ModalService.Hide();
}