// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiTerra1000.Utils
{
    public static class SqlQueriesV2
    {
        public static string GetDateTimeNow => @"
SELECT CAST(SYSDATETIME() AS NVARCHAR(255)) [CURRENT_TIME]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetXmlSimpleV1 => @"
SELECT [dbo].[fnGetXmlSimpleV1]() [fnGetXmlSimpleV1]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetXmlSimpleV2 => @"
SELECT [dbo].[fnGetXmlSimpleV2]() [fnGetXmlSimpleV2]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetXmlSimpleV3 => @"
SELECT [dbo].[fnGetXmlSimpleV3]() [fnGetXmlSimpleV3]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetXmlSimpleV4 => @"
SELECT [dbo].[fnGetXmlSimpleV4]() [fnGetXmlSimpleV4]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetException => @"
SELECT [dbo].[fnGetException123]() [fnGetException]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
        
        public static string GetDeliveryPlaces => @"
SELECT[IIS].[fnGetDeliveryPlaces] (:StartDate, :EndDate, :Offset, :RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetDeliveryPlacesPreview => @"
SELECT[IIS].[fnGetDeliveryPlacesPreview] (:StartDate, :EndDate, :Offset, :RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetContragent => @"
SELECT [IIS].[fnGetContragentByID] (:ID)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetContragentPreview => @"
SELECT [IIS].[fnGetContragentByIDPreview] (:ID)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetContragents => @"
SELECT[IIS].[fnGetContragentChangesList] (:StartDate, :EndDate, :Offset, :RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetContragentsPreview => @"
SELECT[IIS].[fnGetContragentChangesListPreview] (:StartDate, :EndDate, :Offset, :RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        #region Nomenclatures

        public static string GetNomenclatureFromCodeProd => @"
SELECT [IIS].[fnGetNomenclaturesV2] (:code, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclatureFromCodePreview => @"
SELECT [IIS].[fnGetNomenclaturesV2Preview] (:code, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclatureFromIdProd => @"
SELECT [IIS].[fnGetNomenclaturesV2] (DEFAULT, :id, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclatureFromIdPreview => @"
SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, :id, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesEmptyProd => @"
SELECT [IIS].[fnGetNomenclaturesV2] (DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesEmptyPreview => @"
SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t'); 
        
        public static string GetNomenclaturesFromStartDateProd => @"
SELECT [IIS].[fnGetNomenclaturesV2] (DEFAULT, DEFAULT, :start_date, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesFromStartDatePreview => @"
SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, DEFAULT, :start_date, DEFAULT, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesFromDatesProd => @"
SELECT [IIS].[fnGetNomenclaturesV2] (DEFAULT, DEFAULT, :start_date, :end_date, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesFromDatesPreview => @"
SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, DEFAULT, :start_date, :end_date, DEFAULT, DEFAULT)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesFromDatesOffsetProd => @"
SELECT [IIS].[fnGetNomenclaturesV2] (DEFAULT, DEFAULT, :start_date, :end_date, :offset, :row_count)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetNomenclaturesFromDatesOffsetPreview => @"
SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, DEFAULT, :start_date, :end_date, :offset, :row_count)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        #endregion

        public static string GetShipment => @"
SELECT [IIS].[GetRefShipmentsById] (:ID)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetShipmentPreview => @"
SELECT [IIS].[GetRefShipmentsByIdPreview] (:ID)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetShipments => @"
SELECT [IIS].[GetRefShipmentsByDocDate] (:StartDate,:EndDate,:Offset,:RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetShipmentsPreview => @"
SELECT [IIS].[GetRefShipmentsByDocDatePreview] (:StartDate,:EndDate,:Offset,:RowCount)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetSummary => @"
SELECT [IIS].[fnGetSummaryList] (:StartDate, :EndDate)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');


        public static string GetSummaryPreview => @"
SELECT [IIS].[fnGetSummaryListPreview] (:StartDate, :EndDate)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    }
}
