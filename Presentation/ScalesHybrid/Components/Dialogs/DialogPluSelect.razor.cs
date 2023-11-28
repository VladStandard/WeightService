using Microsoft.AspNetCore.Components;
using Radzen;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogPluSelect: ComponentBase
{
    [Inject] private DialogService DialogService { get; set; }
    [Inject] private LineContext LineContext { get; set; }

    private IEnumerable<SqlPluEntity> GetPluEntities() => LineContext.PluEntities;

    private async Task OnRowSelect(SqlPluEntity sqlPluEntity)
    {
        await LineContext.ChangePlu(sqlPluEntity);
        DialogService.Close();
    }
}

