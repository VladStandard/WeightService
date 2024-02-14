using Blazorise;
using DeviceControl.Features.Sections.Shared.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Ws.Database.Core.Helpers;
using Ws.Domain.Abstractions.Entities.Common;

namespace DeviceControl.Features.Sections.Shared.DataGrid;

public class SectionDataGridBase<TItem> : ComponentBase where TItem : EntityBase, new()
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter] public string SearchingSectionItemId { get; set; } = string.Empty;

    protected IEnumerable<TItem> SectionItems { get; set; } = [];
    protected SectionDataGridWrapper<TItem> DataGridWrapperRef { get; set; } = null!;
    private bool IsFirstLoading { get; set; } = true;

    protected virtual IEnumerable<TItem> SetSqlSectionCast()
    {
        throw new NotImplementedException();
    }

    protected virtual IEnumerable<TItem> SetSqlSearchingCast()
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


    protected async Task OpenSectionModal<T>(TItem sectionEntity) where T : SectionDialogBase<TItem>
    {
        await ModalService.Show<T>(p => {
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
        SqlCoreHelper.Delete(item);
        return Task.CompletedTask;
    }

    private async Task ReloadGrid() => await DataGridWrapperRef.ReloadData();

    public async Task GetSectionData()
    {
        if (!string.IsNullOrEmpty(SearchingSectionItemId) && IsFirstLoading)
        {
            SectionItems = await Task.Run(SetSqlSearchingCast);
            IsFirstLoading = false;
            SectionItems = SectionItems.Where(i => !i.IsNew).ToList();
            if (SectionItems.Any()) await OpenDataGridEntityModal(SectionItems.First());
            return;
        }

        SectionItems = await Task.Run(SetSqlSectionCast);
    }
}