using Ws.Desktop.Models.Features.Plus.Piece.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Common;

public interface IPluPieceService
{
    public List<PluPiece> GetAllPieceByArm(Guid uid);
}