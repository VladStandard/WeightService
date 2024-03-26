using Mapster;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.WebApiScales.Features.Brands.Dto;

namespace Ws.WebApiScales.Features.Brands.Mapster;

internal sealed class BrandDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BrandDto, BrandEntity>()
            .Map(d => d.Uid1C, s => s.Uid)
            .Map(d => d.Name, s => s.Name)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}