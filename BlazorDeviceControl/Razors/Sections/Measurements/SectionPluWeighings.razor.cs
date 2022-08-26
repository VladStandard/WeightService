// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Measurements;

public partial class SectionPluWeighings : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	private List<PluWeighingEntity> ItemsCast => Items == null ? new() : Items.Select(x => (PluWeighingEntity)x).ToList();

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusWeighings);
		Items = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		RunActions(new()
		{
			() =>
			{
				Items = AppSettings.DataAccess.Crud.GetEntitiesNotNull<PluWeighingEntity>(
					IsShowMarkedItems, IsSelectTopRows, null)
					//new() { Name = DbField.ProductDt, Direction = DbOrderDirection.Desc })
					.ToList<BaseEntity>();
				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}