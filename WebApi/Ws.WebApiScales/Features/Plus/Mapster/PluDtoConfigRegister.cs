using Mapster;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.WebApiScales.Features.Plus.Dto;

namespace Ws.WebApiScales.Features.Plus.Mapster;

internal sealed class PluDtoConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PluDto, PluEntity>()
            .Map(d => d.Uid1C, s => s.Uid)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.FullName, s => s.FullName)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Number, s => s.Number)
            .Map(d => d.ShelfLifeDays, s => s.ShelfLifeDays)
            .Map(d => d.IsCheckWeight, s => s.IsCheckWeight)
            .Map(d => d.Ean13, s => s.Ean13)
            .Map(d => d.Itf14, s => s.IsCheckWeight == true ? "" : s.Itf14)
            .Map(d => d.Gtin, s => s.IsCheckWeight == true ? "0" + s.Ean13 : s.Itf14)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
}