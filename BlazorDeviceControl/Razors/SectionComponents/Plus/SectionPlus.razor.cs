// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.Fields;
using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlus : RazorComponentSectionBase<PluModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionPlus()
	{
		SqlCrudConfigSection.IsGuiShowItemsCount = true;
		SqlCrudConfigSection.IsGuiShowFilterMarked = true;

        SqlCrudConfigSection.IsResultOrder = false;
		SqlCrudConfigSection.AddOrders(new SqlFieldOrderModel($"{nameof(PluModel.Number)}", 
			SqlFieldOrderEnum.Asc));
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlSectionCast = DataContext.GetListNotNullable<PluModel>(SqlCrudConfigSection);

				ButtonSettings = new(false, false, true, true, false, false, false);
			}
		});
	}

	#endregion
}