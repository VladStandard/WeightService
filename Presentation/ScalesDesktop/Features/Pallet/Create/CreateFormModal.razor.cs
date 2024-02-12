using Blazorise;
using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Features.Pallet.Create;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class CreateFormModal: ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;

    private async Task CloseModal() => await ModalService.Hide();
}