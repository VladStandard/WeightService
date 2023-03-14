// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.ItemComponents;

public partial class RazorItemReload<TItem> : RazorComponentItemBase<TItem> where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor
    
    [Parameter] public EventCallback OnItemUpdate { get; set; }

	#endregion

	#region Public and private methods
    
	#endregion
}
