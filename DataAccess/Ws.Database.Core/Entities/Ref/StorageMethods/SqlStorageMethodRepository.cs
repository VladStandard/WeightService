using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository : IGetItemByUid<StorageMethodEntity>
{
    public StorageMethodEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<StorageMethodEntity>(uid);
    
    public IEnumerable<StorageMethodEntity> GetList()
    {
        DetachedCriteria criteria = DetachedCriteria.For<StorageMethodEntity>().AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<StorageMethodEntity>(criteria);
    }
    
    public StorageMethodEntity GetItemByName(string name)
    {
        DetachedCriteria criteria = DetachedCriteria.For<StorageMethodEntity>()
            .Add(SqlRestrictions.Equal(nameof(StorageMethodEntity.Name), name));
        return SqlCoreHelper.Instance.GetItemByCriteria<StorageMethodEntity>(criteria);
    }
}