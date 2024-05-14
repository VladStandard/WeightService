using Ws.Database.Nhibernate.Entities.Ref1c.Bundles;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Bundle;

internal class BundleService(SqlBundleRepository bundleRepo) : IBundleService
{
    [Transactional]
    public Models.Entities.Ref1c.Bundle GetItemByUid(Guid uid) => bundleRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Models.Entities.Ref1c.Bundle> GetAll() => bundleRepo.GetAll();
}