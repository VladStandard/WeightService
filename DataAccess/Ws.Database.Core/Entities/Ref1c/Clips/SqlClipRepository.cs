using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Clips;

public sealed class SqlClipRepository :  BaseRepository, IGetItemByUid1C<ClipEntity>, IGetItemByUid<ClipEntity>, IGetAll<ClipEntity>
{
    public ClipEntity GetByUid(Guid uid) => Session.Get<ClipEntity>(uid) ?? new();

    public IEnumerable<ClipEntity> GetAll() =>
        Session.Query<ClipEntity>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
    
    public ClipEntity GetByUid1C(Guid uid1C) => 
        Session.Query<ClipEntity>().FirstOrDefault(i => i.Uid1C == uid1C) ?? new();
}