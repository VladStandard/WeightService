using Ws.Database.Nhibernate.Entities.Ref1c.Bundles;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Bundles;

internal class BundleService(SqlBundleRepository bundleRepo) : IBundleService
{
    [Transactional]
    public Bundle GetItemByUid(Guid uid) => bundleRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Bundle> GetAll() => bundleRepo.GetAll();
}