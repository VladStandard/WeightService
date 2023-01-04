// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.NestingFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluNestingFk : RazorComponentItemBase<PluNestingFkModel>
{
    #region Public and private fields, properties, constructor

    private PluBundleFkModel _pluBundleFk;
    private PluBundleFkModel PluBundleFk { get => _pluBundleFk; set { _pluBundleFk = value; SqlLinkedItems = new() { _pluBundleFk, _nestingFk }; } }
    private NestingFkModel _nestingFk;
    private NestingFkModel NestingFk { get => _nestingFk; set { _nestingFk = value; SqlLinkedItems = new() { _pluBundleFk, _nestingFk }; } }

    public ItemPluNestingFk()
    {
        _pluBundleFk = SqlItemNewEmpty<PluBundleFkModel>();
        _nestingFk = SqlItemNewEmpty<NestingFkModel>();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () =>
            {
                DataContext.GetListNotNullable<PluBundleFkModel>(SqlCrudConfigList);
                DataContext.GetListNotNullable<NestingFkModel>(SqlCrudConfigList);
                SqlItemCast = DataAccess.GetItemNotNullable<PluNestingFkModel>(IdentityUid);

                if (SqlItemCast.IdentityIsNew)
                {
                    PluBundleFk = DataContext.PluBundleFks.Where(item => item.IdentityIsNew).FirstOrDefault() ?? SqlItemNewEmpty<PluBundleFkModel>();
                    NestingFk = DataContext.NestingFks.Where(item => item.IdentityIsNew).FirstOrDefault() ?? SqlItemNewEmpty<NestingFkModel>();
                    SqlItemCast = SqlItemNew<PluNestingFkModel>();
                    SqlItemCast.PluBundle = PluBundleFk;
                    SqlItemCast.Nesting = NestingFk;
                }
                else
                {
                    PluBundleFk = SqlItemCast.PluBundle;
                    NestingFk = SqlItemCast.Nesting;
                }

                ButtonSettings = new(false, false, false, false, false, true, true);
            }
        );
    }

    #endregion
}