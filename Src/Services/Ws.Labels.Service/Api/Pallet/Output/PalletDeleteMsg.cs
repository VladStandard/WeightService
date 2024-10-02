using System.Xml.Serialization;

namespace Ws.Labels.Service.Api.Pallet.Output;

[XmlRoot(ElementName = "ResponseType", Namespace = "http://www.kolbasa-vs.ru/scales/ResponseVesovaya2_0_Status")]
public class PalletDeleteWrapperMsg
{
    [XmlElement(ElementName = "Status")]
    public PalletDeleteMsg Status { get; set; } = new();
}

public class PalletDeleteMsg
{
    [XmlAttribute(AttributeName = "IsSuccess")]
    public bool IsSuccess { get; set; }

    [XmlAttribute(AttributeName = "Message")]
    public string Message { get; set; } = string.Empty;
}