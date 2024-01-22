using Ws.Database.Core.Entities.Diag.LogWebs;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Services.Features.LogWeb;

internal class LogWebService : ILogWebService
{
    public IEnumerable<LogWebEntity> GetAll() => new SqlLogWebRepository().GetList(new());
    public void Save(DateTime requestStampDt, string request, string response, string url, int success, int errors)
    {
        LogWebEntity webLog = new()
        {
            CreateDt = requestStampDt,
            StampDt = DateTime.Now,
            Version = "beta",
            Url = url,
            DataRequest = request,
            DataResponse = response,
            CountSuccess = success,
            CountErrors = errors,
            CountAll = errors + success
        };
        SqlCoreHelper.Instance.Save(webLog);
    }

    public LogWebEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LogWebEntity>(uid);
}