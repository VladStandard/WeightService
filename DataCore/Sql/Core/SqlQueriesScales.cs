// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core;

public static partial class SqlQueries
{
    public static class DbScales
    {
        public static class Tables
        {
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
