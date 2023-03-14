// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using BlazorCore.Settings;
using BlazorCore.Utils;
using Radzen;
using Microsoft.JSInterop;

namespace BlazorCore.Razors;

public class RazorComponentSectionBase<TItem> : RazorComponentBase
    where TItem : SqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    #region Parameters

    [Parameter] public SqlCrudConfigModel SqlCrudConfigSection { get; set; }
    
    [Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }

    protected bool IsSqlSectionGet = false;
    
    #endregion

    public IList<TItem>? SelectedRow { get; set; }

    protected List<TItem> SqlSectionCast
    {
        get => SqlSection is null ? new() : SqlSection.Select(x => (TItem)x).ToList();
        set => SqlSection = !value.Any() ? null : new(value);
    }

    public RazorComponentSectionBase()
    {
        SelectedRow = new List<TItem>();
        SqlCrudConfigSection = SqlCrudConfigUtils.GetCrudConfigSection(false);
        SqlCrudConfigSection.IsGuiShowItemsCount = true;
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = false;

        ButtonSettings = new(true, true, true, true, true, false, false);
    }

    #endregion
    
    protected void AutoShowFilterOnlyTopSetup()
    {
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop =
            (SqlSectionCast.Count >= DataAccess.JsonSettings.Local.SelectTopRowsCount);
    }

    protected void RowRender(RowRenderEventArgs<TItem> args)
    {
        //if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
        //if (args.Data is AccessModel access)
        //{
        //	args.Attributes.Add("class", UserSettings.GetColorAccessRights((AccessRightsEnum)access.Rights));
        //}
    }

    protected async Task SqlItemEditAsync()
    {
        if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        RunActionsSafe(string.Empty, () => { SetRouteItemNavigate(SqlItem); });
    }

    protected async Task SqlItemSetAsync(TItem item)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        SelectedRow = new List<TItem>() { item };
        SqlItem = SelectedRow.Last();
    }
    
    protected async Task SqlItemEditAsync(TItem item)
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SetRouteItemNavigate(SqlItem);
        });
	}

	//TODO: insert into DataCore
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
		if ((UserSettings?.AccessRightsIsWrite == true || UserSettings?.AccessRightsIsAdmin == true) &&
            (ButtonSettings?.IsShowDelete == true || ButtonSettings?.IsShowMark == true))
		{
			contextMenuItems.Add(new() { Text = locale.Mark, Value = ContextMenuAction.Mark });
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
					if (JsRuntime != null)
						await JsRuntime.InvokeAsync<object>("open", 
							GetRouteItemPathForLink(args.Data), "_blank");
					break;
				case ContextMenuAction.Open:
					await SqlItemEditAsync(args.Data);
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
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            base.OnAfterRender(firstRender);
            return;
        }
        GetSectionData();
    }
    
    protected void GetSectionData()
    {
        RunActionsSafe(string.Empty, SetSqlSectionCast);
        AutoShowFilterOnlyTopSetup();
        SqlItem = null;
        SelectedRow = null;
        IsSqlSectionGet = true;
        StateHasChanged();
    }

    protected virtual void SetSqlSectionCast()
    {
        SqlSectionCast = DataContext.GetListNotNullable<TItem>(SqlCrudConfigSection);
    }
    
    
	#region Public and private methods

	#endregion
}