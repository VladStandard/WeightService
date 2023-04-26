// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

/// <summary>
/// Content utils.
/// </summary>
public static class WsContentUtils
{
    #region Public and private methods

    public static List<T> GetNodesListCore<T>(XElement xml, string nodeIdentity, Action<XmlNode, T> action) where T : WsSqlTableBase, new()
    {
        List<T> itemsXml = new();
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
            itemsXml.Add(itemXml);
        }
        return itemsXml;
    }

    public static void SetItemParseResultException<T>(T item, string xmlPropertyName) where T : WsSqlTableBase
    {
        item.ParseResult.Status = ParseStatus.Error;
        item.ParseResult.Exception = string.IsNullOrEmpty(item.ParseResult.Exception)
            ? $"{xmlPropertyName} {LocaleCore.WebService.IsEmpty}!"
            : $"{item.ParseResult.Exception} | {xmlPropertyName} {LocaleCore.WebService.IsEmpty}!";
    }

    public static void SetItemPropertyFromXmlAttribute<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase
    {
        SetItemPropertyFromXmlAttributeForBase(xmlNode, item, xmlPropertyName);
        switch (item)
        {
            case BrandModel brand:
                SetItemPropertyFromXmlAttributeForBrand(xmlNode, brand, xmlPropertyName);
                break;
            case PluModel plu:
                SetItemPropertyFromXmlAttributeForPlu(xmlNode, plu, xmlPropertyName);
                break;
            case PluGroupModel pluGroup:
                SetItemPropertyFromXmlAttributeForPluGroup(xmlNode, pluGroup, xmlPropertyName);
                break;
            case PluCharacteristicModel pluCharacteristic:
                SetItemPropertyFromXmlAttributeForPluCharacteristic(xmlNode, pluCharacteristic, xmlPropertyName);
                break;
        }
    }

    public static void SetItemPropertyFromXmlAttributeForBase<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.IdentityValueUid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ISMARKED":
                item.IsMarked = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "NAME":
                item.Name = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "DESCRIPTION":
                item.Description = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    public static void SetItemPropertyFromXmlAttributeForBrand(XmlNode xmlNode, BrandModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    public static void SetItemPropertyFromXmlAttributeForPlu(XmlNode xmlNode, PluModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ISGROUP":
                item.IsGroup = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "FULLNAME":
                item.FullName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "MEASUREMENTTYPE":
                item.MeasurementType = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                item.IsCheckWeight = GetXmlAttributeBool(xmlNode, item, xmlPropertyName, "ШТ", "КГ");
                break;
            case "PLUNUMBER":
                item.Number = GetXmlAttributeShort(xmlNode, item, xmlPropertyName);
                break;
            case "SHELFLIFE":
                item.ShelfLifeDays = GetXmlAttributeByte(xmlNode, item, xmlPropertyName);
                break;
            case "PARENTGROUPGUID":
                item.ParentGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "GROUPGUID":
                item.GroupGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "CATEGORYGUID":
                item.CategoryGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "BRANDGUID":
                item.BrandGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "BOXTYPEGUID":
                item.BoxTypeGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "BOXTYPENAME":
                item.BoxTypeName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "BOXTYPEWEIGHT":
                item.BoxTypeWeight = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
            case "PACKAGETYPEGUID":
                item.PackageTypeGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "PACKAGETYPENAME":
                item.PackageTypeName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "PACKAGETYPEWEIGHT":
                item.PackageTypeWeight = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
            case "CLIPTYPEGUID":
                item.ClipTypeGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "CLIPTYPENAME":
                item.ClipTypeName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "CLIPTYPEWEIGHT":
                item.ClipTypeWeight = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
            case "ATTACHMENTSCOUNT":
                item.AttachmentsCount = GetXmlAttributeShort(xmlNode, item, xmlPropertyName);
                break;
            case "GTIN":
                item.Gtin = GetXmlAttributeString(xmlNode, item, xmlPropertyName, false);
                break;
        }
    }

    public static void SetItemPropertyFromXmlAttributeForPluGroup(XmlNode xmlNode, PluGroupModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ISGROUP":
                item.IsGroup = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "GROUPGUID":
                item.ParentGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    public static void SetItemPropertyFromXmlAttributeForPluCharacteristic(XmlNode xmlNode, PluCharacteristicModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "NOMENCLATUREGUID":
                item.NomenclatureGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ATTACHMENTSCOUNT":
                item.AttachmentsCount = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
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

    public static string GetAttributeValue(string xml, string nodeIdentity)
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

    public static string GetXmlAttributeString<T>(XmlNode? xmlNode, T item, string attributeName,
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
            SetItemParseResultException(item, attributeName);
        return string.Empty;
    }

    public static bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName,
        List<string> valuesFalse, List<string> valuesTrue) where T : WsSqlTableBase
    {
        string value = GetXmlAttributeString(xmlNode, item, xmlPropertyName).ToUpper();
        if (Enumerable.Contains(valuesFalse, value)) return false;
        if (Enumerable.Contains(valuesTrue, value)) return true;
        return default;
    }

    public static bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase =>
        GetXmlAttributeBool(xmlNode, item, xmlPropertyName, new List<string> { "0", "FALSE" }, new() { "1", "TRUE" });

    public static bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName,
        string valueFalse, string valueTrue) where T : WsSqlTableBase =>
        GetXmlAttributeBool(xmlNode, item, xmlPropertyName, new List<string> { valueFalse }, new() { valueTrue });

    public static Guid GetXmlAttributeGuid<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase =>
        Guid.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out Guid uid) ? uid : Guid.Empty;

    public static byte GetXmlAttributeByte<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase =>
        byte.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out byte result) ? result : default;

    public static short GetXmlAttributeShort<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase =>
        short.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out short result) ? result : default;

    public static decimal GetXmlAttributeDecimal<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase =>
        decimal.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out decimal result) ? result : default;

    public static TResult GetXmlAttributeGeneric<T, TResult>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : WsSqlTableBase where TResult : struct
    {
        TResult result = default;
        switch (typeof(TResult))
        {
            case var cls when cls == typeof(Guid):
                Guid resultGuid = Guid.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out Guid iGuid) ? iGuid : Guid.Empty;
                if (resultGuid is TResult guidResult)
                    result = guidResult;
                break;
            case var cls when cls == typeof(byte):
                byte resultByte = byte.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out byte iByte) ? iByte : default;
                if (resultByte is TResult byteResult)
                    result = byteResult;
                break;
            case var cls when cls == typeof(bool):
                bool resultBool = bool.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out bool iBool) ? iBool : default;
                if (resultBool is TResult boolResult)
                    result = boolResult;
                break;
            case var cls when cls == typeof(short):
                short resultShort = short.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out short iShort) ? iShort : default;
                if (resultShort is TResult shortResult)
                    result = shortResult;
                break;
            case var cls when cls == typeof(ushort):
                ushort resultUshort = ushort.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out ushort iUshort) ? iUshort : default;
                if (resultUshort is TResult ushortResult)
                    result = ushortResult;
                break;
            case var cls when cls == typeof(int):
                int resultInt = int.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out int iInt) ? iInt : default;
                if (resultInt is TResult intResult)
                    result = intResult;
                break;
            case var cls when cls == typeof(uint):
                uint resultUint = uint.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out uint iUint) ? iUint : default;
                if (resultUint is TResult uintResult)
                    result = uintResult;
                break;
        }
        return result;
    }

    // \\palych\Exchange\Bulk insert\PLUS_13.xlsx
    public static List<short> AclSardelki =>
        new() { 701, 702, 703, 705, 706, 707, 710, 712, 713, 715, 719, 720, 724, 725, 729, 730, 731, 732 };
    public static List<short> AclSosiski => new() { 801, 802, 803, 804, 805, 806, 808, 809, 810, 811, 812, 813, 814, 817, 819, 820, 821, 822,
        823, 825, 826, 827, 828, 830, 832, 834, 835, 836, 837, 841, 842, 843, 844, 846, 847, 848, 849, 855, 831, 858, 860,
        861, 862, 863, 807 };


    #endregion
}