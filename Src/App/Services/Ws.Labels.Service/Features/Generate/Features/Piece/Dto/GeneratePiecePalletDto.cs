using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Labels.Service.Features.Generate.Features.Piece.Dto;

public record GeneratePiecePalletDto
{
    public required PalletManEntity PalletMan { get; init; }
    public required decimal Weight { get; init; }
    public required DateTime ProductDt { get; init; }
    public required LineEntity Line { get; init; }
    public required PluEntity Plu { get; init; }
    public required CharacteristicEntity Characteristic { get; init; }
    public required short Kneading { get; init; }
    public required DateTime ExpirationDt { get; init; }
}