using Ws.Database.Core.Entities.Ref1c.Boxes;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Services.Features.Box;

internal class BoxService : IBoxService
{
    public IEnumerable<BoxEntity> GetAll() => new SqlBoxRepository().GetEnumerable();
    
    public BoxEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BoxEntity>(uid);
    
    public BoxEntity GetByUid1С(Guid uid) => new SqlBoxRepository().GetItemByUid1C(uid);
    
    public BoxEntity GetDefaultForCharacteristic()
    {
        BoxEntity entity = GetByUid1С(new("71BC8E8A-99CF-11EA-A220-A4BF0139EB1B"));
        return entity.IsExists ? entity : GetDefault();
    }
    
    public BoxEntity GetDefault()
    {
        BoxEntity entity = GetByUid1С(Guid.Empty);
        entity.Name = "Без коробки";
        entity.Weight = 0;
        SqlCoreHelper.Instance.SaveOrUpdate(entity);
        return entity;
    }
}