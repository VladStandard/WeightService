using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.ZplResource;

public interface IZplResourceService : IGetItemByUid<TemplateResourceEntity>, IGetAll<TemplateResourceEntity>;