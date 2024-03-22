using Mapster;
using Ws.Domain.Models.Entities.Scale;
using Ws.WebApiScales.Features.Characteristics.Dto;

namespace Ws.WebApiScales.Features.Characteristics.Mapster;

internal sealed class PluCharacteristicDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CharacteristicDto, PluNestingEntity>()
            .Map(d => d.BundleCount, s => s.BundleCount)
            .Map(d => d.Uid1C, s => s.Uid)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}