namespace WsStorageCore.Views.ViewRefModels.PluLines;

public interface IViewPluLineRepository
{
    IEnumerable<WsSqlViewPluLineModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig);
}