namespace WsStorageCore.Tables.TableScaleModels.Tasks;

public class WsSqlTaskRepository : WsSqlTableRepositoryBase<WsSqlTaskModel>
{
    public List<WsSqlTaskModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableTasks(sqlCrudConfig);
}