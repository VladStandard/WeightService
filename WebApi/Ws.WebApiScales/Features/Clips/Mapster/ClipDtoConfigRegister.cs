using Mapster;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.WebApiScales.Features.Clips.Dto;

namespace Ws.WebApiScales.Features.Clips.Mapster;

internal sealed class ClipDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClipDto, ClipEntity>()
            .Map(d => d.Uid1C, s => s.Uid)
            .Map(d => d.Weight, s => s.Weight)
            .Map(d => d.Name, s => s.Name)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}