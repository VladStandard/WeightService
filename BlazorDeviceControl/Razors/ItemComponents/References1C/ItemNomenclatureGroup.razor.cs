// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.NomenclaturesGroups;

namespace BlazorDeviceControl.Razors.ItemComponents.References1C;

public partial class ItemNomenclatureGroup : RazorComponentItemBase<NomenclatureGroupModel>
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
                SqlItemCast = DataAccess.GetItemNotNullable<NomenclatureGroupModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
					SqlItemCast = SqlItemNew<NomenclatureGroupModel>();
				}
            }
		});
	}

	#endregion
}