using Mapster;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.WebApiScales.Features.Plu.Dto;

namespace Ws.WebApiScales.Features.Plu.Mapster;

internal sealed class PluDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PluDto, ClipEntity>()
            .Map(d => d.Uid1C, s => s.ClipTypeGuid)
            .Map(d => d.Name, s => s.ClipTypeName)
            .Map(d => d.Weight, s => s.ClipTypeWeight)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
        
        config.NewConfig<PluDto, BoxEntity>()
            .Map(d => d.Uid1C, s => s.BoxTypeGuid)
            .Map(d => d.Name, s => s.BoxTypeName)
            .Map(d => d.Weight, s => s.BoxTypeWeight)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
        
        config.NewConfig<PluDto, BundleEntity>()
            .Map(d => d.Uid1C, s => s.PackageTypeGuid)
            .Map(d => d.Name, s => s.PackageTypeName)
            .Map(d => d.Weight, s => s.PackageTypeWeight)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
        
        config.NewConfig<PluDto, PluEntity>()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.FullName, s => s.FullName)
            .Map(d => d.Description, s => s.Description)
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