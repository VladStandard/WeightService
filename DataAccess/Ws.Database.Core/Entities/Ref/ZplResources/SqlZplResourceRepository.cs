using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ZplResources;

public class SqlZplResourceRepository :  BaseRepository, IGetItemByUid<ZplResourceEntity>, IGetAll<ZplResourceEntity>
{
    public ZplResourceEntity GetByUid(Guid uid) =>
        SqlCoreHelper.GetItemById<ZplResourceEntity>(uid);
    
    public IEnumerable<ZplResourceEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<ZplResourceEntity>().OrderBy(i => i.Name).Asc
        );
    }
    
    public ZplResourceEntity GetByName(string name)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<ZplResourceEntity>().Where(i => i.Name == name)
        );
    }
}