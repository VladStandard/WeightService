using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.LogWeb;

public interface ILogWebService : IGetItemByUid<LogWebEntity>
{
    IEnumerable<LogWebEntity> GetAll();

    void Save(DateTime requestStampDt, string request, string response, string url, int success, int errors);
}