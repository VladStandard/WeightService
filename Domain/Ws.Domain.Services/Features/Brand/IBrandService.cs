using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Brand;

public interface IBrandService : IAll<BrandEntity>, IUid<BrandEntity>, IUid1C<BrandEntity>;