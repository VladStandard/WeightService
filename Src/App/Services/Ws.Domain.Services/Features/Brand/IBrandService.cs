using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Brand;

public interface IBrandService : IGetAll<Models.Entities.Ref1c.Brand>, IGetItemByUid<Models.Entities.Ref1c.Brand>,
    IDelete<Models.Entities.Ref1c.Brand>;