using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.ZplResources.Validators;

namespace Ws.Domain.Services.Features.ZplResources;

internal partial class ZplResourceService(SqlZplResourceRepository zplResourceRepo, IRedisCachingProvider provider)
    : IZplResourceService
{
    #region Items

    [Transactional]
    public ZplResource GetItemByUid(Guid uid) => zplResourceRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<ZplResource> GetAll() => zplResourceRepo.GetAll().ToList();

    #endregion

    #region CRUD

    [Transactional, Validate<ZplResourceNewValidator>]
    public ZplResource Create(ZplResource item) => UpdateCache(zplResourceRepo.Save(item));

    [Transactional, Validate<ZplResourceUpdateValidator>]
    public ZplResource Update(ZplResource item) => UpdateCache(zplResourceRepo.Update(item));

    [Transactional]
    public void Delete(ZplResource item)
    {
        zplResourceRepo.Delete(item);
        provider.HDel("ZPL_RESOURCES", [item.Name]);
    }

    #endregion

    [Transactional]
    public Dictionary<string, string> GetAllResourcesFromCacheOrDb()
    {
        Dictionary<string, string>? cached = provider.HGetAll("ZPL_RESOURCES");

        if (cached != null && cached.Count != 0) return cached;

        cached = zplResourceRepo.GetAll().ToDictionary(i => i.Name, i => i.Zpl);
        provider.HMSet("ZPL_RESOURCES", cached, TimeSpan.FromHours(1));

        return cached;
    }
}