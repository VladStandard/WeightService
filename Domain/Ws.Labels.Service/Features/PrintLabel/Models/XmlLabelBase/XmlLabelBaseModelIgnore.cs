using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ws.Labels.Service.Features.PrintLabel.Models.XmlLabelBase;

public abstract partial class XmlLabelBaseModel {
    [XmlIgnore] public required short Kneading { get; set; }
    [XmlIgnore] public required DateTime ExpirationDtValue { get; set; } = DateTime.MinValue;
    [XmlIgnore] public required DateTime ProductDtValue { get; set; } = DateTime.MinValue;
    [XmlIgnore] public string ProductDate => $"{ProductDtValue:yyMMdd}";
    [XmlIgnore] public string ProductTime => $"{ProductDtValue:HHmmss}";
    [XmlIgnore] public string ProductDateShort => $"{ProductDtValue:yyMM}";


    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}