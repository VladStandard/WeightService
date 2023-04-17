// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.PlusGroups;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionNomenclaturesGroups;

public sealed partial class ItemNomenclatureGroup : RazorComponentItemBase<PluGroupModel>
{
	#region Public and private fields, properties, constructor

	private PluGroupModel? ParentGroup { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                SqlItemCast = DataAccess.GetItemNotNullable<PluGroupModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
					SqlItemCast = SqlItemNew<PluGroupModel>();
				}

                ParentGroup = DataAccess.GetItemNomenclatureGroupParentNotNullable(SqlItemCast);
            }
		});
	}

	#endregion
}
