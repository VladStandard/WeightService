using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Templates;

public interface ITemplateService :
    IGetItemByUid<Template>,
    IGetAll<Template>,
    ICreate<Template>,
    IUpdate<Template>,
    IDelete<Template>
{
    IEnumerable<Template> GetTemplatesByIsWeight(bool isWeight);
    string? GetTemplateByUidFromCacheOrDb(Guid templateUid);
}