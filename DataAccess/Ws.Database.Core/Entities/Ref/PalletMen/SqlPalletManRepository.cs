using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PalletMen;

public class SqlPalletManRepository : IGetItemByUid<PalletManEntity>, IGetAll<PalletManEntity>
{
    public PalletManEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<PalletManEntity>(uid);
    
    public IEnumerable<PalletManEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<PalletManEntity>()
            .AddOrder(SqlOrder.Asc(nameof(PalletManEntity.Surname)));
        return SqlCoreHelper.Instance.GetEnumerable<PalletManEntity>(criteria);
    }
}