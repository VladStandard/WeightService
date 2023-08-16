namespace WsStorageCore.Tables.TableScaleModels.PlusGroups;

public class WsSqlPluGroupRepository : WsSqlTableRepositoryBase<WsSqlPluGroupModel>
{
    
    public WsSqlPluGroupModel GetItemParentFromChildGroup(WsSqlPluGroupModel pluGroup)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlPluGroupFkModel.PluGroup), pluGroup);
        return SqlCore.GetItemByCrud<WsSqlPluGroupFkModel>(sqlCrudConfig).Parent;
    }

    public IEnumerable<WsSqlPluGroupModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlPluGroupModel>(sqlCrudConfig);
    }
}