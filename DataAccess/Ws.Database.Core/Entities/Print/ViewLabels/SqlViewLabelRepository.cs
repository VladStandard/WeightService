using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.ViewLabels;

public sealed class ViewLabelRepository : IGetAll<ViewLabel>
{
    public IEnumerable<ViewLabel> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<ViewLabel>().AddOrder(SqlOrder.CreateDtDesc());
        return SqlCoreHelper.Instance.GetEnumerable<ViewLabel>(criteria).ToList();
    }
}