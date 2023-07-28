// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// SQL-контроллер таблицы DEVICES_SCALES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlDeviceLineFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceScaleFkModel>
{
    #region Public and private methods

    public WsSqlDeviceScaleFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceScaleFkModel>();

    public List<WsSqlDeviceScaleFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceScaleFkModel> list = SqlCore.GetListNotNullable<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            list = list.OrderBy(item => item.Device.Name).ToList();
        return list;
    }
    
    public WsSqlDeviceScaleFkModel GetItemByDevice(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlDeviceScaleFkModel.Device), device);
        return SqlCore.GetItemByCrud<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
    }
    
    public WsSqlDeviceScaleFkModel GetItemByLine(WsSqlScaleModel line)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlDeviceScaleFkModel.Scale), line);
        return SqlCore.GetItemByCrud<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
    }
    
    #endregion
}