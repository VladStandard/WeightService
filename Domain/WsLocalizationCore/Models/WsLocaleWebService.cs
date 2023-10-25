namespace WsLocalizationCore.Models;

public sealed class WsLocaleWebService : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string BoxZero => Lang == WsEnumLanguage.English ? "Without the box" : "Без коробки";
    public string BundleZero => Lang == WsEnumLanguage.English ? "Without the bundle" : "Без пакета";
    public string ClipZero => Lang == WsEnumLanguage.English ? "Without the clip" : "Без клипсы";
    public string DtStamp => Lang == WsEnumLanguage.English ? "Date time stamp" : "Отметка времени даты";
    public string Dublicate => Lang == WsEnumLanguage.English ? "Dublicate" : "Дубликат";
    public string FieldBrand => Lang == WsEnumLanguage.English ? "Brand" : "Бренд";
    public string FieldBundle => Lang == WsEnumLanguage.English ? "Bundle" : "Пакет";
    public string FieldClip => Lang == WsEnumLanguage.English ? "Clip" : "Клипса";
    public string FieldCode => Lang == WsEnumLanguage.English ? "code" : "код";
    public string FieldCodes => Lang == WsEnumLanguage.English ? "codes" : "коды";
    public string FieldGroup => Lang == WsEnumLanguage.English ? "Group" : "Группа";
    public string FieldGroup1Level => Lang == WsEnumLanguage.English ? "Level 1 group" : "Группа 1 уровня";
    public string FieldGuid => Lang == WsEnumLanguage.English ? "GUID" : "ГУИД";
    public string FieldNomenclature => Lang == WsEnumLanguage.English ? "Nomenclature" : "Номенклатура";
    public string FieldNomenclatureCharacteristic => Lang == WsEnumLanguage.English ? "Nomeclature characteristic" : "Номенклатурная характеристика";
    public string FieldNomenclatureGroup => Lang == WsEnumLanguage.English ? "Nomenclature group" : "Номенклатурная группа";
    public string FieldNomenclatureIsDenyForLoadByUid1C => Lang == WsEnumLanguage.English ? "Nomenclature is deny for load by UID_1C" : "Номенклатура запрещена для загрузки по UID_1C";
    public string FieldNomenclatureIsDenyForLoadByNumber => Lang == WsEnumLanguage.English ? "Nomenclature is deny for load by PLU number" : "Номенклатура запрещена для загрузки по номеру ПЛУ";
    public string FieldNomenclatureIsDiffForLoadByNumber(short pluNumberDb, short pluNumberXml) => Lang == WsEnumLanguage.English ? "The PLU number is forbidden or does not match! | In the database {pluNumberDb} in the XML {pluNumberXml}" : $"Номер ПЛУ запрещён либо не соответствует! | В БД '{pluNumberDb}' в XML '{pluNumberXml}'";
    public string FieldNomenclatureIsZeroNumber(short pluNumber) => Lang == WsEnumLanguage.English ? $"Invalid PLU number is specified '{pluNumber}'" : $"Указан недопустимый номер ПЛУ '{pluNumber}'";
    public string FieldNomenclatureIsErrorUid1C => Lang == WsEnumLanguage.English ? "Nomenclature UID_1C is error" : "Ошибка UID_1C номенклатуры";
    public string FieldNomenclatureIsNotFound => Lang == WsEnumLanguage.English ? "Nomenclature is not found" : "Номенклатура не найдена";
    public string FieldNomenclatureParent => Lang == WsEnumLanguage.English ? "Parent nomenclature" : "Родительская номенклатура";
    public string FieldPluNumber => Lang == WsEnumLanguage.English ? "PLU number" : "Номер ПЛУ";
    public string FieldPluNumberTemplate(short pluNumber) => Lang == WsEnumLanguage.English ? $"PLU number '{pluNumber:0000}' | " : $"Номер ПЛУ '{pluNumber:0000}' | ";
    public string FieldPluNumberNotInAcl => Lang == WsEnumLanguage.English ? "PLU number is not included in the list of allowed" : "Номер ПЛУ не входит в список разрешённых";
    public string ForDbRecord => Lang == WsEnumLanguage.English ? "for DB record" : "для записи БД";
    public string ForRecord => Lang == WsEnumLanguage.English ? "for record" : "для записи";
    public string IsEmpty => Lang == WsEnumLanguage.English ? "is empty value" : "пустое значение";
    public string IsFound => Lang == WsEnumLanguage.English ? "is found" : "найдено";
    public string IsNotFound => Lang == WsEnumLanguage.English ? "is not found" : "не найдено";
    public string IsNotIdent => Lang == WsEnumLanguage.English ? "is not ident" : "не определено";
    public string IsStatusSuccess => Lang == WsEnumLanguage.English ? "Done successfully" : "Выполнено успешно";
    public string LogTypeMetaData => Lang == WsEnumLanguage.English ? "Metadata" : "Метаданные";
    public string LogTypeRequest => Lang == WsEnumLanguage.English ? "Request" : "Запрос";
    public string LogTypeResponse => Lang == WsEnumLanguage.English ? "Response" : "Ответ";
    public string Name => Lang == WsEnumLanguage.English ? "WebService 1C" : "ВебСервис 1С";
    public string Node => Lang == WsEnumLanguage.English ? "node" : "узел";
    public string PackageZero => Lang == WsEnumLanguage.English ? "Without the package" : "Без пакета";
    public string Underdevelopment(int percent) => Lang == WsEnumLanguage.English ? $"Under development, contact the developer! Progress of execution {percent}%." : $"Находится в разработке, свяжитесь с разработчиком! Прогресс выполнения {percent}%.";
    public string With => Lang == WsEnumLanguage.English ? "with" : "с";
    public string WithFieldCode => Lang == WsEnumLanguage.English ? "with code" : "с кодом";
    public string WithFieldNumber => Lang == WsEnumLanguage.English ? "with number" : "с номером";
    public string XmlItemBrand => "Brand";
    public string XmlItemCharacteristic => "Characteristic";
    public string XmlItemNomenclature => "Nomenclature";
    public string XmlItemNomenclatureGroup => "NomenclatureGroup";
    public string PluFoundMoreThen1() => "Найдено более 1 номенклатуры!";
    public string PluNotFound() => "Номенклатуры не найдено!";
    public string FieldPluCharacteristicMustBeNotDefault() => "Характеристика совпадает со вложенностью по-молчанию!";
    public string FieldPluCharacteristicNotFoundDefault() => "Вложенности по-молчанию не найдено!";

    #endregion
}