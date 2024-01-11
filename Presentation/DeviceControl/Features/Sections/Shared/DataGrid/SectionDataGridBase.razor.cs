using Blazorise;
using DeviceControl.Features.Sections.Shared.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Ws.StorageCore.Common;
using Ws.StorageCore.Helpers;
using Ws.StorageCore.Models;

namespace DeviceControl.Features.Sections.Shared.DataGrid;

public class SectionDataGridBase<TItem> : ComponentBase where TItem : SqlEntityBase, new()
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    
    [Parameter] public string SearchingSectionItemId { get; set; } = string.Empty;
    
    protected IEnumerable<TItem> SectionItems { get; set; } = [];
    protected SqlCrudConfigModel SqlCrudConfigSection { get; set; } = new();
    protected SectionDataGridWrapper<TItem> DataGridWrapperRef { get; set; } = null!;
    protected bool IsLoading { get; set; } = true;
    private bool IsFirstLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        await GetSectionData();
    }

    protected virtual void SetSqlSectionCast()
    {
        throw new NotImplementedException();
    }

    protected virtual void SetSqlSearchingCast()
    {
        throw new NotImplementedException();
    }
    
    protected virtual Task OpenDataGridEntityModal(TItem item)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OpenSectionCreateForm()
    {
        return Task.CompletedTask;
    }

    protected virtual Task OpenItemInNewTab(TItem item)
    {
        return Task.CompletedTask;
    }

    protected async Task OpenLinkInNewTab(string url) =>
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    

    protected async Task OpenSectionModal<T>(TItem sectionEntity) where T: SectionDialogBase<TItem>
    {
        await ModalService.Show<T>(p =>
        {
            p.Add(x => x.DialogSectionEntity, sectionEntity);
            p.Add(x => x.OnDataChangedAction, new(this, OnModalSubmit));
        });
    }

    protected async Task OnModalSubmit()
    {
        await ReloadGrid();
        await ModalService.Hide();
    }

    protected Task DeleteSqlItem(TItem item)
    {
        SqlCoreHelper.Instance.Delete(item);
        return Task.CompletedTask;
    }

    private async Task ReloadGrid() => await DataGridWrapperRef.ReloadData();
    
    public async Task GetSectionData()
    {
        IsLoading = true;
        
        if (!string.IsNullOrEmpty(SearchingSectionItemId) && IsFirstLoading)
        {
            await Task.Run(SetSqlSearchingCast);
            IsFirstLoading = false;
            SectionItems = SectionItems.Where(i => !i.IsNew).ToList();
            if (SectionItems.Any()) await OpenDataGridEntityModal(SectionItems.First());
        }
        else
        {
            await Task.Run(SetSqlSectionCast);
        }
            
        IsLoading = false;
        StateHasChanged();
    }
}
