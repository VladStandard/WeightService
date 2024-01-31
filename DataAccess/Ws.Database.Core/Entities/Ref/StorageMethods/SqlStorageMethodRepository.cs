using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository : IGetItemByUid<StorageMethodEntity>
{
    public StorageMethodEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<StorageMethodEntity>(uid);
    
    public IEnumerable<StorageMethodEntity> GetList()
    {
        return SqlCoreHelper.Instance.GetEnumerable<StorageMethodEntity>(
            DetachedCriteria.For<StorageMethodEntity>().AddOrder(SqlOrder.NameAsc())
        );
    }
    
    public StorageMethodEntity GetItemByName(string name)
    {
        return SqlCoreHelper.Instance.GetItem<StorageMethodEntity>(
            DetachedCriteria.For<StorageMethodEntity>()
                .Add(SqlRestrictions.Equal(nameof(StorageMethodEntity.Name), name))
        );
    }
}