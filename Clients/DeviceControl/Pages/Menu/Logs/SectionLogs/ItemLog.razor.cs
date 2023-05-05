// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.Logs;

namespace BlazorDeviceControl.Pages.Menu.Logs.SectionLogs;

public sealed partial class ItemLog : RazorComponentItemBase<WsSqlLogModel>
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
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlLogModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<WsSqlLogModel>();
            }
        });
    }

    #endregion
}