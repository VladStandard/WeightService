// This is an independent project of an individual developer. Dear PVS-Studio, please check it
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPrinters;

public sealed partial class ItemPrinter : RazorComponentItemBase<PrinterModel>
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
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<PrinterModel>(IdentityId);
                if (SqlItemCast.IsNew)
				{
					SqlItemCast = SqlItemNew<PrinterModel>();
				}
				ContextManager.AccessManager.AccessList.GetListNotNullable<PrinterTypeModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
            }
		});
	}

	#endregion
}
