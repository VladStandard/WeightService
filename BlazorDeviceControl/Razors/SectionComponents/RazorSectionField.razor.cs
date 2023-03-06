// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.SectionComponents;

public partial class RazorSectionField<TItem>: LayoutComponentBase where TItem : SqlTableBase, new()
{
    #region Public and private fields, properties, constructor
    
    [Parameter] public string Width { get; set; }
    [Parameter] public RazorFieldConfigModel RazorFieldConfig { get; set; }
    
    public RazorSectionField()
    {
        Width = "20%";
	}

	#endregion

	#region Public and private methods

	//

	#endregion
}
