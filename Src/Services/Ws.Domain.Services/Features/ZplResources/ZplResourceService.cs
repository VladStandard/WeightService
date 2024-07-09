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
    public ZplResource Create(ZplResource item) =>
        zplResourceRepo.Save(item);

    [Transactional, Validate<ZplResourceUpdateValidator>]
    public ZplResource Update(ZplResource item)
    {
        string name =  $"{zplResourceRepo.GetByUid(item.Uid).Name.ToLower()}_sql";

        provider.HDel("ZPL_RESOURCES:0", [name]);
        provider.HDel("ZPL_RESOURCES:90", [name]);

        return zplResourceRepo.Update(item);
    }

    [Transactional]
    public void Delete(Guid id, string name)
    {
        provider.HDel("ZPL_RESOURCES:0", [$"{name.ToLower()}_sql"]);
        provider.HDel("ZPL_RESOURCES:90", [$"{name.ToLower()}_sql"]);

        zplResourceRepo.Delete(new() { Uid = id });
    }

    #endregion
}