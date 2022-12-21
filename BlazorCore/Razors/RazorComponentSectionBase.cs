// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using DataCore.CssStyles;
using DataCore.Sql.TableScaleModels.PlusScales;

namespace BlazorCore.Razors;

public class RazorComponentSectionBase<TItem, TItemFilter> : RazorComponentBase 
	where TItem : SqlTableBase, new() where TItemFilter : SqlTableBase, new() 
{
	#region Public and private fields, properties, constructor

	[Parameter] public CssStyleRadzenColumnModel CssStyleRadzenColumn { get; set; }
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

	public RazorComponentSectionBase()
	{
		CssStyleRadzenColumn = new("5%");
		SqlItemFilterCast = new();
		SqlSectionFilterCast = new();
	}

	#endregion

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
						ChangeSqlItemOnTable(pluScale, pluScale.IsActive);
					break;
			}
		});
	}

	#region Public and private methods

	#endregion
}
