using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.Templates;

public sealed class SqlTemplateRepository :  BaseRepository, IGetAll<TemplateEntity>
{
    public TemplateEntity GetByUid(Guid id) => SqlCoreHelper.GetItemById<TemplateEntity>(id);

    public IEnumerable<TemplateEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<TemplateEntity>().OrderBy(i => i.Name).Asc
        );
    }
}