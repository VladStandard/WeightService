// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.NestingFks;
using DataCore.Sql.TableScaleModels.Boxes;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemNestingFk : RazorComponentItemBase<NestingFkModel>
{
    #region Public and private fields, properties, constructor

    private BoxModel _box;
    private BoxModel Box { get => _box; set { _box = value; SqlLinkedItems = new() { _box }; } }

    public ItemNestingFk()
    {
        _box = SqlItemNewEmpty<BoxModel>();
    }

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
                {
                    Box = DataContext.Boxes.Where(item => item.IdentityIsNew).FirstOrDefault() ?? SqlItemNewEmpty<BoxModel>();
                    SqlItemCast = SqlItemNew<NestingFkModel>();
                    SqlItemCast.Box = Box;
                }
                else
                {
                    Box = SqlItemCast.Box;
                }

                ButtonSettings = new(false, false, false, false, false, true, true);
            }
        );
    }

    #endregion
}