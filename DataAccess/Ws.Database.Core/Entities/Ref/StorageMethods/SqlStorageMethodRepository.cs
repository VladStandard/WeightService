using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository :  BaseRepository, IGetItemByUid<StorageMethodEntity>
{
    public StorageMethodEntity GetByUid(Guid uid) => Session.Get<StorageMethodEntity>(uid) ?? new();
    
    public IEnumerable<StorageMethodEntity> GetList() =>
        Session.Query<StorageMethodEntity>().OrderBy(i => i.Name).ToList();
    
    public StorageMethodEntity GetItemByName(string name) =>
        Session.Query<StorageMethodEntity>().FirstOrDefault(i => i.Name == name) ?? new();
}