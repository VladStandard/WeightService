using Ws.Domain.Models.Entities.Print;
using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCore.Entities.Print.ViewLabels;

public sealed class ViewLabelRepository : SqlTableRepositoryBase<ViewLabel>
{
    public List<ViewLabel> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<ViewLabel>(sqlCrudConfig).ToList();
    }
}