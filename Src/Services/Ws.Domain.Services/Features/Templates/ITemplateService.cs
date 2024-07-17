using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Templates;

public interface ITemplateService :
    IGetItemByUid<Template>,
    IGetAll<Template>,
    ICreate<Template>,
    IUpdate<Template>,
    IDelete<Guid>
{
    IList<Template> GetTemplatesByIsWeight(bool isWeight);
}