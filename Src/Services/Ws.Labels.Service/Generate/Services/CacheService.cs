using EasyCaching.Core;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Templates;
using Ws.Domain.Services.Features.ZplResources;
using Ws.Labels.Service.Generate.Models.Cache;

namespace Ws.Labels.Service.Generate.Services;

public class CacheService(
    ITemplateService templateService,
    IZplResourceService zplResourceService,
    IEasyCachingProvider easyCachingProvider,
    IRedisCachingProvider redisCachingProvider)
{
    public TemplateFromCache? GetTemplateByUidFromCacheOrDb(Guid templateUid)
    {
        string zplKey = $"TEMPLATES:{templateUid}";

        if (easyCachingProvider.Exists(zplKey))
            return easyCachingProvider.Get<TemplateFromCache>(zplKey).Value;

        Template temp = templateService.GetItemByUid(templateUid);
        if (!temp.IsExists || temp.Body == string.Empty) return null;

        TemplateFromCache tempFromCache = new(temp);

        easyCachingProvider.Set($"{zplKey}", tempFromCache, TimeSpan.FromHours(1));
        return tempFromCache;
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