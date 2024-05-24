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
    public ZplResource Create(ZplResource item) => zplResourceRepo.Save(item);

    [Transactional, Validate<ZplResourceUpdateValidator>]
    public ZplResource Update(ZplResource item) => zplResourceRepo.Update(item);

    [Transactional]
    public void Delete(ZplResource item)
    {
        zplResourceRepo.Delete(item);
        provider.HDel("ZPL_RESOURCES", [item.Name]);
    }

    #endregion
}