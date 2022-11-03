// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

public partial class ItemWorkshop : RazorComponentItemBase<WorkShopModel>
{
	#region Public and private fields, properties, constructor

	//

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlItemCast = DataContext.GetItemNotNull<WorkShopModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	SqlItemCast.IdentityValueId = (long)IdentityId;
				DataContext.GetListNotNull<ProductionFacilityModel>(SqlCrudConfigItem);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
