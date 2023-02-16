// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Utils;

public static partial class SqlQueries
{
    public static class DbScales
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
	,[P].[NOMENCLATURE_ID] [PLU_NOMENCLATURE_ID]
	-- [DB_SCALES].[BUNDLES] | 31 - 36
	,[BU].[UID] [BUNDLE_UID]
	,[BU].[CREATE_DT] [BUNDLE_CREATE_DT]
	,[BU].[CHANGE_DT] [BUNDLE_CHANGE_DT]
	,[BU].[IS_MARKED] [BUNDLE_IS_MARKED]
	,[BU].[NAME] [BUNDLE_NAME]
	,[BU].[WEIGHT] [BUNDLE_WEIGHT]
	-- [DB_SCALES].[BOXES] | 37 - 42
	,[B].[UID] [BOX_UID]
	,[B].[CREATE_DT] [BOX_CREATE_DT]
	,[B].[CHANGE_DT] [BOX_CHANGE_DT]
	,[B].[IS_MARKED] [BOX_IS_MARKED]
	,[B].[NAME] [BOX_NAME]
	,[B].[WEIGHT] [BOX_WEIGHT]
	-- UID_1C | 43 - 45
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
            public static class WeithingFacts
            {
                public static string GetWeithingFacts(int topRecords) => @$"
-- Table WeithingFact diagram summary
SELECT {GetTopRecords(topRecords)}
	cast([wf].[WeithingDate] as date) [WeithingDate]
	,count(*) [Count]
	,[s].[Description] [Scale]
	,[h].[Name] [Host]
	,[p].[Name] [Printer]
from [db_scales].[WeithingFact] [wf]
left join [db_scales].[Scales] [s] on [wf].[ScaleId] = [s].[Id]
left join [db_scales].[Hosts] [h] on [s].[HostId] = [h].[Id]
left join [db_scales].[ZebraPrinter] [p] on [s].[ZebraPrinterId] = [p].[Id]
group by cast([WeithingDate] as date), [s].[Description], [h].[Name], [p].[Name]
order by [WeithingDate] desc
					".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
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
}
