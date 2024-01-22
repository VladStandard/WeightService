using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Brands;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Brand;

internal class BrandService : IBrandService
{
    public IEnumerable<BrandEntity> GetAll() => new SqlBrandRepository().GetEnumerable();

    public BrandEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BrandEntity>(uid);
}