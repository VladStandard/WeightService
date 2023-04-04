// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Boxes;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionPlusNestingFks;

public sealed partial class ItemPluNestingFk : RazorComponentItemBase<PluNestingFkModel>
{
    #region Public and private fields, properties, constructor

    private BoxModel _box;
    private BoxModel Box { get => _box; set { _box = value; SqlLinkedItems = new() { _box, _pluBundleFk }; } }
    private PluBundleFkModel _pluBundleFk;
    private PluBundleFkModel PluBundleFk { get => _pluBundleFk; set { _pluBundleFk = value; SqlLinkedItems = new() { _box, _pluBundleFk }; } }

    public ItemPluNestingFk() : base()
    {
        _pluBundleFk = SqlItemNewEmpty<PluBundleFkModel>();
        _box = SqlItemNewEmpty<BoxModel>();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () =>
            {
                DataContext.GetListNotNullable<BoxModel>(SqlCrudConfigUtils.GetCrudConfigComboBox());
                DataContext.GetListNotNullable<PluBundleFkModel>(SqlCrudConfigUtils.GetCrudConfigComboBox());

                SqlItemCast = DataAccess.GetItemNotNullable<PluNestingFkModel>(IdentityUid);

                if (SqlItemCast.IsNew)
                {
                    PluBundleFk = DataContext.PluBundleFks.Where(item => item.IsNew).FirstOrDefault() ?? SqlItemNewEmpty<PluBundleFkModel>();
                    Box = DataContext.Boxes.Where(item => item.IsNew).FirstOrDefault() ?? SqlItemNewEmpty<BoxModel>();
                    SqlItemCast = SqlItemNew<PluNestingFkModel>();
                    SqlItemCast.Box = Box;
                    SqlItemCast.PluBundle = PluBundleFk;
                }
                else
                {
                    Box = SqlItemCast.Box;
                    PluBundleFk = SqlItemCast.PluBundle;
                }
            }
        );
    }

    #endregion
}
