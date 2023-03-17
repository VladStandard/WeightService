// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Helpers;

public class XmlHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static XmlHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static XmlHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public methods

	/// <summary>
	/// Проверки.
	/// </summary>
	/// <param name="inputUri"></param>
	/// <param name="elements"></param>
	/// <param name="value"></param>
	public void Checks(string inputUri, Collection<XmlTag> elements, string? value = null)
	{
		if (!File.Exists(inputUri))
			throw new FileNotFoundException(@"FileName is not exists!");
		if (string.IsNullOrEmpty(inputUri))
			throw new ArgumentNullException(inputUri);

		foreach (XmlTag elementName in elements)
		{
			if (string.IsNullOrEmpty(elementName.ElementName))
				throw new ArgumentNullException(elementName.ElementName);
		}

		if (value is not null)
		{
			if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value must be fill!", nameof(value));
        }
	}

	/// <summary>
	/// Чтение.
	/// </summary>
	/// <param name="inputUri"></param>
	/// <param name="elements"></param>
	/// <param name="getValueFromName"></param>
	/// <returns></returns>
	public ResultXmlRead Read(string inputUri, Collection<XmlTag> elements, string? getValueFromName = null)
	{
		Collection<string> str = new();
		string value = string.Empty;
		// Проверки.
		Checks(inputUri, elements);

		using (XmlTextReader xmlReader = new(inputUri))
		{
			xmlReader.MoveToContent();

			ReadInside(xmlReader, elements, getValueFromName, ref value, str);
		}
		return new(!string.IsNullOrEmpty(value), string.IsNullOrEmpty(value) ? string.Empty : value, str);
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
		XmlTag? elementCur = null;
		string? attr = null;

		Collection<XmlTag> elementsTrim = new();
		if (elements.Count > 0)
		{
			foreach (XmlTag element in elements)
			{
				if (elementCur is null)
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
						if (elementCur is not null && xmlReader.Name.Equals(elementCur.ElementName, StringComparison.InvariantCultureIgnoreCase))
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
			if (elementCur is not null && (!string.IsNullOrEmpty(attr) && attr is not null && attr.Equals(elementCur.AttributeValue) ||
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
		StringBuilder sb = new();
		// Проверки.
		Checks(fileName, elements, key);

		try
		{
			return new(true, string.Empty);
		}
		catch (Exception)
		{
			//
		}
		return new();
	}

	#endregion
}