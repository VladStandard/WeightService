using Ws.Database.Core.Entities.Ref.StorageMethods;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.StorageMethod;

internal class StorageMethodService(SqlStorageMethodRepository storageMethodRepo) : IStorageMethodService
{ 
    [Session] public IEnumerable<StorageMethodEntity> GetAll() => storageMethodRepo.GetList();

    [Session] public StorageMethodEntity GetItemByUid(Guid uid) => storageMethodRepo.GetByUid(uid);

    [Session] public StorageMethodEntity GetByName(string name) => storageMethodRepo.GetItemByName(name);

    [Session] public StorageMethodEntity GetDefault()
    {
        StorageMethodEntity defaultMethod = storageMethodRepo.GetItemByName("Без способа хранения");
        if (defaultMethod.IsNew) SqlCoreHelper.Save(defaultMethod);
        return defaultMethod;
    }

    [Session] public StorageMethodEntity GetByNameOrDefault(string name)
    {
        StorageMethodEntity method = GetByName(name);
        return method.IsNew ? GetDefault() : method;
    }
}