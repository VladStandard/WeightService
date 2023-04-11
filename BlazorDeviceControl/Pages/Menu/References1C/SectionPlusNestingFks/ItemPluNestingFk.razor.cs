// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Boxes;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionPlusNestingFks;

public sealed partial class ItemPluNestingFk : RazorComponentItemBase<PluNestingFkModel>
{
    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () =>
            {
                SqlItemCast = DataAccess.GetItemNotNullable<PluNestingFkModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<PluNestingFkModel>();
            }
        );
    }

    #endregion
}
