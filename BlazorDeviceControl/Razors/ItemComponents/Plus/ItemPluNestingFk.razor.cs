// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluNestingFk : RazorComponentItemBase<PluNestingFkModel>
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
				DataContext.GetListNotNullable<BoxModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<BundleModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<PluNestingFkModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<PluModel>(SqlCrudConfigList);

				SqlItemCast = DataContext.GetItemNotNullable<PluNestingFkModel>(IdentityUid);
				if (SqlItemCast.IdentityIsNew)
				{
					SqlItemCast = SqlItemNew<PluNestingFkModel>();
					if (DataContext.Plus.Any())
						SqlItemCast.PluBundle.Plu = DataContext.Plus.First();
					//if (DataContext.BundleFks.Any())
					//	SqlItemCast.BundleFk = DataContext.BundleFks.First();
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}