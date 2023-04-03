// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusLabels;

namespace BlazorDeviceControl.Pages.SectionComponents.Plus;

public sealed partial class SectionPlusLabels : RazorComponentSectionBase<PluLabelModel>
{
	#region Public and private fields, properties, constructor
    
    public SectionPlusLabels() :base()
	{
        ButtonSettings = new(false, true, false, true, false, false, false);
    }

	#endregion

	#region Public and private methods

    #endregion
}
