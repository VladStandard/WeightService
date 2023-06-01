// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using Radzen.Blazor;
using WsBlazorCore.Settings;

namespace DeviceControl.Components.Section;

public partial class SectionBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor
    
    [Inject] private LocalStorageService LocalStorage { get; set; }
    [Inject] protected ContextMenuService? ContextMenuService { get; set; }
    
    protected IList<TItem> SelectedRow { get; set; }
    protected List<TItem> SqlSectionCast { get; set; }
    protected List<TItem> SqlSectionSave { get; set; }
    protected TItem SqlItemCast => SqlItem is null ? new() : (TItem)SqlItem;
    protected WsSqlCrudConfigModel SqlCrudConfigSection { get; set; }
    protected RadzenDataGrid<TItem> DataGrid { get; set; }
    protected ButtonSettingsModel ButtonSettings { get; set; }
    protected bool IsLoading { get; set; } = true;

    public SectionBase()
    {
        SqlSectionCast = new List<TItem>();
        SelectedRow = new List<TItem>();
        SqlSectionSave = new List<TItem>();
        
        SqlCrudConfigSection = WsSqlCrudConfigUtils.GetCrudConfigSection(WsSqlIsMarked.ShowOnlyActual);
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
        SqlCrudConfigSection.SelectTopRowsCount = 200;

        ButtonSettings = ButtonSettingsModel.CreateForSection();
    }

    #endregion
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) 
            return;
        string? rowCount = await LocalStorage.GetItem("DefaultRowCount");
        SqlCrudConfigSection.SelectTopRowsCount = int.TryParse(rowCount, out int parsedNumber) ? parsedNumber : 200;
        GetSectionData();
    }
    
    protected void GetSectionData()
    {
        RunActionsSafe(string.Empty, SetSqlSectionCast);
        IsLoading = false;
        StateHasChanged();
    }

    protected virtual void SetSqlSectionCast()
    {
        SqlSectionCast = ContextManager.ContextList.GetListNotNullable<TItem>(SqlCrudConfigSection);
    }
    
    protected void SqlItemSet(TItem item)
    {
        SelectedRow.Clear();
        SelectedRow.Add(item);
        SqlItem = SelectedRow.Last();
    }
    
    protected void OnCellContextMenu(DataGridCellMouseEventArgs<TItem> args)
    {
        LocaleContextMenu locale = LocaleCore.ContextMenu;

        SelectedRow = new List<TItem>() { args.Data };
        SqlItem = args.Data;
        
        List<ContextMenuItem> contextMenuItems = new()
        {
            new() { Text = locale.Open, Value = ContextMenuAction.Open },
            new() { Text = locale.OpenNewTab, Value = ContextMenuAction.OpenNewTab },
        };
        
        if (User?.IsInRole(UserAccessStr.Write) == true)
        {
            if (ButtonSettings.IsShowMark)
                contextMenuItems.Add(new() { Text = locale.Mark, Value = ContextMenuAction.Mark });
            if (ButtonSettings.IsShowDelete)
                contextMenuItems.Add(new() { Text = locale.Delete, Value = ContextMenuAction.Delete });
        }
        ContextMenuService?.Open(args, contextMenuItems, (e) => ParseContextMenuActions(e, args));
    }
	
    protected void ParseContextMenuActions(MenuItemEventArgs e, DataGridCellMouseEventArgs<TItem> args) 
    {
        InvokeAsync(async () =>
        {
            switch ((ContextMenuAction)e.Value)
            {
                case ContextMenuAction.OpenNewTab:
                    await SqlItemOpenNewTabAsync();
                    break;
                case ContextMenuAction.Open:
                    await SqlItemOpenAsync();
                    break;
                case ContextMenuAction.Mark:
                    await SqlItemMarkAsync();
                    break;
                case ContextMenuAction.Delete:
                    await SqlItemDeleteAsync();
                    break;
            }

            ContextMenuService?.Close();
        });
    }

    protected void SetSqlSectionSave(TItem model)
    {
        if (!SqlSectionSave.Any(item => Equals(item.IdentityValueUid, model.IdentityValueUid)))
            SqlSectionSave.Add(model);
    }
    
    private async void DeleteMarkedOrDeleted()
    {
        await InvokeAsync(() =>
        {
            SqlSectionCast.Remove(SqlItemCast);
            SelectedRow.Remove(SqlItemCast);
            SqlItem = null; 
            DataGrid.Reload();
        });
    }

    #region Auth methods

    [Authorize(Roles = UserAccessStr.Write)]
    protected async Task OnSqlSectionSaveAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsWithQeustion(LocaleCore.Table.TableSave, GetQuestionAdd(), () =>
        {
            foreach (TItem item in SqlSectionSave)
                ContextManager.AccessManager.AccessItem.Update(item);
            SqlSectionSave.Clear();
        });
    }
    
    [Authorize(Roles = UserAccessStr.Write)]
    protected async Task SqlItemDeleteAsync()
    {
        if (SqlItem is null)
        {
            await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
            return;
        }
		
        RunActionsWithQeustion(LocaleCore.Table.TableDelete, GetQuestionAdd(), () =>
        {
            ContextManager.AccessManager.AccessItem.Delete(SqlItem);
            DeleteMarkedOrDeleted();
        });
    }

    [Authorize(Roles = UserAccessStr.Write)]
    protected async Task SqlItemMarkAsync()
    {
        if (SqlItem is null)
        {
            await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
            return;
        }
		
        RunActionsWithQeustion(LocaleCore.Table.TableMark, GetQuestionAdd(), () =>
        {
            ContextManager.AccessManager.AccessItem.Mark(SqlItem);
            DeleteMarkedOrDeleted();
        });
    }
    
    [Authorize(Roles = UserAccessStr.Write)]
    protected async Task SqlItemNewAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsWithQeustion(LocaleCore.Table.TableNew, GetQuestionAdd(), () =>
        {
            SqlItem = SqlItemNew<TItem>();
            SetRouteItemNavigate(SqlItem);
        });
    }
    
    [Authorize(Roles = UserAccessStr.Read)]
    protected async Task SqlItemOpenAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsSafe(string.Empty, () => { SetRouteItemNavigate(SqlItem); });
    }
    
    [Authorize(Roles = UserAccessStr.Read)]
    protected async Task SqlItemOpenNewTabAsync()
    {
        if (JsRuntime == null)
            return;
        await JsRuntime.InvokeAsync<object>("open", GetRouteItemPathForLink(SqlItemCast), "_blank");
    }
    
    #endregion
}