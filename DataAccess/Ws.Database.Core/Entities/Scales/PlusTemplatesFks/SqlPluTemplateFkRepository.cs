using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusTemplatesFks;

public class SqlPluTemplateFkRepository : IGetItemByCriteria<PluTemplateFkEntity>
{
    public PluTemplateFkEntity GetItemByCriteria(DetachedCriteria criteria) =>
        SqlCoreHelper.Instance.GetItem<PluTemplateFkEntity>(criteria);
}