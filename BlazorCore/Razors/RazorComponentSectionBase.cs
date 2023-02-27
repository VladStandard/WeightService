// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using DataCore.CssStyles;
using DataCore.Sql.TableScaleModels.PlusScales;
using BlazorCore.Settings;
using BlazorCore.Utils;
using Radzen;
using Microsoft.JSInterop;

namespace BlazorCore.Razors;

public class RazorComponentSectionBase<TItem, TItemFilter> : RazorComponentBase 
	where TItem : SqlTableBase, new() where TItemFilter : SqlTableBase, new() 
{
	#region Public and private fields, properties, constructor

    #region Parameters

    [Parameter] public CssStyleRadzenColumnModel CssStyleRadzenColumn { get; set; }
    [Parameter] public SqlCrudConfigModel SqlCrudConfigSection { get; set; }
    [Parameter]  public ButtonSettingsModel? ButtonSettings { get; set; }

    #endregion
    public IList<TItem>? SelectedRow { get; set; }
	protected List<TItemFilter> SqlSectionFilterCast { get; set; }
	protected string SqlListCountResult => $"{LocaleCore.Strings.ItemsCount}: {SqlSectionCast.Count:### ### ###}";
	protected TItemFilter SqlItemFilterCast
	{
		get => SqlItemFilter is null ? new() : (TItemFilter)SqlItemFilter;
		set => SqlItemFilter = value;
	}
	protected List<TItem> SqlSectionCast
	{
		get => SqlSection is null ? new() : SqlSection.Select(x => (TItem)x).ToList();
		set => SqlSection = !value.Any() ? null : new(value);
	}
	protected List<TItem> SqlSectionChangedCast
	{
		get => SqlSectionOnTable is null ? new() : SqlSectionOnTable.Select(x => (TItem)x).ToList();
		set => SqlSectionOnTable = !value.Any() ? null : new(value);
	}

	public RazorComponentSectionBase()
    {
	    SelectedRow = new List<TItem>();
	    CssStyleRadzenColumn = new("5%");
		SqlCrudConfigSection = SqlCrudConfigUtils.GetCrudConfigSection(false);
		SqlCrudConfigSection.IsGuiShowItemsCount = true;
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = false;

        ButtonSettings = new(true, true, true, true, true, false, false);
    }

	#endregion

	protected void ClearSelection()
	{
		SqlItem = null;
		SelectedRow = null;
	}

	protected override void OnChangeAdditional()
	{
		// TODO: fixed me, temp usage
		ClearSelection();
	}

	protected void AutoShowFilterOnlyTopSetup()
	{
		SqlCrudConfigSection.IsGuiShowFilterOnlyTop = (SqlSectionCast.Count >= DataAccess.JsonSettings.Local.SelectTopRowsCount);
	}

	protected void RowRender(RowRenderEventArgs<TItem> args)
	{
		//if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		//if (args.Data is AccessModel access)
		//{
		//	args.Attributes.Add("class", UserSettings.GetColorAccessRights((AccessRightsEnum)access.Rights));
		//}
	}

	protected async Task RowClick(TItem item)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		await SqlItemSetAsync(item);

		RunActionsSafe(string.Empty, () =>
		{
			SqlItemOnTable = item;
			switch (typeof(TItem))
			{
				case var cls when cls == typeof(PluScaleModel):
					if (SqlItemOnTable is PluScaleModel pluScale)
						AddSqlItemOnTable(pluScale);
					break;
			}
		});
	}

	protected async Task SqlItemEditAsync()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SetRouteItemNavigate(SqlItem);
			OnChangeAsync();
		});
	}
	
    protected async Task SqlItemSetAsync(TItem item)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
		
		SelectedRow = new List<TItem>() { item };

		RunActionsSafe(string.Empty, () =>
		{
			SqlItem = SelectedRow.Last();
		});
	}
    
	protected async Task SqlItemEditAsync(TItem item)
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SetRouteItemNavigate(SqlItem);
			OnChangeAsync();
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
		if (UserSettings?.AccessRightsIsWrite == true || UserSettings?.AccessRightsIsAdmin == true)
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

    #region DataGrid Config
    
    #endregion

	#region Public and private methods

	#endregion
}