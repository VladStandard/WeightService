using Ws.Database.Nhibernate.Entities.Ref1c.Clips;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Clip;

internal class ClipService(SqlClipRepository clipRepo) : IClipService
{
    [Transactional]
    public IEnumerable<Models.Entities.Ref1c.Clip> GetAll() => clipRepo.GetAll();

    [Transactional]
    public Models.Entities.Ref1c.Clip GetItemByUid(Guid uid) => clipRepo.GetByUid(uid);
}