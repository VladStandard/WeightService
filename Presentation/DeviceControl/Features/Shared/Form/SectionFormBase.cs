using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Common;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Shared.Form;

public class SectionFormBase<TItem>: ComponentBase where TItem: SqlEntityBase, new()
{
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSaveAction { get; set; }
    
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;

    protected async Task SaveItem()
    {
        SqlCore.Save(SectionEntity);
        await OnSaveAction.InvokeAsync();
    }

    protected async Task UpdateItem()
    {
        if (SectionEntity.IsNew) return;
        SqlCore.Update(SectionEntity);
        await OnSaveAction.InvokeAsync();
    }
}