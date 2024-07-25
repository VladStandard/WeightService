using Ws.Desktop.Models.Features.Plus.Piece.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPluPieceApi
{
    #region Queries

    [Get("/arms/{armUid}/plu/piece")]
    Task<PluPiece[]> GetPlusPieceByArm(Guid armUid);

    #endregion
}