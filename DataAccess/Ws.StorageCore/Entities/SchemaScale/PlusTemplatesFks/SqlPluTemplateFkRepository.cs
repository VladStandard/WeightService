namespace Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

public class SqlPluTemplateFkRepository : SqlTableRepositoryBase<SqlPluStorageMethodFkEntity>
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
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        IEnumerable<SqlPluTemplateFkEntity> items = SqlCore.GetEnumerable<SqlPluTemplateFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Template.Title)
                .ThenBy(item => item.Plu.Name);
        return items;
    }

}