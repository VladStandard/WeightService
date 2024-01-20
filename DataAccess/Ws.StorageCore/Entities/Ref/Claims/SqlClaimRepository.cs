using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCore.Entities.Ref.Claims;

public sealed class SqlClaimRepository : SqlTableRepositoryBase<ClaimEntity>
{
    public IEnumerable<ClaimEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<ClaimEntity>(crud);
    }
}