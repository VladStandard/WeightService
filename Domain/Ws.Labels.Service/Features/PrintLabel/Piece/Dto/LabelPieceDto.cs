using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Labels.Service.Features.PrintLabel.Piece.Dto;

public class LabelPieceDto
{
    public required LineEntity Line { get; init; }
    public required PluNestingEntity Nesting { get; init; }
    public required string Template { get; init; }
    public required short Kneading { get; init; }
    public required DateTime ProductDt { get; set; }
    public required DateTime ExpirationDt { get; init; }
}