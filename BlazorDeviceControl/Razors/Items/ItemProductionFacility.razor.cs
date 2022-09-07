// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemProductionFacility : RazorPageItemBase<ProductionFacilityModel>
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
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<ProductionFacilityModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	ItemCast.Identity.Id = (long)IdentityId;
				
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
