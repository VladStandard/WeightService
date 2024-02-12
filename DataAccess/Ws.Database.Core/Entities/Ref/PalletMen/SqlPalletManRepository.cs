using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PalletMen;

public class SqlPalletManRepository : BaseRepository, IGetItemByUid<PalletManEntity>, IGetAll<PalletManEntity>
{
    public PalletManEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<PalletManEntity>(uid);
    
    public IEnumerable<PalletManEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<PalletManEntity>().OrderBy(i => i.Surname).Asc
        );
    }
}