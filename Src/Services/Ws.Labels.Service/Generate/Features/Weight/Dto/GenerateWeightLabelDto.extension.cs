using Ws.Labels.Service.Generate.Features.Weight.Models;

namespace Ws.Labels.Service.Generate.Features.Weight.Dto;

internal static class GenerateWeightPluDtoExtension
{
    internal static BarcodeWeightLabel ToBarcodeModel(this GenerateWeightLabelDto dto) =>
        new()
        {
            LineNumber = dto.Line.Number,
            LineCounter = dto.Line.Counter,
            ProductDt = dto.ProductDt,
            PluGtin = dto.Plu.Gtin,
            PluNumber = dto.Plu.Number,
            Weight = dto.Weight,
            Kneading = dto.Kneading,
            PluEan13 = dto.Plu.Ean13
        };
}