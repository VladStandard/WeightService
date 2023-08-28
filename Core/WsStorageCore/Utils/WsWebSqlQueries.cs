namespace WsStorageCore.Utils;

public static class WsWebSqlQueries
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

    public static string GetContragent => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetContragentByID] (:ID)");

    public static string GetContragents => WsSqlQueries.TrimQuery(@"
SELECT[IIS].[fnGetContragentChangesList] (:StartDate, :EndDate, :Offset, :RowCount)");

    public static string GetNomenclatureFromCode => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetNomenclatureByCode] (:code)");

    public static string GetNomenclatureFromId => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetNomenclatureByID] (:id)");

    public static string GetNomenclatures => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetNomenclatureChangesList] (:StartDate, :EndDate, :Offset, :RowCount)");

    public static string GetNomenclaturesCosts => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetNomenclatureList] (:StartDate, :EndDate, :Offset, :RowCount)");

    public static string GetShipment => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[GetRefShipmentsById] (:ID)");

    public static string GetShipments => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[GetRefShipmentsByDocDate] (:StartDate,:EndDate,:Offset,:RowCount)");

    public static string GetSummary => WsSqlQueries.TrimQuery(@"
SELECT [IIS].[fnGetSummaryList] (:StartDate, :EndDate)");

    public static string UpdatePlu => WsSqlQueries.TrimQuery(@"
IF EXISTS(SELECT 1 FROM [DB_SCALES].[PLUS] WHERE [UID] = :uid) BEGIN
	IF (NOT EXISTS(SELECT 1 FROM [DB_SCALES].[PLUS_FK] WHERE [PLU_UID] = :uid) AND 
	    NOT EXISTS(SELECT 1 FROM [DB_SCALES].[PLUS_FK] WHERE [PARENT_UID] = :uid) AND
		NOT EXISTS(SELECT 1 FROM [DB_SCALES].[PLUS_TEMPLATES_FK] WHERE [PLU_UID] = :uid)) BEGIN
		DELETE FROM [DB_SCALES].[PLUS] WHERE [UID] = :uid;
	END;
END;
UPDATE [DB_SCALES].[PLUS] SET [UID] = :uid WHERE [CODE] = :code AND [NUMBER] = :number;");
}