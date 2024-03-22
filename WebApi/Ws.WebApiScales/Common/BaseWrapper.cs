using System.Xml.Serialization;

namespace Ws.WebApiScales.Common;

[Serializable]
public abstract class BaseWrapper
{
    [XmlAttribute("Count")]
    public int Count { get; set; }
}