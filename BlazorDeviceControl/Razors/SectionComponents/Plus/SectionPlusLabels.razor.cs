// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusLabels : RazorComponentSectionBase<PluLabelModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionPlusLabels()
	{
		RazorComponentConfig.IsShowItemsCount = true;
		RazorComponentConfig.IsShowFilterMarked = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
            () =>
            {
                SqlItemsCast = AppSettings.DataAccess.GetListPluLabels(RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop);

				ButtonSettings = new(false, true, false, true, false, false, false);
            }
		});
	}

    #endregion
}
