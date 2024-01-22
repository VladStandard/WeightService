using Ws.Domain.Models.Entities.Scale;
using Ws.StorageCore.Entities.Scales.Templates;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Template;

public class TemplateService : ITemplateService
{
    public TemplateEntity GetById(long id) => SqlCoreHelper.Instance.GetItemById<TemplateEntity>(id);
    public IEnumerable<TemplateEntity> GetAll() => new SqlTemplateRepository().GetList(new());
}