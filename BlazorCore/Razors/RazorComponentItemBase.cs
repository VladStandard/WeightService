// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Settings;
using DataCore.CssStyles;
using Microsoft.AspNetCore.Components;

namespace BlazorCore.Razors;

public class RazorComponentItemBase<TItem> : RazorComponentBase where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	protected TItem SqlItemCast
	{
		get => SqlItem is null ? new() : (TItem)SqlItem;
		set => SqlItem = value;
	}

	protected TItem SqlItemChangedCast
	{
		get => SqlItem is null ? new() : (TItem)SqlItem;
		set => SqlItemOnTable = value;
	}

    [Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }
    [Parameter] public CssStyleTableHeadModel CssTableStyleHead { get; set; }
	[Parameter] public CssStyleTableBodyModel CssTableStyleBody { get; set; }

    public RazorComponentItemBase()
	{
        CssTableStyleHead = new();
		CssTableStyleBody = new();
        ButtonSettings = new(false, false, false, false, false, true, true);
    }

	#endregion

	#region Public and private methods
	
	#endregion
}