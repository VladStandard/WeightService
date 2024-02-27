using Ws.Database.Core.Entities.Ref1c.Clips;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Clip;

internal class ClipService(SqlClipRepository clipRepo) : IClipService
{
    [Transactional] public IEnumerable<ClipEntity> GetAll() => clipRepo.GetAll();
    [Transactional] public ClipEntity GetItemByUid(Guid uid) => clipRepo.GetByUid(uid);
    [Transactional] public ClipEntity GetItemByUid1С(Guid uid) => clipRepo.GetByUid1C(uid);

    [Transactional] public ClipEntity GetDefault()
    {
        ClipEntity entity = GetItemByUid1С(Guid.Empty);
        return entity.IsExists ? entity : clipRepo.Save(new() { Name = "Без клипсы" });
    }
}