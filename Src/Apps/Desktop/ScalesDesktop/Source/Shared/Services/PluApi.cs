using Phetch.Core;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.Plus.Piece.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class PluApi(IDesktopApi desktopApi)
{
    public Endpoint<Guid, PluWeight[]> WeightPlusEndpoint { get; } = new(
         desktopApi.GetPlusWeightByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<Guid, PluPiece[]> PiecePlusEndpoint { get; } = new(
        desktopApi.GetPlusPieceByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}