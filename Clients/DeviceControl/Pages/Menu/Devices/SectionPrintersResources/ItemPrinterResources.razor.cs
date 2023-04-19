// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.Utils;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.TemplatesResources;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPrintersResources;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public sealed partial class ItemPrinterResources : RazorComponentItemBase<PrinterResourceFkModel>
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
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<PrinterResourceFkModel>(IdentityId);
                if (SqlItemCast.IsNew)
				{
					SqlItemCast = SqlItemNew<PrinterResourceFkModel>();
				}
				ContextManager.AccessManager.AccessList.GetListNotNullable<PrinterModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				ContextManager.AccessManager.AccessList.GetListNotNullable<TemplateResourceModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
            }
		});
	}

	#endregion
}