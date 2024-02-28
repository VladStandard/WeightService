using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository :  BaseRepository, IGetItemByUid<StorageMethodEntity>,
    ISave<StorageMethodEntity>, IUpdate<StorageMethodEntity>, IDelete<StorageMethodEntity>
{
    public StorageMethodEntity GetByUid(Guid uid) => Session.Get<StorageMethodEntity>(uid) ?? new();
    
    public IEnumerable<StorageMethodEntity> GetList() =>
        Session.Query<StorageMethodEntity>().OrderBy(i => i.Name).ToList();
    
    public StorageMethodEntity GetItemByName(string name) =>
        Session.Query<StorageMethodEntity>().FirstOrDefault(i => i.Name == name) ?? new();
    
    public StorageMethodEntity Save(StorageMethodEntity item) { Session.Save(item); return item; }
    public StorageMethodEntity Update(StorageMethodEntity item) { Session.Update(item); return item; }
    public void Delete(StorageMethodEntity item) => Session.Delete(item);
}