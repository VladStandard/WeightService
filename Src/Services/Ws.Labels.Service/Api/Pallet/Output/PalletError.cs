using System.Xml.Serialization;

namespace Ws.Labels.Service.Api.Pallet.Output;

[Serializable]
public sealed record PalletError
{
    [XmlAttribute("Message")]
    public string Message { get; set; } = string.Empty;
}