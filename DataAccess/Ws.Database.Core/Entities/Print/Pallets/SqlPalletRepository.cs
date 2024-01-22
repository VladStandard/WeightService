using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.Pallets;

public sealed class SqlPalletRepository : SqlTableRepositoryBase<PalletEntity>
{
    public IEnumerable<PalletEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(new (nameof(PalletEntity.CreateDt), false));
        return SqlCore.GetEnumerable<PalletEntity>(sqlCrudConfig);
    }
}