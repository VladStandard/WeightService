// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages.SectionComponents;

public partial class SectionReload<TItem> : RazorComponentSectionBase<TItem>
	where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor
    
    private string SqlListCountResult => $"{LocaleCore.Strings.ItemsCount}: {SectionCount:### ### ###}";
    [Parameter] public EventCallback OnSectionUpdate { get; set; }
    [Parameter] public int SectionCount { get; set; }
    
	#endregion

	#region Public and private methods

    protected override void OnAfterRender(bool firstRender)
    {
        
    }

    #endregion
}
