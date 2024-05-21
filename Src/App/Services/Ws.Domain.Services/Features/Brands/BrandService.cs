using Ws.Database.Nhibernate.Entities.Ref1c.Brands;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Brands;

internal class BrandService(SqlBrandRepository brandRepo) : IBrandService
{
    #region List

    [Transactional]
    public IList<Brand> GetAll() => brandRepo.GetAll();

    #endregion

    #region Items

    [Transactional]
    public Brand GetItemByUid(Guid uid) => brandRepo.GetByUid(uid);

    #endregion

    #region CRUD

    [Transactional]
    public void Delete(Brand item) => brandRepo.Delete(item);

    #endregion
}