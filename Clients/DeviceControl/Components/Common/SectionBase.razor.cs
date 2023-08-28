namespace DeviceControl.Components.Common;

public class SectionBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    [Inject] protected IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] protected WsJsService JsService { get; private set; } = default!;
    [Inject] protected WsRouteService RouteService { get; set; } = default!;
    [Inject] private WsLocalStorageService LocalStorage { get; set; } = default!;
    [Inject] protected ContextMenuService ContextMenuService { get; set; } = default!;
    [Parameter] public WsSqlTableBase? SqlItem { get; set; }
    protected IList<TItem> SelectedRow { get; set; }
    protected List<TItem> SqlSectionCast { get; set; }
    private List<TItem> SqlSectionSave { get; set; }
    private List<ContextMenuItem> ContextMenuItems { get; set; } = new();
    protected TItem SqlItemCast => SqlItem is null ? new() : (TItem)SqlItem;
    protected WsSqlCrudConfigModel SqlCrudConfigSection { get; set; }
    protected RadzenDataGrid<TItem> DataGrid { get; set; } = new();
    protected ButtonSettingsModel ButtonSettings { get; set; }
    protected bool IsLoading { get; set; } = true;
    public bool IsGuiShowFilterMarked { get; set; }

    public SectionBase()
    {
        SelectedRow = new List<TItem>();
        SqlSectionCast = new();
        SqlSectionSave = new();

        SqlCrudConfigSection = WsSqlCrudConfigFactory.GetCrudActual();
        IsGuiShowFilterMarked = true;
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
        ContextMenuItems = GetContextMenuItems();
        GetSectionData();
    }

    protected void SqlItemSet(TItem item)
    {
        SelectedRow = new List<TItem> { item };
        SqlItem = SelectedRow.Last();
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

    #region Context Menu

    protected void OnCellContextMenu(DataGridCellMouseEventArgs<TItem> args)
    {
        SqlItem = args.Data;
        SelectedRow = new List<TItem>() { args.Data };
        ContextMenuService?.Open(args, ContextMenuItems, ParseContextMenuActions);
    }

    private List<ContextMenuItem> GetContextMenuItems()
    {
        WsLocaleContextMenu locale = WsLocaleCore.ContextMenu;
        List<ContextMenuItem> contextMenuItems = new()
        {
            new() { Text = locale.Open, Value = WsContextMenuActionEnum.Open },
            new() { Text = locale.OpenNewTab, Value = WsContextMenuActionEnum.OpenNewTab },
        };

        if (User?.IsInRole(WsUserAccessStr.Write) != true)
            return contextMenuItems;

        if (ButtonSettings.IsShowMark)
            contextMenuItems.Add(new() { Text = locale.Mark, Value = WsContextMenuActionEnum.Mark });
        if (ButtonSettings.IsShowDelete)
            contextMenuItems.Add(new() { Text = locale.Delete, Value = WsContextMenuActionEnum.Delete });

        return contextMenuItems;
    }

    private void ParseContextMenuActions(MenuItemEventArgs e)
    {
        WsContextMenuActionEnum menuAction = (WsContextMenuActionEnum)e.Value;
        Func<Task> action = menuAction switch
        {
            WsContextMenuActionEnum.OpenNewTab => SqlItemOpenNewTabAsync,
            WsContextMenuActionEnum.Open => SqlItemOpenAsync,
            WsContextMenuActionEnum.Mark => SqlItemMarkAsync,
            WsContextMenuActionEnum.Delete => SqlItemDeleteAsync,
            _ => throw new NotImplementedException()
        };

        InvokeAsync(async () =>
        {
            await action();
            ContextMenuService.Close();
        });
    }

    #endregion

    #region Load Data

    protected void GetSectionData(bool reload = true)
    {
        if (!reload)
            GetDataWithoutReload();
        else
            GetDataWithReload();
    }

    private void GetDataWithoutReload()
    {
        int selectCount = SqlCrudConfigSection.SelectTopRowsCount;
        if ((SqlCrudConfigSection.OldTopRowsCount > selectCount && selectCount != 0) ||
            SqlCrudConfigSection.OldTopRowsCount == 0)
        {
            SqlSectionCast = SqlSectionCast.Take(SqlCrudConfigSection.SelectTopRowsCount).ToList();
            DataGrid.Reload();
            return;
        }
        if (SqlSectionCast.Count < SqlCrudConfigSection.OldTopRowsCount)
            return;
        GetDataWithReload();
    }

    private void GetDataWithReload()
    {
        RunAction(string.Empty, SetSqlSectionCast);
        IsLoading = false;
        StateHasChanged();
    }

    protected virtual void SetSqlSectionCast()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Auth methods

    [Authorize(Roles = WsUserAccessStr.Write)]
    protected async Task OnSqlSectionSaveAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsWithQuestion(WsLocaleCore.Table.TableSave, WsLocaleCore.Dialog.DialogQuestion, () =>
        {
            foreach (TItem item in SqlSectionSave)
                ContextManager.SqlCore.Update(item);
            SqlSectionSave.Clear();
        });
    }

    [Authorize(Roles = WsUserAccessStr.Write)]
    protected async Task SqlItemDeleteAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (SqlItem is null) return;

        RunActionsWithQuestion(WsLocaleCore.Table.TableDelete, WsLocaleCore.Dialog.DialogQuestion, () =>
        {
            ContextManager.SqlCore.Delete(SqlItem);
            DeleteMarkedOrDeleted();
        });
    }

    [Authorize(Roles = WsUserAccessStr.Write)]
    protected async Task SqlItemMarkAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        if (SqlItem is null) return;

        RunActionsWithQuestion(WsLocaleCore.Table.TableMark, WsLocaleCore.Dialog.DialogQuestion, () =>
        {
            ContextManager.SqlCore.Mark(SqlItem);
            DeleteMarkedOrDeleted();
        });
    }

    [Authorize(Roles = WsUserAccessStr.Write)]
    protected async Task SqlItemNewAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RunActionsWithQuestion(WsLocaleCore.Table.TableNew, WsLocaleCore.Dialog.DialogQuestion, () =>
        {
            SqlItem = SqlItemNewEmpty<TItem>();
            RouteService.NavigateItemRoute(SqlItemCast);
        });
    }

    [Authorize(Roles = WsUserAccessStr.Read)]
    protected async Task SqlItemOpenAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RouteService.NavigateItemRoute(SqlItemCast);
    }

    [Authorize(Roles = WsUserAccessStr.Read)]
    private async Task SqlItemOpenNewTabAsync()
    {
        await JsRuntime.InvokeAsync<string>("open", WsRouteService.GetItemRoute(SqlItemCast), "_blank");
    }

    #endregion
}
