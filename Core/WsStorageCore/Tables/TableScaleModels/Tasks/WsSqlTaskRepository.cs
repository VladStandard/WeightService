namespace WsStorageCore.Tables.TableScaleModels.Tasks;

public class WsSqlTaskRepository : WsSqlTableRepositoryBase<WsSqlTaskModel>
{
    public List<WsSqlTaskModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlTaskModel> list = SqlCore.GetListNotNullable<WsSqlTaskModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            list = list.OrderBy(item => item.Scale.Description).ToList();
        return list;
    }
}