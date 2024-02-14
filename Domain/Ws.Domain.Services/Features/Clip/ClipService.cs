using Ws.Database.Core.Entities.Ref1c.Clips;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Clip;

internal class ClipService : IClipService
{
    public IEnumerable<ClipEntity> GetAll() => new SqlClipRepository().GetAll();
    public ClipEntity GetItemByUid(Guid uid) => new SqlClipRepository().GetByUid(uid);
    public ClipEntity GetItemByUid1С(Guid uid) => new SqlClipRepository().GetByUid1C(uid);

    public ClipEntity GetDefault()
    {
        ClipEntity entity = GetItemByUid1С(Guid.Empty);
        entity.Name = "Без клипсы";
        entity.Weight = 0;
        SqlCoreHelper.SaveOrUpdate(entity);
        return entity;
    }
}