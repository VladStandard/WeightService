using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Brands;

public sealed class SqlBrandRepository : BaseRepository, IGetItemByUid<Brand>, IGetAll<Brand>,
    IDelete<Brand>
{
    public Brand GetByUid(Guid uid) => Session.Get<Brand>(uid) ?? new();

    public IEnumerable<Brand> GetAll() => Session.Query<Brand>().OrderBy(i => i.Name).ToList();

    public void Delete(Brand item) => Session.Delete(item);
}