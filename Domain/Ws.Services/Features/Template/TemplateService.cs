using Ws.Database.Core.Entities.Scales.Templates;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Services.Features.Template;

public class TemplateService : ITemplateService
{
    public TemplateEntity GetById(long id) => SqlCoreHelper.Instance.GetItemById<TemplateEntity>(id);
    public IEnumerable<TemplateEntity> GetAll() => new SqlTemplateRepository().GetList(new());
}