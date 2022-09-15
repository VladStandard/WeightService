// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;

namespace BlazorCore.Razors;

public class RazorPageItemBase<T> : RazorPageBase where T : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	protected T ItemCast { get => Item is null ? new() : (T)Item; set => Item = value; }
	[Parameter] public CssStyleTableHeadModel CssTableStyleHead { get; set; }
	[Parameter] public CssStyleTableBodyModel CssTableStyleBody { get; set; }

	public RazorPageItemBase()
	{
		ItemCast = new();
		CssTableStyleHead = new();
		CssTableStyleBody = new();
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
