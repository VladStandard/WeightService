using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Template;

public interface ITemplateService : IGetAll<TemplateEntity>, IGetItemByUid<TemplateEntity>;