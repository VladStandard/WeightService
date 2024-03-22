using Mapster;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.WebApiScales.Features.Bundles.Dto;

namespace Ws.WebApiScales.Features.Bundles.Mapster;

internal sealed class BundleDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BundleDto, BundleEntity>()
            .Map(d => d.Uid1C, s => s.Uid)
            .Map(d => d.Weight, s => s.Weight)
            .Map(d => d.Name, s => s.Name)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}