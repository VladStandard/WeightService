// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.ProductionFacilities;
using WsStorageCore.Tables.TableScaleModels.WorkShops;

namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class ItemWorkshop : ItemBase<WsSqlWorkShopModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlProductionFacilityModel> ProductionFacilityModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        ProductionFacilityModels = ContextManager.SqlCore.GetListNotNullable<WsSqlProductionFacilityModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
    }

    #endregion
}