using Ws.Database.Core.Entities.Ref1c.Bundles;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Bundle;

internal class BundleService(SqlBundleRepository bundleRepo) : IBundleService
{
    [Transactional] public BundleEntity GetItemByUid(Guid uid) => bundleRepo.GetByUid(uid);
    [Transactional] public BundleEntity GetItemByUid1С(Guid uid) => bundleRepo.GetByUid1C(uid);
    [Transactional] public IEnumerable<BundleEntity> GetAll() => bundleRepo.GetAll();
    [Transactional] public BundleEntity GetDefault()
    {
        BundleEntity bundle = GetItemByUid1С(Guid.Empty);
        return bundle.IsExists ? bundle : bundleRepo.Save(new() { Name = "Без пакета"});
    }
}