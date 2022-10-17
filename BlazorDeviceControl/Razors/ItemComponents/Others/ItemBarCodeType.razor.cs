//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using BlazorCore.Razors;

//namespace BlazorDeviceControl.Razors.ItemComponents.Others;

//public partial class ItemBarCodeType : RazorComponentItemBase<BarCodeTypeModel>
//{
//	#region Public and private methods

//	public ItemBarCodeType()
//	{
//		//
//	}

//	protected override void OnParametersSet()
//	{
//		RunActionsParametersSet(new()
//		{
//			() =>
//			{
//                SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<BarCodeTypeModel>(IdentityUid);
//                if (SqlItemCast.Identity.IsNew())
//	                SqlItem = SqlItemNew<BarCodeTypeModel>();

//				ButtonSettings = new(false, false, false, false, false, true, true);
//			}
//		});
//	}

//	#endregion
//}
