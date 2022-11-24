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
	protected List<TItem> SqlSectionCast
	{
		get => SqlSection is null ? new() : SqlSection.Select(x => (TItem)x).ToList();
		set => SqlSection = !value.Any() ? null : new(value);
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
}
