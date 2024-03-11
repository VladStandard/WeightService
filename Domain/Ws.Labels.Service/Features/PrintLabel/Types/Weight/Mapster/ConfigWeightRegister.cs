using Mapster;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Models;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Weight.Mapster;

public class ConfigWeightRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LabelWeightDto, XmlWeightLabelModel>()
            .Map(d => d.LineName, s => s.Line.Name)
            .Map(d => d.LineAddress, s => s.Line.Warehouse.ProductionSite.Address)
            .Map(d => d.LineNumber, s => s.Line.Number)
            .Map(d => d.LineCounter, s => s.Line.Counter)
            .Map(d => d.ProductDtValue, s => s.ProductDt)
            .Map(d => d.ExpirationDtValue, s => s.ExpirationDt)
            .Map(d => d.PluGtin, s => s.Nesting.Plu.Gtin)
            .Map(d => d.PluNumber, s => s.Nesting.Plu.Number)
            .Map(d => d.PluFullName, s => s.Nesting.Plu.FullName)
            .Map(d => d.PluDescription, s => s.Nesting.Plu.Description)
            .Map(d => d.Kneading, s => s.Kneading)
            .Map(d => d.Weight, s => s.Weight)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.Map);
    }
}