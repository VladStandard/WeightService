using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Clips;

public sealed class SqlClipRepository :  BaseRepository, IGetItemByUid1C<ClipEntity>, IGetItemByUid<ClipEntity>, IGetAll<ClipEntity>
{
    public ClipEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<ClipEntity>(uid);
    
    public IEnumerable<ClipEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<ClipEntity>().OrderBy(i => i.Weight).Asc.ThenBy(i => i.Name).Asc
        );
    }

    public ClipEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<ClipEntity>().Where(i => i.Uid1C == uid1C)
        );
    }
}