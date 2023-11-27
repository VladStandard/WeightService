using Microsoft.AspNetCore.Components;
using Radzen;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogLineSelect : ComponentBase
{
    [Inject] private DialogService DialogService { get; set; }
    [Inject] private LineContext LineContext { get; set; }

    private IEnumerable<SqlLineEntity> GetLineEntities() => LineContext.LineEntities;

    private void OnRowSelect(SqlLineEntity sqlLineEntity)
    {
        LineContext.ChangeLine(sqlLineEntity);
        DialogService.Close();
    }
}