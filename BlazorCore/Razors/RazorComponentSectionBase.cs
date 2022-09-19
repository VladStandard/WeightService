// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using DataCore.CssStyles;

namespace BlazorCore.Razors;

public class RazorComponentSectionBase<TItem> : RazorComponentBase where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	[Parameter] public CssStyleRadzenColumnModel CssStyleRadzenColumn { get; set; }
	//[Parameter] public RenderFragment<TItem>? Template { get; set; }
	protected List<TItem> SqlItemsCast
	{
		get => SqlItems is null ? new() : SqlItems.Select(x => (TItem)x).ToList();
		set => SqlItems = !value.Any() ? null : new(value);
	}
	protected string ItemsCountResult => $"{LocaleCore.Strings.ItemsCount}: {SqlItemsCast.Count:### ### ###}";

	public RazorComponentSectionBase()
	{
		SqlItemsCast = new();
		CssStyleRadzenColumn = new("5%");
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
