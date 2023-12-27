using Blazorise;
using DeviceControl.Features.Shared.Modal;
using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Common;
using Ws.StorageCore.Helpers;
using Ws.StorageCore.Models;
using Ws.StorageCore.Utils;

namespace DeviceControl.Features.Shared.DataGrid;

public class SectionDataGridBase<TItem> : ComponentBase where TItem : SqlEntityBase, new()
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    [Parameter] public string SearchingSectionItemId { get; set; } = string.Empty;
    
    protected List<TItem> SectionItems { get; set; } = new();
    protected SqlCrudConfigModel SqlCrudConfigSection { get; set; } = SqlCrudConfigFactory.GetCrudAll();
    protected SectionDataGridWrapper<TItem> DataGridWrapperRef { get; set; } = null!;
    protected bool IsLoading { get; set; } = true;
    protected bool IsFirstLoading { get; set; } = true;

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

    protected async Task OpenSectionModal<T>(TItem sectionEntity) where T: SectionDialogBase<TItem>
    {
        await ModalService.Show<T>(p =>
        {
            p.Add(x => x.DialogSectionEntity, sectionEntity);
            p.Add(x => x.OnDataChangedAction, new(this, ReloadGrid));
        });
    }

    protected virtual Task DeleteSqlItem(TItem item)
    {
        SqlCoreHelper.Instance.Delete(item);
        return Task.CompletedTask;
    }

    protected async Task ReloadGrid() => await DataGridWrapperRef.ReloadData();
    
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

