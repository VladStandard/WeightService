// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Utils;

public static partial class SqlQueries
{
	public static class DbSystem
	{
		public static class Properties
		{
			public static string GetInstance => @"
SELECT SERVERPROPERTY('INSTANCENAME') [INSTANCENAME]
	    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

			public static string GetDbSpace => @"
SELECT
	[NAME] [DB_NAME]
	,[SIZE] [DB_SIZE]
	,[SIZE] * 8 / 1024 [DB_SIZE_MB]
	,[MAX_SIZE]
	,[MAX_SIZE] * 8 / 1024 [MAX_SIZE_MB]
FROM [SYS].[DATABASE_FILES]
	    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
		}
	}
}
