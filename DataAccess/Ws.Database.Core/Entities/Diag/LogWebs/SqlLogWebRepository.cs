using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.LogWebs;

public class SqlLogWebRepository : IUidRepo<LogWebEntity>
{
    public LogWebEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LogWebEntity>(uid);
    
    public IEnumerable<LogWebEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.SelectTopRowsCount = 500;
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCoreHelper.Instance.GetEnumerable<LogWebEntity>(sqlCrudConfig).ToList();
    }
}