// namespace DeviceControl.Components.Common;
//
// public class SectionBase<TItem> : RazorComponentBase where TItem : SqlEntityBase, new()
// {
//     #region Public and private fields, properties, constructor
//     
//     private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
//     [Inject] protected IJSRuntime JsRuntime { get; set; } = default!;
//     [Inject] protected RouteService RouteService { get; set; } = default!;
//     [Inject] private LocalStorageService LocalStorage { get; set; } = default!;
//     [Inject] protected ContextMenuService ContextMenuService { get; set; } = default!;
//     [Parameter] public SqlEntityBase? SqlItem { get; set; }
//     protected IList<TItem> SelectedRow { get; set; }
//     protected List<TItem> SqlSectionCast { get; set; }
//     private List<TItem> SqlSectionSave { get; set; }
//     private List<ContextMenuItem> ContextMenuItems { get; set; } = new();
//     protected TItem SqlItemCast => SqlItem is null ? new() : (TItem)SqlItem;
//     protected SqlCrudConfigModel SqlCrudConfigSection { get; set; }
//     protected RadzenDataGrid<TItem> DataGrid { get; set; } = new();
//     protected ButtonSettingsModel ButtonSettings { get; set; }
//     protected bool IsLoading { get; set; } = true;
//     public bool IsGuiShowFilterMarked { get; set; }
//
//     public SectionBase()
//     {
//         SelectedRow = new List<TItem>();
//         SqlSectionCast = new();
//         SqlSectionSave = new();
//
//         SqlCrudConfigSection = SqlCrudConfigFactory.GetCrudActual();
//         IsGuiShowFilterMarked = true;
//         SqlCrudConfigSection.SelectTopRowsCount = 200;
//         ButtonSettings = ButtonSettingsModel.CreateForSection();
//     }
//
//     #endregion
//
//     protected override async Task OnAfterRenderAsync(bool firstRender)
//     {
//         if (!firstRender)
//             return;
//         string? rowCount = await LocalStorage.GetItem("DefaultRowCount");
//         SqlCrudConfigSection.SelectTopRowsCount = int.TryParse(rowCount, out int parsedNumber) ? parsedNumber : 200;
//         ContextMenuItems = GetContextMenuItems();
//         GetSectionData();
//         
//         #pragma warning disable BL0005
//         DataGrid.PageSize = 15; 
//         #pragma warning restore BL0005
//     }
//
//     protected void SqlItemSet(TItem item)
//     {
//         SelectedRow = new List<TItem> { item };
//         SqlItem = SelectedRow.Last();
//     }
//     
//     protected void SetSqlSectionSave(TItem model)
//     {
//         if (!SqlSectionSave.Any(item => Equals(item.IdentityValueUid, model.IdentityValueUid)))
//             SqlSectionSave.Add(model);
//     }
//
//     private async void DeleteMarkedOrDeleted()
//     {
//         await InvokeAsync(() =>
//         {
//             SqlSectionCast.Remove(SqlItemCast);
//             SelectedRow.Remove(SqlItemCast);
//             SqlItem = null;
//             DataGrid.Reload();
//         });
//     }
//
//     #region Context Menu
//
//     protected void OnCellContextMenu(DataGridCellMouseEventArgs<TItem> args)
//     {
//         SqlItem = args.Data;
//         SelectedRow = new List<TItem>() { args.Data };
//         ContextMenuService?.Open(args, ContextMenuItems, ParseContextMenuActions);
//     }
//
//     private List<ContextMenuItem> GetContextMenuItems()
//     {
//         LocaleContextMenu locale = LocaleCore.ContextMenu;
//         List<ContextMenuItem> contextMenuItems = new()
//         {
//             new() { Text = locale.Open, Value = ContextMenuActionEnum.Open },
//             new() { Text = locale.OpenNewTab, Value = ContextMenuActionEnum.OpenNewTab },
//         };
//
//         if (User?.IsInRole(UserAccessStr.Write) != true)
//             return contextMenuItems;
//
//         if (ButtonSettings.IsShowMark)
//             contextMenuItems.Add(new() { Text = locale.Mark, Value = ContextMenuActionEnum.Mark });
//         if (ButtonSettings.IsShowDelete)
//             contextMenuItems.Add(new() { Text = locale.Delete, Value = ContextMenuActionEnum.Delete });
//
//         return contextMenuItems;
//     }
//
//     private void ParseContextMenuActions(MenuItemEventArgs e)
//     {
//         ContextMenuActionEnum menuAction = (ContextMenuActionEnum)e.Value;
//         Func<Task> action = menuAction switch
//         {
//             ContextMenuActionEnum.OpenNewTab => SqlItemOpenNewTabAsync,
//             ContextMenuActionEnum.Open => SqlItemOpenAsync,
//             ContextMenuActionEnum.Mark => SqlItemMarkAsync,
//             ContextMenuActionEnum.Delete => SqlItemDeleteAsync,
//             _ => throw new NotImplementedException()
//         };
//
//         InvokeAsync(async () =>
//         {
//             await action();
//             ContextMenuService.Close();
//         });
//     }
//
//     #endregion
//
//     #region Load Data
//
//     protected void GetSectionData(bool reload = true)
//     {
//         if (!reload)
//             GetDataWithoutReload();
//         else
//             GetDataWithReload();
//     }
//
//     private void GetDataWithoutReload()
//     {
//         int selectCount = SqlCrudConfigSection.SelectTopRowsCount;
//         if ((SqlCrudConfigSection.OldTopRowsCount > selectCount && selectCount != 0) ||
//             SqlCrudConfigSection.OldTopRowsCount == 0)
//         {
//             SqlSectionCast = SqlSectionCast.Take(SqlCrudConfigSection.SelectTopRowsCount).ToList();
//             DataGrid.Reload();
//             return;
//         }
//         if (SqlSectionCast.Count < SqlCrudConfigSection.OldTopRowsCount)
//             return;
//         GetDataWithReload();
//     }
//
//     private void GetDataWithReload()
//     {
//         RunAction(string.Empty, SetSqlSectionCast);
//         IsLoading = false;
//         StateHasChanged();
//     }
//
//     protected virtual void SetSqlSectionCast()
//     {
//         throw new NotImplementedException();
//     }
//
//     #endregion
//
//     #region Auth methods
//
//     [Authorize(Roles = UserAccessStr.Write)]
//     protected async Task OnSqlSectionSaveAsync()
//     {
//         await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//         RunActionsWithQuestion(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogQuestion, () =>
//         {
//             foreach (TItem item in SqlSectionSave)
//                 SqlCore.Update(item);
//             SqlSectionSave.Clear();
//         });
//     }
//
//     [Authorize(Roles = UserAccessStr.Write)]
//     protected async virtual Task SqlItemDeleteAsync()
//     {
//         await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//
//         if (SqlItem is null) return;
//
//         RunActionsWithQuestion(LocaleCore.Table.TableDelete, LocaleCore.Dialog.DialogQuestion, () =>
//         {
//             SqlCore.Delete(SqlItem);
//             DeleteMarkedOrDeleted();
//         });
//     }
//
//     [Authorize(Roles = UserAccessStr.Write)]
//     protected async Task SqlItemMarkAsync()
//     {
//         await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//         if (SqlItem is null) return;
//
//         RunActionsWithQuestion(LocaleCore.Table.TableMark, LocaleCore.Dialog.DialogQuestion, () =>
//         {
//             SqlCore.Mark(SqlItem);
//             DeleteMarkedOrDeleted();
//         });
//     }
//
//     [Authorize(Roles = UserAccessStr.Write)]
//     protected async virtual Task SqlItemNewAsync()
//     {
//         await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//         RunActionsWithQuestion(LocaleCore.Table.TableNew, LocaleCore.Dialog.DialogQuestion, () =>
//         {
//             SqlItem = SqlItemNewEmpty<TItem>();
//             RouteService.NavigateItemRoute(SqlItemCast);
//         });
//     }
//
//     [Authorize(Roles = UserAccessStr.Read)]
//     protected async virtual Task SqlItemOpenAsync()
//     {
//         await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//         RouteService.NavigateItemRoute(SqlItemCast);
//     }
//
//     [Authorize(Roles = UserAccessStr.Read)]
//     protected async virtual Task SqlItemOpenNewTabAsync()
//     {
//         await JsRuntime.InvokeAsync<string>("open", RouteService.GetItemRoute(SqlItemCast), "_blank");
//     }
//
//     #endregion
// }
