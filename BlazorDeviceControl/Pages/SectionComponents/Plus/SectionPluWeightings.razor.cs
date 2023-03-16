// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusWeighings;

namespace BlazorDeviceControl.Pages.SectionComponents.Plus;

public partial class SectionPluWeightings : RazorComponentSectionBase<PluWeighingModel>
{
	#region Public and private fields, properties, constructor

	public SectionPluWeightings() : base()
	{
        ButtonSettings = new(false, true, false, true, false, false, false);
    }

	#endregion

	#region Public and private methods
    
	#endregion
}
