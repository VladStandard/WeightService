using Ws.Database.Core.Entities.Diag.LogWebs;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.LogWeb;

internal class LogWebService(SqlLogWebRepository logWebRepo) : ILogWebService
{
    [Transactional] public IEnumerable<LogWebEntity> GetAll() => logWebRepo.GetList();

    [Transactional] public LogWebEntity GetItemByUid(Guid uid) => logWebRepo.GetByUid(uid);

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
        SqlCoreHelper.Save(webLog);
    }
}