// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages.ItemComponents;

/// <summary>
/// Actions save.
/// </summary>
public partial class RazorItemActions<TItem> : RazorComponentItemBase<TItem> where TItem : WsSqlTableBase, new()
{
	#region Public and private fields, properties, constructor

    [Parameter] public EventCallback OnItemSave { get; set; }
    
    [Parameter] public EventCallback OnItemCancel { get; set; }
    
	#endregion

	#region Public and private methods

	//

	#endregion
}
