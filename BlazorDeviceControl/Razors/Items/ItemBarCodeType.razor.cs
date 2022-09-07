// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemBarCodeType : ItemRazorPageBase<BarCodeTypeModel>
{
	#region Public and private methods

	public ItemBarCodeType()
	{
		//
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						ItemCast = new();
						ItemCast.SetDtNow();
						ItemCast.IsMarked = false;
						ItemCast.Name = "NEW BARCODE_TYPE";
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByUidNotNull<BarCodeTypeModel>(IdentityUid);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
