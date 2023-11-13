namespace WsLocalizationCore.DeviceControlModels;

public sealed class WsLocaleDeviceControl : WsLocalizationDeviceControl
{
    #region Public and private fields, properties, constructor
    public string Index => Lang == WsEnumLanguage.English ? "DeviceControl" : "Управление устройствами";
    public string IndexContinue => Lang == WsEnumLanguage.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
    public string ItemBarcode => Lang == WsEnumLanguage.English ? "Barcode" : "Штрихкод";
    public string ItemBarCodeType => Lang == WsEnumLanguage.English ? "Barcodes type" : "Тип штрихкода";
    public string ItemBox => Lang == WsEnumLanguage.English ? "Box" : "Коробка";
    public string ItemBundle => Lang == WsEnumLanguage.English ? "Bundle" : "Пакет";
    public string ItemHost => Lang == WsEnumLanguage.English ? "Host" : "Хост";
    public string ItemLabel => Lang == WsEnumLanguage.English ? "Label" : "Этикетка";
    public string ItemLog => Lang == WsEnumLanguage.English ? "Log" : "Лог";
    public string ItemNomenclature => Lang == WsEnumLanguage.English ? "Nomenclature" : "Номенклатура";
    public string ItemPluNestingFk => Lang == WsEnumLanguage.English ? "PLU's nesting" : "Вложенность ПЛУ";
    public string ItemPlusStorage => Lang == WsEnumLanguage.English ? "PLU storage" : "Cпособ хранения ПЛУ";
    public string ItemPluWeighing => Lang == WsEnumLanguage.English ? "Plu weighings" : "Взвешивание ПЛУ"; 
    public string ItemProductionFacility => Lang == WsEnumLanguage.English ? "Prod. facility" : "Произв. площадка";
    public string ItemScale => Lang == WsEnumLanguage.English ? "Line" : "Линия";
    public string ItemTemplate => Lang == WsEnumLanguage.English ? "Template" : "Шаблон";
    public string ItemTemplateResource => Lang == WsEnumLanguage.English ? "Template resource" : "Ресурс шаблона";
    public string ItemWorkshop => Lang == WsEnumLanguage.English ? "Workshop" : "Цех";
    public string LinkLabelary => "http://labelary.com/viewer.html";
    public string SectionAdministering => Lang == WsEnumLanguage.English ? "Administering" : "Администрирование";
    public string SectionBarCodes => Lang == WsEnumLanguage.English ? "Barcodes" : "Штрихкоды";
    public string SectionBoxes => Lang == WsEnumLanguage.English ? "Boxes" : "Коробки";
    public string SectionBrands => Lang == WsEnumLanguage.English ? "Brands" : "Бренды";
    public string SectionBundles => Lang == WsEnumLanguage.English ? "Bundles" : "Пакеты";
    public string SectionClips => Lang == WsEnumLanguage.English ? "Clips" : "Клипсы";
    public string SectionDevices => Lang == WsEnumLanguage.English ? "Devices" : "Устройства";
    public string SectionHosts => Lang == WsEnumLanguage.English ? "Hosts" : "Хосты";
    public string SectionLabels => Lang == WsEnumLanguage.English ? "Labels" : "Этикетки";
    public string SectionOperations => Lang == WsEnumLanguage.English ? "Operations" : "Операции";
    public string SectionOrganizations => Lang == WsEnumLanguage.English ? "Organizations" : "Организации";
    public string SectionPlus => Lang == WsEnumLanguage.English ? "PLUs" : "ПЛУ";
    public string SectionPlusNestingFk => Lang == WsEnumLanguage.English ? "PLU's nesting" : "Вложенности ПЛУ";
    public string SectionPlusScales => Lang == WsEnumLanguage.English ? "PLU & Lines" : "ПЛУ и Линия";
    public string SectionPlusStorage => Lang == WsEnumLanguage.English ? "PLUs storage" : "Cпособы хранения ПЛУ";
    public string SectionPlusWeightings => Lang == WsEnumLanguage.English ? "Plus Weightings" : "Взвешивания ПЛУ";
    public string SectionProductionFacilities => Lang == WsEnumLanguage.English ? "Production facilities" : "Производственные площадки";
    public string SectionProductionFacilitiesShort => Lang == WsEnumLanguage.English ? "Facilities" : "Площадки";
    public string SectionReferences => Lang == WsEnumLanguage.English ? "References" : "Справочники";
    public string SectionReferences1C => Lang == WsEnumLanguage.English ? "References 1C" : "Данные из 1C";
    public string SectionScales => Lang == WsEnumLanguage.English ? "Lines" : "Линии";
    public string SectionTemplateResources => Lang == WsEnumLanguage.English ? "Template resources" : "Ресурсы шаблонов";
    public string SectionTemplates => Lang == WsEnumLanguage.English ? "Templates" : "Шаблоны";
    public string SectionWeighings => Lang == WsEnumLanguage.English ? "Weighings" : "Взвешивания";
    public string SectionWeithingFactsAggregation => Lang == WsEnumLanguage.English ? "Aggregation weithings" : "Агрегированные этикетки";
    public string SectionWeithingFactsAggregationShort => Lang == WsEnumLanguage.English ? "Aggr. weithings" : "Агр. этикетки";
    public string SectionWorkShops => Lang == WsEnumLanguage.English ? "Workshops" : "Цеха";
    public string TableActionCancel => Lang == WsEnumLanguage.English ? "Cancel" : "Отмена";
    public string TableActionCopy => Lang == WsEnumLanguage.English ? "Copy" : "Копировать";
    public string TableActionSave => Lang == WsEnumLanguage.English ? "Save" : "Сохранить";
    
    #endregion
}