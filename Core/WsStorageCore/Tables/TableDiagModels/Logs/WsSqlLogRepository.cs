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
        return SqlCore.GetEnumerableNotNullable<WsSqlLogModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlLogModel> GetEnumerable(int maxResults) => 
        SqlCore.GetEnumerableNotNullable<WsSqlLogModel>(maxResults);

    public async Task<IEnumerable<WsSqlLogModel>> GetEnumerableAsync(int maxResults) => 
        await SqlCore.GetEnumerableNotNullableAsync<WsSqlLogModel>(maxResults);

    public IList<WsSqlLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetListNotNullable<WsSqlLogModel>(sqlCrudConfig);
    }

    public IList<WsSqlLogModel> GetList(int maxResults) => 
        SqlCore.GetListNotNullable<WsSqlLogModel>(maxResults);

    public async Task<IList<WsSqlLogModel>> GetListAsync(int maxResults) => 
        await SqlCore.GetListNotNullableAsync<WsSqlLogModel>(maxResults);
}