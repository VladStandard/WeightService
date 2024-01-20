using Ws.Domain.Models.Entities.Print;

namespace Ws.StorageCore.Entities.Print.ViewLabels;

public sealed class ViewLabelRepository : SqlTableRepositoryBase<ViewLabel>
{
    public List<ViewLabel> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<ViewLabel>(sqlCrudConfig).ToList();
    }
}