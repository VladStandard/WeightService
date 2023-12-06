using Blazorise;
using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogHeader: ComponentBase
{
    [Parameter] public string Label { get; set; }
    [Inject] public IModalService ModalService { get; set; }
    
    private async Task CloseModal() => await ModalService.Hide();
}