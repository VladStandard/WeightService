namespace WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;

public class WsSqlScaleScreenshotRepository: WsSqlTableRepositoryBase<WsSqlScaleScreenShotModel>
{

    public WsSqlScaleScreenShotModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlScaleScreenShotModel>(uid);

    public List<WsSqlScaleScreenShotModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.ChangeDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetEnumerableNotNullable<WsSqlScaleScreenShotModel>(sqlCrudConfig).ToList();
    }
}