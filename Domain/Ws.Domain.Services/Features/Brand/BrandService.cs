using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Brand;

internal class BrandService : IBrandService
{
    public BrandEntity GetByUid(Guid uid) => new SqlBrandRepository().GetByUid(uid);
    public BrandEntity GetByUid1С(Guid uid) => new SqlBrandRepository().GetByUid1C(uid);
    public IEnumerable<BrandEntity> GetAll() => new SqlBrandRepository().GetEnumerable();
}