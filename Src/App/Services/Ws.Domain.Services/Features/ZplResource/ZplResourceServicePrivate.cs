namespace Ws.Domain.Services.Features.ZplResource;

internal partial class ZplResourceService
{
    private Models.Entities.Print.ZplResource UpdateCache(Models.Entities.Print.ZplResource item)
    {
        Dictionary<string, string> cached = GetAllResourcesFromCacheOrDb();
        cached.Remove(item.Name);
        cached.Add(item.Name, item.Zpl);
        provider.HMSet("ZPL_RESOURCES", cached, TimeSpan.FromHours(1));
        return item;
    }
}