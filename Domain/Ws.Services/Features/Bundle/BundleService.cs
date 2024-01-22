using Ws.Database.Core.Entities.Ref1c.Bundles;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Services.Features.Bundle;

internal class BundleService : IBundleService
{
    public IEnumerable<BundleEntity> GetAll() => new SqlBundleRepository().GetEnumerable();
    public BundleEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BundleEntity>(uid);
    public BundleEntity GetByUid1С(Guid uid) => new SqlBundleRepository().GetItemByUid1C(uid);
    public BundleEntity GetDefault()
    {
        BundleEntity bundle = GetByUid1С(Guid.Empty);
        bundle.Name = "Без пакета";
        bundle.Weight = 0;
        SqlCoreHelper.Instance.SaveOrUpdate(bundle);
        return bundle;
    }
}