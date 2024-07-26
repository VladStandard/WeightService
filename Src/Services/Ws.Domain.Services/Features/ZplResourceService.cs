using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features;

public class ZplResourceService : BaseRepository
{
    [Transactional]
    public IList<ZplResource> GetAll() => Session.Query<ZplResource>()
        .OrderBy(i => i.Type).ThenBy(i => i.Name).ToList();
}