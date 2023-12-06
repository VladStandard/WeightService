using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogNestingSelect : ComponentBase
{
    [Inject] public IModalService ModalService { get; set; }
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }

    private IEnumerable<SqlPluNestingFkEntity> GetPluNestingsEntities() => LineContext.PluNestingEntities;

    private async void OnRowSelected(SqlPluNestingFkEntity obj)
    {
        LineContext.ChangePluNesting(obj);
        await ModalService.Hide();
    }
}