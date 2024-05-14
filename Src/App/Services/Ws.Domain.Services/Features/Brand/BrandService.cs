using Ws.Database.Nhibernate.Entities.Ref1c.Brands;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Brand;

internal class BrandService(SqlBrandRepository brandRepo) : IBrandService
{
    [Transactional]
    public IEnumerable<Models.Entities.Ref1c.Brand> GetAll() => brandRepo.GetAll();

    [Transactional]
    public Models.Entities.Ref1c.Brand GetItemByUid(Guid uid) => brandRepo.GetByUid(uid);

    [Transactional]
    public void Delete(Models.Entities.Ref1c.Brand item) => brandRepo.Delete(item);
}