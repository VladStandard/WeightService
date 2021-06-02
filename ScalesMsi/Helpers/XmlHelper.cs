// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace ScalesMsi.Helpers
{
    /// <summary>
    /// Помощник XML.
    /// </summary>
    public class XmlHelper
    {
        #region Design pattern "Lazy Singleton"

        private static XmlHelper _instance;
        public static XmlHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public XmlHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            //
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Проверки.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="elements"></param>
        /// <param name="value"></param>
        public void Checks(string uri, Collection<XmlTag> elements, string value = null)
        {
            if (!File.Exists(uri))
                throw new FileNotFoundException(@"FileName is not exists!");
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException(uri);

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

        public string Read(string uri, string element)
        {
            var result = string.Empty;
            try
            {
                var fsRead = new FileStream(uri, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fsRead))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line1 = streamReader.ReadLine();
                        if (!string.IsNullOrEmpty(line1) && line1.Contains(element))
                        {
                            var line2 = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(line2) && line2.Contains("<value>"))
                            {
                                if (line2.Contains("</value>"))
                                {
                                    var posStart = line2.IndexOf("<value>", StringComparison.Ordinal) + 7; 
                                    result = line2.Substring(posStart, line2.Length - 8 - posStart);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (!(ex.InnerException is null))
                    msg += Environment.NewLine + ex.InnerException.Message;
                MessageBox.Show(@"Ошибка чтения значения конфига!" + Environment.NewLine + msg);
                return string.Empty;
            }
            return result;
        }

        [Obsolete(@"Deprecated method")]
        public bool WriteObsolete(string uri, string element, string value, bool showException)
        {
            try
            {
                var lines = new List<string>();
                var fsRead = new FileStream(uri, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fsRead))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        lines.Add(line);
                        if (!string.IsNullOrEmpty(line) && line.Contains(element))
                        {
                            var line2 = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(line2) && line2.Contains("<value>"))
                            {
                                if (line2.Contains("</value>"))
                                {
                                    var posStart = line2.IndexOf("<value>", StringComparison.Ordinal);
                                    var spaces = line2.Substring(0, posStart);
                                    line2 = spaces + "<value>" + value + "</value>";
                                }
                            }
                            lines.Add(line2);
                        }
                    }
                }

                var fsWrite = new FileStream(uri, FileMode.Open, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fsWrite))
                {
                    foreach (var line in lines)
                    {
                        streamWriter.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (!(ex.InnerException is null))
                    msg += Environment.NewLine + ex.InnerException.Message;
                if (showException)
                    MessageBox.Show(@"Ошибка записи значения конфига!" + Environment.NewLine + msg);
                return false;
            }
            return true;
        }

        public bool Write(string uri, string element, string value, bool showException)
        {
            try
            {
                var lines = new List<string>();
                var fsRead = new FileStream(uri, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fsRead))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        if (!string.IsNullOrEmpty(line) && line.Contains(element))
                        {
                            var lineNew = line;
                            if (!string.IsNullOrEmpty(lineNew) && lineNew.Contains("<value>"))
                            {
                                if (lineNew.Contains("</value>"))
                                {
                                    var posStart = lineNew.IndexOf("<value>", StringComparison.Ordinal);
                                    var spaces = lineNew.Substring(0, posStart);
                                    lineNew = spaces + "<value>" + value + "</value>";
                                }
                            }
                            lines.Add(lineNew);
                        }
                        else
                        {
                            lines.Add(line);
                        }
                    }
                }

                var fsWrite = new FileStream(uri, FileMode.Open, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fsWrite))
                {
                    foreach (var line in lines)
                    {
                        streamWriter.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (!(ex.InnerException is null))
                    msg += Environment.NewLine + ex.InnerException.Message;
                if (showException)
                    MessageBox.Show(@"Ошибка записи значения конфига!" + Environment.NewLine + msg);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Чтение.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="elements"></param>
        /// <param name="getValueFromName"></param>
        /// <returns></returns>
        public ResultXmlRead Read(string uri, Collection<XmlTag> elements, string getValueFromName = null)
        {
            ResultXmlRead result = new ResultXmlRead();
            try
            {
                var str = new Collection<string>();
                var value = string.Empty;
                // Проверки.
                Checks(uri, elements);

                using (var xmlReader = new XmlTextReader(uri))
                {
                    xmlReader.MoveToContent();

                    ReadInside(xmlReader, elements, getValueFromName, ref value, str);
                }
                result = new ResultXmlRead(!string.IsNullOrEmpty(value), string.IsNullOrEmpty(value) ? string.Empty : value, str);
            }
            catch (Exception)
            {
                result = new ResultXmlRead(false, "<error>");
            }
            return result;
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
                    ReadInsideNodeType(xmlReader, elementCur, ref attr);
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
                //else
                //{
                //    ReadInside(xmlReader, elements, getValueFromName, ref value, str);
                //}
            }
        }

        private void ReadInsideNodeType(XmlTextReader xmlReader, XmlTag elementCur, ref string attr)
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

        /// <summary>
        /// Запись.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="elements"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ResultXmlRead Write(string uri, Collection<XmlTag> elements, string key, string value)
        {
            // Проверки.
            Checks(uri, elements, key);

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
