using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.DeviceTypesFks;

public class WsSqlDeviceTypeFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeFkEntity>
{
    public WsSqlDeviceTypeFkEntity GetItemByDevice(WsSqlDeviceEntity device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlDeviceTypeFkEntity.Device), device));
        return SqlCore.GetItemByCrud<WsSqlDeviceTypeFkEntity>(sqlCrudConfig);
    }

    public List<WsSqlDeviceTypeFkEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlDeviceTypeFkEntity> items = SqlCore.GetEnumerable<WsSqlDeviceTypeFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Type.Name)
                .ThenBy(item => item.Device.Name);
        return items.ToList();
    }
}