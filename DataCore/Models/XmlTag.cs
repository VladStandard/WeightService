// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models;

public class XmlTag
{
	public string ElementName { get; }
	public string AttributeName { get; }
	public string AttributeValue { get; }

	public XmlTag(string elementName, string attributeName = null, string attributeValue = null)
	{
		ElementName = elementName;
		AttributeName = attributeName;
		AttributeValue = attributeValue;
	}
}