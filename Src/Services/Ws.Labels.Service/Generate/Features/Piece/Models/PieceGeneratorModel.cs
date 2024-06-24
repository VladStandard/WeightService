using Ws.Labels.Service.Generate.Common;
using Ws.Labels.Service.Generate.Common.BarcodeLabel;

namespace Ws.Labels.Service.Generate.Features.Piece.Models;

public class PieceGeneratorModel : BarcodeGeneratorModel, IBarcodePieceLabel
{
    public short BundleCount { get; init; }
}