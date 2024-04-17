using XmlWeightLabelModel=Ws.Labels.Service.Features.PrintLabel.Features.Weight.Models.XmlWeightLabel.XmlWeightLabelModel;

namespace Ws.Labels.Service.Features.PrintLabel.Features.Weight.Dto.PrintWeightPlu;

internal static class GenerateWeightPluDtoExtension
{
    internal static XmlWeightLabelModel ToXmlWeightLabel(this GenerateWeightLabelDto dto) =>
        new()
        {
            LineName = dto.Line.Name,
            LineAddress = dto.Line.Warehouse.ProductionSite.Address,
            LineNumber = dto.Line.Number,
            LineCounter = dto.Line.Counter,
            ProductDtValue = dto.ProductDt,
            ExpirationDtValue = dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),
            PluGtin = dto.Plu.Gtin,
            PluNumber = dto.Plu.Number,
            PluFullName = dto.Plu.FullName,
            PluDescription = dto.Plu.Description,
            Weight = dto.Weight,
            Kneading = dto.Kneading,
        };
}