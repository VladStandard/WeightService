using Ws.Database.Core.Entities.Ref1c.Bundles;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Bundle;

internal class BundleService(SqlBundleRepository bundleRepo) : IBundleService
{
    public BundleEntity GetItemByUid(Guid uid) => bundleRepo.GetByUid(uid);
    public BundleEntity GetItemByUid1С(Guid uid) => bundleRepo.GetByUid1C(uid);
    public IEnumerable<BundleEntity> GetAll() => bundleRepo.GetAll();

    public BundleEntity GetDefault()
    {
        BundleEntity bundle = GetItemByUid1С(Guid.Empty);
        bundle.Name = "Без пакета";
        bundle.Weight = 0;
        SqlCoreHelper.SaveOrUpdate(bundle);
        return bundle;
    }
}