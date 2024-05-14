using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Ref.StorageMethods;

public class SqlStorageMethodRepository : BaseRepository, IGetItemByUid<StorageMethod>,
    ISave<StorageMethod>, IUpdate<StorageMethod>, IDelete<StorageMethod>
{
    public StorageMethod GetByUid(Guid uid) => Session.Get<StorageMethod>(uid) ?? new();

    public IEnumerable<StorageMethod> GetList() =>
        Session.Query<StorageMethod>().OrderBy(i => i.Name).ToList();

    public StorageMethod GetItemByName(string name) =>
        Session.Query<StorageMethod>().FirstOrDefault(i => i.Name == name) ?? new();

    public StorageMethod Save(StorageMethod item) { Session.Save(item); return item; }
    public StorageMethod Update(StorageMethod item) { Session.Update(item); return item; }
    public void Delete(StorageMethod item) => Session.Delete(item);
}