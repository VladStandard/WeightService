using WsStorageCore.OrmUtils;
namespace WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

public class WsSqlPluTemplateFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkModel>
{
    public WsSqlPluTemplateFkModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew) return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluTemplateFkModel.Plu), plu));
        return SqlCore.GetItemByCrud<WsSqlPluTemplateFkModel>(sqlCrudConfig);
    }
    
    public List<WsSqlPluTemplateFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        IEnumerable<WsSqlPluTemplateFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPluTemplateFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Template.Title)
                .ThenBy(item => item.Plu.Name);
        return items.ToList();
    }

}