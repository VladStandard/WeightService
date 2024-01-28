using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Brand;

public interface IBrandService : IGetAll<BrandEntity>, IGetItemByUid<BrandEntity>, IGetItemByUid1C<BrandEntity>
{
    BrandEntity GetDefault();
}