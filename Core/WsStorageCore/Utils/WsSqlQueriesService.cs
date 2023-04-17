// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlQueriesService
{
    public static class Tables
    {
        public static class Access
        {
            public static string GetAccessRightsAll => @"
-- Table Access
SELECT
[UID]
,[CREATE_DT]
,[CHANGE_DT]
,[IS_MARKED]
,[USER]
,[RIGHTS]
FROM [DB_SCALES].[ACCESS]
ORDER BY [USER] ASC
".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

            public static string GetAccessRights(string userName) => @$"
-- Table Access
SELECT
[UID]
,[CREATE_DT]
,[CHANGE_DT]
,[IS_MARKED]
,[USER]
,[RIGHTS]
FROM [DB_SCALES].[ACCESS]
WHERE [USER] = N'{userName}'
".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
        }

        public static class Apps
        {
            public static string AddApp => @"
if not exists (select 1 from [db_scales].[APPS] where [NAME] = @app) begin
insert into [db_scales].[APPS]([NAME]) values(@app)
end
select [UID]
from [db_scales].[APPS]
where [NAME] = @app
	".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
        }
    }
}