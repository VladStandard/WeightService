// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core;

/// <summary>
/// SQL extensions.
/// </summary>
public static class SqlExtensions
{
    #region Public methods

    /// <summary>
    /// Check field exists.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static bool IsFieldExists(this SqlDataReader reader, string fieldName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i).Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                return true;
        }
        return false;
    }

    #endregion
}
