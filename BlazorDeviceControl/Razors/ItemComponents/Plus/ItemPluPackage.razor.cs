// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.Packages;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusPackages;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluPackage : RazorComponentItemBase<PluPackageModel>
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
                SqlItemCast = DataContext.GetItemNotNullable<PluPackageModel>(IdentityUid);
                DataContext.GetListNotNullable<PluModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<PackageModel>(SqlCrudConfigList);
                
				if (SqlItemCast.IdentityIsNew)
                {
	                SqlItem = SqlItemNew<PluPackageModel>();
                    if (DataContext.Plus.Any())
						SqlItemCast.Plu = DataContext.Plus.First();
                    if (DataContext.Packages.Any())
						SqlItemCast.Package = DataContext.Packages.First();
                }

	            ButtonSettings = new(false, false, false, false, false, true, true);
            }
        });
    }

    #endregion
}
