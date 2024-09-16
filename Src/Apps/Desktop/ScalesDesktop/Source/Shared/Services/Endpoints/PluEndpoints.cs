using Ws.Desktop.Models.Features.Plus.Piece.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace ScalesDesktop.Source.Shared.Services.Endpoints;

public class PluEndpoints(IDesktopApi desktopApi)
{
    public ParameterlessEndpoint<PluWeight[]> WeightPlusEndpoint { get; } = new(
         desktopApi.GetPlusWeightByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public ParameterlessEndpoint<PluPiece[]> PiecePlusEndpoint { get; } = new(
        desktopApi.GetPlusPieceByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}