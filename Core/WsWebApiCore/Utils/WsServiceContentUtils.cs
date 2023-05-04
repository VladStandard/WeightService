// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

/// <summary>
/// Content utils.
/// </summary>
public static class WsServiceContentUtils
{
    #region Public and private methods

    public static List<WsXmlContentRecord<T>> GetNodesListCore<T>(XElement xml, string nodeIdentity, 
        Action<XmlNode, T> action) where T : WsSqlTable1CBase, new()
    {
        List<WsXmlContentRecord<T>> itemsXml = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return itemsXml;

        XmlNodeList xmlNodes = xmlDocument.DocumentElement.ChildNodes;
        if (xmlNodes.Count <= 0) return itemsXml;
        foreach (XmlNode xmlNode in xmlNodes)
        {
            T itemXml = new() { ParseResult = { Status = ParseStatus.Success, Exception = string.Empty } };
            if (xmlNode.Name.Equals(nodeIdentity, StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    action(xmlNode, itemXml);
                    // Fix ParseResult.
                    itemXml.ParseResult.Message = string.IsNullOrEmpty(itemXml.ParseResult.Exception) ? "Is success" : itemXml.ParseResult.Exception;
                }
                catch (Exception ex)
                {
                    itemXml.ParseResult.Status = ParseStatus.Error;
                    itemXml.ParseResult.Exception = ex.Message;
                    if (ex.InnerException is not null)
                        itemXml.ParseResult.InnerException = ex.InnerException.Message;
                }
            }
            else
            {
                itemXml.ParseResult.Status = ParseStatus.Error;
                itemXml.ParseResult.Exception =
                    $"{LocaleCore.WebService.Node} `{nodeIdentity}` {LocaleCore.WebService.With} `{xmlNode.Name}` {LocaleCore.WebService.IsNotIdent}!";
            }
            itemsXml.Add(new(itemXml, xmlNode.OuterXml));
        }
        return itemsXml;
    }

    public static void SetItemParseResultException<T>(T itemXml, string xmlPropertyName) where T : WsSqlTableBase
    {
        itemXml.ParseResult.Status = ParseStatus.Error;
        itemXml.ParseResult.Exception = string.IsNullOrEmpty(itemXml.ParseResult.Exception)
            ? $"{xmlPropertyName} {LocaleCore.WebService.IsEmpty}!"
            : $"{itemXml.ParseResult.Exception} | {xmlPropertyName} {LocaleCore.WebService.IsEmpty}!";
    }

