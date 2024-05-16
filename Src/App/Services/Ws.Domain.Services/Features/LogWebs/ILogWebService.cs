using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.LogWebs;

public interface ILogWebService : IGetItemByUid<LogWeb>
{
    IEnumerable<LogWeb> GetAll();

    void Save(DateTime requestStampDt, string request, string response, string url, int success, int errors);
}