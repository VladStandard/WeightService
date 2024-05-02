using System.Xml.Serialization;
using Ws.Labels.Service.Features.Generate.Common.XmlBarcode;
using Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;
using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Features.Generate.Features.Weight.Models;

[Serializable]
public class XmlWeightLabel : XmlLabelBaseModel, IXmlBarcodeWeightXml
{
    [XmlIgnore] public required decimal Weight { get; set; }
    [XmlElement] public string WeightStr { get => Weight.ToSepStr(","); set => _ = value; }
}