// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using DataCore.CssStyles;
using DataCore.Sql.TableScaleModels.PlusScales;
using BlazorCore.Settings;
using Radzen;

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

    protected PagerPosition PagerPos = PagerPosition.TopAndBottom;
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
	protected TItemFilter SqlItemFilterCast
	{
		get => SqlItemFilter is null ? new(): (TItemFilter)SqlItemFilter;
		set => SqlItemFilter = value;
	}
	protected List<TItemFilter> SqlSectionFilterCast { get; set; }
	protected string SqlListCountResult => $"{LocaleCore.Strings.ItemsCount}: {SqlSectionCast.Count:### ### ###}";
	protected void AutoShowFilterOnlyTopSetup()
    {
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = (SqlSectionCast.Count >= DataAccess.JsonSettings.Local.SelectTopRowsCount);
    }

    public RazorComponentSectionBase()
    {
        CssStyleRadzenColumn = new("5%");

        SqlCrudConfigSection = SqlCrudConfigUtils.GetCrudConfigSection(false);

        SqlCrudConfigSection.IsGuiShowItemsCount = true;
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = false;

        ButtonSettings = new(true, true, true, true, true, false, false);
    }

	#endregion

	protected void RowRender<TItem>(RowRenderEventArgs<TItem> args) where TItem : SqlTableBase, new()
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
	protected async Task SqlItemSetAsync<TItem>(TItem item) where TItem : SqlTableBase, new()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SqlItem = item;
		});
	}
	protected async Task SqlItemEditAsync<TItem>(TItem item) where TItem : SqlTableBase, new()
	{
		if (UserSettings is null || !UserSettings.AccessRightsIsWrite) return;
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		RunActionsSafe(string.Empty, () =>
		{
			SetRouteItemNavigate(SqlItem);
			OnChangeAsync();
		});
	}

	#region Public and private methods

	#endregion
}