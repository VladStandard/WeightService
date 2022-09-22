// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemLog : RazorComponentItemBase<LogModel>
{
	#region Public and private fields, properties, constructor

	public ItemLog()
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
                SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<LogModel>(IdentityUid);
                if (SqlItemCast.Identity.IsNew())
	                SqlItem = SqlItemNew<LogModel>();

				ButtonSettings = new(false, false, false, false, false, false, true);
			}
		});
	}

	#endregion
}
