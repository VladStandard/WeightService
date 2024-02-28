using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PalletMen;

public class SqlPalletManRepository : BaseRepository, IGetItemByUid<PalletManEntity>, IGetAll<PalletManEntity>,
    ISave<PalletManEntity>, IUpdate<PalletManEntity>, IDelete<PalletManEntity>
{
    public PalletManEntity GetByUid(Guid uid) => Session.Get<PalletManEntity>(uid) ?? new();
    public IEnumerable<PalletManEntity> GetAll() => Session.Query<PalletManEntity>().OrderBy(i => i.Surname).ToList();
    public PalletManEntity Save(PalletManEntity item) { Session.Save(item); return item; }
    public PalletManEntity Update(PalletManEntity item) { Session.Update(item); return item; }
    public void Delete(PalletManEntity item) => Session.Delete(item);
}