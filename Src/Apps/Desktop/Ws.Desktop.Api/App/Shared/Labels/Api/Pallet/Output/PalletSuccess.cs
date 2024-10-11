using System.Xml.Serialization;

namespace Ws.Desktop.Api.App.Shared.Labels.Api.Pallet.Output;

[Serializable]
public sealed record PalletSuccess
{
    [XmlAttribute("Guid")]
    public Guid Uid { get; set; }

    [XmlAttribute("DocNumber")]
    public string Number { get; set; } = string.Empty;
}