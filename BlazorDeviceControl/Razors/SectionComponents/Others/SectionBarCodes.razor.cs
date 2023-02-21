// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.BarCodes;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionBarCodes : RazorComponentSectionBase<BarCodeModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionBarCodes() : base()
	{
        ButtonSettings = new(false, true, true, true, false, false, false);
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlSectionCast = DataContext.GetListNotNullable<BarCodeModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
		});
	}

	#endregion
}
