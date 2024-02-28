using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Template;

public interface ITemplateService : IGetAll<TemplateEntity>, IGetItemByUid<TemplateEntity>,
    ICreate<TemplateEntity>, IUpdate<TemplateEntity>;