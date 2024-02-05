using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : IGetItemByUid1C<BoxEntity>, IGetItemByUid<BoxEntity>, IGetAll<BoxEntity>, ISave<BoxEntity>
{
    public BoxEntity Save(BoxEntity item) => SqlCoreHelper.Instance.Save(item);
    public BoxEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<BoxEntity>(uid);
    public BoxEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.Instance.GetItem(
            QueryOver.Of<BoxEntity>().Where(i => i.Uid1C == uid1C)
        );
    }
    public IEnumerable<BoxEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<BoxEntity>().OrderBy(i => i.Weight).Asc.ThenBy(i => i.Name).Asc
        );
    }
}