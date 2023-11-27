using Microsoft.AspNetCore.Components;
using Radzen;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogPluNestingSelect : ComponentBase
{
    [Inject] private DialogService DialogService { get; set; }
    [Inject] private LineContext LineContext { get; set; }

    private IEnumerable<SqlPluNestingFkEntity> GetPluNestingsEntities() => LineContext.PluNestingEntities;
    
    private void OnRowSelect(SqlPluNestingFkEntity sqlPluNestingEntity)
    {
        LineContext.ChangePluNesting(sqlPluNestingEntity);
        DialogService.Close();
    }
}