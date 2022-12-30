// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.NestingFks;
using DataCore.Sql.TableScaleModels.Boxes;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemBundleFk : RazorComponentItemBase<NestingFkModel>
{
    #region Public and private fields, properties, constructor

    //private BoxModel _box;
    //private BoxModel Box { get => _box; set { _box = value; SqlLinkedItems = new() { _box, _bundle }; } }
    //private BundleModel _bundle;
    //private BundleModel Bundle { get => _bundle; set { _bundle = value; SqlLinkedItems = new() { _box, _bundle }; } }

    //public ItemBundleFk()
    //{
    //    _box = DataAccess.GetItemNew<BoxModel>();
    //    _bundle = DataAccess.GetItemNew<BundleModel>();
    //}

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () => 
            {
                DataContext.GetListNotNullable<BoxModel>(SqlCrudConfigList);
                SqlItemCast = DataAccess.GetItemNotNullable<NestingFkModel>(IdentityUid);
                    if (SqlItemCast.IdentityIsNew)
                        SqlItem = SqlItemNew<NestingFkModel>();
                ButtonSettings = new(false, false, false, false, false, true, true);
            }
        );
    }

    #endregion
}