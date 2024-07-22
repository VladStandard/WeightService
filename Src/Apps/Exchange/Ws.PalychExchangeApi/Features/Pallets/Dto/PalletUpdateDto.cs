using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Pallets.Dto;


[XmlRoot(ElementName = "Pallet")]
public class PalletUpdateWrapper
{
    [XmlElement(ElementName = "Record")]
    public PalletUpdateDto Pallet { get; set; } = new();
}

public class PalletUpdateDto
{
    [XmlAttribute(AttributeName = "Number")]
    public string Number { get; set; } = string.Empty;

    [XmlAttribute(AttributeName = "IsDelete")]
    public bool IsDelete { get; set; }

    [XmlAttribute(AttributeName = "IsShipped")]
    public bool IsShipped { get; set; }
}