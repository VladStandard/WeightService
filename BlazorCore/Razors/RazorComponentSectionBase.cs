// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using DataCore.CssStyles;

namespace BlazorCore.Razors;

public class RazorComponentSectionBase<TItem, TItemFilter> : RazorComponentBase 
	where TItem : SqlTableBase, new() where TItemFilter : SqlTableBase, new() 
{
	#region Public and private fields, properties, constructor

	[Parameter] public CssStyleRadzenColumnModel CssStyleRadzenColumn { get; set; }
	protected List<TItem> SqlItemsCast
	{
		get => SqlItems is null ? new() : SqlItems.Select(x => (TItem)x).ToList();
		set => SqlItems = !value.Any() ? null : new(value);
	}
	protected TItemFilter SqlItemFilterCast
	{
		get => SqlItemFilter is null ? new(): (TItemFilter)SqlItemFilter;
		set => SqlItemFilter = value;
	}
	protected List<TItemFilter> SqlItemsFilterCast { get; set; }
	protected string ItemsCountResult => $"{LocaleCore.Strings.ItemsCount}: {SqlItemsCast.Count:### ### ###}";

	public RazorComponentSectionBase()
	{
		CssStyleRadzenColumn = new("5%");
		SqlItemFilterCast = new();
		SqlItemsFilterCast = new();
	}

	#endregion

	#region Public and private methods

	//protected override void OnParametersSet()
	//{
	//	RunActionsParametersSet(new()
	//	{
	//		() =>
	//		{
	//			//
	//		}
	//	});
	//}

	#endregion
}
