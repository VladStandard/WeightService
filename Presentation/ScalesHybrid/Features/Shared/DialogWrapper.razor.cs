using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Features.Shared;

public sealed partial class DialogWrapper: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    private async Task CloseCurrentDialog() => await ModalService.Hide();
}