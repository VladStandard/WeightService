using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Services.Features.Brand;

internal class BrandService : IBrandService
{
    public IEnumerable<BrandEntity> GetAll() => new SqlBrandRepository().GetEnumerable();
    public BrandEntity GetByUid1С(Guid uid) => new SqlBrandRepository().GetItemByUid1C(uid);
    public BrandEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BrandEntity>(uid);
}