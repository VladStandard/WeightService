using Ws.Domain.Models.Entities.Diag;
using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCore.Entities.Diag.LogWebs;

public class SqlLogWebRepository : SqlTableRepositoryBase<LogWebEntity>
{
    public IEnumerable<LogWebEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.SelectTopRowsCount = 500;
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<LogWebEntity>(sqlCrudConfig).ToList();
    }

    public void Save(DateTime requestStampDt, string requestDataString, string responseDataString, string url,
        int success, int errors)
    {
        LogWebEntity webLog = new()
        {
            CreateDt = requestStampDt,
            StampDt = DateTime.Now,
            Version = "beta",
            Url = url,
            DataRequest = requestDataString,
            DataResponse = responseDataString,
            CountSuccess = success,
            CountErrors = errors,
            CountAll = errors + success
        };
        SqlCore.Save(webLog);
    }
}