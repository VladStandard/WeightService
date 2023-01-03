// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.NestingFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluNestingFk : RazorComponentItemBase<PluNestingFkModel>
{
    #region Public and private fields, properties, constructor

    private PluModel _plu;
    private PluModel Plu { get => _plu; set { _plu = value; SqlLinkedItems = new() { _plu, _bundle, _pluBundleFk, _nestingFk }; } }
    private BundleModel _bundle;
    private BundleModel Bundle { get => _bundle; set { _bundle = value; SqlLinkedItems = new() { _plu, _bundle, _pluBundleFk, _nestingFk }; } }
    private PluBundleFkModel _pluBundleFk;
    private PluBundleFkModel PluBundleFk { get => _pluBundleFk; set { _pluBundleFk = value; SqlLinkedItems = new() { _plu, _bundle, _pluBundleFk, _nestingFk }; } }
    private NestingFkModel _nestingFk;
    private NestingFkModel NestingFk { get => _nestingFk; set { _nestingFk = value; SqlLinkedItems = new() { _plu, _bundle, _pluBundleFk, _nestingFk }; } }

    public ItemPluNestingFk()
    {
        _plu = SqlItemNewEmpty<PluModel>();
        _bundle = SqlItemNewEmpty<BundleModel>();
        _nestingFk = SqlItemNewEmpty<NestingFkModel>();
        _pluBundleFk = SqlItemNewEmpty<PluBundleFkModel>();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () =>
            {
                SqlItemCast = DataAccess.GetItemNotNullable<PluNestingFkModel>(IdentityUid);
                DataContext.GetListNotNullable<PluModel>(SqlCrudConfigList);
                DataContext.GetListNotNullable<BundleModel>(SqlCrudConfigList);
                DataContext.GetListNotNullable<NestingFkModel>(SqlCrudConfigList);
                DataContext.GetListNotNullable<PluBundleFkModel>(SqlCrudConfigList);

                if (SqlItemCast.IdentityIsNew)
                {
                    PluBundleFk = DataContext.PluBundleFks.FirstOrDefault() ?? SqlItemNewEmpty<PluBundleFkModel>();
                    PluBundleFk.Plu = Plu;
                    PluBundleFk.Bundle = Bundle;
                    NestingFk = DataContext.NestingFks.FirstOrDefault() ?? SqlItemNewEmpty<NestingFkModel>();
                    SqlItemCast = SqlItemNew<PluNestingFkModel>();
                    SqlItemCast.PluBundle = PluBundleFk;
                    SqlItemCast.Nesting = NestingFk;
                }
                else
                {
                    PluBundleFk = SqlItemCast.PluBundle;
                    Plu = SqlItemCast.PluBundle.Plu;
                    Bundle = SqlItemCast.PluBundle.Bundle;
                    NestingFk = SqlItemCast.Nesting;
                }

                ButtonSettings = new(false, false, false, false, false, true, true);
            }
        );
    }

    #endregion
}