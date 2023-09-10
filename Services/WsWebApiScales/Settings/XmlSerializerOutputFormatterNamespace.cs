using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace WsWebApiScales.Settings;

public class XmlSerializerOutputFormatterNamespace : XmlSerializerOutputFormatter
{
    protected override void Serialize(XmlSerializer xmlSerializer, XmlWriter xmlWriter, object? value)
    {
        XmlSerializerNamespaces emptyNamespaces = new();
        emptyNamespaces.Add("", "any-non-empty-string");
        xmlSerializer.Serialize(xmlWriter, value, emptyNamespaces);
    }
}