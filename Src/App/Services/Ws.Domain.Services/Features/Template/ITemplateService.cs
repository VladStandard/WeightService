using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Template;

public interface ITemplateService : IGetAll<Models.Entities.Print.Template>, IGetItemByUid<Models.Entities.Print.Template>,
    ICreate<Models.Entities.Print.Template>, IUpdate<Models.Entities.Print.Template>, IDelete<Models.Entities.Print.Template>
{
    IEnumerable<Models.Entities.Print.Template> GetTemplatesByIsWeight(bool isWeight);
    string? GetTemplateByUidFromCacheOrDb(Guid templateUid);
}