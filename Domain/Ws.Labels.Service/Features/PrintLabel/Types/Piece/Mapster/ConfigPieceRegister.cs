using Mapster;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Models;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Piece.Mapster;

public class ConfigPieceRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LabelPiecePalletDto, XmlPieceLabelModel>()
            .Map(d => d.LineName, s => s.Line.Name)
            .Map(d => d.LineAddress, s => s.Line.Warehouse.ProductionSite.Address)
            .Map(d => d.LineNumber, s => s.Line.Number)
            .Map(d => d.LineCounter, s => s.Line.Counter)
            .Map(d => d.ProductDtValue, s => s.ProductDt)
            .Map(d => d.ExpirationDtValue, s => s.ExpirationDt)
            .Map(d => d.PluGtin, s => s.Plu.Gtin)
            .Map(d => d.PluNumber, s => s.Plu.Number)
            .Map(d => d.PluFullName, s => s.Plu.FullName)
            .Map(d => d.PluDescription, s => s.Plu.Description)
            .Map(d => d.Kneading, s => s.Kneading)
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.Map);
    }
}