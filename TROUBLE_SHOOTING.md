# TROUBLE_SHOOTING

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

## TSC Print offline
ESET -> Setup -> Network Protection -> Network attack protection -> IDS rules:
Remote IP address: 192.168.4.159, 192.168.4.164, 192.168.7.41
ARP Cache Poisoning attack | In | Any profile | No Block | Notify | No Log
Unexpected protocol data | In | Any profile | No Block | Notify | No Log
