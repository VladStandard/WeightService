using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Brands;

public interface IBrandService : IGetAll<Brand>, IGetItemByUid<Brand>, IDelete<Brand>;