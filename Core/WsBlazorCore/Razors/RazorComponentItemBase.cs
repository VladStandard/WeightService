// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;
using BlazorCore.Settings;
using Microsoft.AspNetCore.Components;
using WsStorageCore.Tables;

namespace BlazorCore.Razors;

public class RazorComponentItemBase<TItem> : RazorComponentBase where TItem : WsSqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	protected TItem SqlItemCast
	{
		get => SqlItem is null ? new() : (TItem)SqlItem;
		set => SqlItem = value;
	}
    
    [Parameter] public ButtonSettingsModel? ButtonSettings { get; set; }
    [Parameter] public CssStyleTableHeadModel CssTableStyleHead { get; set; }
    
    public RazorComponentItemBase()
	{
        CssTableStyleHead = new();
        ButtonSettings = new(false, false, false, false, false, true, true);
    }

	#endregion

	#region Public and private methods
	
	#endregion
}
