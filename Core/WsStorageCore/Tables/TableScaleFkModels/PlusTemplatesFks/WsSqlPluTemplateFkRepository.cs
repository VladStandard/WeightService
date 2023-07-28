namespace WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

public class WsSqlPluTemplateFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkModel>
{
    public WsSqlPluTemplateFkModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew) return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlPluTemplateFkModel.Plu), plu);
        return SqlCore.GetItemByCrud<WsSqlPluTemplateFkModel>(sqlCrudConfig);
    }
    
    public List<WsSqlPluTemplateFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        List<WsSqlPluTemplateFkModel> list = SqlCore.GetListNotNullable<WsSqlPluTemplateFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            list = list
                .OrderBy(item => item.Template.Title)
                .ThenBy(item => item.Plu.Name).ToList();
        return list;
    }

}