namespace Ws.StorageCore.Entities.SchemaDiag.LogsWebs;

public class SqlLogWebRepository : SqlTableRepositoryBase<SqlLogWebEntity>
{
    public List<SqlLogWebEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlLogWebEntity>(sqlCrudConfig).ToList();
    }

    public void Save(DateTime requestStampDt, string requestDataString, string responseDataString, string url,
        int success, int errors)
    {
        SqlLogWebEntity webLog = new()
        {
            CreateDt = requestStampDt,
            StampDt = DateTime.Now,
            Version = AppVersionHelper.Instance.Version,
            Url = url,
            DataRequest = requestDataString,
            DataResponse = responseDataString,
            CountSuccess = success,
            CountErrors = errors,
            CountAll = errors + success
        };
        SqlCore.Save(webLog, SqlEnumSessionType.IsolatedAsync);
    }
}