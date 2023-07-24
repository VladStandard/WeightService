namespace WsStorageCore.Tables.TableScaleModels.PlusGroups;

public class WsSqlPluGroupRepository : WsSqlTableRepositoryBase<WsSqlPluGroupModel>
{
    
    public WsSqlPluGroupModel GetItemParentFromChildGroup(WsSqlPluGroupModel pluGroup)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(WsSqlCrudConfigModel.GetFilters(
                $"{nameof(WsSqlPluGroupFkModel.PluGroup)}.{nameof(WsSqlTableBase.IdentityValueUid)}", pluGroup.IdentityValueUid),
            WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlPluGroupFkModel>(sqlCrudConfig).Parent;
    }

    public List<WsSqlPluGroupModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPluGroupModel> list = SqlCore.GetListNotNullable<WsSqlPluGroupModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

}