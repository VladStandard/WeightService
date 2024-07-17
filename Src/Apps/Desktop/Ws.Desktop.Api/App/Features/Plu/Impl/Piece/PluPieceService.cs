using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Plu.Common;
using Ws.Desktop.Models.Features.Plus.Piece.Output;
using Ws.Domain.Services.Features.Arms;

namespace Ws.Desktop.Api.App.Features.Plu.Impl.Piece;

public class PluPieceService(IArmService armService, WsDbContext dbContext) : IPluPieceService
{
    #region Queries

    public List<PluPiece> GetAllPieceByArm(Guid uid)
    {
        List<Domain.Models.Entities.Ref1c.Plus.Plu> plus = armService.GetArmPiecePlus(uid).ToList();

        List<PluPiece> plusPiece = [];
        plusPiece.AddRange(plus.Select(plu => new PluPiece
        {
            Id = plu.Uid,
            Number = (ushort)plu.Number,
            Name = plu.Name,
            FullName = plu.FullName,
            Bundle = plu.Bundle.Name,
            WeightNet = plu.Weight,
            Nestings = plu.CharacteristicsWithNesting.Select(i => new Nesting
            {
                Id = i.Uid,
                BundleCount = (byte)i.BundleCount,
                Box = i.Box.Name,
                Name = i.Uid == Guid.Empty ? $"{i.BundleCount} (По умолчанию)" : $"{i.BundleCount} (Кор)"
            }).ToList()
        }));
        return plusPiece;
    }

    #endregion
}