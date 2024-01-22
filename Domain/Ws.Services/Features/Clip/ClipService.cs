using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Clips;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Clip;

internal class ClipService : IClipService
{
    public IEnumerable<ClipEntity> GetAll() => new SqlClipRepository().GetEnumerable();

    public ClipEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ClipEntity>(uid);
}