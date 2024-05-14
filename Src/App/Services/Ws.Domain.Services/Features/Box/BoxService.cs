using Ws.Database.Nhibernate.Entities.Ref1c.Boxes;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Box;

internal class BoxService(SqlBoxRepository boxRepo) : IBoxService
{
    [Transactional]
    public IEnumerable<Models.Entities.Ref1c.Box> GetAll() => boxRepo.GetAll();

    [Transactional]
    public Models.Entities.Ref1c.Box GetItemByUid(Guid uid) => boxRepo.GetByUid(uid);
}