namespace WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;

public class WsSqlScaleScreenshotRepository: WsSqlTableRepositoryBase<WsSqlScaleScreenShotModel>
{
    public List<WsSqlScaleScreenShotModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        ContextList.GetListNotNullableScaleScreenShots(sqlCrudConfig);
}