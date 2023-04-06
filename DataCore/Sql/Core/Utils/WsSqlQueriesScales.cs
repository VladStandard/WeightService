// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Utils;

/// <summary>
/// SQL queries for Scales.
/// </summary>
public static class WsSqlQueriesScales
{
    public static class Tables
    {
        public static class PluNestingFks
        {
            public static string GetList(bool isSetPluUid) => @$"
-- PLUS_NESTING_FK SELECT AS OBJECTS
SELECT 
-- [DB_SCALES].[PLUS_NESTING_FK] | 0 - 10
 [PNFK].[UID]
,[PNFK].[CREATE_DT]
,[PNFK].[CHANGE_DT]
,[PNFK].[IS_MARKED]
,[PNFK].[IS_DEFAULT]
,[PNFK].[BUNDLE_COUNT]
,[PNFK].[WEIGHT_MAX]
,[PNFK].[WEIGHT_MIN]
,[PNFK].[WEIGHT_NOM]
,[PNFK].[PLU_BUNDLE_FK]
,[PNFK].[BOX_UID]
-- [DB_SCALES].[PLUS_BUNDLES_FK] | 11 - 16
,[PBFK].[UID] [PLU_BUNDLE_FK_UID]
,[PBFK].[CREATE_DT] [PLU_BUNDLE_FK_CREATE_DT]
,[PBFK].[CHANGE_DT] [PLU_BUNDLE_FK_CHANGE_DT]
,[PBFK].[IS_MARKED] [PLU_BUNDLE_FK_IS_MARKED]
,[PBFK].[PLU_UID] [PLU_BUNDLE_FK_PLU_UID]
,[PBFK].[BUNDLE_UID] [PLU_BUNDLE_FK_BUNDLE_UID]
-- [DB_SCALES].[PLUS] | 17 - 30
,[P].[UID] [PLU_UID]
,[P].[CREATE_DT][PLU_CREATE_DT]
,[P].[CHANGE_DT] [PLU_CHANGE_DT]
,[P].[IS_MARKED] [PLU_IS_MARKED]
,[P].[NUMBER] [PLU_NUMBER]
,[P].[NAME] [PLU_NAME]
,[P].[FULL_NAME] [PLU_FULL_NAME]
,[P].[DESCRIPTION] [PLU_DESCRIPTION]
,[P].[SHELF_LIFE_DAYS] [PLU_SHELF_LIFE_DAYS]
,[P].[GTIN] [PLU_GTIN]
,[P].[EAN13] [PLU_EAN13]
,[P].[ITF14] [PLU_ITF14]
,[P].[IS_CHECK_WEIGHT] [PLU_IS_CHECK_WEIGHT]
-- [DB_SCALES].[BUNDLES] | 30 - 35
,[BU].[UID] [BUNDLE_UID]
,[BU].[CREATE_DT] [BUNDLE_CREATE_DT]
,[BU].[CHANGE_DT] [BUNDLE_CHANGE_DT]
,[BU].[IS_MARKED] [BUNDLE_IS_MARKED]
,[BU].[NAME] [BUNDLE_NAME]
,[BU].[WEIGHT] [BUNDLE_WEIGHT]
-- [DB_SCALES].[BOXES] | 36 - 41
,[B].[UID] [BOX_UID]
,[B].[CREATE_DT] [BOX_CREATE_DT]
,[B].[CHANGE_DT] [BOX_CHANGE_DT]
,[B].[IS_MARKED] [BOX_IS_MARKED]
,[B].[NAME] [BOX_NAME]
,[B].[WEIGHT] [BOX_WEIGHT]
-- UID_1C | 42 - 44
,[P].[UID_1C] [PLU_UID_1C]
,[B].[UID_1C] [BOX_UID_1C]
,[BU].[UID_1C] [BUNDLE_UID_1C]
FROM [DB_SCALES].[PLUS_NESTING_FK] [PNFK]
LEFT JOIN [DB_SCALES].[PLUS_BUNDLES_FK] [PBFK] ON [PNFK].[PLU_BUNDLE_FK] = [PBFK].[UID]
LEFT JOIN [DB_SCALES].[PLUS] [P] ON [PBFK].[PLU_UID] = [P].[UID]
LEFT JOIN [DB_SCALES].[BUNDLES] [BU] ON [PBFK].[BUNDLE_UID] = [BU].[UID]
LEFT JOIN [DB_SCALES].[BOXES] [B] ON [PNFK].[BOX_UID] = [B].[UID]
{(isSetPluUid ? "WHERE [P].[UID] = :P_UID" : "")}
ORDER BY [P].[NUMBER];
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
        }

