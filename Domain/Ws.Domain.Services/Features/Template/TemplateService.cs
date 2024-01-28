using Ws.Database.Core.Entities.Scales.Templates;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.Template;

public class TemplateService : ITemplateService
{
    public TemplateEntity GetById(long id) => new SqlTemplateRepository().GetById(id);
    public IEnumerable<TemplateEntity> GetAll() => new SqlTemplateRepository().GetAll();
}