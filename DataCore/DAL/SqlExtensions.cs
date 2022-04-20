// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.DAL
{
    /// <summary>
    /// SQL extebsions.
    /// </summary>
    public static class SqlExtensions
    {
        #region Public methods

        /// <summary>
        /// Check field exists.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool ExistsField(this Microsoft.Data.SqlClient.SqlDataReader dr, string field)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(field, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        #endregion
    }
}
