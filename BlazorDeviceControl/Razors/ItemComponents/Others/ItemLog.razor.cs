// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Logs;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemLog : RazorComponentItemBase<LogModel>
{
	#region Public and private fields, properties, constructor

    public ItemLog() : base()
    {
        ButtonSettings = new(false, false, false, false, false, false, true);
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                SqlItemCast = DataContext.GetItemNotNullable<LogModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
					SqlItemCast = SqlItemNew < LogModel >();
				}
            }
		});
	}

	#endregion
}