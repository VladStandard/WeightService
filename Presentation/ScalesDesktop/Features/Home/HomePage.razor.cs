// ReSharper disable ClassNeverInstantiated.Global

using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Home;

public sealed partial class HomePage: ComponentBase
{
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    private async Task ShowCloseAppDialog() => await ModalService.Show<CloseAppDialog>(string.Empty,
        new ModalInstanceOptions { Size = ModalSize.Default });
}