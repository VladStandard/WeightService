using System.Xml.Serialization;

namespace Ws.Desktop.Api.App.Shared.Labels.Api.Pallet.Output;

[Serializable]
public sealed record PalletError
{
    [XmlAttribute("Message")]
    public string Message { get; set; } = string.Empty;
}