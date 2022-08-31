// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemWorkshop : RazorBase
{
	#region Public and private fields, properties, constructor

	private WorkShopModel ItemCast { get => Item == null ? new() : (WorkShopModel)Item; set => Item = value; }
	private List<ProductionFacilityModel> ProductionFacilities { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleModel(ProjectsEnums.TableScale.Workshops);
		ItemCast = new();
		ProductionFacilities = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
			() =>
			{
				ItemCast = AppSettings.DataAccess.Crud.GetItemByIdNotNull<WorkShopModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	ItemCast.Identity.Id = (long)IdentityId;
				ProductionFacilities = AppSettings.DataAccess.Crud.GetListProductionFacilities(false, false, false);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
