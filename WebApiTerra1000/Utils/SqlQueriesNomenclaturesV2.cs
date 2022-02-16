// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiTerra1000.Utils
{
    public static class SqlQueriesNomenclaturesV2
    {
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
    }
}
