using Ws.Database.Core.Entities.Ref.StorageMethods;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.StorageMethod;

internal class StorageMethodService : IStorageMethodService
{
    public IEnumerable<StorageMethodEntity> GetAll() => new SqlStorageMethodRepository().GetList();

    public StorageMethodEntity GetItemByUid(Guid uid) => new SqlStorageMethodRepository().GetByUid(uid);

    public StorageMethodEntity GetByName(string name) => new SqlStorageMethodRepository().GetItemByName(name);

    public StorageMethodEntity GetDefault()
    {
        StorageMethodEntity defaultMethod = new SqlStorageMethodRepository().GetItemByName("Без способа хранения");
        if (defaultMethod.IsNew) SqlCoreHelper.Save(defaultMethod);
        return defaultMethod;
    }

    public StorageMethodEntity GetByNameOrDefault(string name)
    {
        StorageMethodEntity method = GetByName(name);
        return method.IsNew ? GetDefault() : method;
    }
}