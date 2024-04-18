using Ws.Database.Nhibernate.Entities.Ref1c.Boxes;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Box;

internal class BoxService(SqlBoxRepository boxRepo) : IBoxService
{
    [Transactional]
    public IEnumerable<BoxEntity> GetAll() => boxRepo.GetAll();

    [Transactional]
    public BoxEntity GetItemByUid(Guid uid) => boxRepo.GetByUid(uid);
}