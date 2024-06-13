using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Ws.Shared.Utils;

public static class XmlUtil
{
    public static string XsltTransformation(string xslInput, string xmlInput)
    {
        using StringReader stringReaderXslt = new(xslInput.Trim());
        using StringReader stringReaderXml = new(xmlInput.Trim());
        using XmlReader xmlReaderXslt = XmlReader.Create(stringReaderXslt);
        using XmlReader xmlReaderXml = XmlReader.Create(stringReaderXml);

        XslCompiledTransform xslt = new();
        xslt.Load(xmlReaderXslt);

        using StringWriter stringWriter = new();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings);

        xslt.Transform(xmlReaderXml, xmlWriter);
        return stringWriter.ToString();
    }

    public static XmlDocument SerializeAsXmlDocument<T>(T item) where T : ISerializable
    {
        try
        {
            using MemoryStream memoryStream = new();
            XmlSerializer serializer = new(typeof(T));
            serializer.Serialize(memoryStream, item);
            memoryStream.Position = 0;
            XmlDocument xmlDocument = new();
            xmlDocument.Load(memoryStream);
            return xmlDocument;
        }
        catch
        {
            return new();
        }
    }
}