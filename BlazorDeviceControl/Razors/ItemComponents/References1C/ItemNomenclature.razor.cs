// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.Nomenclatures;

namespace BlazorDeviceControl.Razors.ItemComponents.References1C;

public partial class ItemNomenclature : RazorComponentItemBase<NomenclatureModel>
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
                SqlItemCast = DataAccess.GetItemNotNullable<NomenclatureModel>(IdentityId);
                if (SqlItemCast.IdentityIsNew)
                {
					SqlItemCast = SqlItemNew<NomenclatureModel>();
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
