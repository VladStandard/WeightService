using System;

namespace Ws.StorageCore.Utils;

public static class DataFormatUtils
{
    public static string GetPrettyXml(string xml)
    {
        try
        {
            return string.IsNullOrEmpty(xml) ? string.Empty : XDocument.Parse(xml).ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    
    public static string XmlReplaceNextLine(string xml) => xml.Replace("|", "\\&");

    public static string XsltTransformation(string xslInput, string? xmlInput)
    {
        if (xmlInput is null || string.IsNullOrEmpty(xmlInput)) return xslInput;

        using StringReader stringReaderXslt = new(xslInput.Trim());
        using StringReader stringReaderXml = new(xmlInput.Trim());
        using XmlReader xmlReaderXslt = XmlReader.Create(stringReaderXslt);
        using XmlReader xmlReaderXml = XmlReader.Create(stringReaderXml);
        XslCompiledTransform xslt = new();
        xslt.Load(xmlReaderXslt);
        using StringWriter stringWriter = new();
        // Use OutputSettings of xsl, so it can be output as HTML.
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings);
        xslt.Transform(xmlReaderXml, xmlWriter);
        string result = stringWriter.ToString();
        return result;
    }
    
    private static XmlWriterSettings GetXmlWriterSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        OmitXmlDeclaration = false,
        Indent = true,
        IndentChars = "\t"
    };

    private static string SerializeAsXmlString<T>(ISerializable item, bool isAddEmptyNamespace, bool isUtf16)
    {
        try
        {
            XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
            using StringWriter stringWriter = isUtf16 ? new StringWriter() : new SqlStringWriterUtf8Model();
            switch (isAddEmptyNamespace)
            {
                case true:
                {
                    XmlSerializerNamespaces emptyNamespaces = new();
                    emptyNamespaces.Add(string.Empty, string.Empty);
                    using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, GetXmlWriterSettings());
                    xmlSerializer.Serialize(xmlWriter, item, emptyNamespaces);
                    xmlWriter.Flush();
                    xmlWriter.Close();
                    break;
                }
                default:
                    xmlSerializer.Serialize(stringWriter, item);
                    break;
            }
            return stringWriter.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static XmlDocument SerializeAsXmlDocument<T>(ISerializable item, bool isAddEmptyNamespace, bool isUtf16)
    {
        XmlDocument xmlDocument = new();
        string xmlString = SerializeAsXmlString<T>(item, isAddEmptyNamespace, isUtf16);
        byte[] bytes = isUtf16 ? Encoding.Unicode.GetBytes(xmlString) : Encoding.UTF8.GetBytes(xmlString);
        using MemoryStream memoryStream = new(bytes);
        memoryStream.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);
        xmlDocument.Load(memoryStream);
        return xmlDocument;
    }
}
