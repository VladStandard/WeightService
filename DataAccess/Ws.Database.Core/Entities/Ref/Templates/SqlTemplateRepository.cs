using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Templates;

public sealed class SqlTemplateRepository :  BaseRepository, IGetAll<TemplateEntity>, IGetItemByUid<TemplateEntity>
{
    public TemplateEntity GetByUid(Guid id) => Session.Get<TemplateEntity>(id) ?? new();
    public IEnumerable<TemplateEntity> GetAll() => Session.Query<TemplateEntity>().OrderBy(i => i.Name).ToList();
}