using Ws.Database.Entities.Ref1C.Brands;
using Ws.DeviceControl.Models.Features.References1C.Brands;

namespace Ws.DeviceControl.Api.App.Features.References1C.Brands.Impl.Expressions;

public static class BrandExpressions
{
    public static Expression<Func<BrandEntity, BrandDto>> ToDto =>
        brand => new()
        {
            Id = brand.Id,
            Name = brand.Name,
            CreateDt = brand.CreateDt,
            ChangeDt = brand.ChangeDt
        };
}