using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusTemplatesFks;

public class SqlPluTemplateFkRepository :  BaseRepository, IGetItemByQuery<PluTemplateFkEntity>
{
    public PluTemplateFkEntity GetItemByQuery(QueryOver<PluTemplateFkEntity> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).UniqueResult<PluTemplateFkEntity>();
}