using Mapster;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;
using Ws.WebApiScales.Dto.Brand;

namespace Ws.WebApiScales.Mapster;

public class BrandDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BrandDto, SqlBrandEntity>()
            .Map(d => d.Uid1C, s => s.Guid)
            .Map(d => d.Code, s => s.Code)
            .Map(d => d.IsMarked, s => s.IsMarked)
            .Map(d => d.Name, s => s.Name)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}