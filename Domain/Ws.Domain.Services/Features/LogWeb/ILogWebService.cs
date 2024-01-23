using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.LogWeb;

public interface ILogWebService : IUid<LogWebEntity>
{
    IEnumerable<LogWebEntity> GetAll();

    void Save(DateTime requestStampDt, string request, string response, string url, int success,
        int errors);
}