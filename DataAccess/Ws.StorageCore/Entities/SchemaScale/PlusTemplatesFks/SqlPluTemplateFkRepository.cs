using Ws.StorageCore.Entities.SchemaRef1c.Plus;
namespace Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

public class SqlPluTemplateFkRepository : SqlTableRepositoryBase<SqlPluTemplateFkEntity>
{
    public SqlPluTemplateFkEntity GetItemByPlu(SqlPluEntity plu)
    {
        if (plu.IsNew) return new();
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluTemplateFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluTemplateFkEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluTemplateFkEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluTemplateFkEntity> items = SqlCore.GetEnumerable<SqlPluTemplateFkEntity>(sqlCrudConfig);
        items = items
            .OrderBy(item => item.Template.Title)
            .ThenBy(item => item.Plu.Name);
        return items;
    }
}