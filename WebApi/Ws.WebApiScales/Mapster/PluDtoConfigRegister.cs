using Mapster;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Bundles;
using Ws.StorageCore.Entities.SchemaRef1c.Clips;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.WebApiScales.Dto.Plu;

namespace Ws.WebApiScales.Mapster;

public class PluDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PluDto, SqlClipEntity>()
            .Map(d => d.Uid1C, s => s.ClipTypeGuid)
            .Map(d => d.Name, s => s.ClipTypeName)
            .Map(d => d.Weight, s => s.ClipTypeWeight)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
        
        config.NewConfig<PluDto, SqlBoxEntity>()
            .Map(d => d.Uid1C, s => s.BoxTypeGuid)
            .Map(d => d.Name, s => s.BoxTypeName)
            .Map(d => d.Weight, s => s.BoxTypeWeight)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
        
        config.NewConfig<PluDto, SqlBundleEntity>()
            .Map(d => d.Uid1C, s => s.PackageTypeGuid)
            .Map(d => d.Name, s => s.PackageTypeName)
            .Map(d => d.Weight, s => s.PackageTypeWeight)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
        
        config.NewConfig<PluDto, SqlPluEntity>()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.FullName, s => s.FullName)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.IsMarked, s => s.IsMarked)
            .Map(d => d.IsGroup, s => s.IsGroup)
            .Map(d => d.Number, s => s.PluNumber)
            .Map(d => d.ShelfLifeDays, s => s.ShelfLife)
            .Map(d => d.IsCheckWeight, s => s.IsCheckWeight)
            .Map(d => d.Code, s => s.Code)
            .Map(d => d.Ean13, s => s.Ean13)
            .Map(d => d.Itf14, s =>  s.IsCheckWeight == true ?  "" : s.Itf14)
            .Map(d => d.Gtin, s => s.IsCheckWeight == true ?  "0" + s.Ean13 : s.Itf14)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}