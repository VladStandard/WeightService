// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

public partial class ItemDevice : RazorComponentItemBase<DeviceModel>
{
	#region Public and private fields, properties, constructor

	public DeviceTypeFkModel DeviceTypeFk { get; set; }

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
				SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<DeviceModel>(IdentityUid);
				if (SqlItemCast.IdentityIsNew)
					SqlItem = SqlItemNew<DeviceModel>();
				DeviceTypeFk = AppSettings.DataAccess.GetItemDeviceTypeFkNotNull(SqlItemCast);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
