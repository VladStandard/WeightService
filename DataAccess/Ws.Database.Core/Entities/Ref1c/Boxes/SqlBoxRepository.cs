using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : BaseRepository, IGetItemByUid1C<BoxEntity>, IGetItemByUid<BoxEntity>, IGetAll<BoxEntity>, ISave<BoxEntity>
{
    public BoxEntity GetByUid(Guid uid) => Session.Get<BoxEntity>(uid) ?? new();
    
    public BoxEntity GetByUid1C(Guid uid1C) =>
        Session.Query<BoxEntity>().FirstOrDefault(i => i.Uid1C == uid1C) ?? new();
    
    public IEnumerable<BoxEntity> GetAll() =>
        Session.Query<BoxEntity>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
    
    public BoxEntity Save(BoxEntity item)
    {
        Session.Save(item);
        return item;
    }
}