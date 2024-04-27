using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;

public abstract partial class XmlLabelBaseModel
{
    [XmlIgnore] public required short Kneading { get; set; }
    [XmlIgnore] public required DateTime ExpirationDt { get; set; } = DateTime.MinValue;
    [XmlIgnore] public required DateTime ProductDt { get; set; } = DateTime.MinValue;
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}