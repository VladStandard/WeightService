using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogNestingSelect : ComponentBase
{
    [Inject] public IModalService ModalService { get; set; }
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }

    private IEnumerable<PluNestingEntity> GetPluNestingsEntities() => LineContext.PluNestingEntities;

    private async void OnRowSelected(PluNestingEntity obj)
    {
        LineContext.ChangePluNesting(obj);
        await ModalService.Hide();
    }
}