        public static class PluLabels
        {
            public static string GetLabelsAggrWithoutPlu(int topRecords) => $@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- PLUS_LABELS_SELECT_AGGR_WITHOUT_PLU | АГРЕГИРОВАННЫЕ ЭТИКЕТКИ БЕЗ ПЛУ
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
 CAST([PL].[CHANGE_DT] AS DATE) [PL_CHANGE_DT]
,COUNT(*) [COUNT]
,[S].[DESCRIPTION] [LINE]
,[D].[NAME] [DEVICE]
FROM [db_scales].[PLUS_LABELS] [PL]
LEFT JOIN [db_scales].[PLUS_SCALES] [PS] ON [PS].[UID] = [PL].[PLU_SCALE_UID]
LEFT JOIN [DB_SCALES].[SCALES] [S] ON [S].[ID] = [PS].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES_SCALES_FK] [DFK] ON [S].[ID]=[DFK].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES] [D] ON [DFK].[DEVICE_UID]=[D].[UID]
GROUP BY CAST([PL].[CHANGE_DT] AS DATE), [S].[DESCRIPTION], [D].[NAME]
ORDER BY [PL_CHANGE_DT] DESC;
";
            public static string GetLabelsAggrWithPlu(int topRecords) => $@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- PLUS_LABELS_SELECT_AGGR_WITH_PLU | АГРЕГИРОВАННЫЕ ЭТИКЕТКИ С ПЛУ
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
 CAST([PL].[CHANGE_DT] AS DATE) [PL_CHANGE_DT]
,COUNT(*) [COUNT]
,[S].[DESCRIPTION] [LINE]
,[D].[NAME] [DEVICE]
,[P].[NAME] [PLU]
FROM [db_scales].[PLUS_LABELS] [PL]
LEFT JOIN [db_scales].[PLUS_SCALES] [PS] ON [PS].[UID] = [PL].[PLU_SCALE_UID]
LEFT JOIN [db_scales].[PLUS] [P] ON [P].[UID] = [PS].[PLU_UID]
LEFT JOIN [DB_SCALES].[SCALES] [S] ON [S].[ID] = [PS].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES_SCALES_FK] [DFK] ON [S].[ID]=[DFK].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES] [D] ON [DFK].[DEVICE_UID]=[D].[UID]
GROUP BY CAST([PL].[CHANGE_DT] AS DATE), [S].[DESCRIPTION], [D].[NAME], [P].[NAME]
ORDER BY [PL_CHANGE_DT] DESC;
";
        }

        public static class PluWeighings
        {
            public static string GetWeighingsAggrWithoutPlu(int topRecords) => $@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- PLUS_WEIGHINGS_SELECT_AGGR_WITHOUT_PLU | АГРЕГИРОВАННЫЕ ВЗВЕШИВАНИЯ БЕЗ ПЛУ
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
 CAST([PW].[CHANGE_DT] AS DATE) [PW_CHANGE_DT]
,COUNT(*) [COUNT]
,[S].[DESCRIPTION] [LINE]
,[D].[NAME] [DEVICE]
FROM [db_scales].[PLUS_WEIGHINGS] [PW]
LEFT JOIN [db_scales].[PLUS_SCALES] [PS] ON [PS].[UID] = [PW].[PLU_SCALE_UID]
LEFT JOIN [DB_SCALES].[SCALES] [S] ON [S].[ID] = [PS].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES_SCALES_FK] [DFK] ON [S].[ID]=[DFK].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES] [D] ON [DFK].[DEVICE_UID]=[D].[UID]
GROUP BY CAST([PW].[CHANGE_DT] AS DATE), [S].[DESCRIPTION], [D].[NAME]
ORDER BY [PW_CHANGE_DT] DESC;
";
            public static string GetWeighingsAggrWithPlu(int topRecords) => $@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- PLUS_WEIGHINGS_SELECT_AGGR_WITH_PLU | АГРЕГИРОВАННЫЕ ВЗВЕШИВАНИЯ С ПЛУ
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
 CAST([PW].[CHANGE_DT] AS DATE) [PW_CHANGE_DT]
,COUNT(*) [COUNT]
,[S].[DESCRIPTION] [LINE]
,[D].[NAME] [DEVICE]
,[P].[NAME] [PLU]
FROM [db_scales].[PLUS_WEIGHINGS] [PW]
LEFT JOIN [db_scales].[PLUS_SCALES] [PS] ON [PS].[UID] = [PW].[PLU_SCALE_UID]
LEFT JOIN [db_scales].[PLUS] [P] ON [P].[UID] = [PS].[PLU_UID]
LEFT JOIN [DB_SCALES].[SCALES] [S] ON [S].[ID] = [PS].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES_SCALES_FK] [DFK] ON [S].[ID]=[DFK].[SCALE_ID]
LEFT JOIN [DB_SCALES].[DEVICES] [D] ON [DFK].[DEVICE_UID]=[D].[UID]
GROUP BY CAST([PW].[CHANGE_DT] AS DATE), [S].[DESCRIPTION], [D].[NAME], [P].[NAME]
ORDER BY [PW_CHANGE_DT] DESC;
";
        }
    }

    public static class Functions
    {
        public static string GetCurrentProductSeriesV2 => @"
DECLARE @SSCC VARCHAR(50)
DECLARE @WeithingDate DATETIME
DECLARE @XML XML

EXECUTE [db_scales].[SP_SET_PRODUCT_SERIES_V2] @SCALE_ID, @SSCC OUTPUT, @XML OUTPUT

SELECT [ID], [CREATE_DT], [UUID], [SSCC], [COUNT_UNIT],[TOTAL_NET_WEIGHT], [TOTAL_TARE_WEIGHT], [IS_MARKED]
FROM [db_scales].[FN_GET_PRODUCT_SERIES_V2](@SCALE_ID)
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
    }
}