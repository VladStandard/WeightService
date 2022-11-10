// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

public partial class ItemDevice : RazorComponentItemBase<DeviceModel>
{
	#region Public and private fields, properties, constructor

	private DeviceTypeFkModel DeviceTypeFk { get; set; }

	#endregion

	public ItemDevice()
	{
		DeviceTypeFk = new();
	}

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				DataContext.GetListNotNullable<DeviceTypeModel>(SqlCrudConfigList);

				SqlItemCast = DataContext.GetItemNotNullable<DeviceModel>(IdentityUid);
				if (SqlItemCast.IdentityIsNew)
					SqlItem = SqlItemNew<DeviceModel>();
				DeviceTypeFk = DataAccess.GetItemDeviceTypeFkNotNullable(SqlItemCast);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
