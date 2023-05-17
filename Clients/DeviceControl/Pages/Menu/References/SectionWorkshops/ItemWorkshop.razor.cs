// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.WorkShops;

namespace BlazorDeviceControl.Pages.Menu.References.SectionWorkshops;

public sealed partial class ItemWorkshop : RazorComponentItemBase<WsSqlWorkShopModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlProductionFacilityModel> ProductionFacilityModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlWorkShopModel>(IdentityId);
        ProductionFacilityModels = ContextManager.AccessManager.AccessList.
            GetListNotNullable<WsSqlProductionFacilityModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
    }

    #endregion
}
