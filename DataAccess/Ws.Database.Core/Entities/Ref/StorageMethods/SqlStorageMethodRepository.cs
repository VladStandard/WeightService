using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository :  BaseRepository, IGetItemByUid<StorageMethodEntity>
{
    public StorageMethodEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<StorageMethodEntity>(uid);
    
    public IEnumerable<StorageMethodEntity> GetList()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<StorageMethodEntity>().OrderBy(i => i.Name).Asc
        );
    }
    
    public StorageMethodEntity GetItemByName(string name)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<StorageMethodEntity>().Where(i => i.Name == name)
        );
    }
}