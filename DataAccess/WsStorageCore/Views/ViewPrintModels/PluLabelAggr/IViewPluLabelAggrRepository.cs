namespace WsStorageCore.Views.ViewPrintModels.PluLabelAggr;

public interface IViewPluLabelAggrRepository
{
    List<SqlViewPluLabelAggrModel> GetList(SqlCrudConfigModel sqlCrudConfig);
}