// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleWebService : WsLocalizationBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsLocaleWebService _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsLocaleWebService Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor
    
    public string BoxZero => Lang == Lang.English ? "Without the box" : "Без коробки";
    public string ClipZero => Lang == Lang.English ? "Without the clip" : "Без клипсы";
    public string DtStamp => Lang == Lang.English ? "Date time stamp" : "Отметка времени даты";
    public string Dublicate => Lang == Lang.English ? "Dublicate" : "Дубликат";
    public string FieldBrand => Lang == Lang.English ? "Brand" : "Бренд";
    public string FieldBundle => Lang == Lang.English ? "Bundle" : "Пакет";
    public string FieldClip => Lang == Lang.English ? "Clip" : "Клипса";
    public string FieldCode => Lang == Lang.English ? "code" : "код";
    public string FieldGroup => Lang == Lang.English ? "Group" : "Группа";
    public string FieldGroup1Level => Lang == Lang.English ? "Level 1 group" : "Группа 1 уровня";
    public string FieldGuid => Lang == Lang.English ? "GUID" : "ГУИД";
    public string FieldNomenclature => Lang == Lang.English ? "Nomenclature" : "Номенклатура";
    public string FieldNomenclatureCharacteristic => Lang == Lang.English ? "Nomeclature characteristic" : "Номенклатурная характеристика";
    public string FieldNomenclatureGroup => Lang == Lang.English ? "Nomenclature group" : "Номенклатурная группа";
    public string FieldNomenclatureParent => Lang == Lang.English ? "Parent nomenclature" : "Родительская номенклатура";
    public string FieldPluIsDenyForLoad => Lang == Lang.English ? "PLU is deny for load" : "ПЛУ запрещена для загрузки";
    public string FieldPluIsErrorUid1c => Lang == Lang.English ? "PLU UID_1C is error" : "Ошибка UID_1C ПЛУ";
    public string FieldPluIsNotFound => Lang == Lang.English ? "PLU is not found" : "ПЛУ не найдена";
    public string FieldPluNumber => Lang == Lang.English ? "PLU number" : "Номер ПЛУ";
    public string FieldPluNumberNotInAcl => Lang == Lang.English ? "PLU number is not included in the list of allowed" : "Номер ПЛУ не входит в список разрешённых";
    public string ForDbRecord => Lang == Lang.English ? "for DB record" : "для записи БД";
    public string ForRecord => Lang == Lang.English ? "for record" : "для записи";
    public string IsEmpty => Lang == Lang.English ? "is empty value" : "пустое значение";
    public string IsFound => Lang == Lang.English ? "is found" : "найдено";
    public string IsNotFound => Lang == Lang.English ? "is not found" : "не найдено";
    public string IsNotIdent => Lang == Lang.English ? "is not ident" : "не определено";
    public string IsStatusSuccess => Lang == Lang.English ? "Done successfully" : "Выполнено успешно";
    public string LogTypeMetaData => Lang == Lang.English ? "Metadata" : "Метаданные";
    public string LogTypeRequest => Lang == Lang.English ? "Request" : "Запрос";
    public string LogTypeResponse => Lang == Lang.English ? "Response" : "Ответ";
    public string Name => Lang == Lang.English ? "WebService 1C" : "ВебСервис 1С";
    public string Node => Lang == Lang.English ? "node" : "узел";
    public string PackageZero => Lang == Lang.English ? "Without the package" : "Без пакета";
    public string Underdevelopment => Lang == Lang.English ? "Under development, contact the developer." : "Находится в разработке, свяжитесь с разработчиком.";
    public string With => Lang == Lang.English ? "with" : "с";
    public string WithFieldCode => Lang == Lang.English ? "with code" : "с кодом";
    public string XmlItemBrand => "Brand";
    public string XmlItemCharacteristic => "Characteristic";
    public string XmlItemNomenclature => "Nomenclature";
    public string XmlItemNomenclatureGroup => "NomenclatureGroup";

    #endregion
}