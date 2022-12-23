// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using MDSoft.BarcodePrintUtils.Tsc;
using Newtonsoft.Json.Linq;
using System.Text.Unicode;
using System.Xml.Xsl;
using TableDirectModels = DataCore.Sql.TableDirectModels;

namespace DataCore.Utils;

public static class XmlUtils
{
	#region Public and private fields and properties

	private static readonly char[] DigitsCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

	private static readonly char[] SpecialCharacters = { ' ', ',', '.', '-', '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', '"', '№', ';', ':', '?', '/', '|', '\\', '{', '}', '<', '>' };

	#endregion

	#region Public and private methods

	/// <summary>
	/// Get pretty formatted XML or JSON string.
	/// </summary>
	/// <param name="value"></param>
	public static string GetPrettyXmlOrJson(string value)
	{
		if (value.StartsWith("<") && value.EndsWith(">"))
			return GetPrettyXml(value);
		if (value.StartsWith("{") && value.EndsWith("}"))
			return GetPrettyJson(value);
		return value;
	}

	/// <summary>
	/// Get pretty formatted XML string.
	/// </summary>
	/// <param name="xml"></param>
	public static string GetPrettyXml(string xml) => 
		string.IsNullOrEmpty(xml) ? string.Empty : XDocument.Parse(xml).ToString();

    /// <summary>
    /// Get pretty formatted JSON string.
    /// </summary>
    /// <param name="json"></param>
    public static string GetPrettyJson(string json) => 
		string.IsNullOrEmpty(json) ? string.Empty : JToken.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);

	public static XmlDocument XmlCompatibleReplace(XmlDocument xmlDocument)
	{
		XmlDocument result = new();
		result.LoadXml(XmlCompatibleReplace(xmlDocument.OuterXml));
		return result;
	}

	private static string XmlCompatibleReplace(string xml)
	{
		if (string.IsNullOrEmpty(xml)) return xml;

		//xml = xml.Replace(nameof(TableDirectModels.HostDirect), "HostEntity");
		xml = xml.Replace(nameof(TableDirectModels.NomenclatureDirect), "NomenclatureEntity");
		//xml = xml.Replace(nameof(TableDirectModels.PrinterDirect), "ZebraPrinterEntity");
		xml = xml.Replace(nameof(TableDirectModels.ProductSeriesDirect), "ProductSeriesEntity");
		xml = xml.Replace(nameof(TableDirectModels.SsccDirect), "SsccEntity");

		return xml;
	}

	public static string XmlReplaceNextLine(string xml)
	{
		xml = xml.Replace("|", "\\&");
		return xml;
	}

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

	public static bool IsCyrillic(char value)
	{
		char[] cyrillic = Enumerable
			.Range(UnicodeRanges.Cyrillic.FirstCodePoint, UnicodeRanges.Cyrillic.Length)
			.Select(ch => (char)ch)
			.ToArray();
		return Array.BinarySearch(cyrillic, value) >= 0;
	}

	public static bool IsDigit(char value) => DigitsCharacters.Contains(value);

	public static bool IsSpecial(char value, bool isExcludeTop = true)
	{
		if (isExcludeTop && value == '^')
			return false;
		return SpecialCharacters.Contains(value);
	}

	/// <summary>
	/// Replace zpl-resources.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string PrintCmdReplaceZplResources(string value)
	{
		string result = value;
		if (string.IsNullOrEmpty(result))
			return result;

		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFilters(nameof(TemplateResourceModel.Type), "ZPL"), 
			new SqlFieldOrderModel(nameof(TemplateResourceModel.Name), SqlFieldOrderEnum.Asc), false, false);
		TemplateResourceModel[]? templateReources = DataAccessHelper.Instance.GetArrayNullable<TemplateResourceModel>(sqlCrudConfig);
		if (templateReources is not null)
		{
			foreach (TemplateResourceModel resource in templateReources.ToList())
			{
				string resourceHex = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(resource.ImageData.ValueUnicode);
				result = result.Replace($"[{resource.Name}]", resourceHex);
			}
		}
		//result = PrintCmdReplaceArea(result);
		return result;
	}

	/// <summary>
	/// Replace area-resource.
	/// </summary>
	/// <param name="area"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string PrintCmdReplaceArea(ProductionFacilityModel? area, string value)
	{
		string result = value;
		if (string.IsNullOrEmpty(result))
			return result;
		if (area is not null)
		{
			string areaName = area.Name;
			if (string.IsNullOrEmpty(areaName))
				return result;
			string resourceHex = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(areaName);
			result = result.Replace($"[AREA]", resourceHex);
		}
		return result;
	}

	public static void CmdConvertZpl(TscDriverHelper tscDriver, bool isUsePicReplace)
	{
		tscDriver.Cmd = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(tscDriver.TextPrepare);
		if (isUsePicReplace)
		{
			tscDriver.Cmd = PrintCmdReplaceZplResources(tscDriver.Cmd);
			//Cmd = Cmd.Replace("[EAC_107x109_090]", ZplSamples.GetEac);
			//Cmd = Cmd.Replace("[FISH_94x115_000]", ZplSamples.GetFish);
			//Cmd = Cmd.Replace("[TEMP6_116x113_090]", ZplSamples.GetTemp6);
		}
	}

	public static string XmlMerge(string xmlParent, string xmlChild)
	{
		string result = string.Empty;
		bool isSkipHeader = false;
		foreach (string lineParent in xmlParent.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
		{
			if (isSkipHeader && !string.IsNullOrEmpty(xmlChild))
			{
				foreach (string lineChild in xmlChild.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
				{
					if (!lineChild.StartsWith("<?xml "))
						result += "\t" + lineChild + Environment.NewLine;
				}
				xmlChild = string.Empty;
			}
			if (lineParent.StartsWith("<") && !lineParent.StartsWith("<?xml "))
				isSkipHeader = true;
			result += lineParent + Environment.NewLine;
		}
		return result;
	}

	public static XmlDocument XmlMerge(XmlDocument documentA, XmlDocument documentB)
	{
		if (documentA.DocumentElement is not null && documentB.DocumentElement is not null)
		{
			if (!documentA.DocumentElement.Name.Equals(documentB.DocumentElement.Name))
			{
				XmlNode xmlImport = documentA.ImportNode(documentB.DocumentElement, true);
				documentA.DocumentElement.AppendChild(xmlImport);
			}
			else
			{
				foreach (XmlNode xmlNode in documentB.DocumentElement.ChildNodes)
				{
					XmlNode xmlImport = documentA.ImportNode(xmlNode, true);
					documentA.DocumentElement.AppendChild(xmlImport);
				}
			}
		}
		return documentA;
	}

	#endregion
}
