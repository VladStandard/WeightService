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

    private void OnRowSelect(SqlPluEntity sqlPluEntity)
    {
        LineContext.ChangePlu(sqlPluEntity);
        DialogService.Close();
    }
}

