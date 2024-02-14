using Ws.Database.Core.Entities.Ref1c.Clips;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Clip;

internal class ClipService(SqlClipRepository clipRepo) : IClipService
{
    public IEnumerable<ClipEntity> GetAll() => clipRepo.GetAll();
    public ClipEntity GetItemByUid(Guid uid) => clipRepo.GetByUid(uid);
    public ClipEntity GetItemByUid1С(Guid uid) => clipRepo.GetByUid1C(uid);

    public ClipEntity GetDefault()
    {
        ClipEntity entity = GetItemByUid1С(Guid.Empty);
        entity.Name = "Без клипсы";
        entity.Weight = 0;
        SqlCoreHelper.SaveOrUpdate(entity);
        return entity;
    }
}