namespace WsStorageCore.Tables.TableScaleModels.PlusGroups;

public class WsSqlPluGroupRepository : WsSqlTableRepositoryBase<WsSqlPluGroupModel>
{
    public List<WsSqlPluGroupModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePlusGroups(sqlCrudConfig);

}