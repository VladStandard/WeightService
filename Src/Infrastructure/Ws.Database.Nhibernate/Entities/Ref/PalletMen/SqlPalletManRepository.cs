using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Database.Nhibernate.Entities.Ref.PalletMen;

public class SqlPalletManRepository : BaseRepository, IGetItemByUid<PalletMan>,
    ISave<PalletMan>, IUpdate<PalletMan>, IDelete<PalletMan>
{
    public List<PalletMan> GetAllByProductionSite(ProductionSite site) =>
        Session.Query<PalletMan>().Where(i => i.Warehouse.ProductionSite == site)
            .OrderBy(i => i.Warehouse.Name).ThenBy(i => i.Fio.Surname).ToList();
    public PalletMan GetByUid(Guid uid) => Session.Get<PalletMan>(uid) ?? new();
    public PalletMan Save(PalletMan item) { Session.Save(item); return item; }
    public PalletMan Update(PalletMan item) { Session.Update(item); return item; }
    public void Delete(PalletMan item) => Session.Delete(item);
}