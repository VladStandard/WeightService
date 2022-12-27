// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.BundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluBundleFk : RazorComponentItemBase<PluBundleFkModel>
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
				SqlItemCast = DataContext.GetItemNotNullable<PluBundleFkModel>(IdentityUid);
				DataContext.GetListNotNullable<BoxModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<BundleModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<BundleFkModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<PluModel>(SqlCrudConfigList);

				if (SqlItemCast.IdentityIsNew)
				{
					SqlItem = SqlItemNew<PluBundleFkModel>();
					if (DataContext.Plus.Any())
						SqlItemCast.Plu = DataContext.Plus.First();
					if (DataContext.BundleFks.Any())
						SqlItemCast.BundleFk = DataContext.BundleFks.First();
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
