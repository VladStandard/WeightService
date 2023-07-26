namespace WsStorageCore.Tables.TableScaleModels.PlusGroups;

public class WsSqlPluGroupRepository : WsSqlTableRepositoryBase<WsSqlPluGroupModel>
{
    
    public WsSqlPluGroupModel GetItemParentFromChildGroup(WsSqlPluGroupModel pluGroup)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudConfig(WsSqlCrudConfigModel.GetFilters(
                $"{nameof(WsSqlPluGroupFkModel.PluGroup)}.{nameof(WsSqlTableBase.IdentityValueUid)}", pluGroup.IdentityValueUid),
            WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlPluGroupFkModel>(sqlCrudConfig).Parent;
    }

    public List<WsSqlPluGroupModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        return SqlCore.GetListNotNullable<WsSqlPluGroupModel>(sqlCrudConfig);
    }

}