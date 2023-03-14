// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Razors.ItemComponents.References1C;

public partial class ItemPluBundleFk : RazorComponentItemBase<PluBundleFkModel>
{
    #region Public and private fields, properties, constructor

    private PluModel _plu;
    private PluModel Plu { get => _plu; set { _plu = value; SqlLinkedItems = new() { _plu, _bundle }; } }

    private BundleModel _bundle;
    private BundleModel Bundle { get => _bundle; set { _bundle = value; SqlLinkedItems = new() { _plu, _bundle }; } }

    public ItemPluBundleFk() : base()
    {
        _plu = SqlItemNewEmpty<PluModel>();
        _bundle = SqlItemNewEmpty<BundleModel>();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () =>
            {
                SqlItemCast = DataAccess.GetItemNotNullable<PluBundleFkModel>(IdentityUid);
                DataContext.GetListNotNullable<PluModel>(SqlCrudConfigList);
                DataContext.GetListNotNullable<BundleModel>(SqlCrudConfigList);

                if (SqlItemCast.IsNew)
                {
                    Plu = DataContext.Plus.Where(item => item.IsNew).FirstOrDefault() ?? SqlItemNewEmpty<PluModel>();
                    Bundle = DataContext.Bundles.Where(item => item.IsNew).FirstOrDefault() ?? SqlItemNewEmpty<BundleModel>();
                    SqlItemCast = SqlItemNew<PluBundleFkModel>();
                    SqlItemCast.Plu = Plu;
                    SqlItemCast.Bundle = Bundle;
                }
                else
                {
                    Plu = SqlItemCast.Plu;
                    Bundle = SqlItemCast.Bundle;
                }
            }
        );
    }

    #endregion
}
