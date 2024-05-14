using Ws.Database.Nhibernate.Entities.Diag.LogWebs;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.LogWeb;

internal class LogWebService(SqlLogWebRepository logWebRepo) : ILogWebService
{
    [Transactional]
    public IEnumerable<Models.Entities.Diag.LogWeb> GetAll() => logWebRepo.GetList();

    [Transactional]
    public Models.Entities.Diag.LogWeb GetItemByUid(Guid uid) => logWebRepo.GetByUid(uid);

    public void Save(DateTime requestStampDt, string request, string response, string url, int success, int errors)
    {
        Models.Entities.Diag.LogWeb webLog = new()
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