using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.Labels;

public sealed class SqlLabelRepository : IUidRepo<LabelEntity>
{
    public LabelEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LabelEntity>(uid);
    
    public IEnumerable<LabelEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCoreHelper.Instance.GetEnumerable<LabelEntity>(sqlCrudConfig).ToList();
    }
}