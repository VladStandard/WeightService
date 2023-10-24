namespace WsStorageCore.Entities.SchemaScale.Tasks;

public class WsSqlTaskRepository : WsSqlTableRepositoryBase<WsSqlTaskEntity>
{
    public List<WsSqlTaskEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlTaskEntity> items = SqlCore.GetEnumerable<WsSqlTaskEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Scale.Description);
        return items.ToList();
    }
}