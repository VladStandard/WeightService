using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Domain.Models.Entities.Ref1c;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogPluSelect: ComponentBase
{
    [Inject] public IModalService ModalService { get; set; }
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }

    private IEnumerable<PluEntity> GetPluEntities() => LineContext.PluEntities;

    private async void OnRowSelected(PluEntity obj)
    {
        await LineContext.ChangePlu(obj);
        await ModalService.Hide();
    }
}

