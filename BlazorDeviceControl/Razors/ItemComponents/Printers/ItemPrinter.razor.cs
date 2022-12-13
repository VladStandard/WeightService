// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersTypes;

namespace BlazorDeviceControl.Razors.ItemComponents.Printers;

public partial class ItemPrinter : RazorComponentItemBase<PrinterModel>
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
                SqlItemCast = DataContext.GetItemNotNullable<PrinterModel>(IdentityId);
                if (SqlItemCast.IdentityIsNew)
	                SqlItem = SqlItemNew<PrinterModel>();
				DataContext.GetListNotNullable<PrinterTypeModel>(SqlCrudConfigList);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
