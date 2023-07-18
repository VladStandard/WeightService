namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

public class WsSqlTaskTypeRepository : WsSqlTableRepositoryBase<WsSqlTaskTypeModel>
{
    public List<WsSqlTaskTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableTasksTypes(sqlCrudConfig);
}