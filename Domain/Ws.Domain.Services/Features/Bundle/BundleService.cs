using Ws.Database.Core.Entities.Ref1c.Bundles;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Bundle;

internal class BundleService(SqlBundleRepository bundleRepo) : IBundleService
{
    [Session] public BundleEntity GetItemByUid(Guid uid) => bundleRepo.GetByUid(uid);
    [Session] public BundleEntity GetItemByUid1С(Guid uid) => bundleRepo.GetByUid1C(uid);
    [Session] public IEnumerable<BundleEntity> GetAll() => bundleRepo.GetAll();

    [Session] public BundleEntity GetDefault()
    {
        BundleEntity bundle = GetItemByUid1С(Guid.Empty);
        bundle.Name = "Без пакета";
        bundle.Weight = 0;
        SqlCoreHelper.SaveOrUpdate(bundle);
        return bundle;
    }
}