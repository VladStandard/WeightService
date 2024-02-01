using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusTemplatesFks;

public class SqlPluTemplateFkRepository : IGetItemByQuery<PluTemplateFkEntity>
{
    public PluTemplateFkEntity GetItemByQuery(QueryOver<PluTemplateFkEntity> query) =>
        SqlCoreHelper.Instance.GetItem(query);
}