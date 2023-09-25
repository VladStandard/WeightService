using Microsoft.AspNetCore.Mvc.Formatters;

namespace WsWebApiCore.Settings;

public class XmlSerializerOutputFormatterNamespace : XmlSerializerOutputFormatter
{
    protected override void Serialize(XmlSerializer xmlSerializer, XmlWriter xmlWriter, object? value)
    {
        XmlSerializerNamespaces emptyNamespaces = new();
        emptyNamespaces.Add("", "any-non-empty-string");
        xmlSerializer.Serialize(xmlWriter, value, emptyNamespaces);
    }
}