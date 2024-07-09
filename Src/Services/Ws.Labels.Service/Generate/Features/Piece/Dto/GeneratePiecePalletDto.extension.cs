using Ws.Labels.Service.Generate.Common;

namespace Ws.Labels.Service.Generate.Features.Piece.Dto;

public static class LabelPiecePalletDtoMapper
{
    public static BarcodeModel ToBarcodeModel(this GeneratePiecePalletDto palletDto)
    {
        return new()
        {
            Kneading = palletDto.Kneading,
            BundleCount = palletDto.PluCharacteristic.BundleCount,
            ProductDt = palletDto.ProductDt,
            LineNumber = palletDto.Line.Number,
            LineCounter = palletDto.Line.Counter,
            PluNumber = palletDto.Plu.Number,
            PluGtin = palletDto.Plu.Gtin,
            PluEan13 = palletDto.Plu.Ean13,
            WeightNet = palletDto.Plu.Weight,
            ExpirationDt = palletDto.ProductDt.AddDays(palletDto.Plu.ShelfLifeDays)
        };
    }
}