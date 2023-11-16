namespace Ws.StorageCore.Views.ViewRefModels.PluLines;

public interface IViewPluLineRepository
{
    IEnumerable<SqlViewPluLineModel> GetEnumerable(SqlCrudConfigModel sqlCrudConfig);
}