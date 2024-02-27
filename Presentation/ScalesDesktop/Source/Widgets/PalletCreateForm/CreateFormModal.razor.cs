using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;

namespace ScalesDesktop.Source.Widgets.PalletCreateForm;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class CreateFormModal: ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<Resources> PalletLocalizer { get; set; } = null!;

    private async Task CloseModal() => await ModalService.Hide();
}