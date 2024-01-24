using Mapster;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.WebApiScales.Features.Box.Dto;

namespace Ws.WebApiScales.Features.Box.Mapster;

internal sealed class BoxDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BoxDto, BoxEntity>()
            .Map(d => d.Uid1C, s => s.Uid)
            .Map(d => d.Weight, s => s.Weight)
            .Map(d => d.Name, s => s.Name)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}