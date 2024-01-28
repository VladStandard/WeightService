using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Brand;

internal class BrandService : IBrandService
{
    public BrandEntity GetDefault()
    {
        BrandEntity brand = GetItemByUid1С(Guid.Empty);
        if (brand.IsExists) return brand;

        brand = new() { Name = "Без бренда", Uid1C = Guid.Empty };
        SqlCoreHelper.Instance.Save(brand);
        return brand;
    }
    
    public BrandEntity GetItemByUid(Guid uid) => new SqlBrandRepository().GetByUid(uid);
    public BrandEntity GetItemByUid1С(Guid uid) => new SqlBrandRepository().GetByUid1C(uid);
    public IEnumerable<BrandEntity> GetAll() => new SqlBrandRepository().GetAll();
}