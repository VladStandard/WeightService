using Ws.Database.Core.Entities.Ref.StorageMethods;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.StorageMethod;

internal class StorageMethodService(SqlStorageMethodRepository storageMethodRepo) : IStorageMethodService
{ 
    [Transactional] public IEnumerable<StorageMethodEntity> GetAll() => storageMethodRepo.GetList();

    [Transactional] public StorageMethodEntity GetItemByUid(Guid uid) => storageMethodRepo.GetByUid(uid);

    [Transactional] public StorageMethodEntity GetByName(string name) => storageMethodRepo.GetItemByName(name);

    [Transactional] public StorageMethodEntity GetDefault()
    {
        const string name = "Без способа хранения";
        StorageMethodEntity defaultMethod = storageMethodRepo.GetItemByName(name);
        return defaultMethod.IsExists ? defaultMethod : storageMethodRepo.Save(new() { Name = name });
    }

    [Transactional] public StorageMethodEntity GetByNameOrDefault(string name)
    {
        StorageMethodEntity method = GetByName(name);
        return method.IsExists ? method : GetDefault();
    }
}