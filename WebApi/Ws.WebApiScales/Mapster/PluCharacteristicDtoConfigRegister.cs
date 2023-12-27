using Mapster;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.WebApiScales.Dto.PluCharacteristic;

namespace Ws.WebApiScales.Mapster;

public class PluCharacteristicDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PluCharacteristicDto, SqlPluNestingFkEntity>()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.IsMarked, s => s.IsMarked)
            .Map(d => d.BundleCount, s => s.AttachmentsCountAsInt)
            .Map(d => d.Uid1C, s => s.Guid)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}