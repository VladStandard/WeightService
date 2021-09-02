// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesCore.Utils
{
    /// <summary>
    /// SQL utils.
    /// </summary>
    public static class SqlUtils
    {
        #region Public properties

        /// <summary>
        /// SQL-query search GUID.
        /// </summary>
        public static string QueryFindGuid
        {
            get => string.Format(@"
-- ПОИСК GUID.
-- ВЕРСИЯ 0.0.10.
IF EXISTS (SELECT 1 FROM [DB_SCALES].[SCALES] WHERE [DB_SCALES].[SCALES].[1CRREFID] = @GUID)
	SELECT 'TRUE' [RESULT]
ELSE
	SELECT 'FALSE' [RESULT]
                ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n'));
        }

        #endregion
    }
}
