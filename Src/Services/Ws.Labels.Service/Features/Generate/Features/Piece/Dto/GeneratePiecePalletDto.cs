using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Labels.Service.Features.Generate.Features.Piece.Dto;

public record GeneratePiecePalletDto
{
    public required Plu Plu { get; init; }
    public required Arm Line { get; init; }
    public required PalletMan PalletMan { get; init; }
    public required PluCharacteristic PluCharacteristic { get; init; }
    public required short Kneading { get; init; }
    public required decimal Weight { get; init; }
    public required DateTime ProductDt { get; init; }
    public required DateTime ExpirationDt { get; init; }
}