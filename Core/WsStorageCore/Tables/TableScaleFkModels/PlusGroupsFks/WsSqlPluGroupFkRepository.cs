namespace WsStorageCore.Tables.TableScaleFkModels.PlusGroupsFks;

public sealed class WsSqlPluGroupFkRepository : WsSqlTableRepositoryBase<WsSqlPluGroupFkModel>
{
    public List<WsSqlPluGroupFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<WsSqlPluGroupFkModel> list = SqlCore.GetListNotNullable<WsSqlPluGroupFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.PluGroup.Name)
                .ThenBy(item => item.Parent.Name).ToList();
        return list;
    }
}