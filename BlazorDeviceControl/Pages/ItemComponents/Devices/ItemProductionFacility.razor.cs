// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ProductionFacilities;

namespace BlazorDeviceControl.Pages.ItemComponents.Devices;

public sealed partial class ItemProductionFacility : RazorComponentItemBase<ProductionFacilityModel>
{
	#region Public and private fields, properties, constructor


	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlItemCast = DataAccess.GetItemNotNullable<ProductionFacilityModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	SqlItemCast.IdentityValueId = (long)IdentityId;
            }
		});
	}

	#endregion
}
