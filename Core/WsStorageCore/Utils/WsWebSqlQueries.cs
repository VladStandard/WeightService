namespace WsStorageCore.Utils;

public static class WsWebSqlQueries
{
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
}