using Mapster;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.WebApiScales.Features.Nesting.Dto;

namespace Ws.WebApiScales.Features.Nesting.Mapster;

public class PluCharacteristicDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PluCharacteristicDto, PluNestingEntity>()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.BundleCount, s => s.AttachmentsCountAsInt)
            .Map(d => d.Uid1C, s => s.Guid)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}