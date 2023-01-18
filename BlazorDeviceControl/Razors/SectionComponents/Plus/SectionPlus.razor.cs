// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlus : RazorComponentSectionBase<PluModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionPlus() : base()
	{
        SqlCrudConfigSection.IsResultOrder = false;
		SqlCrudConfigSection.AddOrders(new SqlFieldOrderModel($"{nameof(PluModel.Number)}", 
			SqlFieldOrderEnum.Asc));
        ButtonSettings = new(false, false, true, true, false, false, false);
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
            }
		});
	}

	#endregion
}