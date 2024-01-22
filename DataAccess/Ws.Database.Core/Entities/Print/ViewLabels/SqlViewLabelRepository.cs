using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.ViewLabels;

public sealed class ViewLabelRepository
{
    public IEnumerable<ViewLabel> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCoreHelper.Instance.GetEnumerable<ViewLabel>(sqlCrudConfig).ToList();
    }
}