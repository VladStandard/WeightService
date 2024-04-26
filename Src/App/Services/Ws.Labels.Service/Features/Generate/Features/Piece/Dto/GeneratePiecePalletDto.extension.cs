using Ws.Labels.Service.Features.Generate.Features.Piece.Models;

namespace Ws.Labels.Service.Features.Generate.Features.Piece.Dto;

public static class LabelPiecePalletDtoMapper
{
    public static XmlPieceLabel AdaptToXmlPieceLabel(this GeneratePiecePalletDto palletDto)
    {
        return new XmlPieceLabel
        {
            Kneading = palletDto.Kneading,
            ExpirationDtValue = palletDto.ExpirationDt,
            ProductDtValue = palletDto.ProductDt,
            LineNumber = palletDto.Line.Number,
            LineCounter = palletDto.Line.Counter,
            LineName = palletDto.Line.Name,
            LineAddress = palletDto.Line.Warehouse.ProductionSite.Address,
            PluNumber = palletDto.Plu.Number,
            PluGtin = palletDto.Plu.Gtin,
            PluFullName = palletDto.Plu.FullName,
            PluDescription = palletDto.Plu.Description
        };
    }
}