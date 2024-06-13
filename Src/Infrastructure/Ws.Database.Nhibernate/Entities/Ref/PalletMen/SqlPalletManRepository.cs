using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Database.Nhibernate.Entities.Ref.PalletMen;

public class SqlPalletManRepository : BaseRepository, IGetItemByUid<PalletMan>, IGetAll<PalletMan>,
    ISave<PalletMan>, IUpdate<PalletMan>, IDelete<PalletMan>
{
    public PalletMan GetByUid(Guid uid) => Session.Get<PalletMan>(uid) ?? new();
    public IList<PalletMan> GetAll() => Session.Query<PalletMan>().OrderBy(i => i.Fio.Surname).ToList();
    public PalletMan Save(PalletMan item) { Session.Save(item); return item; }
    public PalletMan Update(PalletMan item) { Session.Update(item); return item; }
    public void Delete(PalletMan item) => Session.Delete(item);
}