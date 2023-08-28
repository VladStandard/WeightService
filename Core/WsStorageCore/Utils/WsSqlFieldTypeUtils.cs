namespace WsStorageCore.Utils;

/// <summary>
/// Утилиты типов данных полей SQL-таблиц.
/// </summary>
public static class WsSqlFieldTypeUtils
{
    #region Public and private fields, properties, constructor

    public static string Bit => "BIT";
    public static string Date => "DATE";
    public static string DateTime => "DATETIME";
    public static string DateTime27 => "DATETIME(2,7)";
    public static string Decimal => "DECIMAL";
    public static string Decimal103 => "DECIMAL(10,3)";
    public static string Int => "INT";
    public static string NvarChar => "NVARCHAR";
    public static string SmallInt => "SMALLINT";
    public static string TinyInt => "TINYINT";
    public static string UniqueIdentifier => "UNIQUEIDENTIFIER";
    public static string VarBinary => "VARBINARY";
    public static string VarChar => "VARCHAR";
    public static string Xml => "XML";

    #endregion
}