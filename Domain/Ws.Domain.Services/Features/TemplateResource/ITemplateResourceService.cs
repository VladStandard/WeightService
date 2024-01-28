using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.TemplateResource;

public interface ITemplateResourceService : IGetItemByUid<TemplateResourceEntity>, IGetAll<TemplateResourceEntity>;