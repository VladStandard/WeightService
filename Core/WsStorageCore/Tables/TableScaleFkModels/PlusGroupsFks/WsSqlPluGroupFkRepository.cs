namespace WsStorageCore.Tables.TableScaleFkModels.PlusGroupsFks;

public sealed class WsSqlPluGroupFkRepository : WsSqlTableRepositoryBase<WsSqlPluGroupFkModel>
{
    public List<WsSqlPluGroupFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPluGroupFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPluGroupFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.PluGroup.Name)
                .ThenBy(item => item.Parent.Name);
        return items.ToList();
    }
}