using Ws.Domain.Models.Entities.Diag;
using Ws.Services.Common;

namespace Ws.Services.Features.LogWeb;

public interface ILogWebService : IUid<LogWebEntity>
{
    IEnumerable<LogWebEntity> GetAll();
}