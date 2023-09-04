namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты получения данных XML веб-сервиса.
/// </summary>
public static class WsServiceUtilsGetXml
{
    #region Public and private methods

    public static List<WsXmlContentRecord<WsSqlBrandModel>> GetXmlBrandList(XElement xml) =>
        WsServiceUtilsGetXmlContent.GetNodesListCore<WsSqlBrandModel>(xml, WsLocaleCore.WebService.XmlItemBrand,
            (xmlNode, itemXml) =>
            {
                WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
                WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
                WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
                WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            });

    /// <summary>
    /// Заполнить список ПЛУ из XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static List<WsXmlContentRecord<WsSqlPluModel>> GetXmlPluList(XElement xml) =>
  WsServiceUtilsGetXmlContent.GetNodesListCore<WsSqlPluModel>(xml, WsLocaleCore.WebService.XmlItemNomenclature, (xmlNode, itemXml) =>
        {
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.FullName));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.CategoryGuid));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BrandGuid));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.MeasurementType));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.GroupGuid));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.AttachmentsCount));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Ean13));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Itf14));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeGuid));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeName));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeWeight));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeGuid));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeName));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeWeight));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeGuid));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeName));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeWeight));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PluNumber");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Description));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ShelfLife");
        });

    /// <summary>
    /// Получить список свойств XML.
    /// </summary>
    /// <returns></returns>
    public static string[] GetXmlPluPropertiesArray() => new[]
    {
        nameof(WsSqlPluModel.BoxTypeGuid),
        nameof(WsSqlPluModel.BoxTypeName),
        nameof(WsSqlPluModel.BoxTypeWeight),
        nameof(WsSqlPluModel.BrandGuid),
        nameof(WsSqlPluModel.CategoryGuid),
        nameof(WsSqlPluModel.ClipTypeGuid),
        nameof(WsSqlPluModel.ClipTypeName),
        nameof(WsSqlPluModel.ClipTypeWeight),
        nameof(WsSqlPluModel.Code),
        nameof(WsSqlPluModel.Description),
        nameof(WsSqlPluModel.FullName),
        nameof(WsSqlPluModel.GroupGuid),
        nameof(WsSqlPluModel.IdentityValueUid),
        nameof(WsSqlPluModel.IsCheckWeight),
        nameof(WsSqlPluModel.IsGroup),
        nameof(WsSqlPluModel.IsMarked),
        nameof(WsSqlPluModel.MeasurementType),
        nameof(WsSqlPluModel.Name),
        nameof(WsSqlPluModel.Number),
        nameof(WsSqlPluModel.PackageTypeGuid),
        nameof(WsSqlPluModel.PackageTypeName),
        nameof(WsSqlPluModel.PackageTypeWeight),
        nameof(WsSqlPluModel.ParentGuid),
        nameof(WsSqlPluModel.ShelfLifeDays),
    };

    /// <summary>
    /// Получить список групп из XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static List<WsXmlContentRecord<WsSqlPluGroupModel>> GetXmlPluGroupsList(XElement xml) =>
        WsServiceUtilsGetXmlContent.GetNodesListCore<WsSqlPluGroupModel>(xml, WsLocaleCore.WebService.XmlItemNomenclatureGroup, (xmlNode, itemXml) =>
        {
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BoxTypeGuid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "BrandGuid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "CategoryGuid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ClipTypeGuid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "GroupGuid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PackageTypeGuid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
        });

    /// <summary>
    /// Заполнить список характеристик ПЛУ из XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static List<WsXmlContentRecord<WsSqlPluCharacteristicModel>> GetXmlPluCharacteristicsList(XElement xml) =>
        WsServiceUtilsGetXmlContent.GetNodesListCore<WsSqlPluCharacteristicModel>(xml, WsLocaleCore.WebService.XmlItemCharacteristic, (xmlNode, itemXml) =>
        {
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            WsServiceUtilsGetXmlContent.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "NomenclatureGuid");
        });

    #endregion
}
