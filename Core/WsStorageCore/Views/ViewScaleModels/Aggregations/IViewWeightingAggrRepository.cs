namespace WsStorageCore.Views.ViewScaleModels.Aggregations;

public interface IViewWeightingAggrRepository
{
    List<WsSqlViewWeightingAggrModel> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}