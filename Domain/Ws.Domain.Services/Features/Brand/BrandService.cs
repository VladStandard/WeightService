using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Brand;

internal class BrandService(SqlBrandRepository brandRepo) : IBrandService
{
    [Transactional] public IEnumerable<BrandEntity> GetAll() => brandRepo.GetAll();
    [Transactional] public BrandEntity GetItemByUid(Guid uid) => brandRepo.GetByUid(uid);
    [Transactional] public BrandEntity GetItemByUid1С(Guid uid) => brandRepo.GetByUid1C(uid);

    [Transactional] public BrandEntity GetDefault()
    {
        BrandEntity brand = GetItemByUid1С(Guid.Empty);
        if (brand.IsExists) return brand;

        brand = new() { Name = "Без бренда", Uid1C = Guid.Empty };
        return brandRepo.Save(brand);
    }
}