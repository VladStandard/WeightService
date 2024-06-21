using Ws.Labels.Service.Generate.Features.Piece.Models;

namespace Ws.Labels.Service.Generate.Features.Piece.Dto;

public static class LabelPiecePalletDtoMapper
{
    public static PieceLabelLabel AdaptToXmlPieceLabel(this GeneratePiecePalletDto palletDto)
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
        };
    }
}