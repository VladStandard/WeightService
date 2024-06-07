using System.Xml.Serialization;
using Ws.Labels.Service.Features.Generate.Common.XmlBarcode;
using Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;

namespace Ws.Labels.Service.Features.Generate.Features.Piece.Models;

[Serializable]
public class XmlPieceLabel : XmlLabelBaseModel, IXmlBarcodePieceXml
{
    [XmlElement] public short BundleCount { get; set; }
}