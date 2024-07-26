using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features;

public class TemplateService : BaseRepository
{
    [Transactional]
    public Template GetItemByUid(Guid uid) => Session.Get<Template>(uid) ?? new();
}