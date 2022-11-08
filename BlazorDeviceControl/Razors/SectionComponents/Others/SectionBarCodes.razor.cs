// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionBarCodes : RazorComponentSectionBase<BarCodeModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionBarCodes()
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
				SqlSectionCast = DataContext.GetListNotNull<BarCodeModel>(SqlCrudConfigSection);

				ButtonSettings = new(false, true, true, true, false, false, false);
			}
		});
	}

	#endregion
}
