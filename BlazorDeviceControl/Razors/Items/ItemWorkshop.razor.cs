// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemWorkshop : ItemRazorPageBase<WorkShopModel>
{
	#region Public and private fields, properties, constructor

	private List<ProductionFacilityModel> ProductionFacilities { get; set; }

	public ItemWorkshop()
	{
		ProductionFacilities = new();
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
				ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<WorkShopModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	ItemCast.Identity.Id = (long)IdentityId;
				ProductionFacilities = AppSettings.DataAccess.GetListProductionFacilities(false, false, false);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
