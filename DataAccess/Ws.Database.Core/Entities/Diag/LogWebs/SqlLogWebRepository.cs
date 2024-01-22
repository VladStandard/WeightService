using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.LogWebs;

public class SqlLogWebRepository : SqlTableRepositoryBase<LogWebEntity>
{
    public IEnumerable<LogWebEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.SelectTopRowsCount = 500;
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<LogWebEntity>(sqlCrudConfig).ToList();
    }
}