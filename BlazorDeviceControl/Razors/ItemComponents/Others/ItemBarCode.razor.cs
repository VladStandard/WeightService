// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using BlazorCore.Razors;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Common;
using WebApiCore.Utils;
using static WebApiCore.Utils.TerraUtils;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemBarCode : RazorComponentItemBase<BarCodeModel>
{
	#region Public and private fields, properties, constructor

	protected ControllerHelper ControllerHelp { get; } = ControllerHelper.Instance;

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<BarCodeModel>(IdentityUid);
                if (SqlItemCast.IdentityIsNew)
	                SqlItem = SqlItemNew<BarCodeModel>();

				ButtonSettings = new(false, false, false, false, false, false, true);
			}
		});
	}

    private string GetBarcodeTop(FormatTypeEnum format) => 
		new BarcodeTopModel(SqlItemCast.ValueTop, false).GetContent(format);

    private string GetBarcodeRight(FormatTypeEnum format) => 
		new BarcodeRightModel(SqlItemCast.ValueRight).GetContent(format);

    private string GetBarcodeBottom(FormatTypeEnum format) => 
		new BarcodeBottomModel(SqlItemCast.ValueBottom).GetContent(format);

	#endregion
}
