// This is an independent project of an individual developer. Dear PVS-Studio, please check it
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
                if (SqlItemCast.IsNew)
				{
					SqlItemCast = SqlItemNew<PrinterModel>();
				}
				DataContext.GetListNotNullable<PrinterTypeModel>(SqlCrudConfigList);
            }
		});
	}

	#endregion
}