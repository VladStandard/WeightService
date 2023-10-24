using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaDiag.LogsTypes;

public class WsSqlLogTypeRepository : WsSqlTableRepositoryBase<WsSqlLogTypeEntity>
{
    
    public WsSqlLogTypeEntity GetItemByEnumType(WsEnumLogType logType)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(
            SqlRestrictions.Equal(nameof(WsSqlLogTypeEntity.Number), (byte)logType)
        );
        return SqlCore.GetItemByCrud<WsSqlLogTypeEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlLogTypeEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(WsSqlLogTypeEntity.Number)));
        return SqlCore.GetEnumerable<WsSqlLogTypeEntity>(sqlCrudConfig);
    }
}