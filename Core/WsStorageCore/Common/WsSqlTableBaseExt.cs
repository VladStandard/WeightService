namespace WsStorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public static class WsSqlTableBaseExt
{
    #region Public and private methods

    public static object? GetPropertyAsObject<T>(this T? item, string propertyName) where T : WsSqlEntityBase
    {
        if (item is null || string.IsNullOrEmpty(propertyName))
            return null;
        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            if (string.Equals(property.Name, propertyName))
                return property.GetValue(item);
        }
        return null;
    }

    #endregion
}