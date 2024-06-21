using Ws.Labels.Service.Generate.Common.BarcodeLabel;
using Ws.Labels.Service.Generate.Models.XmlLabelBase;

namespace Ws.Labels.Service.Generate.Features.Piece.Models;

public class PieceLabelLabel : BarcodeLabelLabel, IBarcodePieceLabel
{
    public short BundleCount { get; set; }
}