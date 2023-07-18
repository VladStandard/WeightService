namespace WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

public class WsSqlPluStorageMethodRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodModel>
{
    public List<WsSqlPluStorageMethodModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePlusStoragesMethods(sqlCrudConfig);
}