using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.ZplResources;

public interface IZplResourceService : IGetItemByUid<ZplResource>, IGetAll<ZplResource>,
    ICreate<ZplResource>, IUpdate<ZplResource>, IDelete<ZplResource>;