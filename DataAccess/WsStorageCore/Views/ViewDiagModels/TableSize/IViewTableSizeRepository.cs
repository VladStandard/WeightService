namespace WsStorageCore.Views.ViewDiagModels.TableSize;

public interface IViewTableSizeRepository
{
    IEnumerable<WsSqlViewTableSizeModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig);
}