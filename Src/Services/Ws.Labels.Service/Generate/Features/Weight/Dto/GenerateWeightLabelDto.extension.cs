using Ws.Labels.Service.Generate.Common;

namespace Ws.Labels.Service.Generate.Features.Weight.Dto;

internal static class GenerateWeightPluDtoExtension
{
    internal static BarcodeModel ToBarcodeModel(this GenerateWeightLabelDto dto) =>
        new()
        {
            LineNumber = dto.Line.Number,
            LineCounter = dto.Line.Counter,
            ProductDt = dto.ProductDt,
            PluGtin = dto.Plu.Gtin,
            PluNumber = dto.Plu.Number,
            WeightNet = dto.Weight,
            Kneading = dto.Kneading,
            PluEan13 = dto.Plu.Ean13,
            ExpirationDt = dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),
            BundleCount = dto.Plu.PluNesting.BundleCount
        };
}