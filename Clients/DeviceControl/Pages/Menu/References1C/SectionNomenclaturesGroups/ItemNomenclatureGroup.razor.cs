// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PlusGroups;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionNomenclaturesGroups;

public sealed partial class ItemNomenclatureGroup : RazorComponentItemBase<WsSqlPluGroupModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlPluGroupModel? ParentGroup { get; set; }

    #endregion

    #region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPluGroupModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<WsSqlPluGroupModel>();

                ParentGroup = ContextManager.ContextItem.GetItemNomenclatureGroupParentNotNullable(SqlItemCast);
            }
        });
    }

    #endregion
}
