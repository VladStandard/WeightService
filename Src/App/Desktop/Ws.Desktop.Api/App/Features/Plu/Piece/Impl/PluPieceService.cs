using Ws.Desktop.Api.App.Features.Plu.Piece.Common;
using Ws.Desktop.Models.Features.Plus.Piece.Output;
using Ws.Domain.Services.Features.Arms;

namespace Ws.Desktop.Api.App.Features.Plu.Piece.Impl;

public class PluPieceService(IArmService armService) : IPluPieceService
{
    public List<PluPiece> GetAllPieceByArm(Guid uid)
    {
        List<Domain.Models.Entities.Ref1c.Plu.Plu> plus = armService.GetArmPiecePlus(uid).ToList();

        List<PluPiece> plusPiece = [];
        plusPiece.AddRange(plus.Select(plu => new PluPiece()
        {
            Id = plu.Uid,
            Number = (ushort)plu.Number,
            Name = plu.Name,
            FullName = plu.FullName,
            Bundle = plu.Bundle.Name,
            WeightNet = plu.Weight,
            Nestings = plu.CharacteristicsWithNesting.Select(i => new Nesting { Id = i.Uid, BundleCount = (byte)i.BundleCount, Box = i.Box.Name }).ToList()
        }));
        return plusPiece;
    }
}