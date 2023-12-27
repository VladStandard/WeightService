namespace Ws.WebApiScales.Common;

[Serializable]
public abstract class BaseWrapper
{
    [XmlAttribute("Count")]
    public int Count { get; set; }
}