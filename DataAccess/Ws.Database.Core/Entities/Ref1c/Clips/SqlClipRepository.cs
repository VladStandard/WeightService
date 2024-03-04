using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Clips;

public sealed class SqlClipRepository : BaseRepository,
    IGetItemByUid1C<ClipEntity>, IGetItemByUid<ClipEntity>, IGetAll<ClipEntity>, ISave<ClipEntity>
{
    public ClipEntity GetByUid(Guid uid) => Session.Get<ClipEntity>(uid) ?? new();

    public ClipEntity GetByUid1C(Guid uid1C) =>
        Session.Query<ClipEntity>().FirstOrDefault(i => i.Uid1C == uid1C) ?? new();

    public IEnumerable<ClipEntity> GetAll() =>
        Session.Query<ClipEntity>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
    
    public ClipEntity Save(ClipEntity item) { Session.Save(item); return item; }
}