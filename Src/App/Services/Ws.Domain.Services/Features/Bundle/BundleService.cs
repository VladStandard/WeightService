using Ws.Database.Nhibernate.Entities.Ref1c.Bundles;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Bundle;

internal class BundleService(SqlBundleRepository bundleRepo) : IBundleService
{
    [Transactional]
    public BundleEntity GetItemByUid(Guid uid) => bundleRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<BundleEntity> GetAll() => bundleRepo.GetAll();
}