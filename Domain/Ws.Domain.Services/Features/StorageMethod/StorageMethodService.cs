using Ws.Database.Core.Entities.Ref.StorageMethods;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.StorageMethod;

internal class StorageMethodService(SqlStorageMethodRepository storageMethodRepo) : IStorageMethodService
{
    public IEnumerable<StorageMethodEntity> GetAll() => storageMethodRepo.GetList();

    public StorageMethodEntity GetItemByUid(Guid uid) => storageMethodRepo.GetByUid(uid);

    public StorageMethodEntity GetByName(string name) => storageMethodRepo.GetItemByName(name);

    public StorageMethodEntity GetDefault()
    {
        StorageMethodEntity defaultMethod = storageMethodRepo.GetItemByName("Без способа хранения");
        if (defaultMethod.IsNew) SqlCoreHelper.Save(defaultMethod);
        return defaultMethod;
    }

    public StorageMethodEntity GetByNameOrDefault(string name)
    {
        StorageMethodEntity method = GetByName(name);
        return method.IsNew ? GetDefault() : method;
    }
}