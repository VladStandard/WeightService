using Ws.Domain.Models.Entities.Diag;
using Ws.StorageCore.Entities.Diag.LogWebs;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.LogWeb;

internal class LogWebService : ILogWebService
{
    public IEnumerable<LogWebEntity> GetAll() => new SqlLogWebRepository().GetList(new());

    public LogWebEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LogWebEntity>(uid);
}