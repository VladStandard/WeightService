namespace WsStorageCore.Views.ViewRefModels.PluLines;

public interface IViewPluLineRepository
{
    List<WsSqlViewPluLineModel> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}