// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiTerra1000.Common
{
    public static class SqlQueries
    {
        public static string GetTest => @"
SELECT CAST(SYSDATETIME() AS NVARCHAR(255)) [CURRENT_TIME]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetDeprecated => @"
SELECT [dbo].[fnGetDeprecated]() [fnGetDeprecated]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetDeliveryPlaces => @"
SELECT[IIS].[fnGetDeliveryPlaces] (:StartDate, :EndDate, :Offset, :RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetContragent => @"
SELECT [IIS].[fnGetContragentByID] (:ID)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetContragents => @"
SELECT[IIS].[fnGetContragentChangesList] (:StartDate, :EndDate, :Offset, :RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclatureFromCode => @"
SELECT [IIS].[fnGetNomenclatureByCode] (:code)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclatureFromId => @"
SELECT [IIS].[fnGetNomenclatureByID] (:id)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclatures => @"
SELECT [IIS].[fnGetNomenclatureChangesList] (:StartDate, :EndDate, :Offset, :RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesCosts => @"
SELECT [IIS].[fnGetNomenclatureList] (:offset, :rowcount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetShipment => @"
SELECT [IIS].[GetRefShipmentsById] (:ID)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetShipments => @"
SELECT [IIS].[GetRefShipmentsByDocDate] (:StartDate,:EndDate,:Offset,:RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetSummary => @"
SELECT [IIS].[fnGetSummaryList] (:StartDate, :EndDate)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    }
}
