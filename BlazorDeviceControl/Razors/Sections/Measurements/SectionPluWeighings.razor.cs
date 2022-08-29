// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace BlazorDeviceControl.Razors.Sections.Measurements;

public partial class SectionPluWeighings : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	private List<PluWeighingEntity> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (PluWeighingEntity)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusWeighings);
		ItemsCast = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		RunActions(new()
		{
			() =>
			{
				ItemsCast = AppSettings.DataAccess.Crud.GetItemsListNotNull<PluWeighingEntity>(IsShowMarked, IsShowOnlyTop);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}