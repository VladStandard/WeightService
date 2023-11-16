using Ws.StorageCore.Models;
namespace Ws.StorageCore.Views.ViewPrintModels.PluLabelAggr;

public interface IViewPluLabelAggrRepository
{
    List<SqlViewPluLabelAggrModel> GetList(SqlCrudConfigModel sqlCrudConfig);
}