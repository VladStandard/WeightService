namespace Ws.StorageCore.Entities.SchemaRef.Claims;

public sealed class SqlClaimRepository : SqlTableRepositoryBase<SqlClaimEntity>
{
    public IEnumerable<SqlClaimEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlClaimEntity>(crud);
    }
}