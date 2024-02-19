using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Brand;

internal class BrandService(SqlBrandRepository brandRepo) : IBrandService
{
    [Session] public IEnumerable<BrandEntity> GetAll() => brandRepo.GetAll();
    [Session] public BrandEntity GetItemByUid(Guid uid) => brandRepo.GetByUid(uid);
    [Session] public BrandEntity GetItemByUid1С(Guid uid) => brandRepo.GetByUid1C(uid);

    [Session] public BrandEntity GetDefault()
    {
        BrandEntity brand = GetItemByUid1С(Guid.Empty);
        if (brand.IsExists) return brand;

        brand = new() { Name = "Без бренда", Uid1C = Guid.Empty };
        return brandRepo.Save(brand);
    }
}