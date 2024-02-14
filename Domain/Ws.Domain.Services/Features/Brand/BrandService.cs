using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Brand;

internal class BrandService(SqlBrandRepository brandRepo) : IBrandService
{
    public IEnumerable<BrandEntity> GetAll() => brandRepo.GetAll();
    public BrandEntity GetItemByUid(Guid uid) => brandRepo.GetByUid(uid);
    public BrandEntity GetItemByUid1С(Guid uid) => brandRepo.GetByUid1C(uid);

    public BrandEntity GetDefault()
    {
        BrandEntity brand = GetItemByUid1С(Guid.Empty);
        if (brand.IsExists) return brand;

        brand = new() { Name = "Без бренда", Uid1C = Guid.Empty };
        return brandRepo.Save(brand);
    }
}