using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusTemplatesFks;

public class SqlPluTemplateFkRepository : SqlTableRepositoryBase<PluTemplateFkEntity>
{
    public PluTemplateFkEntity GetItemByPlu(PluEntity plu)
    {
        if (plu.IsNew) return new();
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(PluTemplateFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<PluTemplateFkEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<PluTemplateFkEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<PluTemplateFkEntity> items = SqlCore.GetEnumerable<PluTemplateFkEntity>(sqlCrudConfig);
        items = items
            .OrderBy(item => item.Template.Title)
            .ThenBy(item => item.Plu.Name);
        return items;
    }
}