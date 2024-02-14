using Ws.Database.Core.Entities.Scales.Templates;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.Template;

public class TemplateService : ITemplateService
{
    public IEnumerable<TemplateEntity> GetAll() => new SqlTemplateRepository().GetAll();
    public TemplateEntity GetItemByUid(Guid uid) => new SqlTemplateRepository().GetByUid(uid);
}