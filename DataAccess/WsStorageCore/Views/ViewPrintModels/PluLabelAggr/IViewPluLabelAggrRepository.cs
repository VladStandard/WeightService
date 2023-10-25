namespace WsStorageCore.Views.ViewPrintModels.PluLabelAggr;

public interface IViewPluLabelAggrRepository
{
    List<WsSqlViewPluLabelAggrModel> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}