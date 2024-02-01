using Ws.Database.Core.Entities.Ref1c.Boxes;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Box;

internal class BoxService : IBoxService
{
    public IEnumerable<BoxEntity> GetAll() => new SqlBoxRepository().GetAll();
    
    public BoxEntity GetItemByUid(Guid uid) => new SqlBoxRepository().GetByUid(uid);
    
    public BoxEntity GetItemByUid1С(Guid uid) => new SqlBoxRepository().GetByUid1C(uid);
    
    public BoxEntity GetDefaultForCharacteristic()
    {
        BoxEntity entity = GetItemByUid1С(Guid.Parse("71BC8E8A-99CF-11EA-A220-A4BF0139EB1B"));
        return entity.IsExists ? entity : GetDefault();
    }
    
    public BoxEntity GetDefault()
    {
        BoxEntity entity = GetItemByUid1С(Guid.Empty);
        if (entity.IsExists) return entity;

        entity = new() { Name = "Без коробки", Weight = 0, Uid1C = Guid.Empty };
        
        return new SqlBoxRepository().Save(entity);
    }
}