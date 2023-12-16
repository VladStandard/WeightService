using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Common;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Shared.Form;

public class SectionFormBase<TItem>: ComponentBase where TItem: SqlEntityBase, new()
{
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSaveAction { get; set; }
    
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;

    protected async Task SaveItem(TItem item)
    {
        SqlCore.Save(item);
        await OnSaveAction.InvokeAsync();
    }

    protected async Task UpdateItem(TItem item)
    {
        if (item.IsNew) return;
        SqlCore.Update(item);
        await OnSaveAction.InvokeAsync();
    }
}