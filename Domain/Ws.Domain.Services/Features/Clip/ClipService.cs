using Ws.Database.Core.Entities.Ref1c.Clips;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Clip;

internal class ClipService(SqlClipRepository clipRepo) : IClipService
{
    [Session] public IEnumerable<ClipEntity> GetAll() => clipRepo.GetAll();
    [Session] public ClipEntity GetItemByUid(Guid uid) => clipRepo.GetByUid(uid);
    [Session] public ClipEntity GetItemByUid1С(Guid uid) => clipRepo.GetByUid1C(uid);

    [Session] public ClipEntity GetDefault()
    {
        ClipEntity entity = GetItemByUid1С(Guid.Empty);
        entity.Name = "Без клипсы";
        entity.Weight = 0;
        SqlCoreHelper.SaveOrUpdate(entity);
        return entity;
    }
}