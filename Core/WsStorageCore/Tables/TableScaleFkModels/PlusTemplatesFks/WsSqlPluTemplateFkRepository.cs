namespace WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

public class WsSqlPluTemplateFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkModel>
{
    public WsSqlPluTemplateFkModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew) return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluTemplateFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid,
            WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlPluTemplateFkModel>(sqlCrudConfig);
    }
    
    public List<WsSqlPluTemplateFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        List<WsSqlPluTemplateFkModel> list = SqlCore.GetListNotNullable<WsSqlPluTemplateFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Template.Title)
                .ThenBy(item => item.Plu.Name).ToList();
        return list;
    }

}