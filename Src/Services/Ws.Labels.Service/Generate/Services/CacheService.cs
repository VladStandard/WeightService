using EasyCaching.Core;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.StorageMethods;
using Ws.Domain.Services.Features.Templates;
using Ws.Domain.Services.Features.ZplResources;
using Ws.Labels.Service.Generate.Models.Cache;

namespace Ws.Labels.Service.Generate.Services;

public class CacheService(
    ITemplateService templateService,
    IZplResourceService zplResourceService,
    IStorageMethodService storageMethodService,
    IEasyCachingProvider easyCachingProvider,
    IRedisCachingProvider redisCachingProvider)
{
    public string? GetStorageByNameFromCacheOrDb(string name)
    {
        string key = $"STORAGE_METHODS:{name}";

        if (redisCachingProvider.KeyExists(key))
            return redisCachingProvider.StringGet(key);

        StorageMethod temp = storageMethodService.GetByName(name);

        if (!temp.IsExists || temp.Zpl == string.Empty) return null;

        redisCachingProvider.StringSet(key, temp.Zpl, TimeSpan.FromHours(1));
        return temp.Zpl;
    }
    public TemplateCache? GetTemplateByUidFromCacheOrDb(Guid templateUid)
    {
        string zplKey = $"TEMPLATES:{templateUid}";

        if (easyCachingProvider.Exists(zplKey))
            return easyCachingProvider.Get<TemplateCache>(zplKey).Value;

        Template temp = templateService.GetItemByUid(templateUid);
        if (!temp.IsExists || temp.Body == string.Empty) return null;

        TemplateCache tempCache = new(temp);

        easyCachingProvider.Set($"{zplKey}", tempCache, TimeSpan.FromHours(1));
        return tempCache;
    }
    public Dictionary<string, string> GetResourcesFromCacheOrDb(List<string> resourcesName)
    {
        Dictionary<string, string>? cached = redisCachingProvider.HMGet("ZPL_RESOURCES", resourcesName);

        if (cached != null && cached.Count == resourcesName.Capacity) return cached;

        cached = zplResourceService.GetAll().ToDictionary(i => $"{i.Name.ToLower()}_sql", i => i.Zpl);
        redisCachingProvider.HMSet("ZPL_RESOURCES", cached, TimeSpan.FromHours(1));

        return cached;
    }
}