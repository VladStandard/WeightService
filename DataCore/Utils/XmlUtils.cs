// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Linq;

namespace DataCore.Utils
{
    public static class XmlUtils
    {
        /// <summary>
        /// Get pretty formatted XML string.
        /// </summary>
        /// <param name="xml"></param>
        public static string GetXmlpretty(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return string.Empty;
            XDocument document = XDocument.Parse(xml);
            return document.ToString();
        }
    }
}
