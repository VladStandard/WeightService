using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Boxes;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Box;

internal class BoxService : IBoxService
{
    public IEnumerable<BoxEntity> GetAll() => new SqlBoxRepository().GetEnumerable();
    public BoxEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BoxEntity>(uid);
}