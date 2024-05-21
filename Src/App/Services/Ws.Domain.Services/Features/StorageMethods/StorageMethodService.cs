using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.StorageMethods;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.StorageMethods.Validators;

namespace Ws.Domain.Services.Features.StorageMethods;

internal class StorageMethodService(SqlStorageMethodRepository storageMethodRepo, IRedisCachingProvider provider) : IStorageMethodService
{
    #region List

    [Transactional]
    public IList<StorageMethod> GetAll() => storageMethodRepo.GetList();

    #endregion

    #region Items

    [Transactional]
    public StorageMethod GetItemByUid(Guid uid) => storageMethodRepo.GetByUid(uid);

    [Transactional]
    public StorageMethod GetByName(string name) => storageMethodRepo.GetItemByName(name);

    #endregion

    #region CRUD

    [Transactional, Validate<StorageMethodNewValidator>]
    public StorageMethod Create(StorageMethod item) => storageMethodRepo.Save(item);


    [Transactional, Validate<StorageMethodUpdateValidator>]
    public StorageMethod Update(StorageMethod item)
    {
        item = storageMethodRepo.Update(item);

        string zplKey = $"STORAGE_METHODS:{item.Name}";
        if (provider.KeyExists(zplKey))
            provider.StringSet(zplKey, item.Zpl, TimeSpan.FromHours(1));

        return item;
    }

    [Transactional]
    public void Delete(StorageMethod item) => storageMethodRepo.Delete(item);


    #endregion

    public string? GetStorageByNameFromCacheOrDb(string name)
    {
        string key = $"STORAGE_METHODS:{name}";

        if (provider.KeyExists(key))
            return provider.StringGet(key);

        StorageMethod temp = GetByName(name);

        if (!temp.IsExists || temp.Zpl == string.Empty) return null;

        provider.StringSet(key, temp.Zpl, TimeSpan.FromHours(1));
        return temp.Zpl;
    }
}