using System.Xml.Serialization;

namespace Ws.Labels.Service.Api.Pallet.Output;

[Serializable]
public sealed record PalletSuccess(Guid Uid, string Number)
{
    [XmlAttribute("Guid")]
    public Guid Uid = Uid;

    [XmlAttribute("DocNumber")]
    public string Number = Number;

    public PalletSuccess() : this(Guid.Empty, string.Empty)
    {
    }
}