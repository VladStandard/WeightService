// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPluWeighings : RazorBase
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

		Table = new TableScaleModel(ProjectsEnums.TableScale.PlusWeighings);
		ItemsCast = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
			() =>
			{
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, IsShowMarked, IsShowOnlyTop);
				ItemsCast = AppSettings.DataAccess.Crud.GetList<PluWeighingModel>(sqlCrudConfig);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}