using System.Xml.Serialization;

namespace Ws.Labels.Service.Api.Pallet.Output;

[Serializable]
public sealed record PalletError(string Message)
{
    [XmlAttribute("Message")]
    public string Message = Message;

    public PalletError() : this(string.Empty)
    {
    }
}