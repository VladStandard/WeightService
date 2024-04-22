using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ZplResource;

internal partial class ZplResourceService
{
    private ZplResourceEntity UpdateCache(ZplResourceEntity item)
    {
        Dictionary<string, string> cached = GetAllResourcesFromCacheOrDb();
        cached.Remove(item.Name);
        cached.Add(item.Name, item.Zpl);
        provider.HMSet("ZPL_RESOURCES", cached, TimeSpan.FromHours(1));
        return item;
    }
}