namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

public class WsSqlTaskTypeRepository : WsSqlTableRepositoryBase<WsSqlTaskTypeModel>
{
    public List<WsSqlTaskTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.Name)));
        return SqlCore.GetListNotNullable<WsSqlTaskTypeModel>(sqlCrudConfig);
    }
}