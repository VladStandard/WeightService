// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.Logs;

public class WsSqlLogRepository : WsSqlTableRepositoryBase<WsSqlLogModel>
{
    public WsSqlLogModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlLogModel>(uid);

    public WsSqlLogModel GetItemFirst() => SqlCore.GetItemFirst<WsSqlLogModel>();

    public IEnumerable<WsSqlLogModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.CreateDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetEnumerableNotNullable<WsSqlLogModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlLogModel> GetEnumerable(int maxResults) => 
        SqlCore.GetEnumerableNotNullable<WsSqlLogModel>(maxResults, true);

    [Obsolete(@"Use GetEnumerable")]
    public IList<WsSqlLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.CreateDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetListNotNullable<WsSqlLogModel>(sqlCrudConfig);
    }

    [Obsolete(@"Use GetEnumerable")]
    public IList<WsSqlLogModel> GetList(int maxResults) => 
        SqlCore.GetListNotNullable<WsSqlLogModel>(maxResults, true);
}