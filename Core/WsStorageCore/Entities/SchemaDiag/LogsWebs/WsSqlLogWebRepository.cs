using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaDiag.LogsWebs;

public class WsSqlLogWebRepository: WsSqlTableRepositoryBase<WsSqlLogWebEntity>
{
    public List<WsSqlLogWebEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlLogWebEntity>(sqlCrudConfig).ToList();
    }
    
    public void Save(DateTime requestStampDt, string requestDataString, string responseDataString, string url,
        int success, int errors)
    {
        WsSqlLogWebEntity webLog = new()
        {
            CreateDt = requestStampDt,
            StampDt = DateTime.Now,
            Version = WsAppVersionHelper.Instance.Version,
            Url = url,
            DataRequest = requestDataString,
            DataResponse = responseDataString,
            CountSuccess = success,
            CountErrors = errors,
            CountAll = errors + success
        };
        SqlCore.Save(webLog, WsSqlEnumSessionType.IsolatedAsync);
    }

}