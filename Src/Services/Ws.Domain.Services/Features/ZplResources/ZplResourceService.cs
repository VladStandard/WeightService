using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.ZplResources.Validators;

namespace Ws.Domain.Services.Features.ZplResources;

internal class ZplResourceService(SqlZplResourceRepository zplResourceRepo, IRedisCachingProvider provider)
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
    public ZplResource Create(ZplResource item)
    {
        ZplResource data = zplResourceRepo.Save(item);
        Dictionary<string, string>? cacheData = provider.HGetAll("ZPL_RESOURCES");
        cacheData[data.Name] = data.Zpl;
        provider.HMSet("ZPL_RESOURCES", cacheData, TimeSpan.FromHours(1));
        return data;
    }

    [Transactional, Validate<ZplResourceUpdateValidator>]
    public ZplResource Update(ZplResource item)
    {
        ZplResource data = zplResourceRepo.Update(item);
        Dictionary<string, string>? cacheData = provider.HGetAll("ZPL_RESOURCES");
        cacheData[data.Name] = data.Zpl;
        provider.HMSet("ZPL_RESOURCES", cacheData, TimeSpan.FromHours(1));
        return data;
    }

    [Transactional]
    public void Delete(Guid id, string name)
    {
        zplResourceRepo.Delete(new() { Uid = id });
        provider.HDel("ZPL_RESOURCES", [name]);
    }

    #endregion
}