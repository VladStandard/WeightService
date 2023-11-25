using Microsoft.AspNetCore.Components;
using Radzen;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogLineSelect : ComponentBase
{
    [Inject] private DialogService DialogService { get; set; }
    
    [Parameter] public IEnumerable<SqlLineEntity> LineEntities { get; set; }
    [Parameter] public Action<SqlLineEntity> CallbackFunction { get; set; }

    private void OnRowSelect(SqlLineEntity sqlLineEntity)
    {
        CallbackFunction.Invoke(sqlLineEntity);
        DialogService.Close();
    }
}