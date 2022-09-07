// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemBarCode : ItemRazorPageBase<BarCodeModel>
{
	#region Public and private fields, properties, constructor

	public ItemBarCode()
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
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						ItemCast = new();
						ItemCast.SetDtNow();
						ItemCast.IsMarked = false;
						ItemCast.Value = "NEW BARCODE";
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByUidNotNull<BarCodeModel>(IdentityUid);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
