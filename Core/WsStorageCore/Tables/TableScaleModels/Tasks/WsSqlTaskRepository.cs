namespace WsStorageCore.Tables.TableScaleModels.Tasks;

public class WsSqlTaskRepository : WsSqlTableRepositoryBase<WsSqlTaskModel>
{
    public List<WsSqlTaskModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlTaskModel> items = SqlCore.GetEnumerableNotNullable<WsSqlTaskModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Scale.Description);
        return items.ToList();
    }
}