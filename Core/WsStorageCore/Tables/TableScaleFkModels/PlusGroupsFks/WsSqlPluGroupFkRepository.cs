namespace WsStorageCore.Tables.TableScaleFkModels.PlusGroupsFks;

public sealed class WsSqlPluGroupFkRepository : WsSqlTableRepositoryBase<WsSqlPluGroupFkModel>
{
    public List<WsSqlPluGroupFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePlusGroupsFks(sqlCrudConfig);
}