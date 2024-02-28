using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Templates;

public sealed class SqlTemplateRepository :  BaseRepository, IGetAll<TemplateEntity>, IGetItemByUid<TemplateEntity>, 
    ISave<TemplateEntity>, IUpdate<TemplateEntity>, IDelete<TemplateEntity>
{
    public TemplateEntity GetByUid(Guid id) => Session.Get<TemplateEntity>(id) ?? new();
    public IEnumerable<TemplateEntity> GetAll() => Session.Query<TemplateEntity>().OrderBy(i => i.Name).ToList();
    public TemplateEntity Save(TemplateEntity item) { Session.Save(item); return item; }
    public TemplateEntity Update(TemplateEntity item) { Session.Update(item); return item; }
    public void Delete(TemplateEntity item) => Session.Delete(item);
}