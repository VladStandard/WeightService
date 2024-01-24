using Mapster;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.WebApiScales.Features.Brand.Dto;

namespace Ws.WebApiScales.Features.Brand.Mapster;

internal sealed class BrandDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BrandDto, BrandEntity>()
            .Map(d => d.Uid1C, s => s.Guid)
            .Map(d => d.Code, s => s.Code)
            .Map(d => d.Name, s => s.Name)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}