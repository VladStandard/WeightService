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
SELECT COALESCE(SERVERPROPERTY('INSTANCENAME'), 'EMPTY') [INSTANCENAME]
	    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

			public static string GetDbFileSizes => @"
SELECT
	 [TYPE]
	,[NAME] [FILE_NAME]
	,[SIZE] * 8 / 1024 [SIZE_MB]
	,[MAX_SIZE] * 8 / 1024 [MAX_SIZE_MB]
FROM [SYS].[DATABASE_FILES]
ORDER BY [TYPE_DESC] DESC, [NAME];
	    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
		}
	}
}
