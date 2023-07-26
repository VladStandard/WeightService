namespace WsStorageCore.Tables.TableScaleFkModels.PlusGroupsFks;

public sealed class WsSqlPluGroupFkRepository : WsSqlTableRepositoryBase<WsSqlPluGroupFkModel>
{
    public List<WsSqlPluGroupFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPluGroupFkModel> list = SqlCore.GetListNotNullable<WsSqlPluGroupFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            list = list
                .OrderBy(item => item.PluGroup.Name)
                .ThenBy(item => item.Parent.Name).ToList();
        return list;
    }
}