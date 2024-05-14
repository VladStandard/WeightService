using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.LogWeb;

public interface ILogWebService : IGetItemByUid<Models.Entities.Diag.LogWeb>
{
    IEnumerable<Models.Entities.Diag.LogWeb> GetAll();

    void Save(DateTime requestStampDt, string request, string response, string url, int success, int errors);
}