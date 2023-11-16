namespace WsStorageCore.Views.ViewRefModels.PluLines;

public interface IViewPluLineRepository
{
    IEnumerable<SqlViewPluLineModel> GetEnumerable(SqlCrudConfigModel sqlCrudConfig);
}