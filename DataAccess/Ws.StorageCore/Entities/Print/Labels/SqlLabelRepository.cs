using Ws.Domain.Models.Entities.Print;

namespace Ws.StorageCore.Entities.Print.Labels;

public sealed class SqlLabelRepository : SqlTableRepositoryBase<LabelEntity>
{
    public IEnumerable<LabelEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<LabelEntity>(sqlCrudConfig).ToList();
    }
    
    public LabelEntity GetItem(SqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetItemByCrud<LabelEntity>(sqlCrudConfig);
    }
}