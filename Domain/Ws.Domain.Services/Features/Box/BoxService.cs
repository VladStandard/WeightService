using Ws.Database.Core.Entities.Ref1c.Boxes;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Box;

internal class BoxService(SqlBoxRepository boxRepo) : IBoxService
{
    [Transactional] public IEnumerable<BoxEntity> GetAll() => boxRepo.GetAll();
    [Transactional] public BoxEntity GetItemByUid(Guid uid) => boxRepo.GetByUid(uid);
    [Transactional] public BoxEntity GetItemByUid1С(Guid uid) => boxRepo.GetByUid1C(uid);
    
    [Transactional] public BoxEntity GetDefaultForCharacteristic()
    {
        Guid uid1C = Guid.Parse("71BC8E8A-99CF-11EA-A220-A4BF0139EB1B");
        BoxEntity entity = GetItemByUid1С(uid1C);
        return entity.IsExists ? entity : GetDefault();
    }
    
    private BoxEntity GetDefault()
    {
        BoxEntity entity = GetItemByUid1С(Guid.Empty);
        return entity.IsExists ? entity : boxRepo.Save(new() { Name = "Без коробки" });
    }
}