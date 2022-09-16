// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using DataCore.CssStyles;

namespace BlazorCore.Razors;

public class RazorPageSectionBase<TItems, TItemFilter> : RazorPageBase where TItems : SqlTableBase, new() where TItemFilter : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	[Parameter] public CssStyleRadzenColumnModel CssStyleRadzenColumn { get; set; }
	[Parameter] public RenderFragment<TItems>? Template { get; set; }

	protected List<TItems> ItemsCast
	{
		get => Items is null ? new() : Items.Select(x => (TItems)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	protected TItemFilter ItemFilterCast { get => Item is null ? new() : (TItemFilter)Item; set => Item = value; }
    protected List<TItemFilter> ItemsFilterCast
    {
        get => Items is null ? new() : Items.Select(x => (TItemFilter)x).ToList();
        set => Items = !value.Any() ? null : new(value);
    }
	protected string ItemsCountResult => $"{LocaleCore.Strings.ItemsCount}: {ItemsCast.Count:### ### ###}";

	public RazorPageSectionBase()
	{
		ItemsCast = new();
		CssStyleRadzenColumn = new("5%");
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				//
			}
		});
	}

	#endregion
}
