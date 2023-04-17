// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Plus;
using WsStorage.TableScaleModels.PlusScales;
using WsStorage.TableScaleModels.Scales;
using WsStorage.Utils;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPluScales;

public sealed partial class ItemPluScales : RazorComponentItemBase<PluScaleModel>
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
                SqlItemCast = DataContext.GetItemNotNullable<PluScaleModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<PluScaleModel>();
                DataContext.GetListNotNullable<PluModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
	            DataContext.GetListNotNullable<ScaleModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
            }
        });
    }

    #endregion
}
