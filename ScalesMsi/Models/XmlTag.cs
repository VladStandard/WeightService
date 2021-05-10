// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesMsi.Models
{
    public class XmlTag
    {
        public string ElementName { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

        public XmlTag(string elementName, string attributeName = null, string attributeValue = null)
        {
            ElementName = elementName;
            AttributeName = attributeName;
            AttributeValue = attributeValue;
        }
    }
}