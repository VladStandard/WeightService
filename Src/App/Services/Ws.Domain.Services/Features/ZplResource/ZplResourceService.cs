using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.ZplResources;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.ZplResource.Validators;

namespace Ws.Domain.Services.Features.ZplResource;

internal partial class ZplResourceService(SqlZplResourceRepository zplResourceRepo, IRedisCachingProvider provider)
    : IZplResourceService
{
    [Transactional]
    public Models.Entities.Print.ZplResource GetItemByUid(Guid uid) => zplResourceRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Models.Entities.Print.ZplResource> GetAll() => zplResourceRepo.GetAll().ToList();

    [Transactional, Validate<ZplResourceNewValidator>]
    public Models.Entities.Print.ZplResource Create(Models.Entities.Print.ZplResource item) => UpdateCache(zplResourceRepo.Save(item));

    [Transactional, Validate<ZplResourceUpdateValidator>]
    public Models.Entities.Print.ZplResource Update(Models.Entities.Print.ZplResource item) => UpdateCache(zplResourceRepo.Update(item));

    [Transactional]
    public void Delete(Models.Entities.Print.ZplResource item)
    {
        zplResourceRepo.Delete(item);
        provider.HDel("ZPL_RESOURCES", [item.Name]);
    }

    [Transactional]
    public Dictionary<string, string> GetAllResourcesFromCacheOrDb()
    {
        Dictionary<string, string>? cached = provider.HGetAll("ZPL_RESOURCES");

        if (cached != null && cached.Any()) return cached;

        cached = zplResourceRepo.GetAll().ToDictionary(i => i.Name, i => i.Zpl);
        provider.HMSet("ZPL_RESOURCES", cached, TimeSpan.FromHours(1));

        return cached;
    }
}