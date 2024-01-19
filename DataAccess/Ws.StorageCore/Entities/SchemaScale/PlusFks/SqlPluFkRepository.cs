using Ws.StorageCore.Entities.SchemaRef1c.Plus;
namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

public sealed class SqlPluFkRepository : SqlTableRepositoryBase<SqlPluFkEntity>
{
    public IEnumerable<SqlPluFkEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluFkEntity> items = SqlCore.GetEnumerable<SqlPluFkEntity>(sqlCrudConfig);
        return items.OrderBy(item => item.Plu.Number);
    }
    
    public SqlPluFkEntity GetByPlu(SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluFkEntity>(sqlCrudConfig);
    }
}