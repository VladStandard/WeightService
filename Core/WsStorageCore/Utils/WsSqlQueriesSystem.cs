// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlQueriesSystem
{
    public static class Properties
    {
        public static string GetInstance => WsSqlQueries.TrimQuery(@"
SELECT COALESCE(SERVERPROPERTY('INSTANCENAME'), 'EMPTY') [INSTANCENAME]");

        public static string GetDbFileSizes => WsSqlQueries.TrimQuery(@"
SELECT
 [TYPE]
,[NAME] [FILE_NAME]
,[SIZE] * 8 / 1024 [SIZE_MB]
,[MAX_SIZE] * 8 / 1024 [MAX_SIZE_MB]
FROM [SYS].[DATABASE_FILES]
ORDER BY [TYPE_DESC] DESC, [NAME];");
    }
}