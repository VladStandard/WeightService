using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.ZplResources;

internal partial class ZplResourceService
{
    // TODO: Fix cache system
    private ZplResource UpdateCache(ZplResource item)
    {
        return item;
        // Dictionary<string, string> cached = GetAllResourcesFromCacheOrDb();
        // cached.Remove(item.Name);
        // cached.Add(item.Name, item.Zpl);
        // provider.HMSet("ZPL_RESOURCES", cached, TimeSpan.FromHours(1));
        // return item;
    }
}

