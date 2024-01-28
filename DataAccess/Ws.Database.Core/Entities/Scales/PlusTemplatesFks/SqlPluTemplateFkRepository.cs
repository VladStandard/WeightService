using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusTemplatesFks;

public class SqlPluTemplateFkRepository
{
    public PluTemplateFkEntity GetItemByPlu(PluEntity plu)
    {
        if (plu.IsNew) return new();
        DetachedCriteria criteria = DetachedCriteria.For<PluTemplateFkEntity>()
            .Add(SqlRestrictions.EqualFk(nameof(PluTemplateFkEntity.Plu), plu));
        return SqlCoreHelper.Instance.GetItemByCriteria<PluTemplateFkEntity>(criteria);
    }
}