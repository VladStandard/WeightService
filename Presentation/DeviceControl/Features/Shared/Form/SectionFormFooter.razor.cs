using Blazorise;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormFooter: ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Parameter] public DateTime? CreateDate { get; set; }
    [Parameter] public DateTime? ChangeDate { get; set; }
    [Parameter] public EventCallback OnSaveAction { get; set; }
    [Parameter] public bool IsSaveable { get; set; } = true;
    
    private async Task CloseModal() => await ModalService.Hide();

    private async Task SaveAction()
    {
        await OnSaveAction.InvokeAsync();
        await CloseModal();
    }
}