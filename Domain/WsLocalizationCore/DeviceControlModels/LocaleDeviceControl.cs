namespace WsLocalizationCore.DeviceControlModels;

public sealed class LocaleDeviceControl : LocalizationDeviceControl
{
    #region Public and private fields, properties, constructor
    public string Index => Lang == EnumLanguage.English ? "DeviceControl" : "Управление устройствами";
    public string IndexContinue => Lang == EnumLanguage.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
    public string ItemBarcode => Lang == EnumLanguage.English ? "Barcode" : "Штрихкод";
    public string ItemBarCodeType => Lang == EnumLanguage.English ? "Barcodes type" : "Тип штрихкода";
    public string ItemBox => Lang == EnumLanguage.English ? "Box" : "Коробка";
    public string ItemBundle => Lang == EnumLanguage.English ? "Bundle" : "Пакет";
    public string ItemHost => Lang == EnumLanguage.English ? "Host" : "Хост";
    public string ItemLabel => Lang == EnumLanguage.English ? "Label" : "Этикетка";
    public string ItemLog => Lang == EnumLanguage.English ? "Log" : "Лог";
    public string ItemNomenclature => Lang == EnumLanguage.English ? "Nomenclature" : "Номенклатура";
    public string ItemPluNestingFk => Lang == EnumLanguage.English ? "PLU's nesting" : "Вложенность ПЛУ";
    public string ItemPlusStorage => Lang == EnumLanguage.English ? "PLU storage" : "Cпособ хранения ПЛУ";
    public string ItemPluWeighing => Lang == EnumLanguage.English ? "Plu weighings" : "Взвешивание ПЛУ"; 
    public string ItemProductionFacility => Lang == EnumLanguage.English ? "Prod. facility" : "Произв. площадка";
    public string ItemScale => Lang == EnumLanguage.English ? "Line" : "Линия";
    public string ItemTemplate => Lang == EnumLanguage.English ? "Template" : "Шаблон";
    public string ItemTemplateResource => Lang == EnumLanguage.English ? "Template resource" : "Ресурс шаблона";
    public string ItemWorkshop => Lang == EnumLanguage.English ? "Workshop" : "Цех";
    public string LinkLabelary => "http://labelary.com/viewer.html";
    public string SectionAdministering => Lang == EnumLanguage.English ? "Administering" : "Администрирование";
    public string SectionBarCodes => Lang == EnumLanguage.English ? "Barcodes" : "Штрихкоды";
    public string SectionBoxes => Lang == EnumLanguage.English ? "Boxes" : "Коробки";
    public string SectionBrands => Lang == EnumLanguage.English ? "Brands" : "Бренды";
    public string SectionBundles => Lang == EnumLanguage.English ? "Bundles" : "Пакеты";
    public string SectionClips => Lang == EnumLanguage.English ? "Clips" : "Клипсы";
    public string SectionDevices => Lang == EnumLanguage.English ? "Devices" : "Устройства";
    public string SectionHosts => Lang == EnumLanguage.English ? "Hosts" : "Хосты";
    public string SectionLabels => Lang == EnumLanguage.English ? "Labels" : "Этикетки";
    public string SectionOperations => Lang == EnumLanguage.English ? "Operations" : "Операции";
    public string SectionOrganizations => Lang == EnumLanguage.English ? "Organizations" : "Организации";
    public string SectionPlus => Lang == EnumLanguage.English ? "PLUs" : "ПЛУ";
    public string SectionPlusNestingFk => Lang == EnumLanguage.English ? "PLU's nesting" : "Вложенности ПЛУ";
    public string SectionPlusScales => Lang == EnumLanguage.English ? "PLU & Lines" : "ПЛУ и Линия";
    public string SectionPlusStorage => Lang == EnumLanguage.English ? "PLUs storage" : "Cпособы хранения ПЛУ";
    public string SectionPlusWeightings => Lang == EnumLanguage.English ? "Plus Weightings" : "Взвешивания ПЛУ";
    public string SectionProductionFacilities => Lang == EnumLanguage.English ? "Production facilities" : "Производственные площадки";
    public string SectionProductionFacilitiesShort => Lang == EnumLanguage.English ? "Facilities" : "Площадки";
    public string SectionReferences => Lang == EnumLanguage.English ? "References" : "Справочники";
    public string SectionReferences1C => Lang == EnumLanguage.English ? "References 1C" : "Данные из 1C";
    public string SectionScales => Lang == EnumLanguage.English ? "Lines" : "Линии";
    public string SectionTemplateResources => Lang == EnumLanguage.English ? "Template resources" : "Ресурсы шаблонов";
    public string SectionTemplates => Lang == EnumLanguage.English ? "Templates" : "Шаблоны";
    public string SectionWeighings => Lang == EnumLanguage.English ? "Weighings" : "Взвешивания";
    public string SectionWeithingFactsAggregation => Lang == EnumLanguage.English ? "Aggregation weithings" : "Агрегированные этикетки";
    public string SectionWeithingFactsAggregationShort => Lang == EnumLanguage.English ? "Aggr. weithings" : "Агр. этикетки";
    public string SectionWorkShops => Lang == EnumLanguage.English ? "Workshops" : "Цеха";
    public string TableActionCancel => Lang == EnumLanguage.English ? "Cancel" : "Отмена";
    public string TableActionCopy => Lang == EnumLanguage.English ? "Copy" : "Копировать";
    public string TableActionSave => Lang == EnumLanguage.English ? "Save" : "Сохранить";
    
    #endregion
}