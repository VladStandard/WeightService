using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Domain.Services.Features;

public class PalletManService : BaseRepository
{
    [Transactional]
    public PalletMan GetItemByUid(Guid uid) => Session.Get<PalletMan>(uid) ?? new();
}