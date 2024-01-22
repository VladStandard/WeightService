using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository : IUidRepo<StorageMethodEntity>
{
    public StorageMethodEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<StorageMethodEntity>(uid);
    
    public IEnumerable<StorageMethodEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<StorageMethodEntity>(crud).ToList();
    }
    
    public StorageMethodEntity GetItemByName(string name)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(StorageMethodEntity.Name), name));
        return SqlCoreHelper.Instance.GetItemByCrud<StorageMethodEntity>(crud);
    }
}