// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

public partial class ItemProductionFacility : RazorComponentItemBase<ProductionFacilityModel>
{
	#region Public and private fields, properties, constructor

	public ItemProductionFacility()
	{
		//
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlItemCast = AppSettings.DataAccess.GetItemByIdNotNull<ProductionFacilityModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	SqlItemCast.Identity.Id = (long)IdentityId;
				
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
