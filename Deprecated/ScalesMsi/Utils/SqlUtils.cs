// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesMsi.Utils
{
    /// <summary>
    /// Утилиты SQL.
    /// </summary>
    public static class SqlUtils
    {
        #region Public properties

        /// <summary>
        /// SQL-запрос поиска GUID.
        /// </summary>
        public static string QueryFindGuid =>
            string.Format(@"
IF EXISTS (SELECT 1 FROM [DB_SCALES].[SCALES] WHERE [DB_SCALES].[SCALES].[1CRREFID] = @GUID)
	SELECT 'TRUE' [RESULT]
ELSE
	SELECT 'FALSE' [RESULT]
                ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n'));

        /// <summary>
        /// SQL-запрос поиска ID.
        /// </summary>
        public static string QueryFindId =>
            string.Format(@"
IF EXISTS (SELECT 1 FROM [DB_SCALES].[SCALES] WHERE [DB_SCALES].[SCALES].[ID] = @id)
	SELECT 'TRUE' [RESULT]
ELSE
	SELECT 'FALSE' [RESULT]
                ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n'));

        /// <summary>
        /// SQL-запрос списка ID.
        /// </summary>
        public static string QueryGetIds =>
            string.Format(@"
select [Id]
from [db_scales].[Scales]
order by [Id]
                ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n'));

        #endregion
    }
}
