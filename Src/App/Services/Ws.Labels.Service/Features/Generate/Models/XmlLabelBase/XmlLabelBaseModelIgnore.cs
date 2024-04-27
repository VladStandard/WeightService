using System.Runtime.Serialization;
using System.Xml.Serialization;
using Ws.Domain.Models.ValueTypes;

namespace Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;

public abstract partial class XmlLabelBaseModel
{
    [XmlIgnore] public required short Kneading { get; set; }
    [XmlIgnore] public required DateTime ExpirationDt { get; set; } = DateTime.MinValue;
    [XmlIgnore] public required DateTime ProductDt { get; set; } = DateTime.MinValue;

    [XmlIgnore] public List<BarcodeItem> BarcodeRightTemplate { get; set; } = [];
    [XmlIgnore] public List<BarcodeItem> BarcodeBottomTemplate { get; set; } = [];
    [XmlIgnore] public List<BarcodeItem> BarcodeTopTemplate { get; set; } = [];

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}