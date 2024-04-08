using Ws.Database.Nhibernate.Entities.Ref1c.Clips;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Clip;

internal class ClipService(SqlClipRepository clipRepo) : IClipService
{
    [Transactional]
    public IEnumerable<ClipEntity> GetAll() => clipRepo.GetAll();

    [Transactional]
    public ClipEntity GetItemByUid(Guid uid) => clipRepo.GetByUid(uid);
}