// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Services;
using Microsoft.JSInterop;
using WsBlazorCore.Settings;

namespace DeviceControl.Components.Item;

public class ItemBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
    [Inject] protected IJSRuntime? JsRuntime { get; set; }
    [Inject] protected RouteService RouteService { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public Guid Uid { get; set; }
    [Parameter] public long Id { get; set; }
    
    #region Public and private fields, properties, constructor

	protected TItem SqlItemCast
	{
		get => SqlItem is null ? new() : (TItem)SqlItem;
		set => SqlItem = value;
	} 
    protected ButtonSettingsModel ButtonSettings { get; set; }

    public ItemBase()
    {
        Title = string.Empty;
        ButtonSettings = ButtonSettingsModel.CreateForItem();
    }

	#endregion

	#region Public and private methods
    protected async void CopyToClipboard(string textToCopy)
    {
        if (JsRuntime != null)
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", textToCopy);
    }

    protected virtual void SqlItemSaveAdditional() {}
    
    protected async Task SqlItemCancelAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RouteService.NavigateSectionRoute(SqlItemCast);
    }

        protected async Task SqlItemSaveAsync()
    {
        // TODO: fix this
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (SqlItem is null)
        {
            await ShowDialog(LocaleCore.Sql.SqlItemIsNotSelect, LocaleCore.Sql.SqlItemDoSelect).ConfigureAwait(true);
            return;
        }

        RunActionsWithQeustion(LocaleCore.Table.TableSave, GetQuestionAdd(), () =>
        {
            SqlItemSave(SqlItem);
            SqlItemSaveAdditional();
            RouteService.NavigateSectionRoute(SqlItemCast);
        });
    }


    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            base.OnAfterRender(firstRender);
            return;
        }
        GetItemData();
    }
    
    protected void GetItemData()
    {
        RunActionsSafe(string.Empty, SetSqlItemCast);
        StateHasChanged();
    }
    
    protected virtual void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullableByUid<TItem>(Uid);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<TItem>();
    }
    
	#endregion
}
