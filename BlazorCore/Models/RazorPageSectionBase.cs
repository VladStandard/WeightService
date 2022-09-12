// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;
using DataCore.Localizations;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorCore.Models;

public class RazorPageSectionBase<T> : RazorPageBase where T : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	[Parameter] public CssStyleRadzenColumnModel CssStyleRadzenColumn { get; set; }
	[Parameter] public string FieldName { get; set; }

	protected List<T> ItemsCast
	{
		get => Items is null ? new() : Items.Select(x => (T)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	protected T ItemFilterCast { get => Item is null ? new() : (T)Item; set => Item = value; }
	protected string ItemsCountResult => $"{LocaleCore.Strings.ItemsCount}: {ItemsCast.Count:### ### ###}";

	public RazorPageSectionBase()
	{
		ItemsCast = new();
		CssStyleRadzenColumn = new("5%");
		FieldName = string.Empty;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

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
