using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PalletMen;

public class SqlPalletManRepository : BaseRepository, IGetItemByUid<PalletManEntity>, IGetAll<PalletManEntity>
{
    public PalletManEntity GetByUid(Guid uid) => Session.Get<PalletManEntity>(uid) ?? new();

    public IEnumerable<PalletManEntity> GetAll() => Session.Query<PalletManEntity>().OrderBy(i => i.Surname).ToList();
}