using Microsoft.AspNetCore.Components;
using Radzen;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogPluSelect: ComponentBase
{
    [Inject] private DialogService DialogService { get; set; }
    
    [Parameter] public IEnumerable<SqlPluEntity> PluEntities { get; set; }
    [Parameter] public Action<SqlPluEntity> CallbackFunction { get; set; }

    private void OnRowSelect(SqlPluEntity sqlPluEntity)
    {
        CallbackFunction.Invoke(sqlPluEntity);
        DialogService.Close();
    }
}

