using Ws.Database.Nhibernate.Entities.Ref1c.Bundles;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Bundles;

internal class BundleService(SqlBundleRepository bundleRepo) : IBundleService
{
    #region Items

    [Transactional]
    public Bundle GetItemByUid(Guid uid) => bundleRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<Bundle> GetAll() => bundleRepo.GetAll();

    #endregion
}