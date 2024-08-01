using Ws.Labels.Service.Generate.Common;

namespace Ws.Labels.Service.Generate.Features.Piece.Dto;

public static class LabelPiecePalletDtoMapper
{
    public static BarcodeModel ToBarcodeModel(this GeneratePiecePalletDto palletDto)
    {
        return new()
        {
            Kneading = palletDto.Kneading,
            BundleCount = (short)palletDto.Nesting.BundleCount,
            ProductDt = palletDto.ProductDt,
            LineNumber = palletDto.Line.Number,
            LineCounter = palletDto.Line.Counter,
            PluNumber = palletDto.Plu.Number,
            PluGtin = palletDto.Plu.Gtin,
            PluEan13 = palletDto.Plu.Ean13,
            WeightNet = palletDto.Nesting.CalculateWeightNet(palletDto.Plu),
            ExpirationDt = palletDto.ExpirationDt,
            ExpirationDay = palletDto.ExpirationDt.DayOfYear
        };
    }
}