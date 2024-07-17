using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Brands;

public interface IBrandService : IGetAll<Brand>, IGetItemByUid<Brand>, IDelete<Brand>;