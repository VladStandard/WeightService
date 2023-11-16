namespace WsStorageCore.Views.ViewDiagModels.TableSize;

public interface IViewTableSizeRepository
{
    IEnumerable<SqlViewTableSizeModel> GetEnumerable(SqlCrudConfigModel sqlCrudConfig);
}