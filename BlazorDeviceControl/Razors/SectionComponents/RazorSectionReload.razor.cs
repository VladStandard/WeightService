// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.SectionComponents;

public partial class RazorSectionReload<TItem, TItemFilter> : RazorComponentSectionBase<TItem, TItemFilter>
	where TItem : SqlTableBase, new() where TItemFilter : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor
    
    private string SqlListCountResult => $"{LocaleCore.Strings.ItemsCount}: {SqlSectionCast.Count:### ### ###}";
    
	#endregion

	#region Public and private methods
	
	
	#endregion
}
