# Trouble Shooting

## SGEN. An attempt was made to load an assembly with an incorrect format: tsclibnet.dll
Project properties - Build tab - Generate serialization assembly: Off

# Deserialize
public static T DeserializeFromXml(string xml)
{
    // Don't use it.
    // XmlSerializer xmlSerializer = new(typeof(T));
    // Use it.
    XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
    return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
}

# UTF8 - xml response
https://stackoverflow.com/questions/9459184/why-is-the-xmlwriter-always-outputting-utf-16-encoding
```
public static string SerializeObject<T>(this T value)
{
    var serializer = new XmlSerializer(typeof(T));           
    var settings = new XmlWriterSettings
                   {
                    Encoding = new UTF8Encoding(true), 
                    Indent = false, 
                    OmitXmlDeclaration = false,
                    NewLineHandling = NewLineHandling.None
                   };

    using(var stringWriter = new UTF8StringWriter())
    {
        using(var xmlWriter = XmlWriter.Create(stringWriter, settings)) 
        {
            serializer.Serialize(xmlWriter, value);
        }

        return stringWriter.ToString();
    }
}

public class UTF8StringWriter : StringWriter
{
    public override Encoding Encoding
    {
        get
        {
            return Encoding.UTF8;
        }
    }
}
```

## TSC Print offline
ESET -> Setup -> Network Protection -> Network attack protection -> IDS rules:
Remote IP address: 192.168.4.159, 192.168.4.164, 192.168.7.41
ARP Cache Poisoning attack | In | Any profile | No Block | Notify | No Log
Unexpected protocol data | In | Any profile | No Block | Notify | No Log
