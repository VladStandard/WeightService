using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Pallets;

public sealed class SqlPalletRepository : BaseRepository, ISave<Pallet>
{
    public Pallet Save(Pallet item) { Session.Save(item); return item; }
}