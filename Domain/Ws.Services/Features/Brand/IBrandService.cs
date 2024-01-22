using Ws.Domain.Models.Entities.Ref1c;
using Ws.Services.Common;

namespace Ws.Services.Features.Brand;

public interface IBrandService : IAll<BrandEntity>, IUid<BrandEntity>, IUid1C<BrandEntity>;