// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WsBlazorCore.Settings;
using WsStorageCore.Models;

namespace WsBlazorCore.Razors;

public class RazorComponentItemBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	protected TItem SqlItemCast
	{
		get => SqlItem is null ? new() : (TItem)SqlItem;
		set => SqlItem = value;
	}
    
    [Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }
    [Parameter] public CssStyleTableHeadModel CssTableStyleHead { get; set; }
    
    public RazorComponentItemBase()
	{
        CssTableStyleHead = new();
        ButtonSettings = new(false, false, false, false, false, true, true);
    }

	#endregion

	#region Public and private methods
    protected async void CopyToClipboard(string textToCopy)
    {
        if (JsRuntime != null)
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", textToCopy);
    }

    protected virtual void SqlItemSaveAdditional()
    {
        
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
            SetRouteSectionNavigate();
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
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullableByUid<TItem>(IdentityUid);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<TItem>();
    }
    
	#endregion
}