    public static void SetItemPropertyFromXmlAttribute<T>(XmlNode xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase
    {
        SetItemPropertyFromXmlAttributeForBase(xmlNode, itemXml, xmlPropertyName);
        switch (itemXml)
        {
            case BrandModel brandXml:
                SetItemPropertyFromXmlAttributeForBrand(xmlNode, brandXml, xmlPropertyName);
                break;
            case PluModel pluXml:
                SetItemPropertyFromXmlAttributeForPlu(xmlNode, pluXml, xmlPropertyName);
                break;
            case PluGroupModel pluGroupXml:
                SetItemPropertyFromXmlAttributeForPluGroup(xmlNode, pluGroupXml, xmlPropertyName);
                break;
            case PluCharacteristicModel pluCharacteristicXml:
                SetItemPropertyFromXmlAttributeForPluCharacteristic(xmlNode, pluCharacteristicXml, xmlPropertyName);
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForBase<T>(XmlNode xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                itemXml.IdentityValueUid = GetXmlAttributeGuid(xmlNode, itemXml, xmlPropertyName);
                break;
            case "ISMARKED":
                itemXml.IsMarked = GetXmlAttributeBool(xmlNode, itemXml, xmlPropertyName);
                break;
            case "NAME":
                itemXml.Name = GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName);
                break;
            case "DESCRIPTION":
                itemXml.Description = GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName);
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForBrand(XmlNode xmlNode, BrandModel brandXml, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                brandXml.Uid1C = GetXmlAttributeGuid(xmlNode, brandXml, xmlPropertyName);
                break;
            case "CODE":
                brandXml.Code = GetXmlAttributeString(xmlNode, brandXml, xmlPropertyName);
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForPlu(XmlNode xmlNode, PluModel pluXml, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                pluXml.Uid1C = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "ISGROUP":
                pluXml.IsGroup = GetXmlAttributeBool(xmlNode, pluXml, xmlPropertyName);
                break;
            case "FULLNAME":
                pluXml.FullName = GetXmlAttributeString(xmlNode, pluXml, xmlPropertyName);
                break;
            case "CODE":
                pluXml.Code = GetXmlAttributeString(xmlNode, pluXml, xmlPropertyName);
                break;
            case "MEASUREMENTTYPE":
                pluXml.MeasurementType = GetXmlAttributeString(xmlNode, pluXml, xmlPropertyName);
                pluXml.IsCheckWeight = GetXmlAttributeBool(xmlNode, pluXml, xmlPropertyName, "ШТ", "КГ");
                break;
            case "PLUNUMBER":
                pluXml.Number = GetXmlAttributeShort(xmlNode, pluXml, xmlPropertyName);
                break;
            case "SHELFLIFE":
                pluXml.ShelfLifeDays = GetXmlAttributeByte(xmlNode, pluXml, xmlPropertyName);
                break;
            case "PARENTGROUPGUID":
                pluXml.ParentGuid = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "GROUPGUID":
                pluXml.GroupGuid = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "CATEGORYGUID":
                pluXml.CategoryGuid = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "BRANDGUID":
                pluXml.BrandGuid = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "BOXTYPEGUID":
                pluXml.BoxTypeGuid = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "BOXTYPENAME":
                pluXml.BoxTypeName = GetXmlAttributeString(xmlNode, pluXml, xmlPropertyName);
                break;
            case "BOXTYPEWEIGHT":
                pluXml.BoxTypeWeight = GetXmlAttributeDecimal(xmlNode, pluXml, xmlPropertyName);
                break;
            case "PACKAGETYPEGUID":
                pluXml.PackageTypeGuid = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "PACKAGETYPENAME":
                pluXml.PackageTypeName = GetXmlAttributeString(xmlNode, pluXml, xmlPropertyName);
                break;
            case "PACKAGETYPEWEIGHT":
                pluXml.PackageTypeWeight = GetXmlAttributeDecimal(xmlNode, pluXml, xmlPropertyName);
                break;
            case "CLIPTYPEGUID":
                pluXml.ClipTypeGuid = GetXmlAttributeGuid(xmlNode, pluXml, xmlPropertyName);
                break;
            case "CLIPTYPENAME":
                pluXml.ClipTypeName = GetXmlAttributeString(xmlNode, pluXml, xmlPropertyName);
                break;
            case "CLIPTYPEWEIGHT":
                pluXml.ClipTypeWeight = GetXmlAttributeDecimal(xmlNode, pluXml, xmlPropertyName);
                break;
            case "ATTACHMENTSCOUNT":
                pluXml.AttachmentsCount = GetXmlAttributeShort(xmlNode, pluXml, xmlPropertyName);
                break;
            case "GTIN":
                pluXml.Gtin = GetXmlAttributeString(xmlNode, pluXml, xmlPropertyName, false);
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForPluGroup(XmlNode xmlNode, PluGroupModel pluGroupXml, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                pluGroupXml.Uid1C = GetXmlAttributeGuid(xmlNode, pluGroupXml, xmlPropertyName);
                break;
            case "ISGROUP":
                pluGroupXml.IsGroup = GetXmlAttributeBool(xmlNode, pluGroupXml, xmlPropertyName);
                break;
            case "CODE":
                pluGroupXml.Code = GetXmlAttributeString(xmlNode, pluGroupXml, xmlPropertyName);
                break;
            case "GROUPGUID":
                pluGroupXml.ParentGuid = GetXmlAttributeGuid(xmlNode, pluGroupXml, xmlPropertyName);
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForPluCharacteristic(XmlNode xmlNode, 
        PluCharacteristicModel pluCharacteristicXml, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                pluCharacteristicXml.Uid1C = GetXmlAttributeGuid(xmlNode, pluCharacteristicXml, xmlPropertyName);
                break;
            case "NOMENCLATUREGUID":
                pluCharacteristicXml.NomenclatureGuid = GetXmlAttributeGuid(xmlNode, pluCharacteristicXml, xmlPropertyName);
                break;
            case "ATTACHMENTSCOUNT":
                pluCharacteristicXml.AttachmentsCount = GetXmlAttributeDecimal(xmlNode, pluCharacteristicXml, xmlPropertyName);
                break;
        }
    }

    public static int GetAttributeValueAsInt(string xml, string nodeIdentity)
    {
        if (!string.IsNullOrEmpty(xml) && GetAttributeValue(xml, nodeIdentity) is { } value)
        {
            if (Int32.TryParse((string?)value, out int iValue))
                return iValue;
        }
        return default;
    }

    private static string GetAttributeValue(string xml, string nodeIdentity)
    {
        try
        {
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(xml);
            if (xmlDocument.DocumentElement is not null)
                foreach (XmlAttribute xmlAttribute in xmlDocument.DocumentElement.Attributes)
                {
                    if (xmlAttribute.Name.Equals(nodeIdentity, StringComparison.InvariantCultureIgnoreCase))
                    {
                        try
                        {
                            return xmlAttribute.Value;
                        }
                        catch (Exception)
                        {
                            //
                        }
                    }
                }
        }
        catch (Exception)
        {
            //
        }
        return string.Empty;
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

    private static decimal GetXmlAttributeDecimal<T>(XmlNode? xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase =>
        decimal.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out decimal result) ? result : default;

    public static TResult GetXmlAttributeGeneric<T, TResult>(XmlNode? xmlNode, T itemXml, string xmlPropertyName) where T : WsSqlTableBase where TResult : struct
    {
        TResult result = default;
        switch (typeof(TResult))
        {
            case var cls when cls == typeof(Guid):
                Guid resultGuid = Guid.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out Guid iGuid) ? iGuid : Guid.Empty;
                if (resultGuid is TResult guidResult)
                    result = guidResult;
                break;
            case var cls when cls == typeof(byte):
                byte resultByte = byte.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out byte iByte) ? iByte : default;
                if (resultByte is TResult byteResult)
                    result = byteResult;
                break;
            case var cls when cls == typeof(bool):
                bool resultBool = bool.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out bool iBool) ? iBool : default;
                if (resultBool is TResult boolResult)
                    result = boolResult;
                break;
            case var cls when cls == typeof(short):
                short resultShort = short.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out short iShort) ? iShort : default;
                if (resultShort is TResult shortResult)
                    result = shortResult;
                break;
            case var cls when cls == typeof(ushort):
                ushort resultUshort = ushort.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out ushort iUshort) ? iUshort : default;
                if (resultUshort is TResult ushortResult)
                    result = ushortResult;
                break;
            case var cls when cls == typeof(int):
                int resultInt = int.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out int iInt) ? iInt : default;
                if (resultInt is TResult intResult)
                    result = intResult;
                break;
            case var cls when cls == typeof(uint):
                uint resultUint = uint.TryParse(GetXmlAttributeString(xmlNode, itemXml, xmlPropertyName), out uint iUint) ? iUint : default;
                if (resultUint is TResult uintResult)
                    result = uintResult;
                break;
        }
        return result;
    }

    #endregion
}