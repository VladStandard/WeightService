namespace WsStorageCore.Views.ViewDiagModels.TableSize;

public interface IViewTableSizeRepository
{
    List<WsSqlViewTableSizeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}