using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogLineSelect : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] public IModalService ModalService { get; set; }

    private IEnumerable<SqlLineEntity> GetLineEntities() => LineContext.LineEntities;

    private async Task CloseModal() => await ModalService.Hide();

    private async void OnRowSelected(SqlLineEntity obj)
    {
        LineContext.ChangeLine(obj);
        await CloseModal();
    }
}