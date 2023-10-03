using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableDiagModels.Logs;

public class WsSqlLogRepository : WsSqlTableRepositoryBase<WsSqlLogModel>
{
    public WsSqlLogModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlLogModel>(uid);

    public WsSqlLogModel GetItemFirst() => SqlCore.GetItemFirst<WsSqlLogModel>();

    public IEnumerable<WsSqlLogModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlLogModel>(sqlCrudConfig);
    }

    public IList<WsSqlLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetList<WsSqlLogModel>(sqlCrudConfig);
    }
}