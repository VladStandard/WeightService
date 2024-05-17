using ProjectionTools.Specifications;
using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Ref.Templates;

public sealed class SqlTemplateRepository : BaseRepository, IGetAll<Template>, IGetItemByUid<Template>,
    ISave<Template>, IUpdate<Template>, IDelete<Template>
{
    public Template GetByUid(Guid id) => Session.Get<Template>(id) ?? new();
    public IEnumerable<Template> GetAll() => Session.Query<Template>().OrderBy(i => i.Name).ToList();
    public Template Save(Template item) { Session.Save(item); return item; }
    public Template Update(Template item) { Session.Update(item); return item; }
    public void Delete(Template item) => Session.Delete(item);

    #region Specs

    public IEnumerable<Template> GetListBySpec(Specification<Template> spec) =>
        Session.Query<Template>().Where(spec).OrderBy(i => i.Name).ToList();

    #endregion
}