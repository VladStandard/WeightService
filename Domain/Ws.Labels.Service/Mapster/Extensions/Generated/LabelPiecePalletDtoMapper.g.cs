using Ws.Labels.Service.Features.PrintLabel.Features.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Features.Piece.Models;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto
{
    public static partial class LabelPiecePalletDtoMapper
    {
        public static XmlPieceLabelModel AdaptToXmlPieceLabelModel(this GeneratePiecePalletDto p1)
        {
            return p1 == null ? null : new XmlPieceLabelModel()
            {
                Kneading = p1.Kneading,
                ExpirationDtValue = p1.ExpirationDt,
                ProductDtValue = p1.ProductDt,
                LineNumber = funcMain1(p1.Line == null ? null : (int?)p1.Line.Number),
                LineCounter = funcMain2(p1.Line == null ? null : (int?)p1.Line.Counter),
                LineName = p1.Line == null ? null : p1.Line.Name,
                LineAddress = p1.Line == null ? null : (p1.Line.Warehouse == null ? null : (p1.Line.Warehouse.ProductionSite == null ? null : p1.Line.Warehouse.ProductionSite.Address)),
                PluNumber = funcMain3(p1.Plu == null ? null : (short?)p1.Plu.Number),
                PluGtin = p1.Plu == null ? null : p1.Plu.Gtin,
                PluFullName = p1.Plu == null ? null : p1.Plu.FullName,
                PluDescription = p1.Plu == null ? null : p1.Plu.Description
            };
        }
        
        private static int funcMain1(int? p2)
        {
            return p2 == null ? 0 : (int)p2;
        }
        
        private static int funcMain2(int? p3)
        {
            return p3 == null ? 0 : (int)p3;
        }
        
        private static short funcMain3(short? p4)
        {
            return p4 == null ? ((short)0) : (short)p4;
        }
    }
}