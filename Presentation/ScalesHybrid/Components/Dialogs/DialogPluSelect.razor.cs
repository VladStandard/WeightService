using Blazorise;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogPluSelect: ComponentBase
{
    [Inject] public IModalService ModalService { get; set; }
    [Inject] private LineContext LineContext { get; set; }

    private IEnumerable<SqlPluEntity> GetPluEntities() => LineContext.PluEntities;
    
    private async Task CloseModal() => await ModalService.Hide();

    private async void OnRowSelected(SqlPluEntity obj)
    {
        await LineContext.ChangePlu(obj);
        await ModalService.Hide();
    }
}

