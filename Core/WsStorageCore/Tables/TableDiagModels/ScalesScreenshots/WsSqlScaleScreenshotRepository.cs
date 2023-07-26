// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;

public class WsSqlScaleScreenshotRepository: WsSqlTableRepositoryBase<WsSqlScaleScreenShotModel>
{

    public WsSqlScaleScreenShotModel GetItemByUid(Guid uid)
    {
        return SqlCore.GetItemNotNullable<WsSqlScaleScreenShotModel>(uid);
    }
    
    public List<WsSqlScaleScreenShotModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlScaleScreenShotModel> list = SqlCore.GetListNotNullable<WsSqlScaleScreenShotModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }
}