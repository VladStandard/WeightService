using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.StorageMethods;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.StorageMethod.Validators;

namespace Ws.Domain.Services.Features.StorageMethod;

internal class StorageMethodService(SqlStorageMethodRepository storageMethodRepo, IRedisCachingProvider provider) : IStorageMethodService
{
    [Transactional]
    public IEnumerable<Models.Entities.Print.StorageMethod> GetAll() => storageMethodRepo.GetList();

    [Transactional]
    public Models.Entities.Print.StorageMethod GetItemByUid(Guid uid) => storageMethodRepo.GetByUid(uid);

    [Transactional]
    public Models.Entities.Print.StorageMethod GetByName(string name) => storageMethodRepo.GetItemByName(name);

    [Transactional, Validate<StorageMethodNewValidator>]
    public Models.Entities.Print.StorageMethod Create(Models.Entities.Print.StorageMethod item) => storageMethodRepo.Save(item);


    [Transactional, Validate<StorageMethodUpdateValidator>]
    public Models.Entities.Print.StorageMethod Update(Models.Entities.Print.StorageMethod item)
    {
        item = storageMethodRepo.Update(item);

        string zplKey = $"STORAGE_METHODS:{item.Name}";
        if (provider.KeyExists(zplKey))
            provider.StringSet(zplKey, item.Zpl, TimeSpan.FromHours(1));

        return item;
    }

    [Transactional]
    public void Delete(Models.Entities.Print.StorageMethod item) => storageMethodRepo.Delete(item);

    public string? GetStorageByNameFromCacheOrDb(string name)
    {
        string key = $"STORAGE_METHODS:{name}";

        if (provider.KeyExists(key))
            return provider.StringGet(key);

        Models.Entities.Print.StorageMethod temp = GetByName(name);

        if (!temp.IsExists || temp.Zpl == string.Empty) return null;

        provider.StringSet(key, temp.Zpl, TimeSpan.FromHours(1));
        return temp.Zpl;
    }
}