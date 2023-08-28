namespace WsStorageCore.Utils;

public static class WsWebSqlQueriesV2
{
    public static string GetDateTimeNow => WsSqlQueries.TrimQuery(@"
SELECT CAST(SYSDATETIME() AS NVARCHAR(255)) [CURRENT_TIME]");

    public static string GetXmlSimpleV1 => WsSqlQueries.TrimQuery(@"
SELECT [dbo].[fnGetXmlSimpleV1]() [fnGetXmlSimpleV1]");

    public static string GetXmlSimpleV2 => WsSqlQueries.TrimQuery(@"
SELECT [dbo].[fnGetXmlSimpleV2]() [fnGetXmlSimpleV2]");

    public static string GetXmlSimpleV3 => WsSqlQueries.TrimQuery(@"
SELECT [dbo].[fnGetXmlSimpleV3]() [fnGetXmlSimpleV3]");

    public static string GetXmlSimpleV4 => WsSqlQueries.TrimQuery(@"
SELECT [dbo].[fnGetXmlSimpleV4]() [fnGetXmlSimpleV4]");

    public static string GetException => WsSqlQueries.TrimQuery(@"
SELECT [dbo].[fnGetException123]() [fnGetException]");

    public static string GetDeliveryPlaces => WsSqlQueries.TrimQuery(@"
SELECT[IIS].[fnGetDeliveryPlaces] (:StartDate, :EndDate, :Offset, :RowCount)");

    public static string GetDeliveryPlacesPreview => WsSqlQueries.TrimQuery(@"
SELECT[IIS].[fnGetDeliveryPlacesPreview] (:StartDate, :EndDate, :Offset, :RowCount)");

    public static string GetShipment => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[GetRefShipmentsById] (:ID)");

    public static string GetShipmentPreview => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[GetRefShipmentsByIdPreview] (:ID)");

    public static string GetShipments => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[GetRefShipmentsByDocDate] (:StartDate,:EndDate,:Offset,:RowCount)");

    public static string GetShipmentsPreview => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[GetRefShipmentsByDocDatePreview] (:StartDate,:EndDate,:Offset,:RowCount)");

    public static string GetSummary => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetSummaryList] (:StartDate, :EndDate)");

    public static string GetSummaryPreview => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetSummaryListPreview] (:StartDate, :EndDate)");
}