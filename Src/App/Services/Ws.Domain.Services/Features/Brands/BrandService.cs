using Ws.Database.Nhibernate.Entities.Ref1c.Brands;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Brands;

internal class BrandService(SqlBrandRepository brandRepo) : IBrandService
{
    [Transactional]
    public IEnumerable<Brand> GetAll() => brandRepo.GetAll();

    [Transactional]
    public Brand GetItemByUid(Guid uid) => brandRepo.GetByUid(uid);

    [Transactional]
    public void Delete(Brand item) => brandRepo.Delete(item);
}