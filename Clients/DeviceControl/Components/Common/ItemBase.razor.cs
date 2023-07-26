// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Services;

namespace DeviceControl.Components.Common;

public class ItemBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
    [Inject] protected JsService JsService { get; set; }
    [Inject] protected RouteService RouteService { get; set; }
    [Parameter] public Guid Uid { get; set; }
    [Parameter] public long Id { get; set; }
    protected WsSqlTableBase? SqlItem { get; set; }

    #region Public and private fields, properties, constructor

    protected ButtonSettingsModel ButtonSettings { get; set; }

    protected TItem SqlItemCast
    {
        get => SqlItem is null ? new() : (TItem)SqlItem;
        set => SqlItem = value;
    }

    public ItemBase()
    {
        ButtonSettings = ButtonSettingsModel.CreateForItem();
    }

    #endregion

    #region Public and private methods
    
    protected async void CopyTextToClipboard(string textToCopy)
    {
        await JsService.CopyTextToClipboard(textToCopy);
    }

    protected async Task RedirectBackAsync()
    {
        await JsService.RedirectBack();
    }

    protected async Task SqlItemSaveAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (!ValidateItemBeforeSave()) return;
        RunActionsWithQuestion(WsLocaleCore.Table.TableSave, WsLocaleCore.Dialog.DialogQuestion, ItemSave);
        
        RouteService.NavigateSectionRoute(SqlItemCast);
    }

    protected virtual bool ValidateItemBeforeSave()
    {
        return SqlItemValidateWithMsg(SqlItem, !(SqlItem?.IsNew ?? false));
    }
    
    protected virtual void ItemSave()
    {
        SqlItemSave(SqlItemCast);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            GetItemData();
        base.OnAfterRender(firstRender);
    }

    protected void GetItemData()
    {
        RunAction(string.Empty, SetSqlItemCast);
        StateHasChanged();
    }

    protected virtual void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.SqlCore.GetItemNotNullable<TItem>(SqlItemCast.Identity);

        if (!SqlItemCast.IsNew)
            return;
        SqlItemCast = SqlItemNewEmpty<TItem>();
    }

    #endregion
}