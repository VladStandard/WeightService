using Ws.Database.Core.Entities.Ref1c.Boxes;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Box;

internal class BoxService(SqlBoxRepository boxRepo) : IBoxService
{
    [Session] public IEnumerable<BoxEntity> GetAll() => boxRepo.GetAll();

    [Session] public BoxEntity GetItemByUid(Guid uid) => boxRepo.GetByUid(uid);

    [Session] public BoxEntity GetItemByUid1С(Guid uid) => boxRepo.GetByUid1C(uid);

    [Session] public BoxEntity GetDefaultForCharacteristic()
    {
        BoxEntity entity = GetItemByUid1С(Guid.Parse("71BC8E8A-99CF-11EA-A220-A4BF0139EB1B"));
        return entity.IsExists ? entity : GetDefault();
    }

    [Session] public BoxEntity GetDefault()
    {
        BoxEntity entity = GetItemByUid1С(Guid.Empty);
        if (entity.IsExists) return entity;

        entity = new() { Name = "Без коробки", Weight = 0, Uid1C = Guid.Empty };

        return new SqlBoxRepository().Save(entity);
    }
}