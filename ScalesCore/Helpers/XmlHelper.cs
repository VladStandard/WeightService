// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesCore.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник XML.
    /// </summary>
    public sealed class XmlHelper
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<XmlHelper> _instance = new Lazy<XmlHelper>(() => new XmlHelper());
        public static XmlHelper Instance => _instance.Value;
        private XmlHelper() { }

        #endregion

        #region Public methods

        /// <summary>
        /// Проверки.
        /// </summary>
        /// <param name="inputUri"></param>
        /// <param name="elements"></param>
        /// <param name="value"></param>
        public void Checks(string inputUri, Collection<XmlTag> elements, string value = null)
        {
            if (!File.Exists(inputUri))
                throw new FileNotFoundException(@"FileName is not exists!");
            if (string.IsNullOrEmpty(inputUri))
                throw new ArgumentNullException(inputUri);

            foreach (var elementName in elements)
            {
                if (string.IsNullOrEmpty(elementName.ElementName))
                    throw new ArgumentNullException(elementName.ElementName);
            }

            if (value != null)
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(@"Value is not correct!");
            }
        }

        /// <summary>
        /// Чтение.
        /// </summary>
        /// <param name="inputUri"></param>
        /// <param name="elements"></param>
        /// <param name="getValueFromName"></param>
        /// <returns></returns>
        public ResultXmlRead Read(string inputUri, Collection<XmlTag> elements, string getValueFromName = null)
        {
            var str = new Collection<string>();
            var value = string.Empty;
            // Проверки.
            Checks(inputUri, elements);

            using (var xmlReader = new XmlTextReader(inputUri))
            {
                xmlReader.MoveToContent();

                ReadInside(xmlReader, elements, getValueFromName, ref value, str);
            }
            return new ResultXmlRead(!string.IsNullOrEmpty(value), string.IsNullOrEmpty(value) ? string.Empty : value, str);
        }

        /// <summary>
        /// Чтение внутри.
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <param name="elements"></param>
        /// <param name="getValueFromName"></param>
        /// <param name="value"></param>
        /// <param name="str"></param>
        public void ReadInside(XmlTextReader xmlReader, Collection<XmlTag> elements, string getValueFromName, ref string value,
            Collection<string> str)
        {
            XmlTag elementCur = null;
            string attr = null;

            var elementsTrim = new Collection<XmlTag>();
            if (elements.Count > 0)
            {
                foreach (var element in elements)
                {
                    if (elementCur == null)
                        elementCur = element;
                    else
                        elementsTrim.Add(element);
                }

                xmlReader.Read();
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.None:
                            break;
                        case XmlNodeType.Element:
                            if (elementCur != null && xmlReader.Name.Equals(elementCur.ElementName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (xmlReader.HasAttributes)
                                {
                                    if (xmlReader.MoveToAttribute(elementCur.AttributeName))
                                    {
                                        attr = xmlReader.GetAttribute(elementCur.AttributeName);
                                    }
                                }
                            }
                            break;
                        case XmlNodeType.Attribute:
                            break;
                        case XmlNodeType.Text:
                            break;
                        case XmlNodeType.CDATA:
                            break;
                        case XmlNodeType.EntityReference:
                            break;
                        case XmlNodeType.Entity:
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            break;
                        case XmlNodeType.Comment:
                            break;
                        case XmlNodeType.Document:
                            break;
                        case XmlNodeType.DocumentType:
                            break;
                        case XmlNodeType.DocumentFragment:
                            break;
                        case XmlNodeType.Notation:
                            break;
                        case XmlNodeType.Whitespace:
                            break;
                        case XmlNodeType.SignificantWhitespace:
                            break;
                        case XmlNodeType.EndElement:
                            break;
                        case XmlNodeType.EndEntity:
                            break;
                        case XmlNodeType.XmlDeclaration:
                            break;
                    }
                }
                // Узел найден или нет атрибутов.
                if (elementCur != null && (!string.IsNullOrEmpty(attr) && attr.Equals(elementCur.AttributeValue) || 
                    !xmlReader.HasAttributes && string.IsNullOrEmpty(elementCur.AttributeName) && string.IsNullOrEmpty(elementCur.AttributeValue)))
                {
                    str.Add($"- get: {xmlReader.Name} {elementCur.AttributeName}=\"{attr}\"");
                    str.Add($"- search: {elementCur.ElementName} {elementCur.AttributeName}=\"{elementCur.AttributeValue}\"");
                    if (elementsTrim.Count == 0)
                    {
                        if (!string.IsNullOrEmpty(getValueFromName))
                        {
                            value = xmlReader.GetAttribute(getValueFromName);
                            str.Add($"+ search: {elementCur.ElementName} {elementCur.AttributeName}=\"{elementCur.AttributeValue}\". {getValueFromName}=\"{value}\"");
                        }
                        else
                        {
                            xmlReader.ReadInnerXml();
                            value = xmlReader.ReadInnerXml();
                            str.Add($"+ search: {elementCur.ElementName} Content=\"{value}\"");
                        }
                    }
                    ReadInside(xmlReader, elementsTrim, getValueFromName, ref value, str);
                }
                // Не тот узел.
                else
                {
                    ReadInside(xmlReader, elements, getValueFromName, ref value, str);
                }
            }
        }

        /// <summary>
        /// Запись.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="elements"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ResultXmlRead Write(string fileName, Collection<XmlTag> elements, string key, string value)
        {
            var sb = new StringBuilder();
            // Проверки.
            Checks(fileName, elements, key);

            try
            {
                return new ResultXmlRead(true, string.Empty);
            }
            catch (Exception)
            {
                //
            }
            return new ResultXmlRead();
        }

        #endregion
    }
}
