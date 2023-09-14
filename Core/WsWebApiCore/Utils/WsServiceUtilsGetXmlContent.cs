namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты получения данных XML веб-сервиса.
/// </summary>
public static class WsServiceUtilsGetXmlContent
{
    #region Public and private methods

    public static ContentResult GetContentResult(Func<ContentResult> action, string format,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            WsServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return WsDataFormatUtils.GetContentResult<WsServiceExceptionModel>(serviceException, format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    public static void SetItemParseResultException<T>(T itemXml, string xmlPropertyName) where T : WsSqlTableBase
    {
        itemXml.ParseResult.Status = WsEnumParseStatus.Error;
        itemXml.ParseResult.Exception = string.IsNullOrEmpty(itemXml.ParseResult.Exception)
            ? $"{xmlPropertyName} {WsLocaleCore.WebService.IsEmpty}!"
            : $"{itemXml.ParseResult.Exception} | {xmlPropertyName} {WsLocaleCore.WebService.IsEmpty}!";
    }

    private static string GetXmlAttributeString<T>(XmlNode? xmlNode, T itemXml, string attributeName,
        bool isAttributeMustExists = true) where T : WsSqlTableBase
    {
        if (xmlNode?.Attributes is null) return string.Empty;
        foreach (XmlAttribute? attribute in xmlNode.Attributes)
        {
            if (attribute is not null)
            {
                if (attribute.Name.ToUpper().Equals(attributeName.ToUpper()))
                    return attribute.Value;
            }
        }
        if (isAttributeMustExists)
            SetItemParseResultException(itemXml, attributeName);
        return string.Empty;
    }

    private static bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName,
        List<string> valuesFalse, List<string> valuesTrue) where T : WsSqlTableBase
    {
        string value = GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName).ToUpper();
        if (Enumerable.Contains(valuesFalse, value)) return false;
        if (Enumerable.Contains(valuesTrue, value)) return true;
        return default;
    }

    private static bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase =>
        GetXmlAttributeBool(xmlNode, itemXml, xmlPropertyName, new List<string> { "0", "FALSE" }, new() { "1", "TRUE" });

    private static bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName,
        string valueFalse, string valueTrue) where T : WsSqlTableBase =>
        GetXmlAttributeBool(xmlNode, itemXml, xmlPropertyName, new List<string> { valueFalse }, new() { valueTrue });

    private static Guid GetXmlAttributeGuid<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase =>
        Guid.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out Guid uid) ? uid : Guid.Empty;

    private static byte GetXmlAttributeByte<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase =>
        byte.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out byte result) ? result : default;

    private static short GetXmlAttributeShort<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase =>
        short.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out short result) ? result : default;

    private static decimal GetXmlAttributeDecimal<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName) 
        where T : WsSqlTableBase
    {
        CultureInfo? culture = CultureInfo.InvariantCulture.Clone() as CultureInfo;
        if (culture is null) throw new ArgumentException(nameof(culture));
        culture.NumberFormat.NumberDecimalSeparator = ".";
        culture.NumberFormat.CurrencyDecimalSeparator = ".";
        return decimal.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName),
            NumberStyles.Any, culture, out decimal result)
            ? result : default;
    }

    #endregion
}