// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace BlazorDeviceControl.Pages.ItemComponents.Printers;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public partial class ItemPrinterResource : RazorComponentItemBase<PrinterResourceFkModel>
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
                SqlItemCast = DataContext.GetItemNotNullable<PrinterResourceFkModel>(IdentityId);
                if (SqlItemCast.IsNew)
				{
					SqlItemCast = SqlItemNew<PrinterResourceFkModel>();
				}
				DataContext.GetListNotNullable<PrinterModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<TemplateResourceModel>(SqlCrudConfigList);
            }
		});
	}

	#endregion
}