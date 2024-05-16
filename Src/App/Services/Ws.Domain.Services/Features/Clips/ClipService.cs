using Ws.Database.Nhibernate.Entities.Ref1c.Clips;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Clips;

internal class ClipService(SqlClipRepository clipRepo) : IClipService
{
    [Transactional]
    public IEnumerable<Clip> GetAll() => clipRepo.GetAll();

    [Transactional]
    public Clip GetItemByUid(Guid uid) => clipRepo.GetByUid(uid);
}