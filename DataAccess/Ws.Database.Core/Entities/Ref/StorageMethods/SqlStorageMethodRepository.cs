using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository : SqlTableRepositoryBase<StorageMethodEntity>
{
    public IEnumerable<StorageMethodEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<StorageMethodEntity>(crud).ToList();
    }
    
    public StorageMethodEntity GetItemByName(string name)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(StorageMethodEntity.Name), name));
        return SqlCore.GetItemByCrud<StorageMethodEntity>(crud);
    }
}