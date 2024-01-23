using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PalletMen;

public class SqlPalletManRepository : IUidRepo<PalletManEntity>
{
    public PalletManEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<PalletManEntity>(uid);
    
    public IEnumerable<PalletManEntity> GetAll()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.Asc(nameof(PalletManEntity.Surname)));
        return SqlCoreHelper.Instance.GetEnumerable<PalletManEntity>(crud);
    }
}