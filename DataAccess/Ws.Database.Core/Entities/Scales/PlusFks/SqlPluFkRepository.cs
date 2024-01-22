using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusFks;

public sealed class SqlPluFkRepository : SqlTableRepositoryBase<PluFkEntity>
{
    public IEnumerable<PluFkEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<PluFkEntity> items = SqlCore.GetEnumerable<PluFkEntity>(sqlCrudConfig);
        return items.OrderBy(item => item.Plu.Number);
    }
    
    public PluFkEntity GetByPlu(PluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(PluFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<PluFkEntity>(sqlCrudConfig);
    }
}