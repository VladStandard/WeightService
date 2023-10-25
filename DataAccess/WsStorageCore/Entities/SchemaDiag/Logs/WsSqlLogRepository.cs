using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaDiag.Logs;

public class WsSqlLogRepository : WsSqlTableRepositoryBase<WsSqlLogEntity>
{
    public WsSqlLogEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlLogEntity>(uid);

    public WsSqlLogEntity GetItemFirst() => SqlCore.GetItemFirst<WsSqlLogEntity>();

    public IEnumerable<WsSqlLogEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlLogEntity>(sqlCrudConfig);
    }

    public IList<WsSqlLogEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetList<WsSqlLogEntity>(sqlCrudConfig);
    }
}