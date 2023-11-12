namespace WsStorageCore.Entities.SchemaScale.PlusTemplatesFks;

public class WsSqlPluTemplateFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkEntity>
{
    public WsSqlPluTemplateFkEntity GetItemByPlu(WsSqlPluEntity plu)
    {
        if (plu.IsNew) return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluTemplateFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<WsSqlPluTemplateFkEntity>(sqlCrudConfig);
    }
    
    public List<WsSqlPluTemplateFkEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        IEnumerable<WsSqlPluTemplateFkEntity> items = SqlCore.GetEnumerable<WsSqlPluTemplateFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Template.Title)
                .ThenBy(item => item.Plu.Name);
        return items.ToList();
    }

}