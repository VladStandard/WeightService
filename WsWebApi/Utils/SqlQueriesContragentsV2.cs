// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Utils;

public static class SqlQueriesContragentsV2
{
    public static string GetContragentFromCodeProd => @"
SELECT [IIS].[fnGetContragentsV2] (:code, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentFromCodePreview => @"
SELECT [IIS].[fnGetContragentsV2Preview] (:code, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentFromIdProd => @"
SELECT [IIS].[fnGetContragentsV2] (DEFAULT, :id, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentFromIdPreview => @"
SELECT [IIS].[fnGetContragentsV2Preview] (DEFAULT, :id, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsEmptyProd => @"
SELECT [IIS].[fnGetContragentsV2] (DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsEmptyPreview => @"
SELECT [IIS].[fnGetContragentsV2Preview] (DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsFromStartDateProd => @"
SELECT [IIS].[fnGetContragentsV2] (DEFAULT, DEFAULT, :start_date, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsFromStartDatePreview => @"
SELECT [IIS].[fnGetContragentsV2Preview] (DEFAULT, DEFAULT, :start_date, DEFAULT, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsFromDatesProd => @"
SELECT [IIS].[fnGetContragentsV2] (DEFAULT, DEFAULT, :start_date, :end_date, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsFromDatesPreview => @"
SELECT [IIS].[fnGetContragentsV2Preview] (DEFAULT, DEFAULT, :start_date, :end_date, DEFAULT, DEFAULT)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsFromDatesOffsetProd => @"
SELECT [IIS].[fnGetContragentsV2] (DEFAULT, DEFAULT, :start_date, :end_date, :offset, :row_count)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string GetContragentsFromDatesOffsetPreview => @"
SELECT [IIS].[fnGetContragentsV2Preview] (DEFAULT, DEFAULT, :start_date, :end_date, :offset, :row_count)
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
}
