// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionBarCodes : RazorPageSectionBase<BarCodeModel>
{
	#region Public and private fields, properties, constructor

	public SectionBarCodes()
	{
		IsShowMarkedFilter = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				ItemsCast = AppSettings.DataAccess.GetListBarCodes(IsShowMarked, IsShowOnlyTop);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}
