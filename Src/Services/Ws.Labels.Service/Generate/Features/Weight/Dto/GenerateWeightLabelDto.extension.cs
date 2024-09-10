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
            PluEan13 = dto.Plu.Ean13,
            WeightNet = dto.Plu.Weight,
            Kneading = dto.Kneading,
            ExpirationDt = dto.ExpirationDt,
            BundleCount = (short)dto.Nesting.BundleCount,
            ExpirationDay = dto.ExpirationDt.DayOfYear
        };
}