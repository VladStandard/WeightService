using Ws.Database.Nhibernate.Entities.Diag.LogWebs;
using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.LogWebs;

internal class LogWebService(SqlLogWebRepository logWebRepo) : ILogWebService
{
    [Transactional]
    public IEnumerable<LogWeb> GetAll() => logWebRepo.GetList();

    [Transactional]
    public LogWeb GetItemByUid(Guid uid) => logWebRepo.GetByUid(uid);

    public void Save(DateTime requestStampDt, string request, string response, string url, int success, int errors)
    {
        LogWeb webLog = new()
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
        logWebRepo.Save(webLog);
    }
}