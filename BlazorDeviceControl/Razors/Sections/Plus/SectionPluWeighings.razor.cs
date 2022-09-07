// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPluWeighings : RazorPageBase
{
	#region Public and private fields, properties, constructor

	private List<PluWeighingModel> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (PluWeighingModel)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableScaleModel(SqlTableScaleEnum.PlusWeighings);
				ItemsCast = new();
			}
		});
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, IsShowMarked, IsShowOnlyTop);
				ItemsCast = AppSettings.DataAccess.GetList<PluWeighingModel>(sqlCrudConfig);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}