// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusLabels;

namespace BlazorDeviceControl.Pages.Menu.Operations.SectionPlusLabels;

public sealed partial class PlusLabels : RazorComponentSectionBase<PluLabelModel>
{
	#region Public and private fields, properties, constructor
    
    public PlusLabels() :base()
	{
        ButtonSettings = new(false, true, false, true, false, false, false);
    }

	#endregion

	#region Public and private methods

    #endregion
}
