namespace WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

public class WsSqlPluStorageMethodRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodModel>
{
    public List<WsSqlPluStorageMethodModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.Name)));
        return SqlCore.GetListNotNullable<WsSqlPluStorageMethodModel>(sqlCrudConfig);
    }
}