// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

namespace WsStorageCore.Common;

/// <summary>
/// Расширение базового класса SQL-таблицы.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public static class WsSqlTableBaseExt
{
    #region Public and private methods

    public static object? GetPropertyAsObject<T>(this T? item, string propertyName) where T : WsSqlTableBase
    {
        if (item is not null && !string.IsNullOrEmpty(propertyName))
        {
            //if (propertyName.Contains('.'))
            //{
            //	foreach (PropertyInfo property in typeof(T).GetProperties())
            //	{
            //		if (string.Equals(property.Name, propertyName.Substring(0, propertyName.IndexOf('.'))))
            //		{
            //			T prop = (T)property.GetValue(item);
            //			string subPropertyName = propertyName.Substring(propertyName.IndexOf('.'), propertyName.Length - propertyName.IndexOf('.') - 1);
            //			return GetPropertyValue(prop, subPropertyName);
            //		}
            //	}
            //}
            //else
            {
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    if (string.Equals(property.Name, propertyName))
                        return property.GetValue(item);
                }
            }
        }
        return null;
    }

    #endregion
}