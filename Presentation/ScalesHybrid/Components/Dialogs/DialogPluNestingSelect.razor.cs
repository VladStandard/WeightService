using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogPluNestingSelect : ComponentBase
{
    [Inject] public IModalService ModalService { get; set; }
    [Inject] private LineContext LineContext { get; set; }

    private IEnumerable<SqlPluNestingFkEntity> GetPluNestingsEntities() => LineContext.PluNestingEntities;
    
    private async Task CloseModal() => await ModalService.Hide();

    private async void OnRowSelected(SqlPluNestingFkEntity obj)
    {
        LineContext.ChangePluNesting(obj);
        await ModalService.Hide();
    }
}