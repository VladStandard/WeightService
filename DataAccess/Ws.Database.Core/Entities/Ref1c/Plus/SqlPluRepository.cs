using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Plus;

public sealed class SqlPluRepository :  BaseRepository, IGetItemByUid1C<PluEntity>, IGetItemByUid<PluEntity>, IGetAll<PluEntity>, 
    IGetListByQuery<PluEntity>
{
    public PluEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<PluEntity>(uid);
    
    public PluEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.Instance.GetItem(
            QueryOver.Of<PluEntity>().Where(i => i.Uid1C == uid1C)
        );
    }
    
    public IEnumerable<PluEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<PluEntity>().OrderBy(i => i.Number).Asc
        );
    }
    
    public IEnumerable<PluEntity> GetListByQuery(QueryOver<PluEntity> query)
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            query.Clone().OrderBy(i => i.Number).Asc
        );
    }
}