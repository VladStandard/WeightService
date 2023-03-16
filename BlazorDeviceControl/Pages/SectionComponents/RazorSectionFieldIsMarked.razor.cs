// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages.SectionComponents;

public partial class RazorSectionFieldIsMarked<TItem> : LayoutComponentBase where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor
    
    [Parameter] public SqlCrudConfigModel SqlCrudConfigSection { get; set; }
    
    public string Width { get; set; }
    
	public RazorSectionFieldIsMarked()
	{
        Width = "5%";
	}

	#endregion

	#region Public and private methods

	//

	#endregion
}
