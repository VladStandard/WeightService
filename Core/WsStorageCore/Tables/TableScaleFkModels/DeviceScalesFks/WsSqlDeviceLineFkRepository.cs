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
        IEnumerable<WsSqlDeviceScaleFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Device.Name);
        return items.ToList();
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