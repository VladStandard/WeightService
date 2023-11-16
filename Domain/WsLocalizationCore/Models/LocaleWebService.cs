using WsLocalizationCore.Common;
namespace WsLocalizationCore.Models;

public sealed class LocaleWebService : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string BoxZero => Lang == EnumLanguage.English ? "Without the box" : "Без коробки";
    public string BundleZero => Lang == EnumLanguage.English ? "Without the bundle" : "Без пакета";
    public string ClipZero => Lang == EnumLanguage.English ? "Without the clip" : "Без клипсы";
    public string DtStamp => Lang == EnumLanguage.English ? "Date time stamp" : "Отметка времени даты";
    public string Dublicate => Lang == EnumLanguage.English ? "Dublicate" : "Дубликат";
    public string FieldBrand => Lang == EnumLanguage.English ? "Brand" : "Бренд";
    public string FieldBundle => Lang == EnumLanguage.English ? "Bundle" : "Пакет";
    public string FieldClip => Lang == EnumLanguage.English ? "Clip" : "Клипса";
    public string FieldCode => Lang == EnumLanguage.English ? "code" : "код";
    public string FieldCodes => Lang == EnumLanguage.English ? "codes" : "коды";
    public string FieldGroup => Lang == EnumLanguage.English ? "Group" : "Группа";
    public string FieldGroup1Level => Lang == EnumLanguage.English ? "Level 1 group" : "Группа 1 уровня";
    public string FieldGuid => Lang == EnumLanguage.English ? "GUID" : "ГУИД";
    public string FieldNomenclature => Lang == EnumLanguage.English ? "Nomenclature" : "Номенклатура";
    public string FieldNomenclatureCharacteristic => Lang == EnumLanguage.English ? "Nomeclature characteristic" : "Номенклатурная характеристика";
    public string FieldNomenclatureGroup => Lang == EnumLanguage.English ? "Nomenclature group" : "Номенклатурная группа";
    public string FieldNomenclatureIsDenyForLoadByUid1C => Lang == EnumLanguage.English ? "Nomenclature is deny for load by UID_1C" : "Номенклатура запрещена для загрузки по UID_1C";
    public string FieldNomenclatureIsDenyForLoadByNumber => Lang == EnumLanguage.English ? "Nomenclature is deny for load by PLU number" : "Номенклатура запрещена для загрузки по номеру ПЛУ";
    public string FieldNomenclatureIsDiffForLoadByNumber(short pluNumberDb, short pluNumberXml) => Lang == EnumLanguage.English ? "The PLU number is forbidden or does not match! | In the database {pluNumberDb} in the XML {pluNumberXml}" : $"Номер ПЛУ запрещён либо не соответствует! | В БД '{pluNumberDb}' в XML '{pluNumberXml}'";
    public string FieldNomenclatureIsZeroNumber(short pluNumber) => Lang == EnumLanguage.English ? $"Invalid PLU number is specified '{pluNumber}'" : $"Указан недопустимый номер ПЛУ '{pluNumber}'";
    public string FieldNomenclatureIsErrorUid1C => Lang == EnumLanguage.English ? "Nomenclature UID_1C is error" : "Ошибка UID_1C номенклатуры";
    public string FieldNomenclatureIsNotFound => Lang == EnumLanguage.English ? "Nomenclature is not found" : "Номенклатура не найдена";
    public string FieldNomenclatureParent => Lang == EnumLanguage.English ? "Parent nomenclature" : "Родительская номенклатура";
    public string FieldPluNumber => Lang == EnumLanguage.English ? "PLU number" : "Номер ПЛУ";
    public string FieldPluNumberTemplate(short pluNumber) => Lang == EnumLanguage.English ? $"PLU number '{pluNumber:0000}' | " : $"Номер ПЛУ '{pluNumber:0000}' | ";
    public string FieldPluNumberNotInAcl => Lang == EnumLanguage.English ? "PLU number is not included in the list of allowed" : "Номер ПЛУ не входит в список разрешённых";
    public string ForDbRecord => Lang == EnumLanguage.English ? "for DB record" : "для записи БД";
    public string ForRecord => Lang == EnumLanguage.English ? "for record" : "для записи";
    public string IsEmpty => Lang == EnumLanguage.English ? "is empty value" : "пустое значение";
    public string IsFound => Lang == EnumLanguage.English ? "is found" : "найдено";
    public string IsNotFound => Lang == EnumLanguage.English ? "is not found" : "не найдено";
    public string IsNotIdent => Lang == EnumLanguage.English ? "is not ident" : "не определено";
    public string IsStatusSuccess => Lang == EnumLanguage.English ? "Done successfully" : "Выполнено успешно";
    public string LogTypeMetaData => Lang == EnumLanguage.English ? "Metadata" : "Метаданные";
    public string LogTypeRequest => Lang == EnumLanguage.English ? "Request" : "Запрос";
    public string LogTypeResponse => Lang == EnumLanguage.English ? "Response" : "Ответ";
    public string Name => Lang == EnumLanguage.English ? "WebService 1C" : "ВебСервис 1С";
    public string Node => Lang == EnumLanguage.English ? "node" : "узел";
    public string PackageZero => Lang == EnumLanguage.English ? "Without the package" : "Без пакета";
    public string Underdevelopment(int percent) => Lang == EnumLanguage.English ? $"Under development, contact the developer! Progress of execution {percent}%." : $"Находится в разработке, свяжитесь с разработчиком! Прогресс выполнения {percent}%.";
    public string With => Lang == EnumLanguage.English ? "with" : "с";
    public string WithFieldCode => Lang == EnumLanguage.English ? "with code" : "с кодом";
    public string WithFieldNumber => Lang == EnumLanguage.English ? "with number" : "с номером";
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