// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Services;
using WsBlazorCore.Settings;

namespace DeviceControl.Components.Common;

public class ItemBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
    [Inject] protected JsService JsService { get; set; }
    [Inject] protected RouteService RouteService { get; set; }
    [Parameter] public Guid Uid { get; set; }
    [Parameter] public long Id { get; set; }
    protected WsSqlTableBase? SqlItem { get; set; }

    #region Public and private fields, properties, constructor

    protected TItem SqlItemCast
    {
        get => SqlItem is null ? new() : (TItem)SqlItem;
        set => SqlItem = value;
    }
    protected ButtonSettingsModel ButtonSettings { get; set; }

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

    protected virtual void SqlItemSaveAdditional() { }

    protected async Task RedirectBackAsync()
    {
        bool isRedirected = await JsService.RedirectBackIfNotHomePage();
        if (!isRedirected)
            await RedirectToSectionAsync();
    }
    
    protected async Task RedirectToSectionAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RouteService.NavigateSectionRoute(SqlItemCast);
    }

    protected async Task SqlItemSaveAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        if (!SqlItemValidate(SqlItem)) return;
        
        RunActionsWithQuestion(WsLocaleCore.Table.TableSave, WsLocaleCore.Dialog.DialogQuestion, () =>
        {
            SqlItemSave(SqlItem);
            SqlItemSaveAdditional();
            RouteService.NavigateSectionRoute(SqlItemCast);
        });
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
        SqlItemCast = ContextManager.SqlCore.GetItemNotNullableByUid<TItem>(Uid);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNewEmpty<TItem>();
    }

    #endregion
}