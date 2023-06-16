// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.WorkShops;

namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class ItemWorkshop : ItemBase<WsSqlWorkShopModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlProductionFacilityModel> ProductionFacilityModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.SqlCoreItem.GetItemNotNullable<WsSqlWorkShopModel>(Id);
        ProductionFacilityModels = ContextManager.AccessManager.SqlCoreList.
            GetListNotNullable<WsSqlProductionFacilityModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
    }

    #endregion
}