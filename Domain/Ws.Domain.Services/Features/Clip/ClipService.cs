using Ws.Database.Core.Entities.Ref1c.Clips;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Clip;

internal class ClipService : IClipService
{
    public ClipEntity GetByUid(Guid uid) => new SqlClipRepository().GetByUid(uid);
    public ClipEntity GetByUid1С(Guid uid) => new SqlClipRepository().GetByUid1C(uid);
    public IEnumerable<ClipEntity> GetAll() => new SqlClipRepository().GetEnumerable();
    
    public ClipEntity GetDefault()
    {
        ClipEntity entity = GetByUid1С(Guid.Empty);
        entity.Name = "Без клипсы";
        entity.Weight = 0;
        SqlCoreHelper.Instance.SaveOrUpdate(entity);
        return entity;
    }
}