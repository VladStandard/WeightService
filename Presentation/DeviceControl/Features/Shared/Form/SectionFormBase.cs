using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Common;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Shared.Form;

public class SectionFormBase<TItem>: ComponentBase where TItem: SqlEntityBase, new()
{
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSubmitAction { get; set; }

    private TItem SectionEntityCopy { get; set; } = new();

    protected override Task OnInitializedAsync()
    {
        SectionEntityCopy = SectionEntity.DeepClone();
        return Task.CompletedTask;
    }

    protected void ResetItem()
    {
        SectionEntity = SectionEntityCopy.DeepClone();
        StateHasChanged();
    }

    protected async Task SaveItem(TItem item)
    {
        SqlCoreHelper.Instance.Save(item);
        await OnSubmitAction.InvokeAsync();
    }

    protected async Task UpdateItem(TItem item)
    {
        if (item.IsNew) return;
        SqlCoreHelper.Instance.Update(item);
        await OnSubmitAction.InvokeAsync();
    }

    protected async Task DeleteItem()
    {
        SqlCoreHelper.Instance.Delete(SectionEntity);
        await OnSubmitAction.InvokeAsync();
    }
}