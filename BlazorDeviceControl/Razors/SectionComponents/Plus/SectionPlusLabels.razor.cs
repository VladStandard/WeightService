// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusLabels : RazorComponentSectionBase<PluLabelModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionPlusLabels()
	{
		SqlCrudConfigSection.IsGuiShowItemsCount = true;
		SqlCrudConfigSection.IsGuiShowFilterMarked = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
            () =>
            {
                SqlSectionCast = DataContext.GetListNotNullable<PluLabelModel>(SqlCrudConfigSection);

				ButtonSettings = new(false, true, false, true, false, false, false);
            }
		});
	}

    #endregion
}
